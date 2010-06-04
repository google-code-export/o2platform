namespace Amazon.ElasticMapReduce.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticmapreduce.amazonaws.com/doc/2009-03-31", IsNullable=false)]
    public class StepConfig
    {
        private string actionOnFailureField;
        private HadoopJarStepConfig hadoopJarStepField;
        private string nameField;

        public bool IsSetActionOnFailure()
        {
            return (this.actionOnFailureField != null);
        }

        public bool IsSetHadoopJarStep()
        {
            return (this.hadoopJarStepField != null);
        }

        public bool IsSetName()
        {
            return (this.nameField != null);
        }

        public StepConfig WithActionOnFailure(string actionOnFailure)
        {
            this.actionOnFailureField = actionOnFailure;
            return this;
        }

        public StepConfig WithHadoopJarStep(HadoopJarStepConfig hadoopJarStep)
        {
            this.hadoopJarStepField = hadoopJarStep;
            return this;
        }

        public StepConfig WithName(string name)
        {
            this.nameField = name;
            return this;
        }

        [XmlElement(ElementName="ActionOnFailure")]
        public string ActionOnFailure
        {
            get
            {
                return this.actionOnFailureField;
            }
            set
            {
                this.actionOnFailureField = value;
            }
        }

        [XmlElement(ElementName="HadoopJarStep")]
        public HadoopJarStepConfig HadoopJarStep
        {
            get
            {
                return this.hadoopJarStepField;
            }
            set
            {
                this.hadoopJarStepField = value;
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
    }
}

