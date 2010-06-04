namespace Amazon.SimpleDB.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sdb.amazonaws.com/doc/2009-04-15/", IsNullable=false)]
    public class ListDomainsResult
    {
        private List<string> domainNameField;
        private string nextTokenField;

        public bool IsSetDomainName()
        {
            return (this.DomainName.Count > 0);
        }

        public bool IsSetNextToken()
        {
            return (this.nextTokenField != null);
        }

        public ListDomainsResult WithDomainName(params string[] list)
        {
            foreach (string str in list)
            {
                this.DomainName.Add(str);
            }
            return this;
        }

        public ListDomainsResult WithNextToken(string nextToken)
        {
            this.nextTokenField = nextToken;
            return this;
        }

        [XmlElement(ElementName="DomainName")]
        public List<string> DomainName
        {
            get
            {
                if (this.domainNameField == null)
                {
                    this.domainNameField = new List<string>();
                }
                return this.domainNameField;
            }
            set
            {
                this.domainNameField = value;
            }
        }

        [XmlElement(ElementName="NextToken")]
        public string NextToken
        {
            get
            {
                return this.nextTokenField;
            }
            set
            {
                this.nextTokenField = value;
            }
        }
    }
}

