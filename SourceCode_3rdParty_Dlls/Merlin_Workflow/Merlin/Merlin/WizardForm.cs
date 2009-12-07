/*
 * The contributors of Merlin license this file to You under the Apache 
 * License, Version 2.0 (the "License"); you may not use this file except 
 * in compliance with the License.  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

 
 
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace Merlin
{
    internal partial class WizardForm : Form
    {
        private const int titleOffsetWithSubtitle = 7;
        private int originalTitleTop = -20;

        public static int MaxWidth
        {
            get { return 535; }
        }

        public static int MaxHeight
        {
            get { return 302; }
        }

        public WizardForm(string name)
        {
            InitializeComponent();
            this.Text = name;
        }


        public string NextButtonName
        {
            get { return btnNext.Text; }
            set { btnNext.Text = value; }
        }

        public void ShowUserControl(Control userControl)
        {
            pnlComponentArea.Controls.Clear();
            pnlComponentArea.Margin = new Padding(0);
            pnlComponentArea.Controls.Add(userControl);
            userControl.Parent = pnlComponentArea;
            userControl.Left = 0;
            userControl.Top = 0;
            userControl.Width = pnlComponentArea.Width;
            userControl.Height = pnlComponentArea.Height;
        }

        internal string HeaderTitle
        {
            get { return lblWizardTitle.Text; }
            set { lblWizardTitle.Text = value; }
        }

        /// <summary>
        /// Shows the specified subtitle underneath the header title
        /// </summary>
        /// <param name="subtitleText"></param>
        internal void ShowSubtitle(string subtitleText) {
            if (originalTitleTop < 0) originalTitleTop = lblWizardTitle.Top;
            lblWizardTitle.Top = originalTitleTop - titleOffsetWithSubtitle;
            txtSubtitle.Text = subtitleText;
            txtSubtitle.Visible = true;
        }

        internal void HideSubtitle() 
        {
            if (originalTitleTop < 0) originalTitleTop = lblWizardTitle.Top;
            txtSubtitle.Visible = false;
            lblWizardTitle.Top = originalTitleTop;
        }

        public Image LogoImage
        {
            get { return pbxLogo.Image; }
            set { 
                pbxLogo.Image = value;
                WizardForm_Resize(this, EventArgs.Empty);
            }
        }

        private void WizardForm_Resize(object sender, EventArgs e)
        {
            int correctClientRectangleHeight
                = btnNext.Bottom + grpButtons.Top + (int)(10f * this.AutoScaleFactor.Height);
            int formClientHeightDifference = this.Height - this.ClientRectangle.Height;
            this.Height = correctClientRectangleHeight + formClientHeightDifference;
            if (pbxLogo.Image != null)
            {
                pbxLogo.Width = pbxLogo.Image.Width;
            }
            pbxLogo.Left = this.ClientRectangle.Width - pbxLogo.Width + 1;
            pnlComponentArea.Left = 0;
            pnlComponentArea.Width = ClientRectangle.Width;
        }
    }
}
