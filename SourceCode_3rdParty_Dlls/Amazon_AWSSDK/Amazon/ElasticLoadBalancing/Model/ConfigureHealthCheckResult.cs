namespace Amazon.ElasticLoadBalancing.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticloadbalancing.amazonaws.com/doc/2009-11-25/", IsNullable=false)]
    public class ConfigureHealthCheckResult
    {
        private Amazon.ElasticLoadBalancing.Model.HealthCheck healthCheckField;

        public bool IsSetHealthCheck()
        {
            return (this.healthCheckField != null);
        }

        public override string ToString()
        {
            return this.ToXML();
        }

        public string ToXML()
        {
            StringBuilder sb = new StringBuilder(0x400);
            XmlSerializer serializer = new XmlSerializer(base.GetType());
            using (StringWriter writer = new StringWriter(sb))
            {
                serializer.Serialize((TextWriter) writer, this);
            }
            return sb.ToString();
        }

        [XmlElement(ElementName="HealthCheck")]
        public Amazon.ElasticLoadBalancing.Model.HealthCheck HealthCheck
        {
            get
            {
                return this.healthCheckField;
            }
            set
            {
                this.healthCheckField = value;
            }
        }
    }
}

