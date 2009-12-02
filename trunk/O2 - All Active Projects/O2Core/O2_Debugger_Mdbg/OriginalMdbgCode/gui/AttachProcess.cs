//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------

#region Using directives

using System;
using System.Diagnostics;
using System.Windows.Forms;
using O2.Debugger.Mdbg.Debugging.CorDebug;
using O2.Debugger.Mdbg.Debugging.CorDebug;
using O2.Debugger.Mdbg.Debugging.CorDebug;
using O2.Debugger.Mdbg.Debugging.CorPublish;
using O2.Debugger.Mdbg.Debugging.CorPublish;
using O2.Debugger.Mdbg.Debugging.CorPublish;

#endregion

namespace O2.Debugger.Mdbg.gui
{
    internal partial class AttachProcess : Form
    {
        private int m_pid;

        public AttachProcess()
        {
            InitializeComponent();

            RefreshProcesses();
        }

        public int SelectedPid
        {
            get { return m_pid; }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            RefreshProcesses();
        }

        private void RefreshProcesses()
        {
            listBoxProcesses.Items.Clear();

            CorPublish cp = null;

            int curPid = Process.GetCurrentProcess().Id;
            try
            {
                int count = 0;

                cp = new CorPublish();
                {
                    foreach (CorPublishProcess cpp in cp.EnumProcesses())
                    {
                        if (curPid != cpp.ProcessId) // let's hide our process
                        {
                            string version = CorDebugger.GetDebuggerVersionFromPid(cpp.ProcessId);
                            string s = "[" + cpp.ProcessId + "] [ver=" + version + "] " + cpp.DisplayName;
                            listBoxProcesses.Items.Add(new Item(cpp.ProcessId, s));
                            count++;
                        }
                    }
                } // using

                if (count == 0)
                {
                    listBoxProcesses.Items.Add(new Item(0, "(No active processes)"));
                }
            }
            catch (Exception)
            {
                if (cp == null)
                {
                    listBoxProcesses.Items.Add(new Item(0, "(Can't enumerate processes"));
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            m_pid = 0;
            Close();
        }


        private void buttonAttach_Click(object sender, EventArgs e)
        {
            object o = listBoxProcesses.SelectedItem;
            var x = (Item) o;
            m_pid = x.Pid;

            Close();
        }

        #region Nested type: Item

        private class Item
        {
            private readonly int m_pid;
            private readonly string m_stName;

            public Item(int pid, string stName)
            {
                m_stName = stName;
                m_pid = pid;
            }

            public int Pid
            {
                get { return m_pid; }
            }

            public override string ToString()
            {
                return m_stName;
            }
        }

        #endregion

        // end refresh
    }
}