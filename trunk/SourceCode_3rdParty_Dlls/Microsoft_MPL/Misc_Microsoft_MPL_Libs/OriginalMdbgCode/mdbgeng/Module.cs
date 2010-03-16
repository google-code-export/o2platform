//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------
using System;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using O2.Debugger.Mdbg.corapi;
using O2.Debugger.Mdbg.corapi;
using O2.Debugger.Mdbg.corapi;
using O2.Debugger.Mdbg.Debugging.CorDebug;
using O2.Debugger.Mdbg.Debugging.CorDebug;
using O2.Debugger.Mdbg.Debugging.CorDebug;
using O2.Debugger.Mdbg.Debugging.CorSymbolStore;
using O2.Debugger.Mdbg.Debugging.CorSymbolStore;
using O2.Debugger.Mdbg.Debugging.CorSymbolStore;
using CorFunction=O2.Debugger.Mdbg.Debugging.CorDebug.CorFunction;
using CorModule=O2.Debugger.Mdbg.Debugging.CorDebug.CorModule;
using SymbolStore=O2.Debugger.Mdbg.Debugging.CorSymbolStore;


namespace O2.Debugger.Mdbg.Debugging.MdbgEngine
{
    /// <summary>
    /// MDbgModuleCollection class.  Allows for grouping of Modules.
    /// </summary>
    public sealed class MDbgModuleCollection : MarshalByRefObject, IEnumerable, IDisposable
    {
        private readonly Hashtable m_items = new Hashtable();
        private readonly MDbgProcess m_process;
        private int m_freeModuleNumber;

        internal MDbgModuleCollection(MDbgProcess process)
        {
            Debug.Assert(process != null);
            m_process = process;
        }

        /// <summary>
        /// How many modules are in the collection.
        /// </summary>
        /// <value>Module Count.</value>
        public int Count
        {
            get { return m_items.Count; }
        }

        #region IDisposable Members

        /// <summary>
        /// Releases all resources used by the MDbgModuleCollection.
        /// </summary>
        /// <remarks>
        ///     This method effectively calls Dispose() on all modules in the collection.
        /// </remarks>
        public void Dispose()
        {
            foreach (MDbgModule module in m_items.Values)
            {
                module.Dispose();
            }
            Clear();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            var ret = new MDbgModule[m_items.Count];
            m_items.Values.CopyTo(ret, 0);
            Array.Sort(ret);
            return ret.GetEnumerator();
        }

        #endregion

        /// <summary>
        /// Looks up a CorFunction.
        /// </summary>
        /// <param name="managedFunction">Which CorFunction to lookup.</param>
        /// <returns>The coresponding MDbgFunction.</returns>
        public MDbgFunction LookupFunction(CorDebug.CorFunction managedFunction)
        {
            return Lookup(managedFunction.Module).GetFunction(managedFunction);
        }

        /// <summary>
        /// Looks up a CorModule.
        /// </summary>
        /// <param name="managedModule">Which CorModule to lookup.</param>
        /// <returns>The coresponding MDbgModule.</returns>
        public MDbgModule Lookup(CorDebug.CorModule managedModule)
        {
            return (MDbgModule) m_items[managedModule];
        }

        /// <summary>
        /// Looks up a Module Name.
        /// </summary>
        /// <param name="moduleName">Which Module Name to lookup.</param>
        /// <returns>The coresponding MDbgModule with the given name.</returns>
        public MDbgModule Lookup(string moduleName)
        {
            MDbgModule matchedModule = null;
            foreach (MDbgModule module in m_items.Values)
            {
                if (module.MatchesModuleName(moduleName))
                {
                    if (matchedModule == null)
                        matchedModule = module;
                    else
                        DI.log.error("MDbgAmbiguousModuleNameException , could not find module with name: {0}", moduleName);
                        //throw new MDbgAmbiguousModuleNameException();
                }
            }
            return matchedModule;
        }

