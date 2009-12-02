using System;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.External.Firefox.WebAutomation;
using O2.External.Firefox.WebAutomation.WebObjects;
using Skybound.Gecko;

namespace O2.External.Firefox.Ascx.WebAutomation
{
    public partial class ascx_WebAutomation : UserControl
    {
        #region Delegates

        public delegate void dMethod_HtmlPage(O2HtmlPage hpO2HtmlPage);

        #endregion
        public bool allOkWithFirefoxControl = false;
        public dMethod_HtmlPage eWebPageLoadedAndProcess;
        public string sDefaultUrlOnTextBox = @"http://www.google.com"; // "http://www.ouncelabs.com";
        public String sProcessedUrl = "";
        private GeckoWebBrowser webBrowser;

        public ascx_WebAutomation()
        {
            InitializeComponent();
        }

        private void ascx_WebAutomation_Load(object sender, EventArgs e)
        {
            if (DesignMode == false)
            {
                tbUrlToProcess.Text = sDefaultUrlOnTextBox;
                checkForFirefoxInstallation();
            }
        }

        public void checkForFirefoxInstallation()
        {
            /*    O2Thread.mtaThread(
                    () =>
                        {
                            Processes.Sleep(1000);*/
            if (false == allOkWithFirefoxControl)
                scGeckoBrowserAndTabControl.invokeOnThread(
                    () =>
                        {
                            try
                            {


                                if (FirefoxXul.is64BitOs())
                                {
                                    scGeckoBrowserAndTabControl.Panel2Collapsed = true;
                                    scGeckoBrowserAndTabControl.Panel1.Controls.Add(new ascx_x64 {Dock = DockStyle.Fill});
                                }
                                else if (false == FirefoxXul.isFirefoxInstalled())
                                {
                                    scGeckoBrowserAndTabControl.Panel2Collapsed = true;
                                    scGeckoBrowserAndTabControl.Panel1.Controls.Add(
                                        new ascx_InstallFirefox(checkForFirefoxInstallation) {Dock = DockStyle.Fill});
                                }
                                else
                                {
                                    scGeckoBrowserAndTabControl.Panel2Collapsed = false;
                                    webBrowser = FirefoxXul.createGeckoWebBrowser();
                                    if (webBrowser != null)
                                    {
                                        scGeckoBrowserAndTabControl.Panel1.Controls.Clear();
                                        var toString = webBrowser.ToString();
                                        scGeckoBrowserAndTabControl.Panel1.Controls.Add(webBrowser);
                                        btOpenRequestInWebBrowser_Click(null, null);
                                        webBrowser.DocumentCompleted +=
                                            (sender, e) => analyzeLoadedUrlHtmlObjects();
                                        allOkWithFirefoxControl = true;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                DI.log.error("in checkForFirefoxInstallation: {0}", ex.Message);

                            }
                        });
            /*
                    });        */
        }

        /*   private void wbWebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            analyzeLoadedUrlHtmlObjects();
        }*/

        private void tbUrl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter)
                processUrl(tbUrlToProcess.Text);
        }

        public void processUrl(O2Link lO2LinkToProcess)
        {
            //  processUrl(lO2LinkToProcess.sHref);
        }

        public void processUrl(String sUrlToProcess)
        {
            try
            {
                if (webBrowser == null)
                    checkForFirefoxInstallation();

                DI.log.debug("Processing URL:{0}", sUrlToProcess);
                sProcessedUrl = sUrlToProcess;
                tbUrlToProcess.Text = sProcessedUrl; // in case processUrl was invoked from an external script
                setDataViewersEnableState(false);
                //wbWebBrowser.Navigate(sProcessedUrl);
                if (webBrowser != null)
                    webBrowser.Navigate(sProcessedUrl);
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in ascx_WebAutomation.processUrl");
            }
        }

        public void analyzeLoadedUrlHtmlObjects()
        {
            lbLinks.Items.Clear();
            dgvFormFields.Rows.Clear();
            var hpHtmPage = new O2HtmlPage();
            try
            {
                hpHtmPage = O2HtmlPage.getHtmlPageObjectFromWebBrowserObject(webBrowser);

                foreach (O2Link lLink in hpHtmPage.lLinks)
                    lbLinks.Items.Add(lLink);
            }
            catch (Exception ex)
            {
                DI.log.error("in analyzeLoadedUrlHtmlObjects:{0}", ex.Message);
            }


            setDataViewersEnableState(true);
            raiseEvent_eWebPageLoadedAndProcess(hpHtmPage);
        }

        private void raiseEvent_eWebPageLoadedAndProcess(O2HtmlPage hpHtmPage)
        {
            if (eWebPageLoadedAndProcess != null)
                eWebPageLoadedAndProcess.Invoke(hpHtmPage);
        }


        public void setDataViewersEnableState(bool bState)
        {
            lbAutomation.Enabled = bState;
            dgvFormFields.Enabled = bState;
            lbLinks.Enabled = bState;
        }

        public void saveHtmlPage(String sTargetFolder)
        {
        }

        private void lbLinks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null != lbLinks.SelectedItem)
            {
                processUrl((O2Link) lbLinks.SelectedItem);
            }
        }


        private void btOpenRequestInWebBrowser_Click(object sender, EventArgs e)
        {
            processUrl(tbUrlToProcess.Text);
        }

        public void open(string utlToOpen)
        {
            processUrl(utlToOpen);
        }
        
    }
}