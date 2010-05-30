using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IfacesEnumsStructsClasses;

namespace DemoApp
{
    public partial class frmPropertyExplorer : Form
    {
        WatinScript wscript = null;
        string browsername = "";
        IHTMLElement currentelement = null;

        public frmPropertyExplorer()
        {
            InitializeComponent();
        }

        public void SetWindowProperties(WatinScript watscript, string BrowserName, IHTMLElement element)
        {
            tabControl1.SelectedTab = pageMethods;

            wscript = watscript;
            browsername = BrowserName;
            currentelement = element;
            this.Text = "Property Explorer - " + element.GetType().ToString();

            IHTMLDOMNode node = element as IHTMLDOMNode;
            gridProperties.Rows.Add("tagName", element.tagName);

            foreach (IHTMLDOMAttribute attr in node.attributes)
            {
                if (attr.specified)
                {
                    gridProperties.Rows.Add(attr.nodeName, attr.nodeValue);
                }
            }

            gridProperties.Rows.Add("innerHTML", element.innerHTML);
            gridProperties.Rows.Add("innerText", element.innerText);
            gridProperties.Rows.Add("outerHTML", element.outerHTML);
            gridProperties.Rows.Add("outerText", element.outerText);

            rbSelectByText.Enabled = false;
            comboSelectByText.Enabled = false;
            rbSelectByValue.Enabled = false;
            comboSelectByValue.Enabled = false;
            rbSetFilename.Enabled = false;
            txtSetFilename.Enabled = false;
            btnSetFilename.Enabled = false;
            rbChecked.Enabled = false;
            rbUnchecked.Enabled = false;

            string tagtype = watscript.ActiveElementAttribute(element, "type").ToLower();

            if (element.tagName.ToLower() == "input" && tagtype == "file")
            {
                rbSetFilename.Enabled = true;
                txtSetFilename.Enabled = true;
                btnSetFilename.Enabled = true;
            }
            else if (element.tagName.ToLower() == "input" && (tagtype == "radio" || tagtype == "checkbox"))
            {
                rbChecked.Enabled = true;
                rbUnchecked.Enabled = true;
            }
            else if (element.tagName.ToLower() == "select")
            {
                rbSelectByText.Enabled = true;
                comboSelectByText.Enabled = true;
                rbSelectByValue.Enabled = true;
                comboSelectByValue.Enabled = true;

                comboSelectByText.Items.Clear();
                comboSelectByValue.Items.Clear();

                mshtml.IHTMLSelectElement sel = element as mshtml.IHTMLSelectElement;
                if (sel==null)
                {
                    return;
                }
                for (int i = 0; i < sel.length; i++)
                {
                    mshtml.IHTMLOptionElement op = sel.item(i, i) as mshtml.IHTMLOptionElement;
                    comboSelectByText.Items.Add(op.text);
                    if (op.value==null)
                    {
                        comboSelectByText.Items.Add(op.text);
                    }
                    else
                    {
                        comboSelectByValue.Items.Add(op.value);
                    }
                }
            }
        }

        private void Generic_CheckedChanged(object sender, EventArgs e)
        {
            string _method = (sender as RadioButton).Name;
            _method = _method.Remove(0, 2)+"();";
            wscript.AddAction(browsername, currentelement, _method);
            this.Close();
        }

        private void rbFireEvent_CheckedChanged(object sender, EventArgs e)
        {
            string eventName = "FireEvent(\""+txtFireEvent.Text+"\");";
            wscript.AddAction(browsername, currentelement, eventName);
            this.Close();
        }

        private void rbFireEventNoWait_CheckedChanged(object sender, EventArgs e)
        {
            string eventName = "FireEventNoWait(\"" + txtFireEventNoWait.Text + "\");";
            wscript.AddAction(browsername, currentelement, eventName);
            this.Close();
        }

        private void rbFlash_CheckedChanged(object sender, EventArgs e)
        {
            string eventName = "Flash(" + numFlash.Value.ToString() + ");";
            wscript.AddAction(browsername, currentelement, eventName);
            this.Close();
        }

        private void rbKeyDown_CheckedChanged(object sender, EventArgs e)
        {
            string eventName = "KeyDown(\"" + txtKeyDown.Text + "\");";
            wscript.AddAction(browsername, currentelement, eventName);
            this.Close();
        }

        private void rbKeyPress_CheckedChanged(object sender, EventArgs e)
        {
            string eventName = "KeyPress(\"" + txtKeyPress.Text + "\");";
            wscript.AddAction(browsername, currentelement, eventName);
            this.Close();
        }

