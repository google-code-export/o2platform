namespace DemoApp
{
    partial class frmPropertyExplorer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gridProperties = new System.Windows.Forms.DataGridView();
            this.colPropertyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPropertyValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.pageMethods = new System.Windows.Forms.TabPage();
            this.comboSelectByValue = new System.Windows.Forms.ComboBox();
            this.comboSelectByText = new System.Windows.Forms.ComboBox();
            this.btnSetFilename = new System.Windows.Forms.Button();
            this.txtSetFilename = new System.Windows.Forms.TextBox();
            this.rbSelectByValue = new System.Windows.Forms.RadioButton();
            this.rbSelectByText = new System.Windows.Forms.RadioButton();
            this.rbSetFilename = new System.Windows.Forms.RadioButton();
            this.rbUnchecked = new System.Windows.Forms.RadioButton();
            this.rbChecked = new System.Windows.Forms.RadioButton();
            this.txtKeyUp = new System.Windows.Forms.TextBox();
            this.txtKeyPress = new System.Windows.Forms.TextBox();
            this.txtKeyDown = new System.Windows.Forms.TextBox();
            this.numFlash = new System.Windows.Forms.NumericUpDown();
            this.txtFireEventNoWait = new System.Windows.Forms.TextBox();
            this.txtFireEvent = new System.Windows.Forms.TextBox();
            this.rbMouseUp = new System.Windows.Forms.RadioButton();
            this.rbMouseEnter = new System.Windows.Forms.RadioButton();
            this.rbKeyPress = new System.Windows.Forms.RadioButton();
            this.rbMouseDown = new System.Windows.Forms.RadioButton();
            this.rbKeyUp = new System.Windows.Forms.RadioButton();
            this.rbKeyDown = new System.Windows.Forms.RadioButton();
            this.rbHighlight = new System.Windows.Forms.RadioButton();
            this.rbFocus = new System.Windows.Forms.RadioButton();
            this.rbFlash = new System.Windows.Forms.RadioButton();
            this.rbFireEventNoWait = new System.Windows.Forms.RadioButton();
            this.rbFireEvent = new System.Windows.Forms.RadioButton();
            this.rbDoubleClick = new System.Windows.Forms.RadioButton();
            this.rbClickNoWait = new System.Windows.Forms.RadioButton();
            this.rbClick = new System.Windows.Forms.RadioButton();
            this.rbChange = new System.Windows.Forms.RadioButton();
            this.rbBlur = new System.Windows.Forms.RadioButton();
            this.pageWait = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.numWait = new System.Windows.Forms.NumericUpDown();
            this.rbWaitUntilRemoved = new System.Windows.Forms.RadioButton();
            this.rbWaitUntilExists = new System.Windows.Forms.RadioButton();
            this.rbWaitForComplete = new System.Windows.Forms.RadioButton();
            this.rbWait = new System.Windows.Forms.RadioButton();
            this.pageAssertions = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.rbAreEqualIgnoringCase = new System.Windows.Forms.RadioButton();
            this.rbEndsWith = new System.Windows.Forms.RadioButton();
            this.rbStartsWith = new System.Windows.Forms.RadioButton();
            this.rbContains = new System.Windows.Forms.RadioButton();
            this.rbLessOrEqual = new System.Windows.Forms.RadioButton();
            this.rbGreaterOrEqual = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTestValue = new System.Windows.Forms.TextBox();
            this.rbLess = new System.Windows.Forms.RadioButton();
            this.rbGreater = new System.Windows.Forms.RadioButton();
            this.rbAreNotEqual = new System.Windows.Forms.RadioButton();
            this.rbAreEqual = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCurrentValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAttribute = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.gridProperties)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.pageMethods.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFlash)).BeginInit();
            this.pageWait.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWait)).BeginInit();
            this.pageAssertions.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridProperties
            // 
            this.gridProperties.AllowUserToAddRows = false;
            this.gridProperties.AllowUserToDeleteRows = false;
            this.gridProperties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridProperties.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPropertyName,
            this.colPropertyValue});
            this.gridProperties.Dock = System.Windows.Forms.DockStyle.Left;
            this.gridProperties.Location = new System.Drawing.Point(0, 0);
            this.gridProperties.Name = "gridProperties";
            this.gridProperties.ReadOnly = true;
            this.gridProperties.RowHeadersVisible = false;
            this.gridProperties.Size = new System.Drawing.Size(208, 407);
            this.gridProperties.TabIndex = 0;
            // 
            // colPropertyName
            // 
            this.colPropertyName.HeaderText = "Property Name";
            this.colPropertyName.Name = "colPropertyName";
            this.colPropertyName.ReadOnly = true;
            // 
            // colPropertyValue
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.colPropertyValue.DefaultCellStyle = dataGridViewCellStyle1;
            this.colPropertyValue.HeaderText = "Value";
            this.colPropertyValue.Name = "colPropertyValue";
            this.colPropertyValue.ReadOnly = true;
            this.colPropertyValue.Width = 1000;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(208, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(5, 407);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.pageMethods);
            this.tabControl1.Controls.Add(this.pageWait);
            this.tabControl1.Controls.Add(this.pageAssertions);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(213, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(414, 407);
            this.tabControl1.TabIndex = 2;
            // 
            // pageMethods
            // 
            this.pageMethods.Controls.Add(this.comboSelectByValue);
            this.pageMethods.Controls.Add(this.comboSelectByText);
            this.pageMethods.Controls.Add(this.btnSetFilename);
            this.pageMethods.Controls.Add(this.txtSetFilename);
            this.pageMethods.Controls.Add(this.rbSelectByValue);
            this.pageMethods.Controls.Add(this.rbSelectByText);
            this.pageMethods.Controls.Add(this.rbSetFilename);
            this.pageMethods.Controls.Add(this.rbUnchecked);
            this.pageMethods.Controls.Add(this.rbChecked);
            this.pageMethods.Controls.Add(this.txtKeyUp);
            this.pageMethods.Controls.Add(this.txtKeyPress);
            this.pageMethods.Controls.Add(this.txtKeyDown);
            this.pageMethods.Controls.Add(this.numFlash);
            this.pageMethods.Controls.Add(this.txtFireEventNoWait);
            this.pageMethods.Controls.Add(this.txtFireEvent);
            this.pageMethods.Controls.Add(this.rbMouseUp);
            this.pageMethods.Controls.Add(this.rbMouseEnter);
            this.pageMethods.Controls.Add(this.rbKeyPress);
            this.pageMethods.Controls.Add(this.rbMouseDown);
            this.pageMethods.Controls.Add(this.rbKeyUp);
            this.pageMethods.Controls.Add(this.rbKeyDown);
            this.pageMethods.Controls.Add(this.rbHighlight);
            this.pageMethods.Controls.Add(this.rbFocus);
            this.pageMethods.Controls.Add(this.rbFlash);
            this.pageMethods.Controls.Add(this.rbFireEventNoWait);
            this.pageMethods.Controls.Add(this.rbFireEvent);
            this.pageMethods.Controls.Add(this.rbDoubleClick);
            this.pageMethods.Controls.Add(this.rbClickNoWait);
            this.pageMethods.Controls.Add(this.rbClick);
            this.pageMethods.Controls.Add(this.rbChange);
            this.pageMethods.Controls.Add(this.rbBlur);
            this.pageMethods.Location = new System.Drawing.Point(4, 22);
            this.pageMethods.Name = "pageMethods";
            this.pageMethods.Padding = new System.Windows.Forms.Padding(3);
            this.pageMethods.Size = new System.Drawing.Size(406, 381);
            this.pageMethods.TabIndex = 1;
            this.pageMethods.Text = "Methods";
            this.pageMethods.UseVisualStyleBackColor = true;
            // 
            // comboSelectByValue
            // 
            this.comboSelectByValue.FormattingEnabled = true;
            this.comboSelectByValue.Location = new System.Drawing.Point(127, 329);
            this.comboSelectByValue.Name = "comboSelectByValue";
            this.comboSelectByValue.Size = new System.Drawing.Size(179, 21);
            this.comboSelectByValue.TabIndex = 31;
            // 
            // comboSelectByText
            // 
            this.comboSelectByText.FormattingEnabled = true;
            this.comboSelectByText.Location = new System.Drawing.Point(127, 306);
            this.comboSelectByText.Name = "comboSelectByText";
            this.comboSelectByText.Size = new System.Drawing.Size(179, 21);
            this.comboSelectByText.TabIndex = 29;
            // 
            // btnSetFilename
            // 
            this.btnSetFilename.Location = new System.Drawing.Point(282, 281);
            this.btnSetFilename.Name = "btnSetFilename";
            this.btnSetFilename.Size = new System.Drawing.Size(24, 23);
            this.btnSetFilename.TabIndex = 27;
            this.btnSetFilename.Text = "...";
            this.btnSetFilename.UseVisualStyleBackColor = true;
            this.btnSetFilename.Click += new System.EventHandler(this.btnSetFilename_Click);
            // 
            // txtSetFilename
            // 
            this.txtSetFilename.Location = new System.Drawing.Point(127, 283);
            this.txtSetFilename.Name = "txtSetFilename";
            this.txtSetFilename.Size = new System.Drawing.Size(149, 20);
            this.txtSetFilename.TabIndex = 26;
            this.txtSetFilename.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GenericTextBox_KeyUp);
            // 
            // rbSelectByValue
            // 
            this.rbSelectByValue.AutoSize = true;
            this.rbSelectByValue.Location = new System.Drawing.Point(6, 330);
            this.rbSelectByValue.Name = "rbSelectByValue";
            this.rbSelectByValue.Size = new System.Drawing.Size(100, 17);
            this.rbSelectByValue.TabIndex = 30;
            this.rbSelectByValue.TabStop = true;
            this.rbSelectByValue.Text = "Select By Value";
            this.rbSelectByValue.UseVisualStyleBackColor = true;
            this.rbSelectByValue.CheckedChanged += new System.EventHandler(this.rbSelectByValue_CheckedChanged);
            // 
            // rbSelectByText
            // 
            this.rbSelectByText.AutoSize = true;
            this.rbSelectByText.Location = new System.Drawing.Point(6, 307);
            this.rbSelectByText.Name = "rbSelectByText";
            this.rbSelectByText.Size = new System.Drawing.Size(94, 17);
            this.rbSelectByText.TabIndex = 28;
            this.rbSelectByText.TabStop = true;
            this.rbSelectByText.Text = "Select By Text";
            this.rbSelectByText.UseVisualStyleBackColor = true;
            this.rbSelectByText.CheckedChanged += new System.EventHandler(this.rbSelectByText_CheckedChanged);
            // 
            // rbSetFilename
            // 
            this.rbSetFilename.AutoSize = true;
            this.rbSetFilename.Location = new System.Drawing.Point(6, 284);
            this.rbSetFilename.Name = "rbSetFilename";
            this.rbSetFilename.Size = new System.Drawing.Size(86, 17);
            this.rbSetFilename.TabIndex = 25;
            this.rbSetFilename.TabStop = true;
            this.rbSetFilename.Text = "Set Filename";
            this.rbSetFilename.UseVisualStyleBackColor = true;
            this.rbSetFilename.CheckedChanged += new System.EventHandler(this.rbSetFilename_CheckedChanged);
            // 
            // rbUnchecked
            // 
            this.rbUnchecked.AutoSize = true;
            this.rbUnchecked.Location = new System.Drawing.Point(127, 261);
            this.rbUnchecked.Name = "rbUnchecked";
            this.rbUnchecked.Size = new System.Drawing.Size(81, 17);
            this.rbUnchecked.TabIndex = 24;
            this.rbUnchecked.TabStop = true;
            this.rbUnchecked.Text = "Unchecked";
            this.rbUnchecked.UseVisualStyleBackColor = true;
            this.rbUnchecked.CheckedChanged += new System.EventHandler(this.rbUnchecked_CheckedChanged);
            // 
            // rbChecked
            // 
            this.rbChecked.AutoSize = true;
            this.rbChecked.Location = new System.Drawing.Point(6, 261);
            this.rbChecked.Name = "rbChecked";
            this.rbChecked.Size = new System.Drawing.Size(68, 17);
            this.rbChecked.TabIndex = 23;
            this.rbChecked.TabStop = true;
            this.rbChecked.Text = "Checked";
            this.rbChecked.UseVisualStyleBackColor = true;
            this.rbChecked.CheckedChanged += new System.EventHandler(this.rbChecked_CheckedChanged);
            // 
            // txtKeyUp
            // 
            this.txtKeyUp.Location = new System.Drawing.Point(127, 212);
            this.txtKeyUp.Name = "txtKeyUp";
            this.txtKeyUp.Size = new System.Drawing.Size(100, 20);
            this.txtKeyUp.TabIndex = 19;
            this.txtKeyUp.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GenericTextBox_KeyUp);
            // 
            // txtKeyPress
            // 
            this.txtKeyPress.Location = new System.Drawing.Point(127, 189);
            this.txtKeyPress.Name = "txtKeyPress";
            this.txtKeyPress.Size = new System.Drawing.Size(100, 20);
            this.txtKeyPress.TabIndex = 17;
            this.txtKeyPress.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GenericTextBox_KeyUp);
            // 
            // txtKeyDown
            // 
            this.txtKeyDown.Location = new System.Drawing.Point(127, 166);
            this.txtKeyDown.Name = "txtKeyDown";
            this.txtKeyDown.Size = new System.Drawing.Size(100, 20);
            this.txtKeyDown.TabIndex = 15;
            this.txtKeyDown.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GenericTextBox_KeyUp);
            // 
            // numFlash
            // 
            this.numFlash.Location = new System.Drawing.Point(127, 121);
            this.numFlash.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFlash.Name = "numFlash";
            this.numFlash.Size = new System.Drawing.Size(56, 20);
            this.numFlash.TabIndex = 11;
            this.numFlash.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // txtFireEventNoWait
            // 
            this.txtFireEventNoWait.Location = new System.Drawing.Point(127, 97);
            this.txtFireEventNoWait.Name = "txtFireEventNoWait";
            this.txtFireEventNoWait.Size = new System.Drawing.Size(179, 20);
            this.txtFireEventNoWait.TabIndex = 9;
            this.txtFireEventNoWait.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GenericTextBox_KeyUp);
            // 
            // txtFireEvent
            // 
            this.txtFireEvent.Location = new System.Drawing.Point(127, 74);
            this.txtFireEvent.Name = "txtFireEvent";
            this.txtFireEvent.Size = new System.Drawing.Size(179, 20);
            this.txtFireEvent.TabIndex = 7;
            this.txtFireEvent.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GenericTextBox_KeyUp);
            // 
            // rbMouseUp
            // 
            this.rbMouseUp.AutoSize = true;
            this.rbMouseUp.Location = new System.Drawing.Point(271, 238);
            this.rbMouseUp.Name = "rbMouseUp";
            this.rbMouseUp.Size = new System.Drawing.Size(74, 17);
            this.rbMouseUp.TabIndex = 22;
            this.rbMouseUp.TabStop = true;
            this.rbMouseUp.Text = "Mouse Up";
            this.rbMouseUp.UseVisualStyleBackColor = true;
            this.rbMouseUp.CheckedChanged += new System.EventHandler(this.Generic_CheckedChanged);
            // 
            // rbMouseEnter
            // 
            this.rbMouseEnter.AutoSize = true;
            this.rbMouseEnter.Location = new System.Drawing.Point(127, 238);
            this.rbMouseEnter.Name = "rbMouseEnter";
            this.rbMouseEnter.Size = new System.Drawing.Size(85, 17);
            this.rbMouseEnter.TabIndex = 21;
            this.rbMouseEnter.TabStop = true;
            this.rbMouseEnter.Text = "Mouse Enter";
            this.rbMouseEnter.UseVisualStyleBackColor = true;
            this.rbMouseEnter.CheckedChanged += new System.EventHandler(this.Generic_CheckedChanged);
            // 
            // rbKeyPress
            // 
            this.rbKeyPress.AutoSize = true;
            this.rbKeyPress.Location = new System.Drawing.Point(6, 190);
            this.rbKeyPress.Name = "rbKeyPress";
            this.rbKeyPress.Size = new System.Drawing.Size(72, 17);
            this.rbKeyPress.TabIndex = 16;
            this.rbKeyPress.TabStop = true;
            this.rbKeyPress.Text = "Key Press";
            this.rbKeyPress.UseVisualStyleBackColor = true;
            this.rbKeyPress.CheckedChanged += new System.EventHandler(this.rbKeyPress_CheckedChanged);
            // 
            // rbMouseDown
            // 
            this.rbMouseDown.AutoSize = true;
            this.rbMouseDown.Location = new System.Drawing.Point(6, 236);
            this.rbMouseDown.Name = "rbMouseDown";
            this.rbMouseDown.Size = new System.Drawing.Size(88, 17);
            this.rbMouseDown.TabIndex = 20;
            this.rbMouseDown.TabStop = true;
            this.rbMouseDown.Text = "Mouse Down";
            this.rbMouseDown.UseVisualStyleBackColor = true;
            this.rbMouseDown.CheckedChanged += new System.EventHandler(this.Generic_CheckedChanged);
            // 
            // rbKeyUp
            // 
            this.rbKeyUp.AutoSize = true;
            this.rbKeyUp.Location = new System.Drawing.Point(6, 213);
            this.rbKeyUp.Name = "rbKeyUp";
            this.rbKeyUp.Size = new System.Drawing.Size(60, 17);
            this.rbKeyUp.TabIndex = 18;
            this.rbKeyUp.TabStop = true;
            this.rbKeyUp.Text = "Key Up";
            this.rbKeyUp.UseVisualStyleBackColor = true;
            this.rbKeyUp.CheckedChanged += new System.EventHandler(this.rbKeyUp_CheckedChanged);
            // 
            // rbKeyDown
            // 
            this.rbKeyDown.AutoSize = true;
            this.rbKeyDown.Location = new System.Drawing.Point(6, 167);
            this.rbKeyDown.Name = "rbKeyDown";
            this.rbKeyDown.Size = new System.Drawing.Size(74, 17);
            this.rbKeyDown.TabIndex = 14;
            this.rbKeyDown.TabStop = true;
            this.rbKeyDown.Text = "Key Down";
            this.rbKeyDown.UseVisualStyleBackColor = true;
            this.rbKeyDown.CheckedChanged += new System.EventHandler(this.rbKeyDown_CheckedChanged);
            // 
            // rbHighlight
            // 
            this.rbHighlight.AutoSize = true;
            this.rbHighlight.Location = new System.Drawing.Point(127, 144);
            this.rbHighlight.Name = "rbHighlight";
            this.rbHighlight.Size = new System.Drawing.Size(66, 17);
            this.rbHighlight.TabIndex = 13;
            this.rbHighlight.TabStop = true;
            this.rbHighlight.Text = "Highlight";
            this.rbHighlight.UseVisualStyleBackColor = true;
            this.rbHighlight.CheckedChanged += new System.EventHandler(this.Generic_CheckedChanged);
            // 
            // rbFocus
            // 
            this.rbFocus.AutoSize = true;
            this.rbFocus.Location = new System.Drawing.Point(6, 144);
            this.rbFocus.Name = "rbFocus";
            this.rbFocus.Size = new System.Drawing.Size(54, 17);
            this.rbFocus.TabIndex = 12;
            this.rbFocus.TabStop = true;
            this.rbFocus.Text = "Focus";
            this.rbFocus.UseVisualStyleBackColor = true;
            this.rbFocus.CheckedChanged += new System.EventHandler(this.Generic_CheckedChanged);
            // 
            // rbFlash
            // 
            this.rbFlash.AutoSize = true;
            this.rbFlash.Location = new System.Drawing.Point(6, 121);
            this.rbFlash.Name = "rbFlash";
            this.rbFlash.Size = new System.Drawing.Size(50, 17);
            this.rbFlash.TabIndex = 10;
            this.rbFlash.TabStop = true;
            this.rbFlash.Text = "Flash";
            this.rbFlash.UseVisualStyleBackColor = true;
            this.rbFlash.CheckedChanged += new System.EventHandler(this.rbFlash_CheckedChanged);
            // 
            // rbFireEventNoWait
            // 
            this.rbFireEventNoWait.AutoSize = true;
            this.rbFireEventNoWait.Location = new System.Drawing.Point(6, 98);
            this.rbFireEventNoWait.Name = "rbFireEventNoWait";
            this.rbFireEventNoWait.Size = new System.Drawing.Size(115, 17);
            this.rbFireEventNoWait.TabIndex = 8;
            this.rbFireEventNoWait.TabStop = true;
            this.rbFireEventNoWait.Text = "Fire Event No Wait";
            this.rbFireEventNoWait.UseVisualStyleBackColor = true;
            this.rbFireEventNoWait.CheckedChanged += new System.EventHandler(this.rbFireEventNoWait_CheckedChanged);
            // 
            // rbFireEvent
            // 
            this.rbFireEvent.AutoSize = true;
            this.rbFireEvent.Location = new System.Drawing.Point(6, 75);
            this.rbFireEvent.Name = "rbFireEvent";
            this.rbFireEvent.Size = new System.Drawing.Size(73, 17);
            this.rbFireEvent.TabIndex = 6;
            this.rbFireEvent.TabStop = true;
            this.rbFireEvent.Text = "Fire Event";
            this.rbFireEvent.UseVisualStyleBackColor = true;
            this.rbFireEvent.CheckedChanged += new System.EventHandler(this.rbFireEvent_CheckedChanged);
            // 
            // rbDoubleClick
            // 
            this.rbDoubleClick.AutoSize = true;
            this.rbDoubleClick.Location = new System.Drawing.Point(271, 52);
            this.rbDoubleClick.Name = "rbDoubleClick";
            this.rbDoubleClick.Size = new System.Drawing.Size(85, 17);
            this.rbDoubleClick.TabIndex = 5;
            this.rbDoubleClick.TabStop = true;
            this.rbDoubleClick.Text = "Double Click";
            this.rbDoubleClick.UseVisualStyleBackColor = true;
            this.rbDoubleClick.CheckedChanged += new System.EventHandler(this.Generic_CheckedChanged);
            // 
            // rbClickNoWait
            // 
            this.rbClickNoWait.AutoSize = true;
            this.rbClickNoWait.Location = new System.Drawing.Point(127, 52);
            this.rbClickNoWait.Name = "rbClickNoWait";
            this.rbClickNoWait.Size = new System.Drawing.Size(115, 17);
            this.rbClickNoWait.TabIndex = 4;
            this.rbClickNoWait.TabStop = true;
            this.rbClickNoWait.Text = "Click With No Wait";
            this.rbClickNoWait.UseVisualStyleBackColor = true;
            this.rbClickNoWait.CheckedChanged += new System.EventHandler(this.Generic_CheckedChanged);
            // 
            // rbClick
            // 
            this.rbClick.AutoSize = true;
            this.rbClick.Location = new System.Drawing.Point(6, 52);
            this.rbClick.Name = "rbClick";
            this.rbClick.Size = new System.Drawing.Size(48, 17);
            this.rbClick.TabIndex = 3;
            this.rbClick.TabStop = true;
            this.rbClick.Text = "Click";
            this.rbClick.UseVisualStyleBackColor = true;
            this.rbClick.CheckedChanged += new System.EventHandler(this.Generic_CheckedChanged);
            // 
            // rbChange
            // 
            this.rbChange.AutoSize = true;
            this.rbChange.Location = new System.Drawing.Point(6, 29);
            this.rbChange.Name = "rbChange";
            this.rbChange.Size = new System.Drawing.Size(62, 17);
            this.rbChange.TabIndex = 2;
            this.rbChange.TabStop = true;
            this.rbChange.Text = "Change";
            this.rbChange.UseVisualStyleBackColor = true;
            this.rbChange.CheckedChanged += new System.EventHandler(this.Generic_CheckedChanged);
            // 
            // rbBlur
            // 
            this.rbBlur.AutoSize = true;
            this.rbBlur.Location = new System.Drawing.Point(6, 6);
            this.rbBlur.Name = "rbBlur";
            this.rbBlur.Size = new System.Drawing.Size(43, 17);
            this.rbBlur.TabIndex = 1;
            this.rbBlur.TabStop = true;
            this.rbBlur.Text = "Blur";
            this.rbBlur.UseVisualStyleBackColor = true;
            this.rbBlur.CheckedChanged += new System.EventHandler(this.Generic_CheckedChanged);
            // 
            // pageWait
            // 
            this.pageWait.Controls.Add(this.label4);
            this.pageWait.Controls.Add(this.numWait);
            this.pageWait.Controls.Add(this.rbWaitUntilRemoved);
            this.pageWait.Controls.Add(this.rbWaitUntilExists);
            this.pageWait.Controls.Add(this.rbWaitForComplete);
            this.pageWait.Controls.Add(this.rbWait);
            this.pageWait.Location = new System.Drawing.Point(4, 22);
            this.pageWait.Name = "pageWait";
            this.pageWait.Padding = new System.Windows.Forms.Padding(3);
            this.pageWait.Size = new System.Drawing.Size(406, 381);
            this.pageWait.TabIndex = 0;
            this.pageWait.Text = "Waits";
            this.pageWait.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(133, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Seconds";
            // 
            // numWait
            // 
            this.numWait.Location = new System.Drawing.Point(60, 6);
            this.numWait.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWait.Name = "numWait";
            this.numWait.Size = new System.Drawing.Size(66, 20);
            this.numWait.TabIndex = 1;
            this.numWait.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // rbWaitUntilRemoved
            // 
            this.rbWaitUntilRemoved.AutoSize = true;
            this.rbWaitUntilRemoved.Location = new System.Drawing.Point(6, 75);
            this.rbWaitUntilRemoved.Name = "rbWaitUntilRemoved";
            this.rbWaitUntilRemoved.Size = new System.Drawing.Size(120, 17);
            this.rbWaitUntilRemoved.TabIndex = 4;
            this.rbWaitUntilRemoved.TabStop = true;
            this.rbWaitUntilRemoved.Text = "Wait Until Removed";
            this.rbWaitUntilRemoved.UseVisualStyleBackColor = true;
            this.rbWaitUntilRemoved.CheckedChanged += new System.EventHandler(this.Generic_CheckedChanged);
            // 
            // rbWaitUntilExists
            // 
            this.rbWaitUntilExists.AutoSize = true;
            this.rbWaitUntilExists.Location = new System.Drawing.Point(6, 52);
            this.rbWaitUntilExists.Name = "rbWaitUntilExists";
            this.rbWaitUntilExists.Size = new System.Drawing.Size(101, 17);
            this.rbWaitUntilExists.TabIndex = 3;
            this.rbWaitUntilExists.TabStop = true;
            this.rbWaitUntilExists.Text = "Wait Until Exists";
            this.rbWaitUntilExists.UseVisualStyleBackColor = true;
            this.rbWaitUntilExists.CheckedChanged += new System.EventHandler(this.Generic_CheckedChanged);
            // 
            // rbWaitForComplete
            // 
            this.rbWaitForComplete.AutoSize = true;
            this.rbWaitForComplete.Location = new System.Drawing.Point(6, 29);
            this.rbWaitForComplete.Name = "rbWaitForComplete";
            this.rbWaitForComplete.Size = new System.Drawing.Size(112, 17);
            this.rbWaitForComplete.TabIndex = 2;
            this.rbWaitForComplete.TabStop = true;
            this.rbWaitForComplete.Text = "Wait For Complete";
            this.rbWaitForComplete.UseVisualStyleBackColor = true;
            this.rbWaitForComplete.CheckedChanged += new System.EventHandler(this.Generic_CheckedChanged);
            // 
            // rbWait
            // 
            this.rbWait.AutoSize = true;
            this.rbWait.Location = new System.Drawing.Point(6, 6);
            this.rbWait.Name = "rbWait";
            this.rbWait.Size = new System.Drawing.Size(47, 17);
            this.rbWait.TabIndex = 0;
            this.rbWait.TabStop = true;
            this.rbWait.Text = "Wait";
            this.rbWait.UseVisualStyleBackColor = true;
            this.rbWait.CheckedChanged += new System.EventHandler(this.rbWait_CheckedChanged);
            // 
            // pageAssertions
            // 
            this.pageAssertions.Controls.Add(this.label6);
            this.pageAssertions.Controls.Add(this.rbAreEqualIgnoringCase);
            this.pageAssertions.Controls.Add(this.rbEndsWith);
            this.pageAssertions.Controls.Add(this.rbStartsWith);
            this.pageAssertions.Controls.Add(this.rbContains);
            this.pageAssertions.Controls.Add(this.rbLessOrEqual);
            this.pageAssertions.Controls.Add(this.rbGreaterOrEqual);
            this.pageAssertions.Controls.Add(this.label5);
            this.pageAssertions.Controls.Add(this.txtMessage);
            this.pageAssertions.Controls.Add(this.label3);
            this.pageAssertions.Controls.Add(this.txtTestValue);
            this.pageAssertions.Controls.Add(this.rbLess);
            this.pageAssertions.Controls.Add(this.rbGreater);
            this.pageAssertions.Controls.Add(this.rbAreNotEqual);
            this.pageAssertions.Controls.Add(this.rbAreEqual);
            this.pageAssertions.Controls.Add(this.label2);
            this.pageAssertions.Controls.Add(this.txtCurrentValue);
            this.pageAssertions.Controls.Add(this.label1);
            this.pageAssertions.Controls.Add(this.txtAttribute);
            this.pageAssertions.Location = new System.Drawing.Point(4, 22);
            this.pageAssertions.Name = "pageAssertions";
            this.pageAssertions.Size = new System.Drawing.Size(406, 381);
            this.pageAssertions.TabIndex = 2;
            this.pageAssertions.Text = "Assertions";
            this.pageAssertions.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(13, 131);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(339, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "If you are not compiling to xUnit, assertion code is in AssertCompare.cs";
            // 
            // rbAreEqualIgnoringCase
            // 
            this.rbAreEqualIgnoringCase.AutoSize = true;
            this.rbAreEqualIgnoringCase.Location = new System.Drawing.Point(16, 300);
            this.rbAreEqualIgnoringCase.Name = "rbAreEqualIgnoringCase";
            this.rbAreEqualIgnoringCase.Size = new System.Drawing.Size(133, 17);
            this.rbAreEqualIgnoringCase.TabIndex = 12;
            this.rbAreEqualIgnoringCase.TabStop = true;
            this.rbAreEqualIgnoringCase.Text = "Equal (Case Insensive)";
            this.rbAreEqualIgnoringCase.UseVisualStyleBackColor = true;
            this.rbAreEqualIgnoringCase.CheckedChanged += new System.EventHandler(this.StringType_CheckedChanged);
            // 
            // rbEndsWith
            // 
            this.rbEndsWith.AutoSize = true;
            this.rbEndsWith.Location = new System.Drawing.Point(16, 277);
            this.rbEndsWith.Name = "rbEndsWith";
            this.rbEndsWith.Size = new System.Drawing.Size(104, 17);
            this.rbEndsWith.TabIndex = 11;
            this.rbEndsWith.TabStop = true;
            this.rbEndsWith.Text = "Ends With String";
            this.rbEndsWith.UseVisualStyleBackColor = true;
            this.rbEndsWith.CheckedChanged += new System.EventHandler(this.StringType_CheckedChanged);
            // 
            // rbStartsWith
            // 
            this.rbStartsWith.AutoSize = true;
            this.rbStartsWith.Location = new System.Drawing.Point(16, 254);
            this.rbStartsWith.Name = "rbStartsWith";
            this.rbStartsWith.Size = new System.Drawing.Size(107, 17);
            this.rbStartsWith.TabIndex = 10;
            this.rbStartsWith.TabStop = true;
            this.rbStartsWith.Text = "Starts With String";
            this.rbStartsWith.UseVisualStyleBackColor = true;
            this.rbStartsWith.CheckedChanged += new System.EventHandler(this.StringType_CheckedChanged);
            // 
            // rbContains
            // 
            this.rbContains.AutoSize = true;
            this.rbContains.Location = new System.Drawing.Point(16, 231);
            this.rbContains.Name = "rbContains";
            this.rbContains.Size = new System.Drawing.Size(96, 17);
            this.rbContains.TabIndex = 9;
            this.rbContains.TabStop = true;
            this.rbContains.Text = "Contains String";
            this.rbContains.UseVisualStyleBackColor = true;
            this.rbContains.CheckedChanged += new System.EventHandler(this.StringType_CheckedChanged);
            // 
            // rbLessOrEqual
            // 
            this.rbLessOrEqual.AutoSize = true;
            this.rbLessOrEqual.Location = new System.Drawing.Point(143, 208);
            this.rbLessOrEqual.Name = "rbLessOrEqual";
            this.rbLessOrEqual.Size = new System.Drawing.Size(89, 17);
            this.rbLessOrEqual.TabIndex = 8;
            this.rbLessOrEqual.TabStop = true;
            this.rbLessOrEqual.Text = "Less or Equal";
            this.rbLessOrEqual.UseVisualStyleBackColor = true;
            this.rbLessOrEqual.CheckedChanged += new System.EventHandler(this.NumericType_CheckedChanged);
            // 
            // rbGreaterOrEqual
            // 
            this.rbGreaterOrEqual.AutoSize = true;
            this.rbGreaterOrEqual.Location = new System.Drawing.Point(143, 185);
            this.rbGreaterOrEqual.Name = "rbGreaterOrEqual";
            this.rbGreaterOrEqual.Size = new System.Drawing.Size(102, 17);
            this.rbGreaterOrEqual.TabIndex = 6;
            this.rbGreaterOrEqual.TabStop = true;
            this.rbGreaterOrEqual.Text = "Greater or Equal";
            this.rbGreaterOrEqual.UseVisualStyleBackColor = true;
            this.rbGreaterOrEqual.CheckedChanged += new System.EventHandler(this.NumericType_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Message";
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(86, 100);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(170, 20);
            this.txtMessage.TabIndex = 2;
            this.txtMessage.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GenericTextBox_KeyUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Expected";
            // 
            // txtTestValue
            // 
            this.txtTestValue.Location = new System.Drawing.Point(86, 74);
            this.txtTestValue.Name = "txtTestValue";
            this.txtTestValue.Size = new System.Drawing.Size(170, 20);
            this.txtTestValue.TabIndex = 1;
            this.txtTestValue.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GenericTextBox_KeyUp);
            this.txtTestValue.TextChanged += new System.EventHandler(this.txtTestValue_TextChanged);
            // 
            // rbLess
            // 
            this.rbLess.AutoSize = true;
            this.rbLess.Location = new System.Drawing.Point(16, 208);
            this.rbLess.Name = "rbLess";
            this.rbLess.Size = new System.Drawing.Size(47, 17);
            this.rbLess.TabIndex = 7;
            this.rbLess.TabStop = true;
            this.rbLess.Text = "Less";
            this.rbLess.UseVisualStyleBackColor = true;
            this.rbLess.CheckedChanged += new System.EventHandler(this.NumericType_CheckedChanged);
            // 
            // rbGreater
            // 
            this.rbGreater.AutoSize = true;
            this.rbGreater.Location = new System.Drawing.Point(16, 185);
            this.rbGreater.Name = "rbGreater";
            this.rbGreater.Size = new System.Drawing.Size(60, 17);
            this.rbGreater.TabIndex = 5;
            this.rbGreater.TabStop = true;
            this.rbGreater.Text = "Greater";
            this.rbGreater.UseVisualStyleBackColor = true;
            this.rbGreater.CheckedChanged += new System.EventHandler(this.NumericType_CheckedChanged);
            // 
            // rbAreNotEqual
            // 
            this.rbAreNotEqual.AutoSize = true;
            this.rbAreNotEqual.Location = new System.Drawing.Point(143, 162);
            this.rbAreNotEqual.Name = "rbAreNotEqual";
            this.rbAreNotEqual.Size = new System.Drawing.Size(72, 17);
            this.rbAreNotEqual.TabIndex = 4;
            this.rbAreNotEqual.TabStop = true;
            this.rbAreNotEqual.Text = "Not Equal";
            this.rbAreNotEqual.UseVisualStyleBackColor = true;
            this.rbAreNotEqual.CheckedChanged += new System.EventHandler(this.EqualityType_CheckedChanged);
            // 
            // rbAreEqual
            // 
            this.rbAreEqual.AutoSize = true;
            this.rbAreEqual.Location = new System.Drawing.Point(16, 162);
            this.rbAreEqual.Name = "rbAreEqual";
            this.rbAreEqual.Size = new System.Drawing.Size(52, 17);
            this.rbAreEqual.TabIndex = 3;
            this.rbAreEqual.TabStop = true;
            this.rbAreEqual.Text = "Equal";
            this.rbAreEqual.UseVisualStyleBackColor = true;
            this.rbAreEqual.CheckedChanged += new System.EventHandler(this.EqualityType_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Current Value";
            // 
            // txtCurrentValue
            // 
            this.txtCurrentValue.Enabled = false;
            this.txtCurrentValue.Location = new System.Drawing.Point(86, 48);
            this.txtCurrentValue.Name = "txtCurrentValue";
            this.txtCurrentValue.ReadOnly = true;
            this.txtCurrentValue.Size = new System.Drawing.Size(170, 20);
            this.txtCurrentValue.TabIndex = 2;
            this.txtCurrentValue.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GenericTextBox_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Attribute";
            // 
            // txtAttribute
            // 
            this.txtAttribute.Location = new System.Drawing.Point(86, 22);
            this.txtAttribute.Name = "txtAttribute";
            this.txtAttribute.Size = new System.Drawing.Size(170, 20);
            this.txtAttribute.TabIndex = 0;
            this.txtAttribute.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GenericTextBox_KeyUp);
            this.txtAttribute.TextChanged += new System.EventHandler(this.txtAttribute_TextChanged);
            // 
            // frmPropertyExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 407);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.gridProperties);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmPropertyExplorer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Property Explorer";
            ((System.ComponentModel.ISupportInitialize)(this.gridProperties)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.pageMethods.ResumeLayout(false);
            this.pageMethods.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFlash)).EndInit();
            this.pageWait.ResumeLayout(false);
            this.pageWait.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWait)).EndInit();
            this.pageAssertions.ResumeLayout(false);
            this.pageAssertions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridProperties;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage pageWait;
        private System.Windows.Forms.TabPage pageMethods;
        private System.Windows.Forms.TabPage pageAssertions;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPropertyName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPropertyValue;
        private System.Windows.Forms.RadioButton rbKeyUp;
        private System.Windows.Forms.RadioButton rbKeyDown;
        private System.Windows.Forms.RadioButton rbHighlight;
        private System.Windows.Forms.RadioButton rbFocus;
        private System.Windows.Forms.RadioButton rbFlash;
        private System.Windows.Forms.RadioButton rbFireEventNoWait;
        private System.Windows.Forms.RadioButton rbFireEvent;
        private System.Windows.Forms.RadioButton rbDoubleClick;
        private System.Windows.Forms.RadioButton rbClickNoWait;
        private System.Windows.Forms.RadioButton rbClick;
        private System.Windows.Forms.RadioButton rbChange;
        private System.Windows.Forms.RadioButton rbBlur;
        private System.Windows.Forms.RadioButton rbWait;
        private System.Windows.Forms.RadioButton rbMouseUp;
        private System.Windows.Forms.RadioButton rbMouseEnter;
        private System.Windows.Forms.RadioButton rbKeyPress;
        private System.Windows.Forms.RadioButton rbMouseDown;
        private System.Windows.Forms.RadioButton rbWaitUntilRemoved;
        private System.Windows.Forms.RadioButton rbWaitUntilExists;
        private System.Windows.Forms.RadioButton rbWaitForComplete;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCurrentValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAttribute;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTestValue;
        private System.Windows.Forms.RadioButton rbLess;
        private System.Windows.Forms.RadioButton rbGreater;
        private System.Windows.Forms.RadioButton rbAreNotEqual;
        private System.Windows.Forms.RadioButton rbAreEqual;
        private System.Windows.Forms.TextBox txtKeyUp;
        private System.Windows.Forms.TextBox txtKeyPress;
        private System.Windows.Forms.TextBox txtKeyDown;
        private System.Windows.Forms.NumericUpDown numFlash;
        private System.Windows.Forms.TextBox txtFireEventNoWait;
        private System.Windows.Forms.TextBox txtFireEvent;
        private System.Windows.Forms.NumericUpDown numWait;
        private System.Windows.Forms.RadioButton rbUnchecked;
        private System.Windows.Forms.RadioButton rbChecked;
        private System.Windows.Forms.RadioButton rbSetFilename;
        private System.Windows.Forms.RadioButton rbSelectByValue;
        private System.Windows.Forms.RadioButton rbSelectByText;
        private System.Windows.Forms.Button btnSetFilename;
        private System.Windows.Forms.TextBox txtSetFilename;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.RadioButton rbLessOrEqual;
        private System.Windows.Forms.RadioButton rbGreaterOrEqual;
        private System.Windows.Forms.RadioButton rbEndsWith;
        private System.Windows.Forms.RadioButton rbStartsWith;
        private System.Windows.Forms.RadioButton rbContains;
        private System.Windows.Forms.RadioButton rbAreEqualIgnoringCase;
        private System.Windows.Forms.ComboBox comboSelectByValue;
        private System.Windows.Forms.ComboBox comboSelectByText;
        private System.Windows.Forms.Label label6;
    }
}