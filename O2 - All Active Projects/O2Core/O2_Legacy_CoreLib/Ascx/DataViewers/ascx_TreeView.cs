// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Windows.Forms;
using O2.DotNetWrappers.Windows;
using O2.Legacy.CoreLib;

namespace O2.Legacy.CoreLib.Ascx.DataViewers
{
    public partial class ascx_TreeView : UserControl
    {
        public UInt32 iMaxRecsToFetch = 20;

        public ascx_TreeView()
        {
            InitializeComponent();
        }

        public void setMaxRecsFetch(UInt32 iNewValue)
        {
            iMaxRecsToFetch = iNewValue;
        }

        public void viewObject(Object oObjectToView)
        {
            if (oObjectToView != null)
            {
                if (oObjectToView.GetType().Name == "TreeView")
                {
                    tvObject = (TreeView) oObjectToView;
                    tvObject.Refresh();
                    //TreeView tvData = (TreeView)oObjectToView;
                    //tvObject.Nodes.Add(tvData.Nodes[0]);                
                }
                else
                {
                    var rptvTreeViewPopulateEngine = new FillTreeViewWithSerializedObjectData();

                    rptvTreeViewPopulateEngine.init(oObjectToView, tvObject, null);
                    rptvTreeViewPopulateEngine.setMaxNumberOfArrayRecordsToFetch(iMaxRecsToFetch);
                    rptvTreeViewPopulateEngine.setRecursiveDepth(2);
                    rptvTreeViewPopulateEngine.loadObjectInTreeView();
                }

                DI.log.debug("In ascx_TreeView,  Object {0} loaded", oObjectToView.ToString());
            }
        }

        public void ascx_DropObject1_eDnDAction_ObjectDataReceived_Event(object oVar)
        {
            tvObject.Nodes.Clear();
            viewObject(oVar);
        }
    }
}
