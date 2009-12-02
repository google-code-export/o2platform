// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Drawing;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.External.Firefox.WebAutomation;
using O2.Kernel.CodeUtils;

namespace O2.External.Firefox.Ascx.WebAutomation
{
    public partial class ascx_InstallFirefox : UserControl
    {
        private const string installFireFoxWebPage = "http://www.mozilla.com/en-US/firefox/";
        private readonly Callbacks.dMethod callbackToRecheckIfFireFoxIsNowInstalled;

        public ascx_InstallFirefox(Callbacks.dMethod _callbackToRecheckIfFireFoxIsNowInstalled)
        {
            InitializeComponent();
            if (DesignMode == false)
            {
                callbackToRecheckIfFireFoxIsNowInstalled = _callbackToRecheckIfFireFoxIsNowInstalled;
                configureControl();
            }
        }

        private void configureControl()
        {
            if (tbPathToFirefoxDirectory.okThread(delegate { configureControl(); }))
            {
                tbPathToFirefoxDirectory.Text = FirefoxXul.pathToFirefoxInstallDir;
                checkIfFireFoxIsAvailable();
                openInstallFirefoxWebPage();
            }
        }

        private void tbPathToFirefoxDirectory_TextChanged(object sender, EventArgs e)
        {
            FirefoxXul.pathToFirefoxInstallDir = tbPathToFirefoxDirectory.Text;
            checkIfFireFoxIsAvailable();
        }

        private void checkIfFireFoxIsAvailable()
        {
            tbPathToFirefoxDirectory.BackColor = (FirefoxXul.isFirefoxInstalled()) ? Color.LightGreen : Color.LightPink;
        }

        private void openInstallFirefoxWebPage()
        {
            wbInstallFirefox.Navigate(installFireFoxWebPage);
        }

        private void btAfterInstall_Click(object sender, EventArgs e)
        {
            callbackToRecheckIfFireFoxIsNowInstalled();
        }
    }
}
