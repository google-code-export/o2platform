using System;
using System.Drawing;
using System.Windows.Forms;

namespace O2.Legacy.CoreLib.Ascx
{
    public partial class ascx_testsViewer : UserControl
    {
        public int iFailedTests;
        public int iPassedTests;
        public String sTestName = "";

        public ascx_testsViewer()
        {
            InitializeComponent();
        }

        public ascx_testsViewer(String sTestName)
        {
            InitializeComponent();
            setTestName(sTestName);
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        public void setTestName(String sTestName)
        {
            Text = sTestName;
            this.sTestName = sTestName;
            lbTestName.Text = sTestName;
        }

        public void setPassedText(String sText)
        {
            lbPassedText.Text = sText;
        }

        public void setFailedText(String sText)
        {
            lbFailedText.Text = sText;
        }

        public void addItemToTestList(String sText, bool bPassed)
        {
            int iNewRowId = dgvTests.Rows.Add(new[] {sText});
            if (bPassed)
            {
                dgvTests.Rows[iNewRowId].DefaultCellStyle.BackColor = Color.Green;
                dgvTests.Rows[iNewRowId].Selected = false;
                iPassedTests++;
                lbPassedText.Text = iPassedTests.ToString();
            }
            else
            {
                dgvTests.Rows[iNewRowId].DefaultCellStyle.BackColor = Color.Red;
                dgvTests.Rows[iNewRowId].Selected = false;
                iFailedTests++;
                lbPassedText.Text = iFailedTests.ToString();
            }
            lbPassedText.Focus();
        }

        public void clearItems()
        {
            dgvTests.Rows.Clear();
            iPassedTests = 0;
            iFailedTests = 0;
        }
    }
}