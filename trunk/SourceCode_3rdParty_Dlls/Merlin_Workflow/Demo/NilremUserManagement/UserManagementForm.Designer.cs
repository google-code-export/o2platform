namespace NilremUserManagement
{
    partial class UserManagementForm
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
            this.txtSlogan = new System.Windows.Forms.TextBox();
            this.lblUserList = new System.Windows.Forms.Label();
            this.grdUsers = new System.Windows.Forms.DataGridView();
            this.clmImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.clmUserId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmFullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmUserType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.picCompanyName = new System.Windows.Forms.PictureBox();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.btnNewUser = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCompanyName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSlogan
            // 
            this.txtSlogan.BackColor = System.Drawing.Color.White;
            this.txtSlogan.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSlogan.Enabled = false;
            this.txtSlogan.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSlogan.ForeColor = System.Drawing.Color.Black;
            this.txtSlogan.Location = new System.Drawing.Point(163, 57);
            this.txtSlogan.Name = "txtSlogan";
            this.txtSlogan.ReadOnly = true;
            this.txtSlogan.ShortcutsEnabled = false;
            this.txtSlogan.Size = new System.Drawing.Size(515, 19);
            this.txtSlogan.TabIndex = 2;
            this.txtSlogan.Text = "Creating top-quality software demos since 1847";
            this.txtSlogan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblUserList
            // 
            this.lblUserList.AutoSize = true;
            this.lblUserList.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserList.Location = new System.Drawing.Point(344, 106);
            this.lblUserList.Name = "lblUserList";
            this.lblUserList.Size = new System.Drawing.Size(163, 24);
            this.lblUserList.TabIndex = 3;
            this.lblUserList.Text = "User Maintenance";
            // 
            // grdUsers
            // 
            this.grdUsers.AllowUserToAddRows = false;
            this.grdUsers.AllowUserToDeleteRows = false;
            this.grdUsers.AllowUserToOrderColumns = true;
            this.grdUsers.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(229)))), ((int)(((byte)(224)))));
            this.grdUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmImage,
            this.clmUserId,
            this.clmFullName,
            this.clmUserType});
            this.grdUsers.Location = new System.Drawing.Point(59, 145);
            this.grdUsers.MultiSelect = false;
            this.grdUsers.Name = "grdUsers";
            this.grdUsers.ReadOnly = true;
            this.grdUsers.RowHeadersVisible = false;
            this.grdUsers.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.grdUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdUsers.ShowCellToolTips = false;
            this.grdUsers.ShowEditingIcon = false;
            this.grdUsers.Size = new System.Drawing.Size(730, 355);
            this.grdUsers.TabIndex = 4;
            // 
            // clmImage
            // 
            this.clmImage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.clmImage.FillWeight = 1F;
            this.clmImage.HeaderText = "";
            this.clmImage.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.clmImage.Name = "clmImage";
            this.clmImage.ReadOnly = true;
            this.clmImage.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.clmImage.Width = 60;
            // 
            // clmUserId
            // 
            this.clmUserId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.clmUserId.HeaderText = "User ID";
            this.clmUserId.Name = "clmUserId";
            this.clmUserId.ReadOnly = true;
            this.clmUserId.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmUserId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmUserId.Width = 49;
            // 
            // clmFullName
            // 
            this.clmFullName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmFullName.HeaderText = "Full Name";
            this.clmFullName.Name = "clmFullName";
            this.clmFullName.ReadOnly = true;
            this.clmFullName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmFullName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmUserType
            // 
            this.clmUserType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.clmUserType.HeaderText = "User Type";
            this.clmUserType.Name = "clmUserType";
            this.clmUserType.ReadOnly = true;
            this.clmUserType.Width = 81;
            // 
            // picCompanyName
            // 
            this.picCompanyName.Image = global::NilremUserManagement.Properties.Resources.nerlimheader;
            this.picCompanyName.Location = new System.Drawing.Point(303, 12);
            this.picCompanyName.Name = "picCompanyName";
            this.picCompanyName.Size = new System.Drawing.Size(243, 48);
            this.picCompanyName.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picCompanyName.TabIndex = 1;
            this.picCompanyName.TabStop = false;
            // 
            // picLogo
            // 
            this.picLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picLogo.Image = global::NilremUserManagement.Properties.Resources.nerlimlogo;
            this.picLogo.Location = new System.Drawing.Point(12, 12);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(82, 106);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // btnNewUser
            // 
            this.btnNewUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(215)))), ((int)(((byte)(198)))));
            this.btnNewUser.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewUser.Location = new System.Drawing.Point(663, 515);
            this.btnNewUser.Name = "btnNewUser";
            this.btnNewUser.Size = new System.Drawing.Size(125, 29);
            this.btnNewUser.TabIndex = 5;
            this.btnNewUser.Text = "&New User...";
            this.btnNewUser.UseVisualStyleBackColor = false;
            this.btnNewUser.Click += new System.EventHandler(this.btnNewUser_Click);
            // 
            // UserManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(847, 559);
            this.Controls.Add(this.btnNewUser);
            this.Controls.Add(this.grdUsers);
            this.Controls.Add(this.lblUserList);
            this.Controls.Add(this.txtSlogan);
            this.Controls.Add(this.picCompanyName);
            this.Controls.Add(this.picLogo);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "UserManagementForm";
            this.ShowIcon = false;
            this.Text = "User Management";
            ((System.ComponentModel.ISupportInitialize)(this.grdUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCompanyName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.PictureBox picCompanyName;
        private System.Windows.Forms.TextBox txtSlogan;
        private System.Windows.Forms.Label lblUserList;
        private System.Windows.Forms.DataGridView grdUsers;
        private System.Windows.Forms.Button btnNewUser;
        private System.Windows.Forms.DataGridViewImageColumn clmImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmUserId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmFullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmUserType;
    }
}

