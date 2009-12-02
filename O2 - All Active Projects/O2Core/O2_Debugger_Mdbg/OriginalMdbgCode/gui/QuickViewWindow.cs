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
    // Tool window to evaluate simple expressions.
    internal partial class QuickViewWindow : DebuggerToolWindow
    {
        public QuickViewWindow(MainForm mainForm)
            : base(mainForm)
        {
            InitializeComponent();

            // Hook handler for lazily expanding
            treeView1.BeforeExpand += treeView1_BeforeExpand;
        }

        protected override void RefreshToolWindowInternal()
        {
            // Nothing to do in refresh since we only respond to 
            // the user entering a value.
        }

        // Trap "Enter" key on dialog input.
        // called on UI thread.
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) 13)
            {
                e.Handled = true;
                string arg = textBox1.Text;
                MDbgValue val = Resolve(arg);
                Util.Print(MainForm, val, treeView1);
            }
        }

        // Resolve the expression to a value.
        // Returns "Null" if we can't resolve the arg.
        // called on UI thread.
        private MDbgValue Resolve(string arg)
        {
            MDbgValue var = null;
            MainForm.ExecuteOnWorkerThreadIfStoppedAndBlock(delegate(MDbgProcess proc)
                                                                {
                                                                    MDbgFrame frame = GetCurrentFrame(proc);
                                                                    if (frame == null)
                                                                    {
                                                                        return;
                                                                    }

                                                                    var = proc.ResolveVariable(arg, frame);
                                                                });
            return var;
        }

        // When tree mode, add stuff to it.
        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            Util.TryExpandNode(MainForm, e.Node);
        }
    } // end QuickViewWindow
}
