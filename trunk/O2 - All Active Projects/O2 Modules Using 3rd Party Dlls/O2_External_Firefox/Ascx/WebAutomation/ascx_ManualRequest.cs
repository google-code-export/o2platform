// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
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
