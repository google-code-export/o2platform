// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Windows.Forms;
using O2.Rnd.JavaVelocityAnalyzer.classes;

namespace O2.Rnd.JavaVelocityAnalyzer.classes
{
    internal class velocityAnalyzer
    {
        public static void showProcessedVelocityFileInTreeView(ProcessedVelocityFile pvfFile, TreeView tvTargetTreeView,
                                                               bool bIgnoreComments)
        {
            tvTargetTreeView.Visible = false;
            tvTargetTreeView.Nodes.Clear();
            TreeNodeCollection tnNodeCollection = tvTargetTreeView.Nodes;
            int iCurrentDepth = 0;
            TreeNode tnCurrentNode = null;
            foreach (velocity.VelocityNode vnVelocityNode in pvfFile.lvnVelocityNodes)
            {
                if (bIgnoreComments == false || vnVelocityNode.ntNodeType != velocity.NodeType.ASTComment)
                {
                    if (vnVelocityNode.iDepth > iCurrentDepth)
                        tnNodeCollection = tnCurrentNode.Nodes;
                    if (vnVelocityNode.iDepth < iCurrentDepth)
                        for (int i = 0; i <= iCurrentDepth - vnVelocityNode.iDepth; i++)
                        {
                            tnCurrentNode = tnCurrentNode.Parent;
                            tnNodeCollection = tnCurrentNode.Nodes;
                        }
                    iCurrentDepth = vnVelocityNode.iDepth;
                    tnCurrentNode = tnNodeCollection.Add(vnVelocityNode.ToString());
                }
            }
/*            foreach (String sLine in pvfFile.lsFileLines)
            {
                String[] lsSplittedLine = sLine.Split('\t');
                String sType = lsSplittedLine[0];
                int iDepth = Int32.Parse(lsSplittedLine[1]);
                String sToken = lsSplittedLine[2];

                if (lsSplittedLine.Length != 3)
                     DI.log.error("in showProcessedVelocityFileInTreeView: Error loading line, there should be 3 fields on it: {0}", sLine);
                else
                {
                    if (iDepth > iCurrentDepth)
                        tnNodeCollection = tnCurrentNode.Nodes;
                    if (iDepth < iCurrentDepth)
                        for (int i = 0; i < iCurrentDepth - iDepth; iDepth++)
                            tnNodeCollection = tnCurrentNode.Parent.Nodes;
                    iCurrentDepth = iDepth;
                    tnCurrentNode = tnNodeCollection.Add(String.Format("{0} [{1}]: {2}", sType, iDepth, sToken));
                }
            }*/
            tvTargetTreeView.ExpandAll();
            if (tvTargetTreeView.Nodes.Count > 0)
                tvTargetTreeView.SelectedNode = tvTargetTreeView.Nodes[0];
            tvTargetTreeView.Visible = true;
        }
    }
}
