using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.Kernel.CodeUtils;

namespace O2.Debugger.Mdbg.O2Debugger
{
    public partial class ascx_DebugggedProcessInfo : UserControl
    {
        public ascx_DebugggedProcessInfo()
        {
            InitializeComponent();
        }


        private void lbCurrentModules_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (File.Exists(lbCurrentModules.Text))
                O2Messages.dotNetAssemblyAvailable(lbCurrentModules.Text);
        }

        private void lbLoadedAssemblies_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (File.Exists(lbLoadedAssemblies.Text))
                O2Messages.dotNetAssemblyAvailable(lbLoadedAssemblies.Text);
        }

        public void RefreshDebuggedProcessInformation()
        {
            O2Thread.mtaThread(() =>
            {
                try
                {
                    O2Forms.populateWindowsControlWithList(lbLoadedAssemblies, DI.o2MDbg.sessionData.getAssemblies());
                    O2Forms.populateWindowsControlWithList(lbCurrentAppDomains, DI.o2MDbg.sessionData.getAppDomains());
                    O2Forms.populateWindowsControlWithList(lbCurrentModules, DI.o2MDbg.sessionData.getModules());
                    O2Forms.populateWindowsControlWithList(lbCurrentThreads, DI.o2MDbg.sessionData.getThreads());
                }
                catch (Exception ex)
                {
                    DI.log.ex(ex, "in ascx_DebugggedProcessInfo.RefreshDebuggedProcessInformation");
                }                
            });
        }        

        private void llRefreshDebuggedProcessInformation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RefreshDebuggedProcessInformation();
        }

        private void ascx_DebugggedProcessInfo_Enter(object sender, EventArgs e)
        {
            RefreshDebuggedProcessInformation();
        }
    }
}
