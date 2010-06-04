namespace Amazon.ElasticMapReduce.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticmapreduce.amazonaws.com/doc/2009-03-31", IsNullable=false)]
    public class RunJobFlowRequest
    {
        private string additionalInfoField;
        private List<BootstrapActionConfig> bootstrapActionsField;
        private JobFlowInstancesConfig instancesField;
        private string logUriField;
        private string nameField;
        private List<StepConfig> stepsField;

        public bool IsSetAdditionalInfo()
        {
            return (this.additionalInfoField != null);
        }

        public bool IsSetBootstrapActions()
        {
            return (this.BootstrapActions.Count > 0);
        }

        public bool IsSetInstances()
        {
            return (this.instancesField != null);
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

        public RunJobFlowRequest WithAdditionalInfo(string additionalInfo)
        {
            this.additionalInfoField = additionalInfo;
            return this;
        }

        public RunJobFlowRequest WithBootstrapActions(params BootstrapActionConfig[] list)
        {
            foreach (BootstrapActionConfig config in list)
            {
                this.BootstrapActions.Add(config);
            }
            return this;
        }

        public RunJobFlowRequest WithInstances(JobFlowInstancesConfig instances)
        {
            this.instancesField = instances;
            return this;
        }

        public RunJobFlowRequest WithLogUri(string logUri)
        {
            this.logUriField = logUri;
            return this;
        }

        public RunJobFlowRequest WithName(string name)
        {
            this.nameField = name;
            return this;
        }

        public RunJobFlowRequest WithSteps(params StepConfig[] list)
        {
            foreach (StepConfig config in list)
            {
                this.Steps.Add(config);
            }
            return this;
        }

        [XmlElement(ElementName="AdditionalInfo")]
        public string AdditionalInfo
        {
            get
            {
                return this.additionalInfoField;
            }
            set
            {
                this.additionalInfoField = value;
            }
        }

        [XmlElement(ElementName="BootstrapActions")]
        public List<BootstrapActionConfig> BootstrapActions
        {
            get
            {
                if (this.bootstrapActionsField == null)
                {
                    this.bootstrapActionsField = new List<BootstrapActionConfig>();
                }
                return this.bootstrapActionsField;
            }
            set
            {
                this.bootstrapActionsField = value;
            }
        }

        [XmlElement(ElementName="Instances")]
        public JobFlowInstancesConfig Instances
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

