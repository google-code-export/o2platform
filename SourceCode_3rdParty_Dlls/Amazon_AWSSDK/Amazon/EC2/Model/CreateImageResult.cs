namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class CreateImageResult
    {
        private string imageIdField;

        public bool IsSetImageId()
        {
            return (this.imageIdField != null);
        }

        public CreateImageResult WithImageId(string imageId)
        {
            this.imageIdField = imageId;
            return this;
        }

        [XmlElement(ElementName="ImageId")]
        public string ImageId
        {
            get
            {
                return this.imageIdField;
            }
            set
            {
                this.imageIdField = value;
            }
        }
    }
}

