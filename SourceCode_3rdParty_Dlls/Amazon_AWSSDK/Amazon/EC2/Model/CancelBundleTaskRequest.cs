namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class CancelBundleTaskRequest
    {
        private string bundleIdField;

        public bool IsSetBundleId()
        {
            return (this.bundleIdField != null);
        }

        public CancelBundleTaskRequest WithBundleId(string bundleId)
        {
            this.bundleIdField = bundleId;
            return this;
        }

        [XmlElement(ElementName="BundleId")]
        public string BundleId
        {
            get
            {
                return this.bundleIdField;
            }
            set
            {
                this.bundleIdField = value;
            }
        }
    }
}

