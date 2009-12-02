using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.Kernel.Interfaces.Rules;

namespace O2.Rules.OunceLabs.Ascx
{
    public partial class ascx_RuleEditor : UserControl
    {
        public ascx_RuleEditor()
        {
            InitializeComponent();
        }

        private void btSaveRuleChanges_Click(object sender, EventArgs e)
        {
            saveCurrentRule();
        }

        private void ascx_RuleEditor_Load(object sender, EventArgs e)
        {
            onLoad();
        }

   
        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void btSaveChangesToAllRules_Click(object sender, EventArgs e)
        {
            saveCurrentRules();
        }

        private void controlUnsavedChanges(object sender, EventArgs e)
        {
            laUnsavedChanges.Visible = true;
            laDataSaved.Visible = false;
        }

        private void btSetRuleTypeAsNotASink_Click(object sender, EventArgs e)
        {
            cbRuleType.Text = O2RuleType.NotASink.ToString();
            if (btSaveRuleChanges.Enabled)
                btSaveRuleChanges_Click(null, null);
            else
                if (btSaveChangesToAllRules.Enabled)
                    btSaveChangesToAllRules_Click(null, null);           
        }       
    }
}