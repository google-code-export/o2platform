// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using O2.Kernel;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.Windows;

//O2File:SearchEngine.cs
namespace O2.XRules.Database.Utils
{
    public static class SearchUtils
    {
        public static List<TextSearchResult> executeSearch(String sRegExToSearch, String sFileToSearch)
        {            
            return executeSearch(sRegExToSearch, new List<String>(new[] { sFileToSearch }));
        }

        public static List<TextSearchResult> executeSearch(String sRegExToSearch, List<String> lsFileToSearch)
        {
            var searchEngine = new SearchEngine();
            searchEngine.loadFiles(lsFileToSearch);
            return searchEngine.executeSearch(sRegExToSearch);
        }

        public static void loadInDataGridView_textSearchResults(this DataGridView dgvTargetDataGridView, List<TextSearchResult> currentSearchResults)
        {
            dgvTargetDataGridView.invokeOnThread(
                () =>
                    {
                        var iMaxRowsToLoad = 500;
                        dgvTargetDataGridView.Columns.Clear();
                        O2Forms.addToDataGridView_Column(dgvTargetDataGridView, "path", 50);
                        O2Forms.addToDataGridView_Column(dgvTargetDataGridView, "file", 50);
                        O2Forms.addToDataGridView_Column(dgvTargetDataGridView, "line #", 50);
                        O2Forms.addToDataGridView_Column(dgvTargetDataGridView, "match text", 100);
                        O2Forms.addToDataGridView_Column(dgvTargetDataGridView, "match line", -1);
                        foreach (TextSearchResult searchResult in currentSearchResults)
                        {
                            O2Forms.addToDataGridView_Row(dgvTargetDataGridView, searchResult,
                                                          new[]
                                                              {
                                                                  Path.GetDirectoryName(searchResult.File),
                                                                  Path.GetFileName(searchResult.File),
                                                                  searchResult.Line_Number.ToString(),
                                                                  searchResult.Match_Text,
                                                                  searchResult.Match_Line.Trim()
                                                              });
                            if (dgvTargetDataGridView.Rows.Count > iMaxRowsToLoad)
                            {
                                PublicDI.log.error("Aborting, MaxRowsToLoad reached: {0}", iMaxRowsToLoad);
                                return;
                            }
                        }

                        // ReSharper disable PossibleNullReferenceException
                        dgvTargetDataGridView.Columns["path"].Width = 20;
                        dgvTargetDataGridView.Columns["line #"].Width = 40;
                        // ReSharper restore PossibleNullReferenceException


                    });

        }

        public static void loadInTreeView_textSearchResults(
            List<TextSearchResult> currentSearchResults, TreeView tvSearchResults,
            string filterType1, string filterText1,
            string filterType2, string filterText2,
            string filterType3, string filterText3,
            string filterType4, string filterText4)
        {            
           /*foreach (TextSearchResult searchResult in currentSearchResults)
                        {
                            O2Forms.addToDataGridView_Row(dgvTargetDataGridView, searchResult,
                                                          new[]
                                                              {
                                                                  Path.GetDirectoryName(searchResult.sFile),
                                                                  Path.GetFileName(searchResult.sFile),
                                                                  searchResult.iLineNumber.ToString(),
                                                                  searchResult.sMatchText,
                                                                  searchResult.sMatchLine.Trim()
                                                              });
                            if (dgvTargetDataGridView.Rows.Count > iMaxRowsToLoad)
                            {
                                DI.log.error("Aborting, MaxRowsToLoad reached: {0}", iMaxRowsToLoad);
                                return;
                            }

                        }*/

            var nodes = getNodesForTreeViewSearchResults(filterType1, filterType2, currentSearchResults);
            loadInThread(nodes, tvSearchResults);            
        }

        public static List<TreeNode> getNodesForTreeViewSearchResults(string currentFilterType, string nextFilterType, List<TextSearchResult>textSearchResults)
        {
            var nodes = new List<TreeNode>();
            if (currentFilterType != "")
            {
                var results = new Dictionary<string, List<TextSearchResult>>();
                foreach (TextSearchResult searchResult in textSearchResults)
                {
                    var nodeKey = "";
                    switch (currentFilterType)
                    {
                        case "directory":
                            nodeKey = Path.GetDirectoryName(searchResult.File);
                            break;
                        case "file":
                            nodeKey = Path.GetFileName(searchResult.File);
                            break;
                        case "matched text":
                            nodeKey = searchResult.Match_Text;
                            break;
                        case "line":
                            nodeKey = searchResult.Match_Line.Trim();
                            break;
                    }
                    if (nodeKey != "")
                    {
                        if (false == results.ContainsKey(nodeKey))
                            results.Add(nodeKey, new List<TextSearchResult>());
                        results[nodeKey].Add(searchResult);
                    }                    
                }

                foreach (var nodeKey in results.Keys)
                {
                    var treeNode = O2Forms.newTreeNode(nodeKey, nextFilterType, 0, results[nodeKey]);
                    if (results[nodeKey].Count >0 && nextFilterType != "")
                        treeNode.Nodes.Add("Dummy Node");
                    nodes.Add(treeNode);
                }
            }
            return nodes;
        }

        public static void loadInThread(List<TreeNode> rootNodes,TreeView tvSearchResults)
        {
            tvSearchResults.invokeOnThread(
                () =>
                    {
                        tvSearchResults.Visible = false;
                        tvSearchResults.Nodes.Clear();
                        tvSearchResults.Nodes.AddRange(rootNodes.ToArray());
                        tvSearchResults.Visible = true;
                    });
        }

        public static void loadInThread(StringBuilder text, TextBox tbSearchResults)
        {
            tbSearchResults.invokeOnThread(
                () =>tbSearchResults.Text = text.ToString());
        }

        public static void loadInTextBox_textSearchResults(List<TextSearchResult> currentSearchResults, TextBox tbSearchResults)
        {
            var text = new StringBuilder();
            text.AppendLine("This is a finding search");
            loadInThread(text, tbSearchResults);            
        }       
    }
}
