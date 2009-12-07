using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NilremUserManagement.CustomSteps;

namespace NilremUserManagement
{
    partial class UserManagementForm : Form
    {
        private List<User> users;
        private const int iconSize = 62;

        public UserManagementForm(IEnumerable<User> users)
        {
            InitializeComponent();
            grdUsers.Columns[0].Width = iconSize;
            this.users = new List<User>(users);
            refreshUserTable();
        }

        /// <summary>
        /// Repopulates the table of users.
        /// </summary>
        private void refreshUserTable()
        {
            grdUsers.Rows.Clear();
            foreach (User user in users)
            {
                var row = new DataGridViewRow();
                var pictureCell = new DataGridViewImageCell();
                pictureCell.Value = user.Picture;
                pictureCell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                row.MinimumHeight = iconSize;
                var userIdCell = new DataGridViewTextBoxCell();
                userIdCell.Value = user.UserId;
                var nameCell = new DataGridViewTextBoxCell();
                nameCell.Value = user.FullName.ToString();
                var roleCell = new DataGridViewTextBoxCell();
                roleCell.Value = user.Role.ToString();
                row.Cells.AddRange(new DataGridViewCell[]{pictureCell, userIdCell, nameCell, roleCell});
                grdUsers.Rows.Add(row);
            }
        }

        private void btnNewUser_Click(object sender, EventArgs e)
        {
            User newUser = NewUserWizard.RunNewUserWizard();
            if (newUser != null)
            {
                users.Add(newUser);
            }
            refreshUserTable();
        }
    }
}
