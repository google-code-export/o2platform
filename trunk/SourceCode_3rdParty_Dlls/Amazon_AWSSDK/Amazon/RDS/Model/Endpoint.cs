namespace Amazon.RDS.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class Endpoint
    {
        private string addressField;
        private decimal? portField;

        public bool IsSetAddress()
        {
            return (this.addressField != null);
        }

        public bool IsSetPort()
        {
            return this.portField.HasValue;
        }

        public Endpoint WithAddress(string address)
        {
            this.addressField = address;
            return this;
        }

        public Endpoint WithPort(decimal port)
        {
            this.portField = new decimal?(port);
            return this;
        }

        [XmlElement(ElementName="Address")]
        public string Address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }

        [XmlElement(ElementName="Port")]
        public decimal Port
        {
            get
            {
                return this.portField.GetValueOrDefault();
            }
            set
            {
                this.portField = new decimal?(value);
            }
        }
    }
}