        internal void Clear()
        {
            m_items.Clear();
        }

        internal MDbgModule Register(CorDebug.CorModule managedModule)
        {
            MDbgModule mdbgModule;
            if (m_items.ContainsKey(managedModule))
            {
                mdbgModule = (MDbgModule) m_items[managedModule];
                return mdbgModule;
            }

            mdbgModule = new MDbgModule(m_process, managedModule, m_freeModuleNumber++);
            m_items.Add(managedModule, mdbgModule);
            return mdbgModule;
        }

        internal void Unregister(CorDebug.CorModule managedModule)
        {
            Debug.Assert(m_items.ContainsKey(managedModule));
            m_items.Remove(managedModule);
        }
    }

    /// <summary>
    /// The MDbgModule class.
    /// </summary>
    public sealed class MDbgModule : MarshalByRefObject, IComparable, IDisposable
    {
        private static ISymbolBinder1 g_symBinder;
        private readonly int m_number;
        private readonly MDbgProcess m_process;
        private int m_editsCounter;

        private ArrayList m_editsSources;

        private MDbgFunctionMgr m_functions;
        private CorMetadataImport m_importer;
        private bool m_isSymReaderInitialized;
        private CorDebug.CorModule m_module;
        private ISymbolReader m_symReader;

        internal MDbgModule(MDbgProcess process, CorDebug.CorModule managedModule, int number)
        {
            Debug.Assert(process != null && managedModule != null);
            m_process = process;
            m_module = managedModule;
            m_functions = new MDbgFunctionMgr(this);
            m_number = number;
        }

        /// <summary>
        /// Gets the MDbgProcess that has loaded the module.
        /// </summary>
        /// <value>The MDbgProcess.</value>
        public MDbgProcess Process
        {
            get { return m_process; }
        }

        /// <summary>
        /// Gets the CorModule encapsulated in the MDbgModule.
        /// </summary>
        /// <value>The CorModule.</value>
        public CorModule CorModule
        {
            get { return m_module; }
        }

        // lazy loading of Importer
        /// <summary>
        /// Gets the metadata importer for the Module.
        /// </summary>
        /// <value>The CorMetadataImport.</value>
        /// <remarks> The metadata provides rich compile-time information such
        /// as all the functions and types in the module. </remarks>
        public CorMetadataImport Importer
        {
            get
            {
                if (m_importer == null)
                {
                    m_importer = new CorMetadataImport(m_module);
                    Debug.Assert(m_importer != null);
                }
                return m_importer;
            }
        }

        /// <summary>
        /// Gets the number for the Module.
        /// </summary>
        /// <value>The number.</value>
        public int Number
        {
            get { return m_number; }
        }

        // lazy loading of SymReader
        /// <summary>
        /// Gets the SymReader for the Module. 
        /// This will attempt to load symbols if not already loaded.
        /// </summary>
        /// <value>The SymReader.</value>
        public ISymbolReader SymReader
        {
            get
            {
                // Try and initialize the symbol reader if we haven't already.  For in-memory and dynamic
                // modules we can't explicitly initialize the symbol reader on demand because the symbols
                // must be provided to us in a stream.
                if (!m_isSymReaderInitialized && !CorModule.IsDynamic && !CorModule.IsInMemory)
                {
                    m_isSymReaderInitialized = true;

                    string sympath = m_process.SymbolPath;
                    if (sympath == null)
                        sympath = m_process.m_engine.Options.SymbolPath;
                    string moduleName = m_module.Name;
                    Debug.Assert(moduleName.Length > 0);
                    try
                    {
                        m_symReader = (SymBinder as ISymbolBinder2).
                            GetReaderForFile(Importer.RawCOMObject, moduleName, sympath);
                    }
                    catch (COMException e)
                    {
                        if (e.ErrorCode == unchecked((int) 0x806D0014)) // E_PDB_CORRUPT
                        {
                            // Ignore it.
                            // This may happen for mismatched pdbs
                        }
                        else
                            throw;
                    }
                }
                return m_symReader;
            }
        }

