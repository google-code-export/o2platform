namespace Amazon.ElasticLoadBalancing.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticloadbalancing.amazonaws.com/doc/2009-11-25/", IsNullable=false)]
    public class CreateLoadBalancerResult
    {
        private string DNSNameField;

        public bool IsSetDNSName()
        {
            return (this.DNSNameField != null);
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

        [XmlElement(ElementName="DNSName")]
        public string DNSName
        {
            get
            {
                return this.DNSNameField;
            }
            set
            {
                this.DNSNameField = value;
            }
        }
    }
}

