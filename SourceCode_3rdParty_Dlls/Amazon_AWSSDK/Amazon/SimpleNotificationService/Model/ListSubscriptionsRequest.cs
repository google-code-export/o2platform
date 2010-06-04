namespace Amazon.SimpleNotificationService.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sns.amazonaws.com/doc/2010-03-31/", IsNullable=false)]
    public class ListSubscriptionsRequest
    {
        private string nextTokenField;

        public bool IsSetNextToken()
        {
            return (this.nextTokenField != null);
        }

        public ListSubscriptionsRequest WithNextToken(string nextToken)
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

