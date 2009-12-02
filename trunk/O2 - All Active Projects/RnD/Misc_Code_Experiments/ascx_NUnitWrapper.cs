using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace O2.Rnd.Misc_Code_Experiments
{
    public partial class ascx_NUnitWrapper : UserControl
    {
        private NUnitWrapper nUnitWrapper;

        public ascx_NUnitWrapper()
        {
            InitializeComponent();
            tbPathToNUnitBinFolder.Text = NUnitWrapper.pathToNUnitBinFolder;
            if (false == checkIfNUnitIsAvailable())
                tcNunit.SelectedTab = tpConfig;
            else
                loadNUnitWrapper();
        }

        private bool checkIfNUnitIsAvailable()
        {
            bool isNUnitAvailble = Directory.Exists(NUnitWrapper.pathToNUnitBinFolder);
            tbPathToNUnitBinFolder.BackColor = (isNUnitAvailble) ? Color.LightGreen : Color.LightPink;
            lbCouldntFindNUnitError.Visible = !isNUnitAvailble;
            llGoToNUnitWrapper.Visible = isNUnitAvailble;
            return isNUnitAvailble;
        }

        private void tbPathToNUnitBinFolder_TextChanged(object sender, EventArgs e)
        {
            NUnitWrapper.pathToNUnitBinFolder = tbPathToNUnitBinFolder.Text;
            checkIfNUnitIsAvailable();
        }

        public void loadNUnitWrapper()
        {
            //    if (nUnitWrapper == null)
            {
                /*       nUnitWrapper = new NUnitWrapper(NUnitWrapper.pathToNUnitBinFolder);
                Form mainForm = nUnitWrapper.getMainForm();
                if (mainForm!= null)
                    mainForm.Show();*/

                /*if (nUnitWrapper.NUnitControl != null)
                    tpNUnitWrapper.Controls.Add(nUnitWrapper.NUnitControl);*/
            }
        }

        private void llGoToNUnitWrapper_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tcNunit.SelectedTab = tpNUnitWrapper;
            loadNUnitWrapper();
        }

        private void btStartNUnit_Click(object sender, EventArgs e)
        {
            startNUnit();
        }

        private void tvAssembliesInAppDomain_AfterSelect(object sender, TreeViewEventArgs e)
        {
        }

        public void startNUnit()
        {
            nUnitWrapper = new NUnitWrapper(NUnitWrapper.pathToNUnitBinFolder);
            string fileToLoadInNUnit = DI.config.ExecutingAssembly;
            //       nUnitWrapper.startNunitInSeparateAppDomain(fileToLoadInNUnit);            
        }

        public void loadAssemblyIntoAssemblyInvokeObject(object AssemblyToLoad)
        {
            assemblyInvoke.loadAssembly(AssemblyToLoad);
        }

        private void btRefreshAppDomainAssemblyList_Click(object sender, EventArgs e)
        {
//            AppDomainUtils.populateTreeNodeCollectionWithAppDomainAssemblies(tvAssembliesInAppDomain.Nodes, nUnitWrapper.appDomain);
            //return;
            //var appDomains = AppDomainUtils.GetAppDomains();
            try
            {
                /*  AppDomain appDomain = appDomains[2];
                var name = appDomain.FriendlyName;
          //      var activationContext = appDomain.ActivationContext;
                var assemblies = appDomain.GetAssemblies();
                */
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

            //appDomainManager.

            //O2Forms.populateTreeNodeCollectionWithAppDomainAssemblies(tvAssembliesInAppDomain.Nodes, nUnitWrapper.appDomain);
        }
    }
}