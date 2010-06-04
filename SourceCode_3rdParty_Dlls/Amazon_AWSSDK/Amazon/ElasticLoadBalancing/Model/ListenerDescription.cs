namespace Amazon.ElasticLoadBalancing.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticloadbalancing.amazonaws.com/doc/2009-11-25/", IsNullable=false)]
    public class ListenerDescription
    {
        private Amazon.ElasticLoadBalancing.Model.Listener listenerField;
        private List<string> policyNamesField;

        public bool IsSetListener()
        {
            return (this.listenerField != null);
        }

        public bool IsSetPolicyNames()
        {
            return (this.PolicyNames.Count > 0);
        }

        public ListenerDescription WithListener(Amazon.ElasticLoadBalancing.Model.Listener listener)
        {
            this.listenerField = listener;
            return this;
        }

        public ListenerDescription WithPolicyNames(params string[] list)
        {
            foreach (string str in list)
            {
                this.PolicyNames.Add(str);
            }
            return this;
        }

        [XmlElement(ElementName="Listener")]
        public Amazon.ElasticLoadBalancing.Model.Listener Listener
        {
            get
            {
                return this.listenerField;
            }
            set
            {
                this.listenerField = value;
            }
        }

        [XmlElement(ElementName="PolicyNames")]
        public List<string> PolicyNames
        {
            get
            {
                if (this.policyNamesField == null)
                {
                    this.policyNamesField = new List<string>();
                }
                return this.policyNamesField;
            }
            set
            {
                this.policyNamesField = value;
            }
        }
    }
}

