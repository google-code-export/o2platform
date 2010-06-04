namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class InstanceMonitoring
    {
        private string instanceIdField;
        private Amazon.EC2.Model.Monitoring monitoringField;

        public bool IsSetInstanceId()
        {
            return (this.instanceIdField != null);
        }

        public bool IsSetMonitoring()
        {
            return (this.monitoringField != null);
        }

        public InstanceMonitoring WithInstanceId(string instanceId)
        {
            this.instanceIdField = instanceId;
            return this;
        }

        public InstanceMonitoring WithMonitoring(Amazon.EC2.Model.Monitoring monitoring)
        {
            this.monitoringField = monitoring;
            return this;
        }

        [XmlElement(ElementName="InstanceId")]
        public string InstanceId
        {
            get
            {
                return this.instanceIdField;
            }
            set
            {
                this.instanceIdField = value;
            }
        }

        [XmlElement(ElementName="Monitoring")]
        public Amazon.EC2.Model.Monitoring Monitoring
        {
            get
            {
                return this.monitoringField;
            }
            set
            {
                this.monitoringField = value;
            }
        }
    }
}

