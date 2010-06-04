namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class AuthorizeSecurityGroupIngressRequest
    {
        private string cidrIpField;
        private decimal? fromPortField;
        private string groupNameField;
        private string ipProtocolField;
        private string sourceSecurityGroupNameField;
        private string sourceSecurityGroupOwnerIdField;
        private decimal? toPortField;
        private string userIdField;

        public bool IsSetCidrIp()
        {
            return (this.cidrIpField != null);
        }

        public bool IsSetFromPort()
        {
            return this.fromPortField.HasValue;
        }

        public bool IsSetGroupName()
        {
            return (this.groupNameField != null);
        }

        public bool IsSetIpProtocol()
        {
            return (this.ipProtocolField != null);
        }

        public bool IsSetSourceSecurityGroupName()
        {
            return (this.sourceSecurityGroupNameField != null);
        }

        public bool IsSetSourceSecurityGroupOwnerId()
        {
            return (this.sourceSecurityGroupOwnerIdField != null);
        }

        public bool IsSetToPort()
        {
            return this.toPortField.HasValue;
        }

        public bool IsSetUserId()
        {
            return (this.userIdField != null);
        }

        public AuthorizeSecurityGroupIngressRequest WithCidrIp(string cidrIp)
        {
            this.cidrIpField = cidrIp;
            return this;
        }

        public AuthorizeSecurityGroupIngressRequest WithFromPort(decimal fromPort)
        {
            this.fromPortField = new decimal?(fromPort);
            return this;
        }

        public AuthorizeSecurityGroupIngressRequest WithGroupName(string groupName)
        {
            this.groupNameField = groupName;
            return this;
        }

        public AuthorizeSecurityGroupIngressRequest WithIpProtocol(string ipProtocol)
        {
            this.ipProtocolField = ipProtocol;
            return this;
        }

        public AuthorizeSecurityGroupIngressRequest WithSourceSecurityGroupName(string sourceSecurityGroupName)
        {
            this.sourceSecurityGroupNameField = sourceSecurityGroupName;
            return this;
        }

        public AuthorizeSecurityGroupIngressRequest WithSourceSecurityGroupOwnerId(string sourceSecurityGroupOwnerId)
        {
            this.sourceSecurityGroupOwnerIdField = sourceSecurityGroupOwnerId;
            return this;
        }

        public AuthorizeSecurityGroupIngressRequest WithToPort(decimal toPort)
        {
            this.toPortField = new decimal?(toPort);
            return this;
        }

        public AuthorizeSecurityGroupIngressRequest WithUserId(string userId)
        {
            this.userIdField = userId;
            return this;
        }

        [XmlElement(ElementName="CidrIp")]
        public string CidrIp
        {
            get
            {
                return this.cidrIpField;
            }
            set
            {
                this.cidrIpField = value;
            }
        }

        [XmlElement(ElementName="FromPort")]
        public decimal FromPort
        {
            get
            {
                return this.fromPortField.GetValueOrDefault();
            }
            set
            {
                this.fromPortField = new decimal?(value);
            }
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

        [XmlElement(ElementName="IpProtocol")]
        public string IpProtocol
        {
            get
            {
                return this.ipProtocolField;
            }
            set
            {
                this.ipProtocolField = value;
            }
        }

        [XmlElement(ElementName="SourceSecurityGroupName")]
        public string SourceSecurityGroupName
        {
            get
            {
                return this.sourceSecurityGroupNameField;
            }
            set
            {
                this.sourceSecurityGroupNameField = value;
            }
        }

        [XmlElement(ElementName="SourceSecurityGroupOwnerId")]
        public string SourceSecurityGroupOwnerId
        {
            get
            {
                return this.sourceSecurityGroupOwnerIdField;
            }
            set
            {
                this.sourceSecurityGroupOwnerIdField = value;
            }
        }

        [XmlElement(ElementName="ToPort")]
        public decimal ToPort
        {
            get
            {
                return this.toPortField.GetValueOrDefault();
            }
            set
            {
                this.toPortField = new decimal?(value);
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

