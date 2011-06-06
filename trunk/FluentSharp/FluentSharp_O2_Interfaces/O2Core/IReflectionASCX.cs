// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace FluentSharp.O2.Interfaces.O2Core
{
    public interface IReflectionASCX : IReflection
    {
        List<TreeNode> populateTreeNodeWithObjectChilds(TreeNode node, object tag, bool populateFirstChildNodes);

        void loadObjectDataIntoTreeNodesCollection(Object oLiveObjectToLoad,
                                                   TreeNodeCollection tnbTargetTreeNodesCollection);

        void loadMethodInfoParametersInDataGridView(MethodInfo mMethod, DataGridView dgvTargetDataGridView);

        void loadTypeDataIntoTreeView(Type tTypeToLoad, TreeView tvTargetTreeView,
                                      bool bViewAllMethods_IncludingOnesWithNotSupportedParams,
                                      bool bShowArguments, bool bShowReturnParameter, string sFilter);

        void loadTypeDataIntoTreeView(Type tTypeToLoad, TreeView tvTargetTreeView,
                                      bool bViewAllMethods_IncludingOnesWithNotSupportedParams,
                                      bool bShowArguments, bool bShowReturnParameter, string sFilter,
                                      bool bClearTreeView);

        int addMethodInfoToTreeNode(TreeNode tnTargetTreeNode, MethodInfo mMethod, bool bShowArguments,
                                    bool bShowReturnParameter);

        Object[] getParameterObjectsFromDataGridColumn(DataGridView dgvDataGridViewWithData,
                                                       String sColumnWithParameters);

        void executeMethodAndOutputResultInTextBoxOrDataGridView(MethodInfo mMethod, Object[] aoParams,
                                                                 Object oLiveInstanceOfObject,
                                                                 TextBox tbTextBox_Results,
                                                                 DataGridView dgvDataGridView_Results);

        bool doesMethodOnlyHasSupportedParameters(MethodInfo mMethod);
    }
}