namespace Amazon.SimpleNotificationService.Model
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sns.amazonaws.com/doc/2010-03-31/", IsNullable=false)]
    public class ListTopicsResult
    {
        private string nextTokenField;
        private List<Topic> topicsField;

        public bool IsSetNextToken()
        {
            return (this.nextTokenField != null);
        }

        public bool IsSetTopics()
        {
            return (this.Topics.Count > 0);
        }

        public override string ToString()
        {
            return this.ToXML();
        }

        public string ToXML()
        {
            StringBuilder sb = new StringBuilder(0x400);
            XmlSerializer serializer = new XmlSerializer(base.GetType());
            using (StringWriter writer = new StringWriter(sb))
            {
                serializer.Serialize((TextWriter) writer, this);
            }
            return sb.ToString();
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

        [XmlElement(ElementName="Topics")]
        public List<Topic> Topics
        {
            get
            {
                if (this.topicsField == null)
                {
                    this.topicsField = new List<Topic>();
                }
                return this.topicsField;
            }
            set
            {
                this.topicsField = value;
            }
        }
    }
}

