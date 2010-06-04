namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DetachVolumeResult
    {
        private Amazon.EC2.Model.Attachment attachmentField;

        public bool IsSetAttachment()
        {
            return (this.attachmentField != null);
        }

        public DetachVolumeResult WithAttachment(Amazon.EC2.Model.Attachment attachment)
        {
            this.attachmentField = attachment;
            return this;
        }

        [XmlElement(ElementName="Attachment")]
        public Amazon.EC2.Model.Attachment Attachment
        {
            get
            {
                return this.attachmentField;
            }
            set
            {
                this.attachmentField = value;
            }
        }
    }
}

