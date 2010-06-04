namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeImagesRequest
    {
        private List<string> executableByField;
        private List<string> imageIdField;
        private List<string> ownerField;

        public bool IsSetExecutableBy()
        {
            return (this.ExecutableBy.Count > 0);
        }

        public bool IsSetImageId()
        {
            return (this.ImageId.Count > 0);
        }

        public bool IsSetOwner()
        {
            return (this.Owner.Count > 0);
        }

        public DescribeImagesRequest WithExecutableBy(params string[] list)
        {
            foreach (string str in list)
            {
                this.ExecutableBy.Add(str);
            }
            return this;
        }

        public DescribeImagesRequest WithImageId(params string[] list)
        {
            foreach (string str in list)
            {
                this.ImageId.Add(str);
            }
            return this;
        }

        public DescribeImagesRequest WithOwner(params string[] list)
        {
            foreach (string str in list)
            {
                this.Owner.Add(str);
            }
            return this;
        }

        [XmlElement(ElementName="ExecutableBy")]
        public List<string> ExecutableBy
        {
            get
            {
                if (this.executableByField == null)
                {
                    this.executableByField = new List<string>();
                }
                return this.executableByField;
            }
            set
            {
                this.executableByField = value;
            }
        }

        [XmlElement(ElementName="ImageId")]
        public List<string> ImageId
        {
            get
            {
                if (this.imageIdField == null)
                {
                    this.imageIdField = new List<string>();
                }
                return this.imageIdField;
            }
            set
            {
                this.imageIdField = value;
            }
        }

        [XmlElement(ElementName="Owner")]
        public List<string> Owner
        {
            get
            {
                if (this.ownerField == null)
                {
                    this.ownerField = new List<string>();
                }
                return this.ownerField;
            }
            set
            {
                this.ownerField = value;
            }
        }
    }
}

