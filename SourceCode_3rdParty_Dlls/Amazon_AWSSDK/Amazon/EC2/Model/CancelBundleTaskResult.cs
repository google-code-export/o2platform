namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class CancelBundleTaskResult
    {
        private Amazon.EC2.Model.BundleTask bundleTaskField;

        public bool IsSetBundleTask()
        {
            return (this.bundleTaskField != null);
        }

        public CancelBundleTaskResult WithBundleTask(Amazon.EC2.Model.BundleTask bundleTask)
        {
            this.bundleTaskField = bundleTask;
            return this;
        }

        [XmlElement(ElementName="BundleTask")]
        public Amazon.EC2.Model.BundleTask BundleTask
        {
            get
            {
                return this.bundleTaskField;
            }
            set
            {
                this.bundleTaskField = value;
            }
        }
    }
}

