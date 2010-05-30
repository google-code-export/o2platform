namespace DemoApp
{
    partial class frmLocateResource
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLocateResource));
            this.dgvResources = new System.Windows.Forms.DataGridView();
            this.Icon = new System.Windows.Forms.DataGridViewImageColumn();
            this.Source = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Locate = new System.Windows.Forms.DataGridViewButtonColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblLocate = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResources)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvResources
            // 
            this.dgvResources.AllowUserToAddRows = false;
            this.dgvResources.AllowUserToDeleteRows = false;
            this.dgvResources.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvResources.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvResources.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResources.ColumnHeadersVisible = false;
            this.dgvResources.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Icon,
            this.Source,
            this.Path,
            this.Locate});
            this.dgvResources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResources.EnableHeadersVisualStyles = false;
            this.dgvResources.Location = new System.Drawing.Point(0, 37);
            this.dgvResources.MultiSelect = false;
            this.dgvResources.Name = "dgvResources";
            this.dgvResources.RowHeadersVisible = false;
            this.dgvResources.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResources.Size = new System.Drawing.Size(608, 207);
            this.dgvResources.TabIndex = 0;
            this.dgvResources.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResources_CellContentClick);
            // 
            // Icon
            // 
            this.Icon.Frozen = true;
            this.Icon.HeaderText = "Located";
            this.Icon.Name = "Icon";
            this.Icon.ReadOnly = true;
            this.Icon.Width = 50;
            // 
            // Source
            // 
            this.Source.Frozen = true;
            this.Source.HeaderText = "Source";
            this.Source.Name = "Source";
            this.Source.ReadOnly = true;
            // 
            // Path
            // 
            this.Path.Frozen = true;
            this.Path.HeaderText = "Path";
            this.Path.Name = "Path";
            this.Path.ReadOnly = true;
            this.Path.Width = 380;
            // 
            // Locate
            // 
            this.Locate.Frozen = true;
            this.Locate.HeaderText = "Locate";
            this.Locate.Name = "Locate";
            this.Locate.Width = 50;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 244);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(608, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageList1.Images.SetKeyName(0, "Delete.bmp");
            this.imageList1.Images.SetKeyName(1, "Tick.bmp");
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Any File (*.*)|*.*";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblLocate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(608, 37);
            this.panel1.TabIndex = 2;
            // 
            // lblLocate
            // 
            this.lblLocate.AutoSize = true;
            this.lblLocate.Location = new System.Drawing.Point(12, 9);
            this.lblLocate.Name = "lblLocate";
            this.lblLocate.Size = new System.Drawing.Size(396, 13);
            this.lblLocate.TabIndex = 0;
            this.lblLocate.Text = "Some items cannot be found, preventing compilation.  Please find these resources:" +
                "";
            // 
            // frmLocateResource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 266);
            this.Controls.Add(this.dgvResources);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "frmLocateResource";
            this.Text = "Locate Resources";
            ((System.ComponentModel.ISupportInitialize)(this.dgvResources)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvResources;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblLocate;
        private System.Windows.Forms.DataGridViewImageColumn Icon;
        private System.Windows.Forms.DataGridViewTextBoxColumn Source;
        private System.Windows.Forms.DataGridViewTextBoxColumn Path;
        private System.Windows.Forms.DataGridViewButtonColumn Locate;
    }
}