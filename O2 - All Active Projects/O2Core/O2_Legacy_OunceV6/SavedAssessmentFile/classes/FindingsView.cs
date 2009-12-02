// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Text;
using System.Windows.Forms;
using O2.DotNetWrappers.Windows;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;

namespace O2.Legacy.OunceV6.SavedAssessmentFile.classes
{
    public class FindingsView
    {
        public static void showFindingDetailsInDataGridView(DataGridView dgvDataGridView,
                                                            AssessmentAssessmentFileFinding fSelectedFinding,
                                                            O2AssessmentData_OunceV6 oadAssessmentDataOunceV6)
        {
            try
            {
                dgvDataGridView.Columns.Clear();
                O2Forms.addToDataGridView_Column(dgvDataGridView, "Name", 90);
                O2Forms.addToDataGridView_Column(dgvDataGridView, "Value", -1);
                dgvDataGridView.Rows.Add("vuln Name",
                                         fSelectedFinding.vuln_name ??
                                         OzasmtUtils_OunceV6.getStringIndexValue(UInt32.Parse(fSelectedFinding.vuln_name_id),
                                                                                 oadAssessmentDataOunceV6));
                dgvDataGridView.Rows.Add("Vuln Type",
                                         fSelectedFinding.vuln_type ??
                                         OzasmtUtils_OunceV6.getStringIndexValue(UInt32.Parse(fSelectedFinding.vuln_type_id),
                                                                                 oadAssessmentDataOunceV6));


                dgvDataGridView.Rows.Add("Caller Name",
                                         fSelectedFinding.caller_name ?? ((fSelectedFinding.caller_name_id != null)
                                                                              ? OzasmtUtils_OunceV6.getStringIndexValue(
                                                                                    UInt32.Parse(fSelectedFinding.caller_name_id),
                                                                                    oadAssessmentDataOunceV6)
                                                                              : ""));
                dgvDataGridView.Rows.Add("Context",
                                         fSelectedFinding.context ?? ((fSelectedFinding.cxt_id != null)
                                                                          ? OzasmtUtils_OunceV6.getStringIndexValue(
                                                                                UInt32.Parse(fSelectedFinding.cxt_id), oadAssessmentDataOunceV6)
                                                                          : ""));

                dgvDataGridView.Rows.Add("Severity", fSelectedFinding.severity.ToString());
                dgvDataGridView.Rows.Add("Confidence", fSelectedFinding.confidence.ToString());
                dgvDataGridView.Rows.Add("Action Object Id", fSelectedFinding.actionobject_id.ToString());

                dgvDataGridView.Rows.Add("Project",
                                         (fSelectedFinding.project_name != null)
                                             ? fSelectedFinding.project_name
                                             : (fSelectedFinding.project_name_id != null)
                                                   ? OzasmtUtils_OunceV6.getStringIndexValue(
                                                         UInt32.Parse(fSelectedFinding.project_name_id),
                                                         oadAssessmentDataOunceV6)
                                                   : "");

                dgvDataGridView.Rows.Add("Column Number", fSelectedFinding.column_number.ToString());
                dgvDataGridView.Rows.Add("Line Number", fSelectedFinding.line_number.ToString());
                dgvDataGridView.Rows.Add("Ordinal", fSelectedFinding.ordinal.ToString());
                dgvDataGridView.Rows.Add("Exclude", fSelectedFinding.exclude.ToString());
                dgvDataGridView.Rows.Add("Property IDs", fSelectedFinding.property_ids);
                dgvDataGridView.Rows.Add("Record ID", fSelectedFinding.record_id.ToString());
                if (fSelectedFinding.Text != null)
                {
                    var sbText = new StringBuilder();
                    foreach (String sLine in fSelectedFinding.Text)
                        sbText.AppendLine(sLine);
                    dgvDataGridView.Rows.Add("Text", sbText.ToString());
                }
                if (fSelectedFinding.Trace != null)
                    dgvDataGridView.Rows.Add("Trace", "Yes");
                else
                    dgvDataGridView.Rows.Add("Trace", "No");

                //       dgvFindingData.Rows.Add("Action Object", Lddb.getActionObjectName(fSelectedFinding.actionobject_id.ToString()));
            }
            catch (Exception ex)
            {
                DI.log.error("in showFindingDetailsInDataGridView :{0}", ex.Message);
            }
        }

        public static void showCallInvocationDetailsInDataGridView(DataGridView dgvDataGridView,
                                                                   CallInvocation ciCallInvocation,
                                                                   O2AssessmentData_OunceV6 oadAssessmentDataOunceV6)
        {
            if (ciCallInvocation != null)
            {
                try
                {
                    dgvDataGridView.Columns.Clear();
                    O2Forms.addToDataGridView_Column(dgvDataGridView, "Name", 90);
                    O2Forms.addToDataGridView_Column(dgvDataGridView, "Value", -1);
                    dgvDataGridView.Rows.Add("sig_id",
                                             OzasmtUtils_OunceV6.getStringIndexValue(ciCallInvocation.sig_id, oadAssessmentDataOunceV6));
                    dgvDataGridView.Rows.Add("cxt_id",
                                             OzasmtUtils_OunceV6.getStringIndexValue(ciCallInvocation.cxt_id, oadAssessmentDataOunceV6));
                    dgvDataGridView.Rows.Add("fn_id",
                                             OzasmtUtils_OunceV6.getFileIndexValue(ciCallInvocation.fn_id, oadAssessmentDataOunceV6));
                    dgvDataGridView.Rows.Add("cn id",
                                             OzasmtUtils_OunceV6.getStringIndexValue(ciCallInvocation.cn_id, oadAssessmentDataOunceV6));
                    dgvDataGridView.Rows.Add("trace_type", ciCallInvocation.trace_type.ToString());
                    dgvDataGridView.Rows.Add("column_number", ciCallInvocation.column_number.ToString());
                    dgvDataGridView.Rows.Add("line_number", ciCallInvocation.line_number.ToString());
                    dgvDataGridView.Rows.Add("mn_id",
                                             OzasmtUtils_OunceV6.getStringIndexValue(ciCallInvocation.mn_id, oadAssessmentDataOunceV6));
                    dgvDataGridView.Rows.Add("ordinal", ciCallInvocation.ordinal.ToString());
                    dgvDataGridView.Rows.Add("taint_propagation", ciCallInvocation.taint_propagation);
                    if (ciCallInvocation.Text != null)
                    {
                        var sbText = new StringBuilder();
                        foreach (String sLine in ciCallInvocation.Text)
                            sbText.AppendLine(sLine);
                        dgvDataGridView.Rows.Add("Text", sbText.ToString());
                    }
                    //ciCallInvocation.Text;
                }
                catch (Exception ex)
                {
                    DI.log.error("in showCallInvocationDetailsInDataGridView :{0}", ex.Message);
                }
            }
        }
    }
}
