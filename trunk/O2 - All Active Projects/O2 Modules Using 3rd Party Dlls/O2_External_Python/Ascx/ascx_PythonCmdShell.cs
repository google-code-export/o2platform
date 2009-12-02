using System;
using System.Windows.Forms;
using O2.External.Python.CPython;
using O2.External.Python.IronPython;
using O2.External.Python.Jython;


namespace O2.External.Python.Ascx
{
    public partial class ascx_PythonCmdShell : UserControl
    {
        
        public ascx_PythonCmdShell()
        {
            InitializeComponent();
        }

        private String lastCommand = "";

        private void tbCommandToExecute_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    lastCommand = tbCommandToExecute.Text;
                    executeCommand(tbCommandToExecute.Text);
                    tbCommandToExecute.Text = "";
                    break;
                case Keys.Up:
                    if (lbCommandHistory.SelectedIndex > 0)
                        lbCommandHistory.SelectedIndex--;
                    //tbCommandToExecute.Text = lastCommand;
                    //tbCommandToExecute.SelectionStart = tbCommandToExecute.TextLength;
                    break;
                case Keys.Down:
                    if (lbCommandHistory.SelectedIndex < lbCommandHistory.Items.Count - 1)
                        lbCommandHistory.SelectedIndex++;
                    else
                        tbCommandToExecute.Text = lbCommandHistory.SelectedItem.ToString();
                    //tbCommandToExecute.Text = "";
                    break;
            }
        }
        

        private void ascx_PythonCmdShell_Load(object sender, EventArgs e)
        {
            onLoad();
        }

        private void btStartIronPython_Click(object sender, EventArgs e)
        {
            lbCurrentEngine.Text = "Executing: IronPython";
            startIronPythonShell();
        }

        private void btStartJPython_Click(object sender, EventArgs e)
        {
            lbCurrentEngine.Text = "Executing: Jython";
            startJythonShell();
        }

        private void btKillProcess_Click(object sender, EventArgs e)
        {
            killCurrentPythonProcess();
        }

        private void tbCommandToExecute_TextChanged(object sender, EventArgs e)
        {

        }

        private void btStartIronPythonInNewWindow_Click(object sender, EventArgs e)
        {
            IronPythonExec.openIronPythonShellOnCmdExe();
        }

        private void btStartJythonInNewWindow_Click(object sender, EventArgs e)
        {
            JythonExec.openJythonShellOnCmdExe();
        }

        private void btStartCPythonInNewWindow_Click(object sender, EventArgs e)
        {
            CPythonExec.openCPythonShellOnCmdExe();
        }

        private void rtbShellWindow_TextChanged(object sender, EventArgs e)
        {

        }

        // this is not working 
        private void btStartCPython_Click(object sender, EventArgs e)
        {
            startCPythonShell();
        }

        private void lbCommandHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbCommandHistory.SelectedItem != null)            
                tbCommandToExecute.Text = lbCommandHistory.SelectedItem.ToString();

        }

        private void lbCommandHistory_DoubleClick(object sender, EventArgs e)
        {
            if (lbCommandHistory.SelectedItem != null)
            {
                var selectedCommand = lbCommandHistory.SelectedItem.ToString();
                executeCommand(selectedCommand, false);
            }
        }
      
        
    }
}