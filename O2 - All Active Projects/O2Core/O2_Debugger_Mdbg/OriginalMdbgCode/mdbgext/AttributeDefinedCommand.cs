// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;
using O2.Debugger.Mdbg.O2Debugger;

namespace O2.Debugger.Mdbg.Tools.Mdbg
{
    /// <summary>
    /// This attribute describes the command.
    /// </summary>
    [
        AttributeUsage(
            AttributeTargets.Method,
            AllowMultiple = true,
            Inherited = false
            )
    ]
    public sealed class CommandDescriptionAttribute : Attribute
    {
        private static Hashtable m_resources;

        private string m_commandName;
        private bool m_isRepeatable = true;
        private string m_longHelp;
        private int m_minimumAbbrev;
        private Type m_resourceMgrKey;
        private string m_shortHelp;

        /// <summary>
        /// Returns the command name.
        /// </summary>
        /// <value>Name of the command.</value>
        public string CommandName
        {
            get { return m_commandName; }
            set
            {
                m_commandName = value;
                m_minimumAbbrev = m_commandName.Length;
            }
        }

        /// <summary>
        /// Returns the minimum number of characters you must use to invoke this command.
        /// </summary>
        /// <value>The minimum number of characters.</value>
        public int MinimumAbbrev
        {
            get { return m_minimumAbbrev; }
            set { m_minimumAbbrev = value; }
        }

        /// <summary>
        /// Returns a brief help message for the command.
        /// </summary>
        /// <value>The help message.</value>
        public string ShortHelp
        {
            get
            {
                if (m_resourceMgrKey != null)
                {
                    var rm = (ResourceManager) m_resources[m_resourceMgrKey];
                    if (rm == null)
                    {
                        return m_shortHelp;
                    }
                    string key = (UseHelpFrom != null) ? UseHelpFrom : m_commandName;
                    return rm.GetString(key + "_ShortHelp", CultureInfo.CurrentUICulture);
                }
                else
                {
                    return m_shortHelp;
                }
            }
            set { m_shortHelp = value; }
        }

        /// <summary>
        /// Returns a more detailed help message for the command.
        /// </summary>
        /// <value>The help message.</value>
        public string LongHelp
        {
            get
            {
                if (m_resourceMgrKey != null)
                {
                    var rm = (ResourceManager) m_resources[m_resourceMgrKey];
                    string key = UseHelpFrom != null ? UseHelpFrom : m_commandName;
                    return rm.GetString(key + "_LongHelp", CultureInfo.CurrentUICulture);
                }
                else
                {
                    return m_longHelp == null ? "usage: \n" + m_shortHelp : m_longHelp;
                }
            }
            set { m_longHelp = value; }
        }

        /// <summary>
        /// Returns if the command is repeatable (hitting enter again will repeat these commands)
        /// </summary>
        /// <value>true if the command is repeatable</value>
        public bool IsRepeatable
        {
            get { return m_isRepeatable; }
            set { m_isRepeatable = value; }
        }

        /// <summary>
        /// Gets or sets the Resource Manager Key.
        /// </summary>
        /// <value>The Resource Manager Key.</value>
        public Type ResourceManagerKey
        {
            get { return m_resourceMgrKey; }
            set
            {
                Debug.Assert(value != null);

                m_resourceMgrKey = value;
            }
        }

        /// <summary>
        /// Gets or sets where to get the help from.
        /// </summary>
        /// <value>Where to get the help from.</value>
        public string UseHelpFrom { get; set; }

        /// <summary>
        /// Registers the Resource Manager.
        /// </summary>
        /// <param name="key">Whet Type to use.</param>
        /// <param name="resourceManager">Which Resource Manager to register.</param>
        public static void RegisterResourceMgr(Type key, ResourceManager resourceManager)
        {
            Debug.Assert(resourceManager != null && key != null);
            if (key == null || resourceManager == null)
            {
                throw new ArgumentException();
            }

            if (m_resources == null)
            {
                m_resources = new Hashtable();
            }
            if (false == m_resources.ContainsKey(key))
            {
                m_resources.Add(key, resourceManager);

                //throw new ArgumentException("key already registered");
            }

            
        }
    }

