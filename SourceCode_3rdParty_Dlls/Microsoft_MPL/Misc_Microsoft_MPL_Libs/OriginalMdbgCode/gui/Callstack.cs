//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------

#region Using directives

using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
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
    internal partial class Callstack : DebuggerToolWindow
    {
        public Callstack(MainForm mainForm)
            : base(mainForm)
        {
            InitializeComponent();
        }


        // Add item to list
        // 'stText' is what we display in the list box.
        // 'frame' is the underlying frame associated with the text. Can be null if there's no frame.
        private void AddItem(string stText, MDbgFrame frame)
        {
            ListBox.ObjectCollection list = listBoxCallstack.Items;
            list.Add(new FramePair(frame, stText));
        }

        // Clear callstack for running
        public void MarkCallstackAsRunning()
        {
            ListBox.ObjectCollection list = listBoxCallstack.Items;
            list.Clear();
            AddItem("Callstack unavailable because process is either running or not available.", null);
        }

        // Update list w/ current callstack. Return an array of FramePair representing the callstack.
        // Run on worker thread.
        private static FramePair[] GetFrameList(MDbgThread thread)
        {
            // Populate listbox with frames.
            MDbgFrame f = thread.BottomFrame;
            MDbgFrame af = thread.HaveCurrentFrame ? thread.CurrentFrame : null;

            var l = new ArrayList();

            int i = 0;
            int depth = 20;
            bool verboseOutput = true;

            while (f != null && (depth == 0 || i < depth))
            {
                string line;
                if (f.IsInfoOnly)
                {
                    line = string.Format(CultureInfo.InvariantCulture, "[{0}]", f);
                }
                else
                {
                    string frameDescription = "<unknown>";
                    try
                    {
                        // Get IP info.
                        uint ipNative;
                        uint ipIL;
                        CorDebugMappingResult result;
                        f.CorFrame.GetNativeIP(out ipNative);
                        f.CorFrame.GetIP(out ipIL, out result);
                        string frameLocation = String.Format(CultureInfo.InvariantCulture,
                                                             " N=0x{0:x}, IL=0x{1:x} ({2})",
                                                             ipNative, ipIL, result);


                        // This may actually do a ton of work, including evaluating parameters.
                        frameDescription = f.ToString(verboseOutput ? "v" : null) + frameLocation;
                    }
                    catch (COMException)
                    {
                        if (f.Function != null)
                        {
                            frameDescription = f.Function.FullName;
                        }
                    }

                    line = string.Format(CultureInfo.InvariantCulture, "{0}{1}. {2}", f.Equals(af) ? "*" : " ", i,
                                         frameDescription);
                    ++i;
                }
                l.Add(new FramePair(f, line));
                f = f.NextUp;
            }
            if (f != null && depth != 0) // means we still have some frames to show....
            {
                l.Add(new FramePair(null,
                                    string.Format(CultureInfo.InvariantCulture,
                                                  "displayed only first {0} frames. For more frames use -c switch",
                                                  depth)
                          ));
            }

            return (FramePair[]) l.ToArray(typeof (FramePair));
        }

        // Refresh the callstack window.
        // runs on UI thread.
        protected override void RefreshToolWindowInternal()
        {
            // Information we need filled out.
            FramePair[] list = null;
            int number = 0;
            int id = 0;


            // Make cross thread call to access ICorDebug and fill out data
            MainForm.ExecuteOnWorkerThreadIfStoppedAndBlock(delegate(MDbgProcess proc)
                                                                {
                                                                    Debug.Assert(proc != null);
                                                                    Debug.Assert(!proc.IsRunning);

                                                                    MDbgThread thread = proc.Threads.Active;
                                                                    try
                                                                    {
                                                                        number = thread.Number;
                                                                        id = thread.Id;
                                                                        list = GetFrameList(thread);
                                                                    }
                                                                    catch (Exception)
                                                                    {
                                                                        list = null;
                                                                    }
                                                                });

            if (list == null || list.Length == 0)
            {
                MarkCallstackAsRunning();
            }
            else
            {
                ListBox.ObjectCollection l = listBoxCallstack.Items;
                l.Clear();

                // Set Title.
                Text = "Callstack on Thread #" + number + " (tid=" + id + ")";

                // Add items
                foreach (FramePair f in list)
                {
                    l.Add(f);
                }
            }
        }

        // Invoked when we change the selection on the list box.
        // This will change the current active frame in the debugger and refresh.
        private void listBoxCallstack_DoubleClicked(object sender, EventArgs e)
        {
            object o = listBoxCallstack.SelectedItem;
            var pair = (FramePair) o;

            MDbgFrame f = pair.m_frame;
            if (f == null)
            {
                return;
            }


            MainForm.ExecuteOnWorkerThreadIfStoppedAndBlock(delegate(MDbgProcess proc)
                                                                {
                                                                    Debug.Assert(proc != null);
                                                                    Debug.Assert(!proc.IsRunning);

                                                                    // Update callstack
                                                                    MDbgThread t = proc.Threads.Active;

                                                                    try
                                                                    {
                                                                        t.CurrentFrame = f;
                                                                    }
                                                                    catch (InvalidOperationException)
                                                                    {
                                                                        // if it throws an invalid op, then that means our frames somehow got out of sync
                                                                        // and we weren't fully refreshed.
                                                                        return;
                                                                    }
                                                                } // end delegate
                );

            // Need to refresh UI to show update.
            MainForm.ShowCurrentLocation();
            MainForm.Invalidate();
        } // end listBoxCallstack_DoubleClicked


        private void Callstack_Load(object sender, EventArgs e)
        {
        }

        #region Nested type: FramePair

        private class FramePair
        {
            private readonly String m_displayString;
            internal readonly MDbgFrame m_frame;

            public FramePair(MDbgFrame f, String s)
            {
                m_frame = f;
                m_displayString = s;
            }

            public override string ToString()
            {
                return m_displayString;
            }
        }

        #endregion

        // end SelectedIndexChanged
    }
}
