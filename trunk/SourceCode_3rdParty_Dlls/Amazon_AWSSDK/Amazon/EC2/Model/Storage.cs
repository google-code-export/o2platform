namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class Storage
    {
        private S3Storage s3Field;

        public bool IsSetS3()
        {
            return (this.s3Field != null);
        }

        public Storage WithS3(S3Storage s3)
        {
            this.s3Field = s3;
            return this;
        }

        [XmlElement(ElementName="S3")]
        public S3Storage S3
        {
            get
            {
                return this.s3Field;
            }
            set
            {
                this.s3Field = value;
            }
        }
    }
}

