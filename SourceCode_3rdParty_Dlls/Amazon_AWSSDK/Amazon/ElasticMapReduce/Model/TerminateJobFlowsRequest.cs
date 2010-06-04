namespace Amazon.ElasticMapReduce.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticmapreduce.amazonaws.com/doc/2009-03-31", IsNullable=false)]
    public class TerminateJobFlowsRequest
    {
        private List<string> jobFlowIdsField;

        public bool IsSetJobFlowIds()
        {
            return (this.JobFlowIds.Count > 0);
        }

        public TerminateJobFlowsRequest WithJobFlowIds(params string[] list)
        {
            foreach (string str in list)
            {
                this.JobFlowIds.Add(str);
            }
            return this;
        }

        [XmlElement(ElementName="JobFlowIds")]
        public List<string> JobFlowIds
        {
            get
            {
                if (this.jobFlowIdsField == null)
                {
                    this.jobFlowIdsField = new List<string>();
                }
                return this.jobFlowIdsField;
            }
            set
            {
                this.jobFlowIdsField = value;
            }
        }
    }
}

