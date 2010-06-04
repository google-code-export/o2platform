namespace Amazon.RDS.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class IPRange
    {
        private string CIDRIPField;
        private string statusField;

        public bool IsSetCIDRIP()
        {
            return (this.CIDRIPField != null);
        }

        public bool IsSetStatus()
        {
            return (this.statusField != null);
        }

        public IPRange WithCIDRIP(string CIDRIP)
        {
            this.CIDRIPField = CIDRIP;
            return this;
        }

        public IPRange WithStatus(string status)
        {
            this.statusField = status;
            return this;
        }

        [XmlElement(ElementName="CIDRIP")]
        public string CIDRIP
        {
            get
            {
                return this.CIDRIPField;
            }
            set
            {
                this.CIDRIPField = value;
            }
        }

        [XmlElement(ElementName="Status")]
        public string Status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }
    }
}

