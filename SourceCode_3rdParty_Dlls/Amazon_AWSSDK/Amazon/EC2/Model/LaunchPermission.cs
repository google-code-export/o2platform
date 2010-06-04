namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class LaunchPermission
    {
        private string groupNameField;
        private string userIdField;

        public bool IsSetGroupName()
        {
            return (this.groupNameField != null);
        }

        public bool IsSetUserId()
        {
            return (this.userIdField != null);
        }

        public LaunchPermission WithGroupName(string groupName)
        {
            this.groupNameField = groupName;
            return this;
        }

        public LaunchPermission WithUserId(string userId)
        {
            this.userIdField = userId;
            return this;
        }

        [XmlElement(ElementName="GroupName")]
        public string GroupName
        {
            get
            {
                return this.groupNameField;
            }
            set
            {
                this.groupNameField = value;
            }
        }

        [XmlElement(ElementName="UserId")]
        public string UserId
        {
            get
            {
                return this.userIdField;
            }
            set
            {
                this.userIdField = value;
            }
        }
    }
}