        /// <summary>
        /// Get the filename of the symbols for this module.
        /// This will attempt to load symbols if not already loaded.
        /// </summary>
        /// <return>
        /// If successful, returns the full path and filename for the symbols loaded for this 
        /// module. If no symbols are loaded or if symbols are loaded but it can't determine the filename, 
        /// returns null. </return>
        public string SymbolFilename
        {
            get
            {
                // Try to get exact PDB name.
                try
                {
                    var s2 = (SymReader as ISymbolReader2);
                    if (s2 != null)
                    {
                        string stPdbName = s2.GetSymbolStoreFileName();
                        return stPdbName;
                    }
                }
                catch
                {
                    // We've already set it to a default, so ignore the rest.   
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the number of edits performed on a module.
        /// </summary>
        /// <value>The number of edits.</value>
        public int EditsCounter
        {
            get { return m_editsCounter; }
        }

        private static ISymbolBinder1 SymBinder
        {
            get
            {
                if (g_symBinder == null)
                {
                    g_symBinder = new SymbolBinder();
                    Debug.Assert(g_symBinder != null);
                }
                return g_symBinder;
            }
        }

        #region IComparable Members

        int IComparable.CompareTo(object obj)
        {
            return Number - (obj as MDbgModule).Number;
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Releases all resources used by the MDbgModule.
        /// </summary>
        public void Dispose()
        {
            // Our funtion list may hold onto unmanaged SymbolMethod objects, so dispose that too.
            m_functions.Dispose();
            m_functions = null;

            // Release unmanaged resources.
            m_symReader = null;
            m_module = null;
            m_importer = null;
        }

        #endregion

        /// <summary>
        /// Checks if the module name string matches this module.
        /// This will attempt to load symbols if not already loaded.
        /// </summary>
        /// <param name="moduleName">Name of the module in one of supported formats.</param>
        /// <return>
        /// True if string matches this module name.
        /// </return>
        public bool MatchesModuleName(string moduleName)
        {
            // module names can be provide in following forms:
            // :1                       -- identifes module by logical number
            // mscorlib                 -- identifes module by base module name without path
            // mscorlib.dll             -- identifes module by name without path
            // c:\path\mscorlib.dll     -- identifes module by full load name
            //
            // futher modules identified by names can be more specified into which appdomian the
            // modules have been loaded. The syntax is as following:
            // moduleName#appdomainNumber.
            // E.g. mscorlib in first appdomain can be identified as:
            // mscrolib#0 or mscorlib.dll#0.
            // 

            // handle special :number syntax.
            if (moduleName.StartsWith(":") && moduleName.Length > 1)
            {
                UInt32 logicalNumber;
                if (!UInt32.TryParse(moduleName.Substring(1), out logicalNumber))
                    return false;
                return logicalNumber == Number;
            }

            // maybe the module has been specified with a full path
            bool fullNameProvided;
            if (moduleName.IndexOfAny(new[]
                                          {
                                              Path.DirectorySeparatorChar,
                                              Path.AltDirectorySeparatorChar
                                          }) == -1)
                fullNameProvided = false;
            else
                fullNameProvided = true;

            // 
            // There is a problem with the # suffix. The # is a legal character for a module
            // name. This means that a module can be named "a#0.dll".
            // What does the string a#0 should refer to? Module a in appdomain 0 or a module name
            // a#0 in any appdomain?
            //
            // There are 3 possible solutions:
            // a) we'll pick antoher char that is illegal in file name: e.g. ?<>|:/\
            //    This is best solution, however it is not portable because we don't about
            //    any universal char that is illegal on all platforms.
            //    Moreover this would also be a breaking change.
            // 
            // b) We'll try to treat "a#0" first as complete filename and then
            //    as name "a" in appdomain 0. This solution however brings problems
            //    in cases you have following modules loaded:
            //    a
            //    a#0
            //    How do you refer to the fist module in appdomain 0? a#0 will match also
            //    module a#0 loaded to any appdomain.
            //
            // c) We'll do nothing and assume that modules loaded don't end with #{number}. If they will
            //    the user cannot refer to them by name but only by syntax :1.
            //    There are other cases where this may be necessary (eg. a dynamic module with a space
            //    in it's name or in-memory modules with no name).
            //
            // Currently implementation goes with c).
            // 
            bool appDomainNumberProvided = false;
            UInt32 appDomainNumber = 0; // just prevent compiler warnings 

            int i = moduleName.LastIndexOf('#');
            if (i != -1)
            {
                // contains at least one #.
                appDomainNumberProvided = UInt32.TryParse(moduleName.Substring(i + 1), out appDomainNumber);
                if (appDomainNumberProvided)
                    moduleName = moduleName.Substring(0, i);
            }

            if (CorModule.IsInMemory && !CorModule.IsDynamic)
            {
                //  in-memory modules need to be referenced only by : syntax
                // because CorModule.Name for those modules is always: "<unknown>".
                // Ideally ICorDebugModule.Name should be returning the metadata name, and
                // we'd have another method for an optional file name.
                return false;
            }

            bool isMatch;
            if (fullNameProvided)
            {
                isMatch = String.Compare(CorModule.Name, moduleName,
                                         true, CultureInfo.InvariantCulture) == 0;
            }
            else
            {
                bool checkOnlyFullName;
                if (moduleName.EndsWith("."))
                {
                    checkOnlyFullName = true;
                    moduleName = moduleName.Substring(0, moduleName.Length - 1);
                }
                else
                    checkOnlyFullName = false;

                isMatch = String.Compare(Path.GetFileName(CorModule.Name), moduleName,
                                         true, CultureInfo.InvariantCulture) == 0;

                // Dot at the end of module name explicitely says that we have specified an extension.
                // e.g. "a." will match only module named: "a." not "a..dll" or "a.dll".
                // On the other hand "a.dll" will match both modules "a.dll" and "a.dll.dll".

                if (!isMatch && !checkOnlyFullName)
                {
                    isMatch = String.Compare(Path.GetFileNameWithoutExtension(CorModule.Name), moduleName,
                                             true, CultureInfo.InvariantCulture) == 0;
                }
            }

            if (isMatch && appDomainNumberProvided)
            {
                // we'll check if the appdomain matches as well.
                isMatch =
                    m_process.AppDomains.Lookup(CorModule.Assembly.AppDomain).Number == appDomainNumber;
            }

            return isMatch;
        }

        /// <summary>
        /// Reloads the symbols for the module.
        /// </summary>
        /// <param name="force">Forces reloading of symbols that have already been successfully loaded.</param>
        public void ReloadSymbols(bool force)
        {
            if (m_isSymReaderInitialized == false)
                return;

            if (m_isSymReaderInitialized && m_symReader != null &&
                !force)
                return; // we don't want to reload symbols that has been sucessfully loaded

            if (EditsCounter > 0)
                throw new MDbgException("Cannot reload symbols for edited module " + CorModule.Name);

            // MdbgFunctions cache symbol information. This doesn't reset that cached info.
            m_isSymReaderInitialized = false;
            m_symReader = null;

            // clear the cache of functions. This is necessary since the cache contains also
            // information from symbol files. Reloding the files might cause the information
            // in the cache to become stale.
            m_functions.Clear();
        }

        /// <summary>
        /// Updates the symbols for the module.
        /// </summary>
        /// <param name="symbolStream">New IStream to use for symbol reading.
        /// If this is null, unloads the symbols for this module.</param>
        /// <returns></returns>
        public bool UpdateSymbols(IStream symbolStream)
        {
            if (symbolStream == null)
            {
                // Leave m_isSymReaderInitialized so that we don't automatically reload.
                // MDbgFunction objects cache symbol information. This won't reset that cache.
                m_isSymReaderInitialized = true;
                m_symReader = null;
                return true;
            }

            ISymbolReader newSymReader = (SymBinder as ISymbolBinder2).GetReaderFromStream(Importer.RawCOMObject,
                                                                                           symbolStream);
            if (newSymReader == null)
                return false;
            m_symReader = newSymReader; // replace symbol reader with the updated one.
            m_isSymReaderInitialized = true;
            return true;
        }

        /// <summary>
        /// Apply an edit.  (Edit and Continue feature)
        /// </summary>
        /// <param name="deltaMetadataFile">File containing the Metadata delta.</param>
        /// <param name="deltaILFile">File containing the IL delta.</param>
        /// <param name="deltaPdbFile">File containing the PDB delta.</param>
        /// <param name="editSourceFile">The edited source file. WARNING - this param may be removed in next release.</param>
        public void ApplyEdit(string deltaMetadataFile,
                              string deltaILFile,
                              string deltaPdbFile,
                              string editSourceFile
            )
        {
            if (SymReader == null && deltaPdbFile != null)
                throw new MDbgException("Cannot update symbols on module without loaded symbols.");

            // read arguments from files
            byte[] deltaMeta;
            using (FileStream dmetaFile = File.OpenRead(deltaMetadataFile))
            {
                deltaMeta = new byte[dmetaFile.Length];
                dmetaFile.Read(deltaMeta, 0, deltaMeta.Length);
            }

            byte[] deltaIL;
            using (FileStream dilFile = File.OpenRead(deltaILFile))
            {
                deltaIL = new byte[dilFile.Length];
                dilFile.Read(deltaIL, 0, deltaIL.Length);
            }

            CorModule.ApplyChanges(deltaMeta, deltaIL);

            if (deltaPdbFile != null)
            {
                // apply dpdb to the symbol store.
                ISymbolReader sr = SymReader;
                (sr as ISymbolReader2).UpdateSymbolStore(deltaPdbFile, null);
            }

            // save file name into of the edit
            if (m_editsSources == null)
            {
                Debug.Assert(EditsCounter == 0); // we don't have any edits
                m_editsSources = new ArrayList();
            }

            m_editsSources.Add(editSourceFile);
            m_editsCounter++;
        }

        /// <summary>
        /// Gets the MDbgFunction for a given CorFunction.
        /// </summary>
        /// <param name="managedFunction">The CorFunction to lookup.</param>
        /// <returns>The coresponding MDbgFunction.</returns>
        public MDbgFunction GetFunction(CorDebug.CorFunction managedFunction)
        {
            return m_functions.Get(managedFunction);
        }

        /// <summary>
        /// Gets the MDbgFunction for a given Function Token.
        /// </summary>
        /// <param name="functionToken">The Function Token to lookup.</param>
        /// <returns>The coresponding MDbgFunction.</returns>
        public MDbgFunction GetFunction(int functionToken)
        {
            CorFunction f = m_module.GetFunctionFromToken(functionToken);
            Debug.Assert(f != null);
            return GetFunction(f);
        }

        /// <summary>
        /// Gets the name of edit file that was to used to do specified edits.
        /// </summary>
        /// <param name="editNumber">Which edit to lookup.</param>
        /// <returns>The source file used to perform the given edit number.</returns>
        public string GetEditsSourceFile(int editNumber)
        {
            Debug.Assert(editNumber <= m_editsCounter);
            Debug.Assert(editNumber > 0);
            if (! (editNumber <= m_editsCounter ||
                   editNumber > 0))
                throw new ArgumentException();

            if (m_editsSources == null)
                return null;
            return (string) m_editsSources[editNumber - 1];
        }
    }
}
