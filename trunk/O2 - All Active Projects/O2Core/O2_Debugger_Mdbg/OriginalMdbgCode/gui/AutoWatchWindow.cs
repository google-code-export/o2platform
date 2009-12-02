// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------

#region Using directives

using System.Windows.Forms;
using O2.Debugger.Mdbg.Debugging.MdbgEngine;
using O2.Debugger.Mdbg.Debugging.MdbgEngine;
using O2.Debugger.Mdbg.Debugging.MdbgEngine;
using O2.Debugger.Mdbg.Tools.Mdbg.Extension;
using O2.Debugger.Mdbg.Tools.Mdbg.Extension;
using O2.Debugger.Mdbg.Tools.Mdbg.Extension;

#endregion

namespace O2.Debugger.Mdbg.gui
{
    internal partial class AutoWatchWindow : DebuggerToolWindow
    {
        public AutoWatchWindow(MainForm mainForm)
            : base(mainForm)
        {
            InitializeComponent();
            // Hook handler for lazily expanding
            treeView1.BeforeExpand += treeView1_BeforeSelect;
        }

        // Called On UI thread.
        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            Util.TryExpandNode(MainForm, e.Node);
        }

        // Called On UI thread.
        protected override void RefreshToolWindowInternal()
        {
            MDbgFrame frame = null;

            MDbgValue[] locals = null;
            MDbgValue[] args = null;

            MainForm.ExecuteOnWorkerThreadIfStoppedAndBlock(delegate(MDbgProcess proc)
                                                                {
                                                                    frame = GetCurrentFrame(proc);
                                                                    if (frame == null)
                                                                    {
                                                                        return;
                                                                    }

                                                                    // Add Vars to window.            
                                                                    MDbgFunction f = frame.Function;

                                                                    locals = f.GetActiveLocalVars(frame);
                                                                    args = f.GetArguments(frame);
                                                                });

            if (frame == null)
            {
                return;
            }

            // Reset
            TreeView t = treeView1;
            t.BeginUpdate();
            t.Nodes.Clear();

            // Add Vars to window.            
            if (locals != null)
            {
                foreach (MDbgValue v in locals)
                {
                    Util.PrintInternal(MainForm, v, t.Nodes);
                }
            }

            if (args != null)
            {
                foreach (MDbgValue v in args)
                {
                    Util.PrintInternal(MainForm, v, t.Nodes);
                }
            }

            t.EndUpdate();
        } // refresh
    } // AutoWatchWindow
}
