namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeBundleTasksRequest
    {
        private List<string> bundleIdField;

        public bool IsSetBundleId()
        {
            return (this.BundleId.Count > 0);
        }

        public DescribeBundleTasksRequest WithBundleId(params string[] list)
        {
            foreach (string str in list)
            {
                this.BundleId.Add(str);
            }
            return this;
        }

        [XmlElement(ElementName="BundleId")]
        public List<string> BundleId
        {
            get
            {
                if (this.bundleIdField == null)
                {
                    this.bundleIdField = new List<string>();
                }
                return this.bundleIdField;
            }
            set
            {
                this.bundleIdField = value;
            }
        }
    }
}

