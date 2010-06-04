namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class IpPermission
    {
        private decimal? fromPortField;
        private string ipProtocolField;
        private List<string> ipRangeField;
        private decimal? toPortField;
        private List<Amazon.EC2.Model.UserIdGroupPair> userIdGroupPairField;

        public bool IsSetFromPort()
        {
            return this.fromPortField.HasValue;
        }

        public bool IsSetIpProtocol()
        {
            return (this.ipProtocolField != null);
        }

        public bool IsSetIpRange()
        {
            return (this.IpRange.Count > 0);
        }

        public bool IsSetToPort()
        {
            return this.toPortField.HasValue;
        }

        public bool IsSetUserIdGroupPair()
        {
            return (this.UserIdGroupPair.Count > 0);
        }

        public IpPermission WithFromPort(decimal fromPort)
        {
            this.fromPortField = new decimal?(fromPort);
            return this;
        }

        public IpPermission WithIpProtocol(string ipProtocol)
        {
            this.ipProtocolField = ipProtocol;
            return this;
        }

        public IpPermission WithIpRange(params string[] list)
        {
            foreach (string str in list)
            {
                this.IpRange.Add(str);
            }
            return this;
        }

        public IpPermission WithToPort(decimal toPort)
        {
            this.toPortField = new decimal?(toPort);
            return this;
        }

        public IpPermission WithUserIdGroupPair(params Amazon.EC2.Model.UserIdGroupPair[] list)
        {
            foreach (Amazon.EC2.Model.UserIdGroupPair pair in list)
            {
                this.UserIdGroupPair.Add(pair);
            }
            return this;
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

        [XmlElement(ElementName="IpRange")]
        public List<string> IpRange
        {
            get
            {
                if (this.ipRangeField == null)
                {
                    this.ipRangeField = new List<string>();
                }
                return this.ipRangeField;
            }
            set
            {
                this.ipRangeField = value;
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

        [XmlElement(ElementName="UserIdGroupPair")]
        public List<Amazon.EC2.Model.UserIdGroupPair> UserIdGroupPair
        {
            get
            {
                if (this.userIdGroupPairField == null)
                {
                    this.userIdGroupPairField = new List<Amazon.EC2.Model.UserIdGroupPair>();
                }
                return this.userIdGroupPairField;
            }
            set
            {
                this.userIdGroupPairField = value;
            }
        }
    }
}

