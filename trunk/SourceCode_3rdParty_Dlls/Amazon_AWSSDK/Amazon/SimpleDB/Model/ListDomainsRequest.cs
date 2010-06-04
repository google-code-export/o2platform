namespace Amazon.SimpleDB.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sdb.amazonaws.com/doc/2009-04-15/", IsNullable=false)]
    public class ListDomainsRequest
    {
        private decimal? maxNumberOfDomainsField;
        private string nextTokenField;

        public bool IsSetMaxNumberOfDomains()
        {
            return this.maxNumberOfDomainsField.HasValue;
        }

        public bool IsSetNextToken()
        {
            return (this.nextTokenField != null);
        }

        public ListDomainsRequest WithMaxNumberOfDomains(decimal maxNumberOfDomains)
        {
            this.maxNumberOfDomainsField = new decimal?(maxNumberOfDomains);
            return this;
        }

        public ListDomainsRequest WithNextToken(string nextToken)
        {
            this.nextTokenField = nextToken;
            return this;
        }

        [XmlElement(ElementName="MaxNumberOfDomains")]
        public decimal MaxNumberOfDomains
        {
            get
            {
                return this.maxNumberOfDomainsField.GetValueOrDefault();
            }
            set
            {
                this.maxNumberOfDomainsField = new decimal?(value);
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

