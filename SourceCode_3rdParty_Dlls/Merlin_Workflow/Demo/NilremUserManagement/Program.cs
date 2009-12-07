using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NilremUserManagement
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            List<User> users = new List<User>();
            User defaultUser = new User();
            defaultUser.UserId = "default";
            defaultUser.Role = User.UserRole.Standard;
            defaultUser.FullName = "Joseph Default Johnson";
            users.Add(defaultUser);

            Application.Run(new UserManagementForm(users));
        }
    }
}
