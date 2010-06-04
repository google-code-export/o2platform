namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeBundleTasksResult
    {
        private List<Amazon.EC2.Model.BundleTask> bundleTaskField;

        public bool IsSetBundleTask()
        {
            return (this.BundleTask.Count > 0);
        }

        public DescribeBundleTasksResult WithBundleTask(params Amazon.EC2.Model.BundleTask[] list)
        {
            foreach (Amazon.EC2.Model.BundleTask task in list)
            {
                this.BundleTask.Add(task);
            }
            return this;
        }

        [XmlElement(ElementName="BundleTask")]
        public List<Amazon.EC2.Model.BundleTask> BundleTask
        {
            get
            {
                if (this.bundleTaskField == null)
                {
                    this.bundleTaskField = new List<Amazon.EC2.Model.BundleTask>();
                }
                return this.bundleTaskField;
            }
            set
            {
                this.bundleTaskField = value;
            }
        }
    }
}

