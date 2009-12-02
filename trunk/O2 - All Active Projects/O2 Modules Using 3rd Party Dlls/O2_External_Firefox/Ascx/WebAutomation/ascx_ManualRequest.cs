using System;
using System.Windows.Forms;

namespace O2.External.Firefox.Ascx.WebAutomation
{
    public partial class ascx_ManualRequest : UserControl
    {
        public ascx_ManualRequest()
        {
            InitializeComponent();
        }

        private void btOpenManualRequest_Click(object sender, EventArgs e)
        {
            DI.log.error("not implemented");
            // tbManualRequestContents.Text = WebRequests.getUrlContents(tbManualRequestUrl.Text, true);
        }
    }
}