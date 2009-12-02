// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.Cmd.SpringMvc.Objects;
using O2.DotNetWrappers.DotNet;
using O2.Kernel.CodeUtils;
using O2.Kernel.Interfaces.CIR;

namespace O2.Cmd.SpringMvc.Ascx
{
    public partial class ascx_SpringMvcMappings : UserControl
    {
        public ascx_SpringMvcMappings()
        {
            InitializeComponent();
        }

        



        private void cbLoadedControlersViewMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            showLoadedControllers();
        }

        private void ascx_SpringMvcMappings_Load(object sender, EventArgs e)
        {
            onLoad();
        }

        private void tvControllers_DragEnter(object sender, DragEventArgs e)
        {
            Dnd.setEffect(e);
        }

        private void tvControllers_DragDrop(object sender, DragEventArgs e)
        {
            handleDrop(Dnd.tryToGetFileOrDirectoryFromDroppedObject(e));
        }

        private void tvControllers_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Callbacks.raiseRegistedCallbacks(_onTreeViewSelect, new object[] {tvControllers});
        }

        private void llClearLoadedMappedControllers_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            loadedSpringMvcControllers = new List<SpringMvcController>();
            showLoadedControllers();
        }
     
    }
}
