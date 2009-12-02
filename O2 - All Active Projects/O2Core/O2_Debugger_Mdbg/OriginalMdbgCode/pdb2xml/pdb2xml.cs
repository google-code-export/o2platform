//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using O2.Debugger.Mdbg.Debugging.CorSymbolStore;
using O2.Debugger.Mdbg.Debugging.CorSymbolStore;
using O2.Debugger.Mdbg.Debugging.CorSymbolStore;

// For symbol store

namespace O2.Debugger.Mdbg.pdb2xml
{
    public static class Program
    {
        public static int Main2(string[] args)
        {
            Console.WriteLine("Test harness for PDB2XML.");
            if (args.Length < 2)
            {
                Usage();
                return 2;
            }
            if (args[0].ToUpperInvariant().TrimStart(new[] {'-', '/'}) == "REVERSE")
            {
                if (args.Length < 3 || args.Length > 4)
                {
                    Usage();
                    return 2;
                }
                string InputXml = args[1];
                string OutputAsm = args[2];

                XMLToPdb(InputXml, OutputAsm);
                return 0;
            }
            //else
            string InputAsm = args[0];
            string OutputXml = args[1];
            PdbToXML(InputAsm, OutputXml);
            return 0;
        } // end Main

        private static void Usage()
        {
            Console.WriteLine("Usage: Pdb2Xml Input OutputXml");
            Console.WriteLine("Usage: Pdb2Xml /reverse InputXml Output");
            Console.WriteLine("Input and Output may be a path to either an exe, a dll, or a pdb.");
        }

        private static void PdbToXML(string Input, string OutputXml)
        {
            // Get a Text Writer to spew the PDB to.
            var doc = new XmlDocument();
            XmlWriter xw = doc.CreateNavigator().AppendChild();

            // Do the pdb for the exe into an xml stream. 
            var converter = new Pdb2XmlConverter(xw, Input);
            converter.ReadPdbAndWriteToXml();
            xw.Close();

            // Save xml to disk
            doc.Save(OutputXml);
        }

        private static void XMLToPdb(string InputXml, string Output)
        {
            var doc = new XmlDocument();
            doc.Load(InputXml);

            // Process XML and emit PDB
            var converter = new Xml2PdbConverter(doc, Output);
            converter.ReadXmlWritePdb();
        }
    }

    /// <summary>
    /// Class to write out XML for a PDB.
    /// </summary>
    internal class Pdb2XmlConverter
    {
        private readonly Dictionary<string, int> m_fileMapping = new Dictionary<string, int>();
        private readonly XmlWriter m_writer;

        // Keep assembly so we can query metadata on it.
        private Assembly m_assembly;
        private string m_fileName;

        /// <summary>
        /// Initialize the Pdb to Xml converter. Actual conversion happens in ReadPdbAndWriteToXml.
        /// Passing a filename also makes it easy for us to use reflection to get some information 
        /// (such as enumeration)
        /// </summary>
        /// <param name="writer">XmlWriter to spew to.</param>
        /// <param name="fileName">Filename for exe/dll. This class will find the pdb to match.</param>
        internal Pdb2XmlConverter(XmlWriter writer, string fileName)
        {
            m_writer = writer;
            m_fileName = fileName;
        }

        // Maps files to ids. 

