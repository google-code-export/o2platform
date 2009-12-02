using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using O2.Debugger.Mdbg.O2Debugger.Objects;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;

namespace O2.Debugger.Mdbg.O2Debugger.Ascx
{
    public partial class  ascx_StartOrAttach
    {
        void updateGuiEnabledControlState()
        {
            if (DI.o2MDbg.IsActive)
            {
            }
            //gbStartProcess.Enabled = !debugggerActive;
            //gbAttachToRunningManagedProcess.Enabled = !debugggerActive;
            //btStartProcess.Enabled = !debugggerActive;

            refreshListOfAvailableManagedProcesses();
            populateListViewWithExecutablesInO2Folders(lvExecutablesInO2Dirs);

        }


        private void refreshListOfAvailableManagedProcesses()
        {
            if (ExtensionMethods.okThread((Control) lvManagedProcesses, delegate { refreshListOfAvailableManagedProcesses(); }))
            {
                lvManagedProcesses.Items.Clear();
                var processes = DI.o2MDbg.sessionData.getManagedProcesses();
                foreach (var process in processes.Keys)
                    lvManagedProcesses.Items.Add(
                        new ListViewItem(new[] { process.ToString(), processes[process] }));
            }
        }

        static void populateListViewWithExecutablesInO2Folders(ListView lvTargetListView)
        {
            var imageKey = 0;
            if (lvTargetListView.okThread(delegate { populateListViewWithExecutablesInO2Folders(lvTargetListView); }))
            {
                var executableFiles = new List<String>();
                executableFiles.AddRange(Files.getFilesFromDir_returnFullPath(DI.config.CurrentExecutableDirectory, "*.exe"));
                executableFiles.AddRange(Files.getFilesFromDir_returnFullPath(DI.config.O2TempDir, "*.exe"));
                lvTargetListView.Items.Clear();
                foreach (var file in executableFiles)
                {
                    var item = new ListViewItem.ListViewSubItem { Text = Path.GetFileName(file), Tag = file };
                    lvTargetListView.Items.Add(new ListViewItem(new[] { item }, imageKey));
                }
            }
        }

        
    }
}