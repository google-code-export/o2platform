namespace Amazon.ElasticMapReduce.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticmapreduce.amazonaws.com/doc/2009-03-31", IsNullable=false)]
    public class JobFlowDetail
    {
        private List<BootstrapActionDetail> bootstrapActionsField;
        private JobFlowExecutionStatusDetail executionStatusDetailField;
        private JobFlowInstancesDetail instancesField;
        private string jobFlowIdField;
        private string logUriField;
        private string nameField;
        private List<StepDetail> stepsField;

        public bool IsSetBootstrapActions()
        {
            return (this.BootstrapActions.Count > 0);
        }

        public bool IsSetExecutionStatusDetail()
        {
            return (this.executionStatusDetailField != null);
        }

        public bool IsSetInstances()
        {
            return (this.instancesField != null);
        }

        public bool IsSetJobFlowId()
        {
            return (this.jobFlowIdField != null);
        }

        public bool IsSetLogUri()
        {
            return (this.logUriField != null);
        }

        public bool IsSetName()
        {
            return (this.nameField != null);
        }

        public bool IsSetSteps()
        {
            return (this.Steps.Count > 0);
        }

        public JobFlowDetail WithBootstrapActions(params BootstrapActionDetail[] list)
        {
            foreach (BootstrapActionDetail detail in list)
            {
                this.BootstrapActions.Add(detail);
            }
            return this;
        }

        public JobFlowDetail WithExecutionStatusDetail(JobFlowExecutionStatusDetail executionStatusDetail)
        {
            this.executionStatusDetailField = executionStatusDetail;
            return this;
        }

        public JobFlowDetail WithInstances(JobFlowInstancesDetail instances)
        {
            this.instancesField = instances;
            return this;
        }

        public JobFlowDetail WithJobFlowId(string jobFlowId)
        {
            this.jobFlowIdField = jobFlowId;
            return this;
        }

        public JobFlowDetail WithLogUri(string logUri)
        {
            this.logUriField = logUri;
            return this;
        }

        public JobFlowDetail WithName(string name)
        {
            this.nameField = name;
            return this;
        }

        public JobFlowDetail WithSteps(params StepDetail[] list)
        {
            foreach (StepDetail detail in list)
            {
                this.Steps.Add(detail);
            }
            return this;
        }

        [XmlElement(ElementName="BootstrapActions")]
        public List<BootstrapActionDetail> BootstrapActions
        {
            get
            {
                if (this.bootstrapActionsField == null)
                {
                    this.bootstrapActionsField = new List<BootstrapActionDetail>();
                }
                return this.bootstrapActionsField;
            }
            set
            {
                this.bootstrapActionsField = value;
            }
        }

        [XmlElement(ElementName="ExecutionStatusDetail")]
        public JobFlowExecutionStatusDetail ExecutionStatusDetail
        {
            get
            {
                return this.executionStatusDetailField;
            }
            set
            {
                this.executionStatusDetailField = value;
            }
        }

        [XmlElement(ElementName="Instances")]
        public JobFlowInstancesDetail Instances
        {
            get
            {
                return this.instancesField;
            }
            set
            {
                this.instancesField = value;
            }
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

        [XmlElement(ElementName="LogUri")]
        public string LogUri
        {
            get
            {
                return this.logUriField;
            }
            set
            {
                this.logUriField = value;
            }
        }

        [XmlElement(ElementName="Name")]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        [XmlElement(ElementName="Steps")]
        public List<StepDetail> Steps
        {
            get
            {
                if (this.stepsField == null)
                {
                    this.stepsField = new List<StepDetail>();
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