        /// <summary>
        /// Load the PDB given the parameters at the ctor and spew it out to the XmlWriter specified
        /// at the ctor.
        /// </summary>
        internal void ReadPdbAndWriteToXml()
        {
            // Actually load the files
            // Dynamically discover if there is 
            ISymbolReader reader = null;

            // Calling OpenScope on a pdb throws a COMException for CLDB_E_FILE_CORRUPT
            // If they passed a pdb file, we'll try for the exe or dll here in a sec
            if (String.Compare(Path.GetExtension(m_fileName), ".pdb", StringComparison.OrdinalIgnoreCase) != 0)
            {
                reader = SymbolAccess.GetReaderForFile(m_fileName);
            }
            if (reader == null)
            {
                m_fileName = Path.ChangeExtension(m_fileName, ".exe");
                if (File.Exists(m_fileName))
                {
                    reader = SymbolAccess.GetReaderForFile(m_fileName);
                }
            }
            if (reader == null)
            {
                m_fileName = Path.ChangeExtension(m_fileName, ".dll");
                if (File.Exists(m_fileName))
                {
                    reader = SymbolAccess.GetReaderForFile(m_fileName);
                }
            }
            if (reader == null)
            {
                Console.WriteLine("Error: No Symbol Reader could be initialized.");
                return;
            }

            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += CurrentDomain_ReflectionOnlyAssemblyResolve;
            m_assembly = Assembly.ReflectionOnlyLoadFrom(m_fileName);

            // Begin writing XML.
            m_writer.WriteStartDocument();
            m_writer.WriteComment("This is an XML file representing the PDB for '" + m_fileName + "'");
            m_writer.WriteStartElement("symbols");


            // Record what input file these symbols are for.
            m_writer.WriteAttributeString("file", m_fileName);

            WriteDocList(reader);
            WriteEntryPoint(reader);
            WriteAllMethods(reader);

            m_writer.WriteEndElement(); // "Symbols";
        }

        // In order to call GetTypes(), we need to manually resolve any assembly references.
        // For example, if a type derives from a type in another module, we need to resolve that module.
        private Assembly CurrentDomain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
        {
            // args.Name is the assembly name, not the filename.
            // Naive implementation that just assumes the assembly is in the working directory.
            // This does not have any knowledge about the initial assembly we were trying to load.
            Assembly a = Assembly.ReflectionOnlyLoad(args.Name);

            return a;
        }

        // Dump all of the methods in the given ISymbolReader to the XmlWriter provided in the ctor.
        private void WriteAllMethods(ISymbolReader reader)
        {
            m_writer.WriteComment("This is a list of all methods in the assembly that matches this PDB.");
            m_writer.WriteComment(
                "For each method, we provide the sequence tables that map from IL offsets back to source.");

            m_writer.WriteStartElement("methods");

            // Use reflection to enumerate all methods            
            foreach (Type t in m_assembly.GetTypes())
            {
                foreach (
                    MethodInfo methodReflection in
                        t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                     BindingFlags.Static | BindingFlags.DeclaredOnly))
                {
                    int token = methodReflection.MetadataToken;

                    m_writer.WriteStartElement("method");
                    {
                        m_writer.WriteAttributeString("name", t.FullName + "." + methodReflection.Name);
                        m_writer.WriteAttributeString("token", Util.AsToken(token));
                        // This localSigMetadataToken information actually comes from the metadata in the assembly because the symbol reading API does not provide it.
                        if (methodReflection.GetMethodBody() != null)
                        {
                            int lSMT = methodReflection.GetMethodBody().LocalSignatureMetadataToken;
                            if (lSMT != 0)
                            {
                                m_writer.WriteAttributeString("localSigMetadataToken", Util.AsToken(lSMT));
                            }
                        }
                        ISymbolMethod methodSymbol = reader.GetMethod(new SymbolToken(token));
                        if (methodSymbol != null)
                        {
                            WriteSequencePoints(methodSymbol);
                            WriteLocals(methodSymbol);
                            ISymbolScope[] children = methodSymbol.RootScope.GetChildren();
                            if (children.Length != 0)
                            {
                                WriteScopes((ISymbolScope2) children[0]);
                            }
                        }
                    }

                    m_writer.WriteEndElement(); // method
                }
            }
            m_writer.WriteEndElement();
        }

        private void WriteScopes(ISymbolScope2 scope)
        {
            m_writer.WriteStartElement("scope");
            {
                m_writer.WriteAttributeString("startOffset", Util.AsIlOffset(scope.StartOffset));
                m_writer.WriteAttributeString("endOffset", Util.AsIlOffset(scope.EndOffset));
                {
                    // If there are no locals, then this element will just be empty.
                    WriteLocalsHelper(scope, false);
                }
                foreach (ISymbolScope child in scope.GetChildren())
                {
                    WriteScopes((ISymbolScope2) child);
                }
            }
            m_writer.WriteEndElement(); // </scope>
        }

