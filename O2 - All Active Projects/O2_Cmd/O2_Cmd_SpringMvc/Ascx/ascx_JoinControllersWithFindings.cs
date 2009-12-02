using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;

namespace O2.Cmd.SpringMvc.Ascx
{
    public partial class ascx_JoinControllersWithFindings : UserControl
    {
        public ascx_JoinControllersWithFindings()
        {
            InitializeComponent();
        }

        private void ascx_JoinControllersWithFindings_Load(object sender, EventArgs e)
        {
            onLoad();
        }

        private void btLoadTestData_Click(object sender, EventArgs e)
        {
            springMvcMappings.loadMappedControllers(@"F:\_AppsToScan\springMVC_petclinic\_OunceApplication\O2 Data\petClinic.SpringMvcControllers");
            findingsViewerWith_ScanResults.loadO2Assessment(@"F:\_AppsToScan\springMVC_petclinic\_OunceApplication\O2 Data\petclinic - O2 scan.ozasmt");
        }

        private void btGenerateJspTraces_Click(object sender, EventArgs e)
        {
            generateJspTrace(findingsViewerWith_ScanResults.currentO2Findings);
        }

        private void springMvcMappings__onTreeViewSelect(TreeView treeView)
        {
            onSpringMvcMappingsTreeSelect(treeView);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }     

        
    }
}
