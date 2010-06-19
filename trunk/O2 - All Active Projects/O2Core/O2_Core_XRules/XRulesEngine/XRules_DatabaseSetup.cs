// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Windows.Forms;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.Windows;
using O2.Kernel.Interfaces.XRules;

namespace O2.Core.XRules.XRulesEngine
{
    public class XRules_DatabaseSetup
    {
        // Todo: change this function to reflect the new XRules SVN Sync wizard
        public static void installXRulesDatabase()
        {
           // if (XRules_Config.xRulesDatabase != null)
           //     XRules_Config.xRulesDatabase.installXRulesDatabase(XRules_Config.PathTo_XRulesDatabase_fromO2, XRules_Config.PathTo_XRulesTemplates);
           // else
           //     DI.log.info("There is no xRulesDatabase available");
        }
        
        public static void loadXRulesTemplates(ListBox lbTargetListBox)
        {
            lbTargetListBox.invokeOnThread(
                () =>
                    {
                        lbTargetListBox.Items.Clear();
                        lbTargetListBox.Items.AddRange(
                            Files.getFilesFromDir(XRules_Config.PathTo_XRulesTemplates).ToArray());
                        if (lbTargetListBox.Items.Count > 0)
                            lbTargetListBox.SelectedIndex = 0;
                    });
        }
    }
}