        // Write all the locals in the given method out to an XML file.
        // Since the symbol store represents the locals in a recursive scope structure, we need to walk a tree.
        // Although the locals are technically a hierarchy (based off nested scopes), it's easiest for clients
        // if we present them as a linear list. We will provide the range for each local's scope so that somebody
        // could reconstruct an approximation of the scope tree. The reconstruction may not be exact.
        // (Note this would still break down if you had an empty scope nested in another scope.
        private void WriteLocals(ISymbolMethod method)
        {
            m_writer.WriteStartElement("locals");
            {
                // If there are no locals, then this element will just be empty.
                WriteLocalsHelper((ISymbolScope2) method.RootScope, true);
            }
            m_writer.WriteEndElement();
        }

        // Helper method to write the local variables in the given scope.
        // Scopes match an IL range, and also have child scopes.
        private void WriteLocalsHelper(ISymbolScope2 scope)
        {
            WriteLocalsHelper(scope, true);
        }

        private void WriteLocalsHelper(ISymbolScope2 scope, bool IncludeChildScopes)
        {
            foreach (ISymbolVariable l in scope.GetLocals())
            {
                m_writer.WriteStartElement("local");
                {
                    m_writer.WriteAttributeString("name", l.Name);

                    // Each local maps to a unique "IL Index" or "slot" number.
                    // This index is what you pass to ICorDebugILFrame::GetLocalVariable() to get
                    // a specific local variable. 
                    Debug.Assert(l.AddressKind == SymAddressKind.ILOffset);
                    int slot = l.AddressField1;
                    m_writer.WriteAttributeString("il_index", Util.CultureInvariantToString(slot));

                    // Provide scope range
                    m_writer.WriteAttributeString("il_start", Util.AsIlOffset(scope.StartOffset));
                    m_writer.WriteAttributeString("il_end", Util.AsIlOffset(scope.EndOffset));
                    m_writer.WriteAttributeString("attributes", l.Attributes.ToString());
                    Byte[] b_signature = l.GetSignature();
                    m_writer.WriteAttributeString("signature", Util.ToHexString(b_signature));
                }
                m_writer.WriteEndElement(); // </local>
            }
            foreach (ISymbolConstant c in scope.GetConstants())
            {
                // BUGBUG - ISymbolConstants are written to the xml, but cannot be easily round-tripped.
                // The SigTokens cannot be easily retrieved from either the pdb or the assembly metadata
                m_writer.WriteStartElement("constant");
                {
                    m_writer.WriteAttributeString("name", c.GetName());
                    m_writer.WriteAttributeString("value", c.GetValue().ToString());
                    m_writer.WriteAttributeString("signature", Util.ToHexString(c.GetSignature()));
                }
                m_writer.WriteEndElement(); // </constant>
            }
            if (!IncludeChildScopes)
            {
                return;
            }
            foreach (ISymbolScope childScope in scope.GetChildren())
            {
                WriteLocalsHelper((ISymbolScope2) childScope);
            }
        }

        // Write the sequence points for the given method
        // Sequence points are the map between IL offsets and source lines.
        // A single method could span multiple files (use C#'s #line directive to see for yourself).        
        private void WriteSequencePoints(ISymbolMethod method)
        {
            m_writer.WriteStartElement("sequencepoints");

            int count = method.SequencePointCount;
            m_writer.WriteAttributeString("total", Util.CultureInvariantToString(count));

            // Get the sequence points from the symbol store. 
            // We could cache these arrays and reuse them.
            var offsets = new int[count];
            var docs = new ISymbolDocument[count];
            var startColumn = new int[count];
            var endColumn = new int[count];
            var startRow = new int[count];
            var endRow = new int[count];
            method.GetSequencePoints(offsets, docs, startRow, startColumn, endRow, endColumn);

            // Write out sequence points
            for (int i = 0; i < count; i++)
            {
                m_writer.WriteStartElement("entry");
                m_writer.WriteAttributeString("il_offset", Util.AsIlOffset(offsets[i]));

                // If it's a special 0xFeeFee sequence point (eg, "hidden"), 
                // place an attribute on it to make it very easy for tools to recognize.
                // See http://blogs.msdn.com/jmstall/archive/2005/06/19/FeeFee_SequencePoints.aspx
                if (startRow[i] == 0xFeeFee)
                {
                    m_writer.WriteAttributeString("hidden", XmlConvert.ToString(true));
                }
                //else
                {
                    m_writer.WriteAttributeString("start_row", Util.CultureInvariantToString(startRow[i]));
                    m_writer.WriteAttributeString("start_column", Util.CultureInvariantToString(startColumn[i]));
                    m_writer.WriteAttributeString("end_row", Util.CultureInvariantToString(endRow[i]));
                    m_writer.WriteAttributeString("end_column", Util.CultureInvariantToString(endColumn[i]));
                    m_writer.WriteAttributeString("file_ref", Util.CultureInvariantToString(m_fileMapping[docs[i].URL]));
                }
                m_writer.WriteEndElement();
            }

            m_writer.WriteEndElement(); // sequencepoints
        }

