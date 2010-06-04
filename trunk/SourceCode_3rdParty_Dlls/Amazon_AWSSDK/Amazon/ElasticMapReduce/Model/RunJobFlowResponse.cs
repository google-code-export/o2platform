namespace Amazon.ElasticMapReduce.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticmapreduce.amazonaws.com/doc/2009-03-31", IsNullable=false)]
    public class RunJobFlowResponse
    {
        private Amazon.ElasticMapReduce.Model.ResponseMetadata responseMetadataField;
        private Amazon.ElasticMapReduce.Model.RunJobFlowResult runJobFlowResultField;

        public bool IsSetResponseMetadata()
        {
            return (this.responseMetadataField != null);
        }

        public bool IsSetRunJobFlowResult()
        {
            return (this.runJobFlowResultField != null);
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

        [XmlElement(ElementName="ResponseMetadata")]
        public Amazon.ElasticMapReduce.Model.ResponseMetadata ResponseMetadata
        {
            get
            {
                return this.responseMetadataField;
            }
            set
            {
                this.responseMetadataField = value;
            }
        }

        [XmlElement(ElementName="RunJobFlowResult")]
        public Amazon.ElasticMapReduce.Model.RunJobFlowResult RunJobFlowResult
        {
            get
            {
                return this.runJobFlowResultField;
            }
            set
            {
                this.runJobFlowResultField = value;
            }
        }
    }
}

