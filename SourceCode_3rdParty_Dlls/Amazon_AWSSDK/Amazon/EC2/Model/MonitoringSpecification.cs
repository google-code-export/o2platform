namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class MonitoringSpecification
    {
        private bool? enabledField;

        public bool IsSetEnabled()
        {
            return this.enabledField.HasValue;
        }

        public MonitoringSpecification WithEnabled(bool enabled)
        {
            this.enabledField = new bool?(enabled);
            return this;
        }

        [XmlElement(ElementName="Enabled")]
        public bool Enabled
        {
            get
            {
                return this.enabledField.GetValueOrDefault();
            }
            set
            {
                this.enabledField = new bool?(value);
            }
        }
    }
}