        // Write all docs, and add to the m_fileMapping list.
        // Other references to docs will then just refer to this list.
        private void WriteDocList(ISymbolReader reader)
        {
            m_writer.WriteComment("This is a list of all source files referred by the PDB.");

            int id = 0;
            // Write doc list
            m_writer.WriteStartElement("files");
            {
                ISymbolDocument[] docs = reader.GetDocuments();
                foreach (ISymbolDocument doc in docs)
                {
                    string url = doc.URL;

                    // Symbol store may give out duplicate documents. We'll fold them here
                    if (m_fileMapping.ContainsKey(url))
                    {
                        m_writer.WriteComment("There is a duplicate entry for: " + url);
                        continue;
                    }
                    id++;
                    m_fileMapping.Add(doc.URL, id);

                    m_writer.WriteStartElement("file");
                    {
                        m_writer.WriteAttributeString("id", Util.CultureInvariantToString(id));
                        m_writer.WriteAttributeString("name", doc.URL);
                        m_writer.WriteAttributeString("language", doc.Language.ToString());
                        m_writer.WriteAttributeString("languageVendor", doc.LanguageVendor.ToString());
                        m_writer.WriteAttributeString("documentType", doc.DocumentType.ToString());
                    }
                    m_writer.WriteEndElement(); // file
                }
            }
            m_writer.WriteEndElement(); // files
        }

        // Write out a reference to the entry point method (if one exists)
        private void WriteEntryPoint(ISymbolReader reader)
        {
            // If there is no entry point token (such as in a dll), this will throw.
            SymbolToken token = reader.UserEntryPoint;
            if (token.GetToken() == 0)
            {
                // If the Symbol APIs fail when looking for an entry point token, there is no entry point.
                m_writer.WriteComment(
                    "There is no entry point token such as a 'Main' method. This module is probably a '.dll'");
                return;
            }
            ISymbolMethod m = reader.GetMethod(token);

            Debug.Assert(m != null); // would have thrown by now.

            // Should not throw past this point
            m_writer.WriteComment(
                "This is the token for the 'entry point' method, which is the method that will be called when the assembly is loaded." +
                " This usually corresponds to 'Main'");

            m_writer.WriteStartElement("EntryPoint");
            WriteMethod(m);
            m_writer.WriteEndElement();
        }

