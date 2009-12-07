using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace NilremUserManagement
{
    class User
    {

        public User()
        {
            this.Picture = NilremUserManagement.Properties.Resources.DefaultFace;
            MayCreateAdministrators = false;
        }

        public string UserId { get; set; }
        public string FullName { get; set; }
        public UserRole Role { get; set; }
        public Image Picture { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets whether this user can create other administrators.
        /// This setting is ignored if the user is not herself an administrator.
        /// </summary>
        public bool MayCreateAdministrators { get; set; }

        #region Nested Types
        public class Name
        {
            public string First{ get; set; }
            public string Middle { get; set; }
            public string Last { get; set; }
            public Image Picture { get; set; }

            public override string ToString()
            {
                return First + " " + Middle + " " + Last;
            }
        }

        public enum UserRole { Guest, Standard, Administrator };
        #endregion
    }
}
