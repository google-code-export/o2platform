namespace Amazon.ElasticMapReduce.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticmapreduce.amazonaws.com/doc/2009-03-31", IsNullable=false)]
    public class DescribeJobFlowsRequest
    {
        private string createdAfterField;
        private string createdBeforeField;
        private List<string> jobFlowIdsField;
        private List<string> jobFlowStatesField;

        public bool IsSetCreatedAfter()
        {
            return (this.createdAfterField != null);
        }

        public bool IsSetCreatedBefore()
        {
            return (this.createdBeforeField != null);
        }

        public bool IsSetJobFlowIds()
        {
            return (this.JobFlowIds.Count > 0);
        }

        public bool IsSetJobFlowStates()
        {
            return (this.JobFlowStates.Count > 0);
        }

        public DescribeJobFlowsRequest WithCreatedAfter(string createdAfter)
        {
            this.createdAfterField = createdAfter;
            return this;
        }

        public DescribeJobFlowsRequest WithCreatedBefore(string createdBefore)
        {
            this.createdBeforeField = createdBefore;
            return this;
        }

        public DescribeJobFlowsRequest WithJobFlowIds(params string[] list)
        {
            foreach (string str in list)
            {
                this.JobFlowIds.Add(str);
            }
            return this;
        }

        public DescribeJobFlowsRequest WithJobFlowStates(params string[] list)
        {
            foreach (string str in list)
            {
                this.JobFlowStates.Add(str);
            }
            return this;
        }

        [XmlElement(ElementName="CreatedAfter")]
        public string CreatedAfter
        {
            get
            {
                return this.createdAfterField;
            }
            set
            {
                this.createdAfterField = value;
            }
        }

        [XmlElement(ElementName="CreatedBefore")]
        public string CreatedBefore
        {
            get
            {
                return this.createdBeforeField;
            }
            set
            {
                this.createdBeforeField = value;
            }
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

        [XmlElement(ElementName="JobFlowStates")]
        public List<string> JobFlowStates
        {
            get
            {
                if (this.jobFlowStatesField == null)
                {
                    this.jobFlowStatesField = new List<string>();
                }
                return this.jobFlowStatesField;
            }
            set
            {
                this.jobFlowStatesField = value;
            }
        }
    }
}

