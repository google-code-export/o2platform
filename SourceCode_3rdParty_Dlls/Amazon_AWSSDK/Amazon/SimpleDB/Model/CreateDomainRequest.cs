namespace Amazon.SimpleDB.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sdb.amazonaws.com/doc/2009-04-15/", IsNullable=false)]
    public class CreateDomainRequest
    {
        private string domainNameField;

        public bool IsSetDomainName()
        {
            return (this.domainNameField != null);
        }

        public CreateDomainRequest WithDomainName(string domainName)
        {
            this.domainNameField = domainName;
            return this;
        }

        [XmlElement(ElementName="DomainName")]
        public string DomainName
        {
            get
            {
                return this.domainNameField;
            }
            set
            {
                this.domainNameField = value;
            }
        }
    }
}