        // Write out XML snippet to refer to the given method.
        private void WriteMethod(ISymbolMethod method)
        {
            m_writer.WriteElementString("methodref", Util.AsToken(method.Token.GetToken()));
        }
    }

    /// <summary>
    /// Class to write out PDB for an XML description.
    /// </summary>
    internal class Xml2PdbConverter
    {
        private readonly XmlDocument m_doc;
        private readonly Dictionary<int, ISymbolDocumentWriter> m_docWriters;
        private readonly string m_outputAssembly;
        private readonly Dictionary<int, List<int>[]> m_sequencePoints;
        private readonly ISymbolWriter2 m_writer;

        internal Xml2PdbConverter(XmlDocument doc, string OutputAssembly)
        {
            m_doc = doc;
            m_docWriters = new Dictionary<int, ISymbolDocumentWriter>();
            m_sequencePoints = new Dictionary<int, List<int>[]>();

            string ext = Path.GetExtension(m_doc.DocumentElement.GetAttribute("file"));
            m_outputAssembly = Path.ChangeExtension(OutputAssembly, ext);

            object emitter = null;
            m_writer = SymbolAccess.GetWriterForFile(m_outputAssembly, ref emitter);
            // Rather than use the emitter here, we are just careful enough to emit pdb metadata that
            // matches what is already in the assembly image.
        }

        internal void ReadXmlWritePdb()
        {
            WriteFiles();
            WriteEntryPoint();
            WriteMethods();
            UpdatePEDebugHeaders();
            m_writer.Close();
        }

        /* 
         * This function (UpdatePEDebugHeaders) represents a bit of a hack.
         * 
         * When a debugger attempts to find debug information (pdb) 
         * for an image file (dll or exe), it uses a data-blob in the
         * image file to decide if the debug info "matches" this version.
         * 
         * Modifying the pdb without updating the image file would cause
         * a debugger to not load this new pdb due to mismatched information.
         * 
         * In most situations when somebody emits debug info, they are also 
         * emitting metadata and an assembly, so they can just include the
         * results of ISymUnmanagedWriter.GetDebugInfo in that new image file.
         * 
         * The intent of this sample is to demonstrate managed symbol reading
         * and writing without too much extra stuff.  For this reason, I didn't
         * include a full implementation of PEFile manipulation because it's
         * not really the point of this sample.  The code that does the PE File
         * manipulation was thrown together to get a specific job done and not
         * to be a good demonstration of how a compiler should deal with this.
         * Managed data structures to mirror the native format would be a better
         * approach if you needed more functionality out of the PEFile class.
         */

        private void UpdatePEDebugHeaders()
        {
            if (File.Exists(m_outputAssembly))
            {
                ImageDebugDirectory DebugDirectory;
                byte[] DebugInfo = m_writer.GetDebugInfo(out DebugDirectory);
                var file = new PEFile(m_outputAssembly);
                file.UpdateHeader(DebugInfo);
            }
            else
            {
                Console.WriteLine("Warning: Assembly couldn't be found to update.");
                Console.WriteLine("New pdb won't \"match\" any assembly and will thus not be useful to a debugger.");
            }
        }

        private void WriteSequencePoints(XmlElement node)
        {
            foreach (int fileRef in m_docWriters.Keys)
            {
                foreach (var list in m_sequencePoints[fileRef])
                {
                    list.Clear();
                }
            }
            foreach (XmlElement entry in node["sequencepoints"].ChildNodes)
            {
                int fileRef = Util.ToInt32(entry.GetAttribute("file_ref"));
                m_sequencePoints[fileRef][(int) SequencePoint.il_offset].Add(
                    Util.ToInt32(entry.GetAttribute("il_offset"), 16));
                m_sequencePoints[fileRef][(int) SequencePoint.start_row].Add(
                    Util.ToInt32(entry.GetAttribute("start_row")));
                m_sequencePoints[fileRef][(int) SequencePoint.start_column].Add(
                    Util.ToInt32(entry.GetAttribute("start_column")));
                m_sequencePoints[fileRef][(int) SequencePoint.end_row].Add(Util.ToInt32(entry.GetAttribute("end_row")));
                m_sequencePoints[fileRef][(int) SequencePoint.end_column].Add(
                    Util.ToInt32(entry.GetAttribute("end_column")));
            }
            foreach (int file_ref in m_sequencePoints.Keys)
            {
                m_writer.DefineSequencePoints(
                    m_docWriters[file_ref],
                    m_sequencePoints[file_ref][(int) SequencePoint.il_offset].ToArray(),
                    m_sequencePoints[file_ref][(int) SequencePoint.start_row].ToArray(),
                    m_sequencePoints[file_ref][(int) SequencePoint.start_column].ToArray(),
                    m_sequencePoints[file_ref][(int) SequencePoint.end_row].ToArray(),
                    m_sequencePoints[file_ref][(int) SequencePoint.end_column].ToArray()
                    );
            }
        }

        private void WriteScopesAndLocals(XmlElement node, SymbolToken localSigToken)
        {
            Debug.Assert(m_writer != null, "XML Writer not innitialized");
            Debug.Assert(node != null, "Node must be passed.");
            m_writer.OpenScope(Util.ToInt32(node.GetAttribute("startOffset"), 16));
            foreach (XmlElement child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "scope":
                        WriteScopesAndLocals(child, localSigToken);
                        break;
                    case "local":
                        string name = child.GetAttribute("name");
                        int addr1 = Util.ToInt32(child.GetAttribute("il_index"));
                        int startOffset = Util.ToInt32(child.GetAttribute("il_start"), 16);
                        int endOffset = Util.ToInt32(child.GetAttribute("il_end"), 16);
                        int attributes = Util.ToInt32(child.GetAttribute("attributes"));
                        m_writer.DefineLocalVariable(name, attributes, localSigToken, (int) SymAddressKind.ILOffset,
                                                     addr1, 0, 0, startOffset, endOffset);
                        break;
                    case "constant":
                        m_writer.DefineConstant(child.GetAttribute("name"), child.GetAttribute("value"),
                                                Util.ToByteArray(child.GetAttribute("signature")));
                        break;
                    default:
                        throw new FormatException("Unrecognized Xml element");
                }
            }
            m_writer.CloseScope(Util.ToInt32(node.GetAttribute("endOffset"), 16));
        }

        private void WriteMethod(XmlElement node)
        {
            m_writer.OpenMethod(Util.AsSymToken(node.GetAttribute("token")));
            if (node.GetAttribute("localSigMetadataToken").Length != 0)
            {
                var SymTok = new SymbolToken(Util.ToInt32(node.GetAttribute("localSigMetadataToken"), 16));
                WriteScopesAndLocals(node["scope"], SymTok);
                WriteSequencePoints(node);
            }
            m_writer.CloseMethod();
        }

        private void WriteMethods()
        {
            foreach (XmlElement node in m_doc.GetElementsByTagName("method"))
            {
                WriteMethod(node);
            }
        }

        private void WriteEntryPoint()
        {
            XmlNode node = m_doc.GetElementsByTagName("EntryPoint")[0];
            m_writer.SetUserEntryPoint(Util.AsSymToken(node.FirstChild.InnerText));
        }

        private void WriteFiles()
        {
            foreach (XmlElement node in m_doc.GetElementsByTagName("file"))
            {
                ISymbolDocumentWriter docwriter = m_writer.DefineDocument(
                    node.GetAttribute("name"),
                    new Guid(node.GetAttribute("language")),
                    new Guid(node.GetAttribute("languageVendor")),
                    new Guid(node.GetAttribute("documentType"))
                    );
                int id = Util.ToInt32(node.GetAttribute("id"));
                m_docWriters.Add(id, docwriter);
                m_sequencePoints.Add(id, new List<int>[(int) SequencePoint.size]);
                for (int i = 0; i < (int) SequencePoint.size; i++)
                {
                    m_sequencePoints[id][i] = new List<int>();
                }
            }
        }

        #region Nested type: SequencePoint

        private enum SequencePoint
        {
            il_offset = 0,
            start_row = 1,
            start_column = 2,
            end_row = 3,
            end_column = 4,
            size = 5,
        }

        #endregion
    }

    internal static class Util
    {
        // Format a token to a string. Tokens are in hex.
        internal static string AsToken(int i)
        {
            return String.Format(CultureInfo.InvariantCulture, "0x{0:x}", i);
        }

        // Since we're spewing this to XML, spew as a decimal number.
        internal static string AsIlOffset(int i)
        {
            return AsToken(i);
        }

        // If I have a string of a hex token and I want a SymbolToken, here's how to do it
        internal static SymbolToken AsSymToken(string token)
        {
            return new SymbolToken(ToInt32(token, 16));
        }

        internal static int ToInt32(string input)
        {
            return ToInt32(input, 10);
        }

        internal static int ToInt32(string input, int numberBase)
        {
            return Convert.ToInt32(input, numberBase);
        }

        internal static string ToHexString(byte[] input)
        {
            var sb = new StringBuilder(input.Length);
            foreach (byte b in input)
            {
                sb.AppendFormat("{0:X2}", b);
            }
            return sb.ToString();
        }

        internal static byte[] ToByteArray(string input)
        {
            var retval = new byte[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                retval[i] = Convert.ToByte(input[i]);
            }
            return retval;
        }

        internal static string CultureInvariantToString(int input)
        {
            return input.ToString(CultureInfo.InvariantCulture);
        }

        internal static void Error(string message)
        {
            Console.WriteLine("Error: {0}", message);
            Debug.Assert(false, message);
        }
    }
}

// XmlPdbReader
