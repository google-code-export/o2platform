namespace Amazon.ElasticMapReduce.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticmapreduce.amazonaws.com/doc/2009-03-31", IsNullable=false)]
    public class StepDetail
    {
        private StepExecutionStatusDetail executionStatusDetailField;
        private Amazon.ElasticMapReduce.Model.StepConfig stepConfigField;

        public bool IsSetExecutionStatusDetail()
        {
            return (this.executionStatusDetailField != null);
        }

        public bool IsSetStepConfig()
        {
            return (this.stepConfigField != null);
        }

        public StepDetail WithExecutionStatusDetail(StepExecutionStatusDetail executionStatusDetail)
        {
            this.executionStatusDetailField = executionStatusDetail;
            return this;
        }

        public StepDetail WithStepConfig(Amazon.ElasticMapReduce.Model.StepConfig stepConfig)
        {
            this.stepConfigField = stepConfig;
            return this;
        }

        [XmlElement(ElementName="ExecutionStatusDetail")]
        public StepExecutionStatusDetail ExecutionStatusDetail
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

        [XmlElement(ElementName="StepConfig")]
        public Amazon.ElasticMapReduce.Model.StepConfig StepConfig
        {
            get
            {
                return this.stepConfigField;
            }
            set
            {
                this.stepConfigField = value;
            }
        }
    }
}

