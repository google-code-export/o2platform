// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------

#region Using directives

using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using O2.Debugger.Mdbg.Debugging.CorDebug;
using O2.Debugger.Mdbg.Debugging.CorDebug;
using O2.Debugger.Mdbg.Debugging.CorDebug;
using O2.Debugger.Mdbg.Debugging.MdbgEngine;
using O2.Debugger.Mdbg.Debugging.MdbgEngine;
using O2.Debugger.Mdbg.Debugging.MdbgEngine;
using O2.Debugger.Mdbg.Tools.Mdbg.Extension;
using O2.Debugger.Mdbg.Tools.Mdbg.Extension;
using O2.Debugger.Mdbg.Tools.Mdbg.Extension;

#endregion

namespace O2.Debugger.Mdbg.gui
{
    internal partial class ModuleWindow : DebuggerToolWindow
    {
        public ModuleWindow(MainForm mainForm)
            : base(mainForm)
        {
            InitializeComponent();
        }

        // Populate the module window with the current list.
        // Called on UI thread.
        protected override void RefreshToolWindowInternal()
        {
            ListView.ListViewItemCollection items = listView1.Items;
            items.Clear();

            ListViewItem[] temp = null;

            // Go to worker thread to collect information

            MainForm.ExecuteOnWorkerThreadIfStoppedAndBlock(delegate(MDbgProcess proc)
                                                                {
                                                                    Debug.Assert(proc != null);
                                                                    Debug.Assert(!proc.IsRunning);


                                                                    temp = new ListViewItem[proc.Modules.Count];
                                                                    int idx = 0;

                                                                    foreach (MDbgModule m in proc.Modules)
                                                                    {
                                                                        var sbFlags = new StringBuilder();

                                                                        if (m.SymReader == null)
                                                                        {
                                                                            sbFlags.Append("[No symbols]");
                                                                        }
                                                                        else
                                                                        {
                                                                            sbFlags.Append("[Symbols]");
                                                                        }

                                                                        string fullname = m.CorModule.Name;
                                                                        string directory =
                                                                            Path.GetDirectoryName(fullname);
                                                                        string name = Path.GetFileName(fullname);

                                                                        bool fIsDynamic = m.CorModule.IsDynamic;
                                                                        if (fIsDynamic)
                                                                        {
                                                                            sbFlags.Append("[Dynamic] ");
                                                                        }

                                                                        CorDebugJITCompilerFlags flags =
                                                                            m.CorModule.JITCompilerFlags;

                                                                        bool fNotOptimized = (flags &
                                                                                              CorDebugJITCompilerFlags.
                                                                                                  CORDEBUG_JIT_DISABLE_OPTIMIZATION) ==
                                                                                             CorDebugJITCompilerFlags.
                                                                                                 CORDEBUG_JIT_DISABLE_OPTIMIZATION;
                                                                        if (fNotOptimized)
                                                                        {
                                                                            sbFlags.Append("[Not-optimized] ");
                                                                        }
                                                                        else
                                                                        {
                                                                            sbFlags.Append("[Optimized] ");
                                                                        }

                                                                        // Columns: Id, Name, Path, Flags
                                                                        temp[idx++] = new ListViewItem(
                                                                            new[]
                                                                                {
                                                                                    m.Number.ToString(), name, directory
                                                                                    ,
                                                                                    sbFlags.ToString()
                                                                                }
                                                                            );
                                                                    }
                                                                }); // end worker


            if (temp != null)
            {
                foreach (ListViewItem x in temp)
                {
                    items.Add(x);
                }
            }
        }
    }
}
