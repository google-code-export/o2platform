namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeImagesResult
    {
        private List<Amazon.EC2.Model.Image> imageField;

        public bool IsSetImage()
        {
            return (this.Image.Count > 0);
        }

        public DescribeImagesResult WithImage(params Amazon.EC2.Model.Image[] list)
        {
            foreach (Amazon.EC2.Model.Image image in list)
            {
                this.Image.Add(image);
            }
            return this;
        }

        [XmlElement(ElementName="Image")]
        public List<Amazon.EC2.Model.Image> Image
        {
            get
            {
                if (this.imageField == null)
                {
                    this.imageField = new List<Amazon.EC2.Model.Image>();
                }
                return this.imageField;
            }
            set
            {
                this.imageField = value;
            }
        }
    }
}

