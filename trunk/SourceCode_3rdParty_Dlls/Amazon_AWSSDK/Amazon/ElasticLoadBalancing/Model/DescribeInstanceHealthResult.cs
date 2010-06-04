namespace Amazon.ElasticLoadBalancing.Model
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticloadbalancing.amazonaws.com/doc/2009-11-25/", IsNullable=false)]
    public class DescribeInstanceHealthResult
    {
        private List<InstanceState> instanceStatesField;

        public bool IsSetInstanceStates()
        {
            return (this.InstanceStates.Count > 0);
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

        [XmlElement(ElementName="InstanceStates")]
        public List<InstanceState> InstanceStates
        {
            get
            {
                if (this.instanceStatesField == null)
                {
                    this.instanceStatesField = new List<InstanceState>();
                }
                return this.instanceStatesField;
            }
            set
            {
                this.instanceStatesField = value;
            }
        }
    }
}