        private void rbKeyUp_CheckedChanged(object sender, EventArgs e)
        {
            string eventName = "KeyUp(\"" + txtKeyUp.Text + "\");";
            wscript.AddAction(browsername, currentelement, eventName);
            this.Close();
        }

        private void rbChecked_CheckedChanged(object sender, EventArgs e)
        {
            string eventName = "Checked = true;";
            wscript.AddAction(browsername, currentelement, eventName);
            this.Close();
        }

        private void rbUnchecked_CheckedChanged(object sender, EventArgs e)
        {
            string eventName = "Checked = false;";
            wscript.AddAction(browsername, currentelement, eventName);
            this.Close();
        }

        private void rbSetFilename_CheckedChanged(object sender, EventArgs e)
        {
            string eventName = "Set(\""+txtSetFilename.Text+"\");";
            wscript.AddAction(browsername, currentelement, eventName);
            this.Close();
        }

        private void btnSetFilename_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                txtSetFilename.Text = openFileDialog1.FileName;
            }
        }

        private void rbSelectByText_CheckedChanged(object sender, EventArgs e)
        {
            string eventName = "Select(\"" + comboSelectByText.Text + "\");";
            wscript.AddAction(browsername, currentelement, eventName);
            this.Close();
        }

        private void rbSelectByValue_CheckedChanged(object sender, EventArgs e)
        {
            string eventName = "SelectByValue(\"" + comboSelectByValue.Text + "\");";
            wscript.AddAction(browsername, currentelement, eventName);
            this.Close();
        }

        private void rbWait_CheckedChanged(object sender, EventArgs e)
        {
            int ticks = Convert.ToInt32(numWait.Value * 1000);
            wscript.AddScriptLine("Thread.Sleep("+ticks+");");
            this.Close();
        }

        private void txtAttribute_TextChanged(object sender, EventArgs e)
        {
            string strValue = currentelement.getAttribute(txtAttribute.Text, 0) as string;
            if (strValue==null)
            {
                txtCurrentValue.Text = "NULL";
            }
            else
            {
                txtCurrentValue.Text = strValue;
            }
        }

        private void txtTestValue_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtTestValue.Text, "\\A\\d+\\z|\\A\\d+[.]\\d+\\z"))
            {
                rbGreater.Enabled = true;
                rbGreaterOrEqual.Enabled = true;
                rbLess.Enabled = true;
                rbLessOrEqual.Enabled = true;
            }
            else
            {
                rbGreater.Enabled = false;
                rbGreaterOrEqual.Enabled = false;
                rbLess.Enabled = false;
                rbLessOrEqual.Enabled = false;
            }
        }

        private void NumericType_CheckedChanged(object sender, EventArgs e)
        {
            string _method = (sender as RadioButton).Name;

            string strNumericType = "as int";
            if (txtTestValue.Text.Contains("."))
            {
                strNumericType = "as decimal";
            }

            string strElement = wscript.DetermineFindMethod(browsername, currentelement) + ".HTMLElement.getAttributeValue(\"" + txtAttribute.Text + "\") " + strNumericType;
            _method = "Assert."+_method.Remove(0, 2) + "("+txtTestValue.Text+", ("+strElement+"), \""+txtMessage.Text+"\");";
            wscript.AddScriptLine(_method);
            this.Close();
        }

        private void EqualityType_CheckedChanged(object sender, EventArgs e)
        {
            string _method = (sender as RadioButton).Name;
            string strElement = wscript.DetermineFindMethod(browsername, currentelement) + ".GetAttributeValue(\"" + txtAttribute.Text + "\")";
            _method = "Assert." + _method.Remove(0, 2) + "(\"" + txtTestValue.Text + "\", " + strElement + ", \"" + txtMessage.Text + "\");";
            wscript.AddScriptLine(_method);
            this.Close();
        }

        private void StringType_CheckedChanged(object sender, EventArgs e)
        {
            string _method = (sender as RadioButton).Name;
            string strElement = wscript.DetermineFindMethod(browsername, currentelement) + ".GetAttributeValue(\"" + txtAttribute.Text + "\")";
            _method = "StringAssert." + _method.Remove(0, 2) + "(\"" + txtTestValue.Text + "\", " + strElement + ", \"" + txtMessage.Text + "\");";
            wscript.AddScriptLine(_method);
            this.Close();
        }

        private void GenericTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                (sender as TextBox).Copy();
            }
            else if (e.Control && e.KeyCode == Keys.X)
            {
                (sender as TextBox).Cut();
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                (sender as TextBox).Paste();
            }
            else if (e.Control && e.KeyCode == Keys.Z)
            {
                (sender as TextBox).Undo();
            }
        }
    }
}