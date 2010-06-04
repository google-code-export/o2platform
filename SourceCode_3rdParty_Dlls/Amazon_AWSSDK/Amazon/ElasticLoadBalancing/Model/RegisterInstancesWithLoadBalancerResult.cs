namespace Amazon.ElasticLoadBalancing.Model
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticloadbalancing.amazonaws.com/doc/2009-11-25/", IsNullable=false)]
    public class RegisterInstancesWithLoadBalancerResult
    {
        private List<Instance> instancesField;

        public bool IsSetInstances()
        {
            return (this.Instances.Count > 0);
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

        [XmlElement(ElementName="Instances")]
        public List<Instance> Instances
        {
            get
            {
                if (this.instancesField == null)
                {
                    this.instancesField = new List<Instance>();
                }
                return this.instancesField;
            }
            set
            {
                this.instancesField = value;
            }
        }
    }
}

