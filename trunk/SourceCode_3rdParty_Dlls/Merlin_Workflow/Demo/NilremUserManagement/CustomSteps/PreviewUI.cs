using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Text;

namespace NilremUserManagement.CustomSteps
{
    partial class PreviewUI : UserControl
    {
        public PreviewUI()
        {
            InitializeComponent();
        }

        internal void DisplayUser(User user)
        {
            picIcon.Image = user.Picture;
            lblUserId.Text = user.UserId;
            lblFullName.Text = user.FullName.ToString();
            lblRole.Text = "Role: " + user.Role.ToString();
            if (user.Role == User.UserRole.Administrator && user.MayCreateAdministrators)
            {
                lblRole.Text += " (may add administrators)";
            }
            StringBuilder passwordMasked = new StringBuilder();
            for (int i = 0; i < user.Password.Length; ++i)
            {
                passwordMasked.Append('*');
            }
            lblPassword.Text = "Password: " + passwordMasked.ToString();
            lblEmail.Text = "E-mail Address: " + user.Email;
        }

        private void PreviewUI_Resize(object sender, System.EventArgs e)
        {
            int margin = 10;
            pnlControls.Height = pnlControls.Parent.ClientRectangle.Height - 2 * margin;
            pnlControls.Width = pnlControls.Parent.ClientRectangle.Width - 2 * margin;
            pnlControls.Top = margin;
            pnlControls.Left = margin;
            lblUserId.Width = pnlControls.Width - lblUserId.Left;
            lblFullName.Width = pnlControls.Width - lblFullName.Left;
            lblEmail.Top = lblPassword.Top + (lblPassword.Top - lblRole.Top);
        }
    }
}
