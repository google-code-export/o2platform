namespace Amazon.ElasticMapReduce.Model
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticmapreduce.amazonaws.com/doc/2009-03-31", IsNullable=false)]
    public class DescribeJobFlowsResult
    {
        private List<JobFlowDetail> jobFlowsField;

        public bool IsSetJobFlows()
        {
            return (this.JobFlows.Count > 0);
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

        [XmlElement(ElementName="JobFlows")]
        public List<JobFlowDetail> JobFlows
        {
            get
            {
                if (this.jobFlowsField == null)
                {
                    this.jobFlowsField = new List<JobFlowDetail>();
                }
                return this.jobFlowsField;
            }
            set
            {
                this.jobFlowsField = value;
            }
        }
    }
}

