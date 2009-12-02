namespace O2.Cmd.SpringMvc.Ascx
{
    partial class ascx_SpringMvcMappings
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbNumberOfNodesShown = new System.Windows.Forms.Label();
            this.cbLoadedControlersViewMode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tvControllers = new System.Windows.Forms.TreeView();
            this.label2 = new System.Windows.Forms.Label();
            this.lbLoadedCirDataFile = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbNumberOfControllersLoaded = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.llClearLoadedMappedControllers = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lbNumberOfNodesShown
            // 
            this.lbNumberOfNodesShown.AutoSize = true;
            this.lbNumberOfNodesShown.Location = new System.Drawing.Point(3, 53);
            this.lbNumberOfNodesShown.Name = "lbNumberOfNodesShown";
            this.lbNumberOfNodesShown.Size = new System.Drawing.Size(16, 13);
            this.lbNumberOfNodesShown.TabIndex = 16;
            this.lbNumberOfNodesShown.Text = "...";
            // 
            // cbLoadedControlersViewMode
            // 
            this.cbLoadedControlersViewMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbLoadedControlersViewMode.DropDownHeight = 200;
            this.cbLoadedControlersViewMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLoadedControlersViewMode.FormattingEnabled = true;
            this.cbLoadedControlersViewMode.IntegralHeight = false;
            this.cbLoadedControlersViewMode.Location = new System.Drawing.Point(70, 32);
            this.cbLoadedControlersViewMode.Name = "cbLoadedControlersViewMode";
            this.cbLoadedControlersViewMode.Size = new System.Drawing.Size(202, 21);
            this.cbLoadedControlersViewMode.TabIndex = 18;
            this.cbLoadedControlersViewMode.SelectedIndexChanged += new System.EventHandler(this.cbLoadedControlersViewMode_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "View Mode";
            // 
            // tvControllers
            // 
            this.tvControllers.AllowDrop = true;
            this.tvControllers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvControllers.HideSelection = false;
            this.tvControllers.Location = new System.Drawing.Point(3, 72);
            this.tvControllers.Name = "tvControllers";
            this.tvControllers.Size = new System.Drawing.Size(269, 217);
            this.tvControllers.TabIndex = 14;
            this.tvControllers.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvControllers_DragDrop);
            this.tvControllers.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvControllers_AfterSelect);
            this.tvControllers.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvControllers_DragEnter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "View Mapped Controllers:";
            // 
            // lbLoadedCirDataFile
            // 
            this.lbLoadedCirDataFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbLoadedCirDataFile.AutoSize = true;
            this.lbLoadedCirDataFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLoadedCirDataFile.Location = new System.Drawing.Point(114, 293);
            this.lbLoadedCirDataFile.Name = "lbLoadedCirDataFile";
            this.lbLoadedCirDataFile.Size = new System.Drawing.Size(19, 13);
            this.lbLoadedCirDataFile.TabIndex = 22;
            this.lbLoadedCirDataFile.Text = "...";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 293);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Loaded CirData File:";
            // 
            // lbNumberOfControllersLoaded
            // 
            this.lbNumberOfControllersLoaded.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbNumberOfControllersLoaded.AutoSize = true;
            this.lbNumberOfControllersLoaded.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNumberOfControllersLoaded.Location = new System.Drawing.Point(125, 312);
            this.lbNumberOfControllersLoaded.Name = "lbNumberOfControllersLoaded";
            this.lbNumberOfControllersLoaded.Size = new System.Drawing.Size(14, 13);
            this.lbNumberOfControllersLoaded.TabIndex = 20;
            this.lbNumberOfControllersLoaded.Text = "0";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 312);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "# of controllers loaded:";
            // 
            // llClearLoadedMappedControllers
            // 
            this.llClearLoadedMappedControllers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.llClearLoadedMappedControllers.AutoSize = true;
            this.llClearLoadedMappedControllers.Location = new System.Drawing.Point(242, 292);
            this.llClearLoadedMappedControllers.Name = "llClearLoadedMappedControllers";
            this.llClearLoadedMappedControllers.Size = new System.Drawing.Size(30, 13);
            this.llClearLoadedMappedControllers.TabIndex = 23;
            this.llClearLoadedMappedControllers.TabStop = true;
            this.llClearLoadedMappedControllers.Text = "clear";
            this.llClearLoadedMappedControllers.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llClearLoadedMappedControllers_LinkClicked);
            // 
            // ascx_SpringMvcMappings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.llClearLoadedMappedControllers);
            this.Controls.Add(this.lbLoadedCirDataFile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbNumberOfControllersLoaded);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbNumberOfNodesShown);
            this.Controls.Add(this.cbLoadedControlersViewMode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tvControllers);
            this.Controls.Add(this.label2);
            this.Name = "ascx_SpringMvcMappings";
            this.Size = new System.Drawing.Size(275, 332);
            this.Load += new System.EventHandler(this.ascx_SpringMvcMappings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbNumberOfNodesShown;
        private System.Windows.Forms.ComboBox cbLoadedControlersViewMode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TreeView tvControllers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbLoadedCirDataFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbNumberOfControllersLoaded;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel llClearLoadedMappedControllers;
    }
}
