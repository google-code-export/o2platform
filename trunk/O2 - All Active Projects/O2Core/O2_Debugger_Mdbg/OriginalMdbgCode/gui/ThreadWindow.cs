// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------

#region Using directives

using System;
using System.Diagnostics;
using System.Windows.Forms;
using O2.Debugger.Mdbg.Debugging.CorDebug.NativeApi;
using O2.Debugger.Mdbg.Debugging.CorDebug.NativeApi;
using O2.Debugger.Mdbg.Debugging.CorDebug.NativeApi;
using O2.Debugger.Mdbg.Debugging.MdbgEngine;
using O2.Debugger.Mdbg.Debugging.MdbgEngine;
using O2.Debugger.Mdbg.Debugging.MdbgEngine;
using O2.Debugger.Mdbg.Tools.Mdbg.Extension;
using O2.Debugger.Mdbg.Tools.Mdbg.Extension;
using O2.Debugger.Mdbg.Tools.Mdbg.Extension;

#endregion

namespace O2.Debugger.Mdbg.gui
{
    // Tool window to display thread list.
    // Deriving from a Generic class seems to confuse the Designer in VS2005 Beta 1. One workaround is to
    // switch the derived class to "Form", use the designer, and then restore the derived class so we can build.
    internal partial class ThreadWindow :
        DebuggerListWindow<MDbgThread>
        //Form
    {
        public ThreadWindow(MainForm mainForm)
            : base(mainForm, "Threads not available while process is running")
        {
            InitializeComponent();

            // Prep the context menu that we use to Freeze / Thaw threads.
            m_menu = new ContextMenu();
            m_menu.Popup += Popup;
            ContextMenu = m_menu;


            listBox1.DoubleClick += OnSelectionChanged;


            Text = "Thread List";
        }

        #region Context Menu to Freeze / Thaw Threads

        private readonly ContextMenu m_menu;

        // Called when ContextMenu is about to popup
        // Called on UI thread.
        private void Popup(object sender, EventArgs args)
        {
            // Don't process UI events if process is running.
            if (!MainForm.IsProcessStopped)
            {
                return;
            }

            string st = null;

            MDbgThread t = SelectedItem;
            if (t == null)
            {
                return;
            }

            // Get selection.
            MainForm.ExecuteOnWorkerThreadIfStoppedAndBlock(delegate
                                                                {
                                                                    st = IsFrozen(t) ? "Thaw" : "Freeze";
                                                                    st += " thread=" + t.Number;
                                                                });
            if (st == null)
            {
                return;
            }

            m_menu.MenuItems.Clear();
            m_menu.MenuItems.Add(st, OnThreadFrozenToggled);
        }

        // Invoked when ContextMenu item is selected to toggle Frozen/Thawed status.
        // This is invoked on the currently selected thread, and that can't change while the ContextMenu is up.
        // Called on UI thread.
        private void OnThreadFrozenToggled(Object sender, EventArgs args)
        {
            MDbgThread t = SelectedItem;

            MainForm.ExecuteOnWorkerThreadIfStoppedAndBlock(delegate(MDbgProcess proc)
                                                                {
                                                                    Debug.Assert(proc != null);
                                                                    Debug.Assert(!proc.IsRunning);

                                                                    CorDebugThreadState state = t.CorThread.DebugState;
                                                                    bool fFrozen = (state &
                                                                                    CorDebugThreadState.THREAD_SUSPEND) ==
                                                                                   CorDebugThreadState.THREAD_SUSPEND;
                                                                    if (fFrozen)
                                                                    {
                                                                        // Thaw the thread
                                                                        state &= ~CorDebugThreadState.THREAD_SUSPEND;
                                                                    }
                                                                    else
                                                                    {
                                                                        // Freeze the thread
                                                                        state |= CorDebugThreadState.THREAD_SUSPEND;
                                                                    }
                                                                    t.CorThread.DebugState = state;
                                                                });

            // Need to redraw the window.
            RefreshToolWindow();
        }

        // Is the MDbgThread frozen?
        // Must be called on worker thread.
        private static bool IsFrozen(MDbgThread t)
        {
            CorDebugThreadState state = t.CorThread.DebugState;
            bool fFrozen = (state & CorDebugThreadState.THREAD_SUSPEND) == CorDebugThreadState.THREAD_SUSPEND;
            return fFrozen;
        }

        #endregion Context Menu to Freeze / Thaw Threads

        protected override ListBox ListBox
        {
            get { return listBox1; }
        }

        // Called when user selects a different thread in the list.
        // Called on UI thread.
        private void OnSelectionChanged(Object sender, EventArgs args)
        {
            MDbgThread t = SelectedItem;
            if (t == null)
            {
                return;
            }
            // If we have an threads in the list, then we must have an active process and 
            // valid thread collection.
            if (!MainForm.IsProcessStopped)
            {
                return;
            }

            MainForm.ExecuteOnWorkerThreadIfStoppedAndBlock(delegate(MDbgProcess proc)
                                                                {
                                                                    Debug.Assert(proc != null);
                                                                    Debug.Assert(!proc.IsRunning);

                                                                    proc.Threads.Active = t;
                                                                });

            RefreshMainWindow();
        }

        // Refresh the Threads window.
        public override void RefreshWhenStopped()
        {
            ListBox.ObjectCollection items = Items;
            items.Clear();


            string[] values = null;
            MDbgThread[] threads = null;

            MainForm.ExecuteOnWorkerThreadIfStoppedAndBlock(delegate(MDbgProcess proc)
                                                                {
                                                                    Debug.Assert(proc != null);
                                                                    Debug.Assert(!proc.IsRunning);

                                                                    MDbgThread tActive = proc.Threads.HaveActive
                                                                                             ? (proc.Threads.Active)
                                                                                             : null;

                                                                    values = new string[proc.Threads.Count];
                                                                    threads = new MDbgThread[values.Length];
                                                                    int idx = 0;

                                                                    foreach (MDbgThread t in proc.Threads)
                                                                    {
                                                                        string stFrame = "<unknown>";

                                                                        if (t.BottomFrame != null)
                                                                        {
                                                                            stFrame = t.BottomFrame.Function.FullName;
                                                                        }
                                                                        string stActive = (t == tActive) ? "*" : " ";

                                                                        string stFrozen = IsFrozen(t) ? "(FROZEN) " : "";

                                                                        string s = stActive + "(" + t.Number + ") TID=" +
                                                                                   t.Id + ", " + stFrozen + stFrame;
                                                                        //this.AddItem(s, t);
                                                                        values[idx] = s;
                                                                        threads[idx] = t;
                                                                        idx++;
                                                                    }
                                                                });

            for (int i = 0; i < values.Length; i++)
            {
                AddItem(values[i], threads[i]);
            }
        }
    } // end class ThreadWindow
} // end GUI namespace
