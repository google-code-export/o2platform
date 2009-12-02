// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Windows.Forms;
using O2.Legacy.OunceV6.SavedAssessmentFile.classes;

namespace O2.Legacy.OunceV6.JoinTraces.classes
{
    public class creator
    {
        public static FindingViewItem createJoinedUpFindingViewItemFromTreeNodeWithFindingViewItemAsTags(
            TreeNode tnTreeNode)
        {
            var fviFindingViewItem = new FindingViewItem();

            fviFindingViewItem = joinFindingViewItemAndFollowTreeNodeChilds_Recursive(fviFindingViewItem, tnTreeNode);

            return fviFindingViewItem;
        }

        private static FindingViewItem joinFindingViewItemAndFollowTreeNodeChilds_Recursive(
            FindingViewItem fviTargetFindingViewItem, TreeNode tnTreeNodeWithTraces)
        {
            if (tnTreeNodeWithTraces != null)
            {
                if (tnTreeNodeWithTraces.Tag != null)
                {
                    if (fviTargetFindingViewItem.fFinding == null) // means this is the first one
                    {
                        fviTargetFindingViewItem =
                            VirtualTraces.createNewFindingViewItemFromFindingViewItem(
                                (FindingViewItem) tnTreeNodeWithTraces.Tag);
                    }
                    else
                    {
                        fviTargetFindingViewItem = VirtualTraces.connectTwoFindingNewItems(fviTargetFindingViewItem,
                                                                                           (FindingViewItem)
                                                                                           tnTreeNodeWithTraces.Tag);
                        /*( if (fviTargetFindingViewItem == null)
                        {                            
                             DI.log.info("appendTrace_FindingSourceToFindingSink error, happened for {0}", analyzer.getUniqueSignature(((FindingViewItem)tnTreeNodeWithTraces.Tag).fFinding, Analysis.TraceType.Known_Sink, ((FindingViewItem)tnTreeNodeWithTraces.Tag).oadO2AssessmentDataOunceV6, true));
                            return null;
                        }*/
                    }
                }
                foreach (TreeNode tnChildNodes in tnTreeNodeWithTraces.Nodes)
                    fviTargetFindingViewItem =
                        joinFindingViewItemAndFollowTreeNodeChilds_Recursive(fviTargetFindingViewItem, tnChildNodes);
            }
            return fviTargetFindingViewItem;
        }
    }
}
