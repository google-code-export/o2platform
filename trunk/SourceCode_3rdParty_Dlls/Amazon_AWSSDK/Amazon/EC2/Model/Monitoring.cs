namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class Monitoring
    {
        private string monitoringStateField;

        public bool IsSetMonitoringState()
        {
            return (this.monitoringStateField != null);
        }

        public Monitoring WithMonitoringState(string monitoringState)
        {
            this.monitoringStateField = monitoringState;
            return this;
        }

        [XmlElement(ElementName="MonitoringState")]
        public string MonitoringState
        {
            get
            {
                return this.monitoringStateField;
            }
            set
            {
                this.monitoringStateField = value;
            }
        }
    }
}

