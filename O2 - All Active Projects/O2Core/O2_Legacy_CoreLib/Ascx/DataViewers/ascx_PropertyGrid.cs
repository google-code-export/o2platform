using System;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.Legacy.CoreLib;

namespace O2.Legacy.CoreLib.Ascx.DataViewers
{
    public partial class ascx_PropertyGrid : UserControl
    {
        public ascx_PropertyGrid()
        {
            InitializeComponent();
        }

        private void propertyGrid1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void propertyGrid1_DragDrop(object sender, DragEventArgs e)
        {
            Object oActionData = e.Data.GetData("ounceLabs.O2.classes.internals.o2DragAndDrop+o2DragAndDropAction");
            if (oActionData != null)
            {
                var ddaAction = (Dnd.DragAndDropAction) oActionData;
                if (ddaAction.aAction == Dnd.Action.ObjectData)
                {
                    pgPropertyGrid.SelectedObject = ddaAction.dActionData["Object"];
                    return;
                }
            }

            String[] sFormats = e.Data.GetFormats();
            if (sFormats.Length > 1)
            {
            }
            var oDataObjects = new Object[sFormats.Length];
            for (int i = 0; i < sFormats.Length; i++)
                oDataObjects[i] = e.Data.GetData(sFormats[i]);

            pgPropertyGrid.SelectedObject = oDataObjects;
        }

        public void viewObject(Object oObjectToView)
        {
            if (oObjectToView != null)
            {
                pgPropertyGrid.SelectedObject = oObjectToView;
                DI.log.debug("In ascx_PropertyGrid, Object {0} loaded", oObjectToView.ToString());
            }
        }

        public void dndArea_eDnDAction_ObjectDataReceived_Event(object oDndPayload)
        {
            pgPropertyGrid.SelectedObject = oDndPayload;
        }

        private void ascx_DropObject1_eDnDAction_ObjectDataReceived_Event(object oVar)
        {
            pgPropertyGrid.SelectedObject = null;
            pgPropertyGrid.SelectedObject = oVar;
        }
    }
}