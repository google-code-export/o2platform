namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class UnmonitorInstancesResult
    {
        private List<Amazon.EC2.Model.InstanceMonitoring> instanceMonitoringField;

        public bool IsSetInstanceMonitoring()
        {
            return (this.InstanceMonitoring.Count > 0);
        }

        public UnmonitorInstancesResult WithInstanceMonitoring(params Amazon.EC2.Model.InstanceMonitoring[] list)
        {
            foreach (Amazon.EC2.Model.InstanceMonitoring monitoring in list)
            {
                this.InstanceMonitoring.Add(monitoring);
            }
            return this;
        }

        [XmlElement(ElementName="InstanceMonitoring")]
        public List<Amazon.EC2.Model.InstanceMonitoring> InstanceMonitoring
        {
            get
            {
                if (this.instanceMonitoringField == null)
                {
                    this.instanceMonitoringField = new List<Amazon.EC2.Model.InstanceMonitoring>();
                }
                return this.instanceMonitoringField;
            }
            set
            {
                this.instanceMonitoringField = value;
            }
        }
    }
}

