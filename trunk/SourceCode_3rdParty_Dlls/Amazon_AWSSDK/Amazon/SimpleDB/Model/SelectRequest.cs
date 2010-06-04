namespace Amazon.SimpleDB.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sdb.amazonaws.com/doc/2009-04-15/", IsNullable=false)]
    public class SelectRequest
    {
        private bool? consistentReadField;
        private string nextTokenField;
        private string selectExpressionField;

        public bool IsSetConsistentRead()
        {
            return this.consistentReadField.HasValue;
        }

        public bool IsSetNextToken()
        {
            return (this.nextTokenField != null);
        }

        public bool IsSetSelectExpression()
        {
            return (this.selectExpressionField != null);
        }

        public SelectRequest WithConsistentRead(bool consistentRead)
        {
            this.consistentReadField = new bool?(consistentRead);
            return this;
        }

        public SelectRequest WithNextToken(string nextToken)
        {
            this.nextTokenField = nextToken;
            return this;
        }

        public SelectRequest WithSelectExpression(string selectExpression)
        {
            this.selectExpressionField = selectExpression;
            return this;
        }

        [XmlElement(ElementName="ConsistentRead")]
        public bool ConsistentRead
        {
            get
            {
                return this.consistentReadField.GetValueOrDefault();
            }
            set
            {
                this.consistentReadField = new bool?(value);
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

        [XmlElement(ElementName="SelectExpression")]
        public string SelectExpression
        {
            get
            {
                return this.selectExpressionField;
            }
            set
            {
                this.selectExpressionField = value;
            }
        }
    }
}

