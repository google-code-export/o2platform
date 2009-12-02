using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.Kernel.Interfaces.O2Findings;

namespace O2.Rules.OunceLabs.Ascx
{
    public partial class ascx_ApplyRulesToFindings : UserControl
    {
        public ascx_ApplyRulesToFindings()
        {
            InitializeComponent();
        }

        private void btFilter_BasicSinksMapping_Click(object sender, EventArgs e)
        {
            bool addFindingsWithNoMatches = cbAddFindingsWithNoMatches.Checked;
            btFilter_BasicSinksMapping.Enabled = false;
            executeFilter(AvailableFilters.BasicSinksMapping, addFindingsWithNoMatches,
                          mappedO2Findings =>
                              {
                                  findingsViewerMappedFindings.loadO2Findings(mappedO2Findings, true);
                                  this.invokeOnThread((O2Thread.FuncVoid) (
                                                                              () =>
                                                                              btFilter_BasicSinksMapping.Enabled = true));
                              });
        }

        private void btFilter_MapSinksToAllTraces_Click(object sender, EventArgs e)
        {
            bool addFindingsWithNoMatches = cbAddFindingsWithNoMatches.Checked;
            btFilter_MapSinksToAllTraces.Enabled = false;
            executeFilter(AvailableFilters.MapSinksToAllTraces, addFindingsWithNoMatches,
                mappedO2Findings =>
                    {
                        findingsViewerMappedFindings.loadO2Findings(mappedO2Findings, true);
                        this.invokeOnThread((O2Thread.FuncVoid) (
                                                                              () =>
                                                                              btFilter_MapSinksToAllTraces.Enabled = true));
                              });                    
        }

        private void btCreateAllPartialTraces_Click(object sender, EventArgs e)
        {
            bool addFindingsWithNoMatches = cbAddFindingsWithNoMatches.Checked;
            btCreateAllPartialTraces.Enabled = false;
            executeFilter(AvailableFilters.CreateAllPartialTraces, addFindingsWithNoMatches,
                mappedO2Findings =>
                    {
                        findingsViewerMappedFindings.loadO2Findings(mappedO2Findings, true);
                        this.invokeOnThread((O2Thread.FuncVoid)(
                                                                              () =>
                                                                              btCreateAllPartialTraces.Enabled = true));
                    });
        }


        private void btMapSourcesToAllTraces_Click(object sender, EventArgs e)
        {
            bool addFindingsWithNoMatches = cbAddFindingsWithNoMatches.Checked;
            btMapSourcesToAllTraces.Enabled = false;
            executeFilter(AvailableFilters.MapSourcesToAllTraces, addFindingsWithNoMatches,
                          mappedO2Findings =>
                              {
                                  findingsViewerMappedFindings.loadO2Findings(mappedO2Findings, true);
                                  this.invokeOnThread((O2Thread.FuncVoid) (
                                                                              () =>
                                                                              btMapSourcesToAllTraces.Enabled = true));
                              });
        }


        private void btMapFirstSourcesThenSinksToAllTraces_Click(object sender, EventArgs e)
        {
            bool addFindingsWithNoMatches = cbAddFindingsWithNoMatches.Checked;
            btMapFirstSourcesThenSinksToAllTraces.Enabled = false;
            executeFilter(AvailableFilters.MapFirstSourcesThenSinksToAllTraces,addFindingsWithNoMatches,
                          mappedO2Findings =>
                              {
                                  findingsViewerMappedFindings.loadO2Findings(mappedO2Findings, true);
                                  this.invokeOnThread((O2Thread.FuncVoid)(
                                                                              () =>
                                                                              btMapFirstSourcesThenSinksToAllTraces.Enabled = true));
                              });
        }

        private void findingsViewerSourceFindings__onTraceSelected(IO2Trace o2SelectedTrace)
        {
            onTraceSelectedEvent(o2SelectedTrace);
        }

        private void findingsViewerMappedFindings__onTraceSelected(IO2Trace o2SelectedTrace)
        {
            onTraceSelectedEvent(o2SelectedTrace);
        }

        private void findingsViewerSourceFindings__onFindingSelected(IO2Finding o2SelectedFinding)
        {
            onFindingSelectedEvent(o2SelectedFinding);
        }

        private void findingsViewerMappedFindings__onFindingSelected(IO2Finding o2SelectedFinding)
        {
            onFindingSelectedEvent(o2SelectedFinding);
        }

        private void findingsViewerSourceFindings__onFolderSelectEvent(string selectedText)
        {
            onFindingsViewerFolderSelectedEvent(selectedText);
        }
        
               
    }
}
