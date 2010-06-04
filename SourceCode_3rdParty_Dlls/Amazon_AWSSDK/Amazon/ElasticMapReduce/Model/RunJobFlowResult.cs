namespace Amazon.ElasticMapReduce.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticmapreduce.amazonaws.com/doc/2009-03-31", IsNullable=false)]
    public class RunJobFlowResult
    {
        private string jobFlowIdField;

        public bool IsSetJobFlowId()
        {
            return (this.jobFlowIdField != null);
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

        [XmlElement(ElementName="JobFlowId")]
        public string JobFlowId
        {
            get
            {
                return this.jobFlowIdField;
            }
            set
            {
                this.jobFlowIdField = value;
            }
        }
    }
}

