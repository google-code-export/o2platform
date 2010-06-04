namespace Amazon.ElasticMapReduce.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticmapreduce.amazonaws.com/doc/2009-03-31", IsNullable=false)]
    public class JobFlowInstancesDetail
    {
        private string ec2KeyNameField;
        private string hadoopVersionField;
        private decimal? instanceCountField;
        private bool? keepJobFlowAliveWhenNoStepsField;
        private string masterInstanceIdField;
        private string masterInstanceTypeField;
        private string masterPublicDnsNameField;
        private PlacementType placementField;
        private string slaveInstanceTypeField;

        public bool IsSetEc2KeyName()
        {
            return (this.ec2KeyNameField != null);
        }

        public bool IsSetHadoopVersion()
        {
            return (this.hadoopVersionField != null);
        }

        public bool IsSetInstanceCount()
        {
            return this.instanceCountField.HasValue;
        }

        public bool IsSetKeepJobFlowAliveWhenNoSteps()
        {
            return this.keepJobFlowAliveWhenNoStepsField.HasValue;
        }

        public bool IsSetMasterInstanceId()
        {
            return (this.masterInstanceIdField != null);
        }

        public bool IsSetMasterInstanceType()
        {
            return (this.masterInstanceTypeField != null);
        }

        public bool IsSetMasterPublicDnsName()
        {
            return (this.masterPublicDnsNameField != null);
        }

        public bool IsSetPlacement()
        {
            return (this.placementField != null);
        }

        public bool IsSetSlaveInstanceType()
        {
            return (this.slaveInstanceTypeField != null);
        }

        public JobFlowInstancesDetail WithEc2KeyName(string ec2KeyName)
        {
            this.ec2KeyNameField = ec2KeyName;
            return this;
        }

        public JobFlowInstancesDetail WithHadoopVersion(string hadoopVersion)
        {
            this.hadoopVersionField = hadoopVersion;
            return this;
        }

        public JobFlowInstancesDetail WithInstanceCount(decimal instanceCount)
        {
            this.instanceCountField = new decimal?(instanceCount);
            return this;
        }

        public JobFlowInstancesDetail WithKeepJobFlowAliveWhenNoSteps(bool keepJobFlowAliveWhenNoSteps)
        {
            this.keepJobFlowAliveWhenNoStepsField = new bool?(keepJobFlowAliveWhenNoSteps);
            return this;
        }

        public JobFlowInstancesDetail WithMasterInstanceId(string masterInstanceId)
        {
            this.masterInstanceIdField = masterInstanceId;
            return this;
        }

        public JobFlowInstancesDetail WithMasterInstanceType(string masterInstanceType)
        {
            this.masterInstanceTypeField = masterInstanceType;
            return this;
        }

        public JobFlowInstancesDetail WithMasterPublicDnsName(string masterPublicDnsName)
        {
            this.masterPublicDnsNameField = masterPublicDnsName;
            return this;
        }

        public JobFlowInstancesDetail WithPlacement(PlacementType placement)
        {
            this.placementField = placement;
            return this;
        }

        public JobFlowInstancesDetail WithSlaveInstanceType(string slaveInstanceType)
        {
            this.slaveInstanceTypeField = slaveInstanceType;
            return this;
        }

        [XmlElement(ElementName="Ec2KeyName")]
        public string Ec2KeyName
        {
            get
            {
                return this.ec2KeyNameField;
            }
            set
            {
                this.ec2KeyNameField = value;
            }
        }

        [XmlElement(ElementName="HadoopVersion")]
        public string HadoopVersion
        {
            get
            {
                return this.hadoopVersionField;
            }
            set
            {
                this.hadoopVersionField = value;
            }
        }

        [XmlElement(ElementName="InstanceCount")]
        public decimal InstanceCount
        {
            get
            {
                return this.instanceCountField.GetValueOrDefault();
            }
            set
            {
                this.instanceCountField = new decimal?(value);
            }
        }

        [XmlElement(ElementName="KeepJobFlowAliveWhenNoSteps")]
        public bool KeepJobFlowAliveWhenNoSteps
        {
            get
            {
                return this.keepJobFlowAliveWhenNoStepsField.GetValueOrDefault();
            }
            set
            {
                this.keepJobFlowAliveWhenNoStepsField = new bool?(value);
            }
        }

        [XmlElement(ElementName="MasterInstanceId")]
        public string MasterInstanceId
        {
            get
            {
                return this.masterInstanceIdField;
            }
            set
            {
                this.masterInstanceIdField = value;
            }
        }

        [XmlElement(ElementName="MasterInstanceType")]
        public string MasterInstanceType
        {
            get
            {
                return this.masterInstanceTypeField;
            }
            set
            {
                this.masterInstanceTypeField = value;
            }
        }

        [XmlElement(ElementName="MasterPublicDnsName")]
        public string MasterPublicDnsName
        {
            get
            {
                return this.masterPublicDnsNameField;
            }
            set
            {
                this.masterPublicDnsNameField = value;
            }
        }

        [XmlElement(ElementName="Placement")]
        public PlacementType Placement
        {
            get
            {
                return this.placementField;
            }
            set
            {
                this.placementField = value;
            }
        }

        [XmlElement(ElementName="SlaveInstanceType")]
        public string SlaveInstanceType
        {
            get
            {
                return this.slaveInstanceTypeField;
            }
            set
            {
                this.slaveInstanceTypeField = value;
            }
        }
    }
}

