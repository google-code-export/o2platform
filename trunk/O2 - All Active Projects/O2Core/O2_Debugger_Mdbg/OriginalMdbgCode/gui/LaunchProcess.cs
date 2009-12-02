// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------

#region Using directives

using System;
using System.IO;
using System.Windows.Forms;

#endregion

namespace O2.Debugger.Mdbg.gui
{
    internal partial class LaunchProcess : Form
    {
        public LaunchProcess()
        {
            InitializeComponent();

            textBoxWorkingDir.Text = Directory.GetCurrentDirectory();
        }

        #region Properties

        // Properties for caller to get stuff.

        // Process working directory.
        private string m_Arguments;
        private string m_ProcessName;
        private string m_WorkingDir;

        public string WorkingDir
        {
            get { return m_WorkingDir; }
        }

        // Arguments to pass to process.

        public string Arguments
        {
            get { return m_Arguments; }
        }

        // Full path to process name
        // This will be null if the cancelled.

        public string ProcessName
        {
            get { return m_ProcessName; }
        }

        #endregion

        private void buttonLaunch_Click(object sender, EventArgs e)
        {
            // Need to cache results because once we close the form,we'll lose all
            // the text boxes.
            m_WorkingDir = textBoxWorkingDir.Text;
            m_Arguments = textBoxArgs.Text;
            m_ProcessName = textBoxProcessName.Text;
            Close();
        }

        private void buttonOpenProcess_Click(object sender, EventArgs e)
        {
            var f = new OpenFileDialog();
            f.DefaultExt = "exe";
            f.CheckFileExists = true;
            f.CheckPathExists = true;
            f.ValidateNames = true;
            f.InitialDirectory = textBoxWorkingDir.Text;
            f.Multiselect = false;
            f.Title = "Select executable to start debugging.";

            DialogResult x = f.ShowDialog();
            if (x != DialogResult.OK)
            {
                return;
            }

            textBoxProcessName.Text = f.FileName;
            textBoxWorkingDir.Text = Directory.GetCurrentDirectory();
        }
    }
}