    /// <summary>
    /// This class defines MDbg Attribute Defined Commands.
    /// </summary>
    public sealed class MDbgAttributeDefinedCommand : IMDbgCommand
    {
        private static readonly ListDictionary g_extensions = new ListDictionary();
        private static int g_freeAssemblySeqNumber;
        private readonly CommandDescriptionAttribute m_cmdDescr;
        private readonly MethodInfo m_mi;

        private MDbgAttributeDefinedCommand(MethodInfo methodInfo, CommandDescriptionAttribute descriptionAttribute)
        {
            m_mi = methodInfo;
            m_cmdDescr = descriptionAttribute;
            if (!g_extensions.Contains(LoadedFrom))
            {
                g_extensions.Add(LoadedFrom, g_freeAssemblySeqNumber++);
            }
        }

        #region IMDbgCommand Members

        /// <summary>
        /// Returns the command name.
        /// </summary>
        /// <value>Name of the command.</value>
        public string CommandName
        {
            get { return m_cmdDescr.CommandName; }
        }

        /// <summary>
        /// Returns the minimum number of characters you must use to invoke this command.
        /// </summary>
        /// <value>The minimum number of characters.</value>
        public int MinimumAbbrev
        {
            get { return m_cmdDescr.MinimumAbbrev; }
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="arguments">Arguments to pass to the command.</param>
        public void Execute(string arguments)
        {
            try
            {
                m_mi.Invoke(null, new object[] {arguments});
            }
            catch (Exception ex)
            {
                OriginalMDbgMessages.WriteLine("Exception while executing " + arguments + "    :   " + ex.Message + "\n\n" +
                                      ex.StackTrace);
                if (ex.InnerException != null)
                    OriginalMDbgMessages.WriteLine("    ## Innerexeption: " + ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Returns a brief help message for the command.
        /// </summary>
        /// <value>The help message.</value>
        public string ShortHelp
        {
            get { return m_cmdDescr.ShortHelp; }
        }

        /// <summary>
        /// Returns a more detailed help message for the command.
        /// </summary>
        /// <value>The help message.</value>
        public string LongHelp
        {
            get { return m_cmdDescr.LongHelp; }
        }

        /// <summary>
        /// Assembly the command was loaded from
        /// </summary>
        /// <value>The Assembly.</value>
        public Assembly LoadedFrom
        {
            get { return m_mi.DeclaringType.Assembly; }
        }

        /// <summary>
        /// Returns if the command is repeatable (hitting enter again will repeat these commands)
        /// </summary>
        /// <value>true if the command is repeatable</value>
        public bool IsRepeatable
        {
            get { return m_cmdDescr.IsRepeatable; }
        }

        int IComparable.CompareTo(object obj)
        {
            // we'll sort the commands first by extensions by which they were loaded
            // and then by it's name.
            if (! (obj is IMDbgCommand))
            {
                return 1; // unknown types always at the start of the list
            }

            var other = obj as IMDbgCommand;
            if (LoadedFrom != other.LoadedFrom)
            {
                return (int) g_extensions[LoadedFrom] - (int) g_extensions[other.LoadedFrom];
            }

            // commands are from same extension
            return String.Compare(CommandName, (obj as IMDbgCommand).CommandName);
        }

        #endregion

        /// <summary>
        /// Adds commands from type.
        /// </summary>
        /// <param name="commandSet">Command Set to add.</param>
        /// <param name="type">Type to add commands for.</param>
        public static void AddCommandsFromType(IMDbgCommandCollection commandSet, Type type)
        {
            foreach (MethodInfo mi in type.GetMethods())
            {
                object[] attribs = mi.GetCustomAttributes(typeof (CommandDescriptionAttribute), false);
                if (attribs != null)
                {
                    foreach (object o in attribs)
                    {
                        if (o is CommandDescriptionAttribute)
                        {
                            var cmd =
                                new MDbgAttributeDefinedCommand(mi, (CommandDescriptionAttribute) o);
                            Debug.Assert(cmd != null);
                            commandSet.Add(cmd);
                        }
                    }
                }
            }
        }
    }
}
