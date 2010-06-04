namespace Amazon.ElasticMapReduce.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticmapreduce.amazonaws.com/doc/2009-03-31", IsNullable=false)]
    public class AddJobFlowStepsRequest
    {
        private string jobFlowIdField;
        private List<StepConfig> stepsField;

        public bool IsSetJobFlowId()
        {
            return (this.jobFlowIdField != null);
        }

        public bool IsSetSteps()
        {
            return (this.Steps.Count > 0);
        }

        public AddJobFlowStepsRequest WithJobFlowId(string jobFlowId)
        {
            this.jobFlowIdField = jobFlowId;
            return this;
        }

        public AddJobFlowStepsRequest WithSteps(params StepConfig[] list)
        {
            foreach (StepConfig config in list)
            {
                this.Steps.Add(config);
            }
            return this;
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

        [XmlElement(ElementName="Steps")]
        public List<StepConfig> Steps
        {
            get
            {
                if (this.stepsField == null)
                {
                    this.stepsField = new List<StepConfig>();
                }
                return this.stepsField;
            }
            set
            {
                this.stepsField = value;
            }
        }
    }
}

