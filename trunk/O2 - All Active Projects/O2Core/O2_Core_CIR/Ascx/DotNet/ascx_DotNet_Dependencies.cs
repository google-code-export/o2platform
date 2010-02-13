// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Kernel.Interfaces.DotNet;
using System.IO;
using O2.External.O2Mono.MonoCecil;
using O2.Views.ASCX.classes;

namespace O2.Core.CIR.Ascx.DotNet
{
    public partial class ascx_DotNet_Dependencies : UserControl
    {

        private void ascx_DotNet_Dependencies_Load(object sender, EventArgs e)
        {
            gacBrowser.loadListOfGacAssemblies();
        }



        public ascx_DotNet_Dependencies()
        {
            InitializeComponent();
        }

        private void gacBrowser__onGacDllSelected(IGacDll selectedDll)
        {
            showDependenciesForFile(selectedDll.fullPath);
        }


        private void directoryLocal__onDirectoryClick(string selectedItem)
        {
            showDependenciesForFile(selectedItem);
        }
        

        public void showDependenciesForFile(string fileToProcess)
        {
            clearDependenciesOnViewControls();
            if (File.Exists(fileToProcess))
            {
                var cecilDependencies =  new CecilAssemblyDependencies(fileToProcess);
                var dependencies = cecilDependencies.calculateDependencies();                
                updateDependenciesFlatList(dependencies);
            }            
        }

        private void clearDependenciesOnViewControls()
        {
            tbDependenciesFlatList.invokeOnThread(
                () =>
                    {
                        tvDependenciesTreeView.Nodes.Clear();
                        tbDependenciesFlatList.Text = "";
                    });
        }

        private void updateDependenciesFlatList(Dictionary<string,string> dependencies)
        {
            tbDependenciesFlatList.invokeOnThread(
                () =>
                    {
                        var dependenciesText = new StringBuilder();
                        tableListWithDependencies.setDataTable(CreateDataTable.fromDictionary_StringString(dependencies,"name","location"));

                        foreach (var dependency in dependencies)
                        {
                            var splittedname = dependency.Key.Split(',');
                            if (splittedname.Count() > 0)
                                dependenciesText.AppendFormat("{0}          ( {1} )     : {2}\n", splittedname[0],dependency, dependency.Value);
                        }
                        tbDependenciesFlatList.Text = dependenciesText.ToString();
                    });
        }
        

    }

}
