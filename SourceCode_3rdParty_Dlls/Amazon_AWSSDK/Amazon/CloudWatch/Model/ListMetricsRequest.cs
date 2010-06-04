namespace Amazon.CloudWatch.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://monitoring.amazonaws.com/doc/2009-05-15/", IsNullable=false)]
    public class ListMetricsRequest
    {
        private string nextTokenField;

        public bool IsSetNextToken()
        {
            return (this.nextTokenField != null);
        }

        public ListMetricsRequest WithNextToken(string nextToken)
        {
            this.nextTokenField = nextToken;
            return this;
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

