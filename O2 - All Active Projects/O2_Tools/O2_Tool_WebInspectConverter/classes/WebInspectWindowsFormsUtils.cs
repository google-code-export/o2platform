using System.Data;
using System.Reflection;
using O2.DotNetWrappers.O2Findings;
using O2.External.WinFormsUI.Forms;
using O2.External.WinFormsUI.O2Environment;
using O2.Kernel;
using O2.Tool.WebInspectConverter;
using O2.Tool.WebInspectConverter.classes;
using O2.Tool.WebInspectConverter.Converter;
using O2.Views.ASCX.DataViewers;
using O2.Views.ASCX.O2Findings;

namespace O2.Tool.WebInspectConverter.classes
{
    public class WebInspectWindowsFormsUtils
    {
        public static void showWebInspectResultsInO2DockWindow(WebInspectResults webInspectResults)
        {
            O2DockPanel.addAscxControlToO2GuiWithDockPanel(typeof (ascx_TableList), true,
                                                           PoC.dockContentTitle_WebInspectResults);
            var tableList = (ascx_TableList)O2AscxGUI.getAscx(PoC.dockContentTitle_WebInspectResults);
            showWebInspectResultsInTableList(webInspectResults, tableList);
        }

        public static void showWebInspectResultsInTableList(WebInspectResults webInspectResults,
                                                            ascx_TableList tableList)
        {
            if (tableList != null)
            {
                var dataTable = new DataTable("WebInspect results");
                foreach (FieldInfo field in typeof (WebInspectFinding).GetFields())
                    dataTable.Columns.Add(field.Name);

                foreach (WebInspectFinding webInspectFinding in webInspectResults.webInspectFindings)
                {
                    DataRow row = dataTable.NewRow();
                    foreach (FieldInfo field in typeof (WebInspectFinding).GetFields())
                        row[field.Name] = DI.reflection.getFieldValue(field.Name, webInspectFinding);

                    dataTable.Rows.Add(row);
                }
                tableList.setDataTable(dataTable);
            }
        }

        internal static void showFindingsCreatedFromWebInspectResults(WebInspectResults webInspectResults)
        {
            //    var webInspectResults = (WebInspectResults)oObject;
            var o2Assessment = new O2Assessment
            {
                o2Findings =
                    WebInspectToOzasmt.createO2FindingsFromWebInspectResults(
                    webInspectResults)
            };

            O2DockPanel.addAscxControlToO2GuiWithDockPanel(typeof(ascx_FindingsViewer),
                                                           true,
                                                           PoC.dockContentTitle_FindingsViewer);
            var findingsViewer = (ascx_FindingsViewer)O2AscxGUI.getAscx(PoC.dockContentTitle_FindingsViewer);
            findingsViewer.loadO2Assessment(o2Assessment);
            findingsViewer.setFilter1Value("vulnName");
            findingsViewer.setFilter2Value("(no Filter)");
            //GlobalStaticVars.dO2LoadedO2DockContent[PoC.dockContentTitle_FindingsViewer].dockContent.ParentForm.Height = 500;
        }
    }
}