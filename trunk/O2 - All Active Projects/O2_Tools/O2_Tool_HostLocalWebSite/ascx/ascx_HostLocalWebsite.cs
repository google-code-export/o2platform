using System;
using System.IO;
using System.Windows.Forms;
using O2.Tool.HostLocalWebsite;
using O2.Tool.HostLocalWebsite.classes;
using O2.Tool.HostLocalWebsite.classes;

namespace O2.Tool.HostLocalWebsite.ascx
{
    public partial class ascx_HostLocalWebsite : UserControl
    {
        public ascx_HostLocalWebsite()
        {
            InitializeComponent();
            ascx_DropObject1.Text = "Drop Folder To Run";
        }

        private void onDispose()
        {
            webservices.StopWebService();
        }

        private void ascx_WebServiceScan_Load(object sender, EventArgs e) // this is not being fired at the moent
        {
            if (DesignMode == false)
            {
                DI.log.error("THIS WAS NOT BEING FIRED IN DEVELOPMENT!!!");
                setupGui();
            }
        }

        public void setupGui()
        {            
            tbSettings_Exe.Text = webservices.sExe;
            tbSettings_Port.Text = webservices.sPort;
            tbSettings_Path.Text = webservices.sPath;
            tbSettings_VPath.Text = webservices.sVPath;
        }

        private void btStartWebService_Click(object sender, EventArgs e)
        {
            if (tbSettings_Path.Text == "")
                setupGui();

            if (tbSettings_Path.Text[tbSettings_Path.Text.Length - 1] == '\\')
                tbSettings_Path.Text = tbSettings_Path.Text.Substring(0, tbSettings_Path.Text.Length - 1);

            webservices.StopWebService();
            webservices.StartWebService();
            tbUrlToLoad.Text = webservices.GetWebServiceURL();
        }

        private void btSetTestEnvironment_Click(object sender, EventArgs e)
        {
            setupGui();
        }

        private void tbSettings_TextChanged(object sender, EventArgs e)
        {
            webservices.setExe(tbSettings_Exe.Text);
            webservices.setPort(tbSettings_Port.Text);
            webservices.setPath(tbSettings_Path.Text);
            webservices.setVPath(tbSettings_VPath.Text);
        }

        private void tbUrlToLoad_TextChanged(object sender, EventArgs e)
        {
            wbWebServices.Navigate(tbUrlToLoad.Text);
        }

        private void btReloadUrl_Click(object sender, EventArgs e)
        {
            wbWebServices.Navigate(tbUrlToLoad.Text);
        }

        private void ascx_DropObject1_eDnDAction_ObjectDataReceived_Event(object oObject)
        {
            String sItemToLoad = oObject.ToString();
            if (Directory.Exists(sItemToLoad))
                startWebServerOnDirectory(sItemToLoad);
            else
                DI.log.error("Item dropped must be a folder");
        }

        public void startWebServerOnDirectory(string sDirectoryToProcess)
        {
            if (tbSettings_Exe.Text == "")
                tbSettings_Exe.Text = webservices.sExe;
            tbSettings_Port.Text = (50000 + new Random().Next(10000)).ToString();
            tbSettings_Path.Text = sDirectoryToProcess;
            tbSettings_VPath.Text = "/" + Path.GetFileName(sDirectoryToProcess);
            btStartWebService_Click(null, null);
        }
    }
}