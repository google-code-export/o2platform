namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class S3Storage
    {
        private string AWSAccessKeyIdField;
        private string bucketField;
        private string prefixField;
        private string uploadPolicyField;
        private string uploadPolicySignatureField;

        public bool IsSetAWSAccessKeyId()
        {
            return (this.AWSAccessKeyIdField != null);
        }

        public bool IsSetBucket()
        {
            return (this.bucketField != null);
        }

        public bool IsSetPrefix()
        {
            return (this.prefixField != null);
        }

        public bool IsSetUploadPolicy()
        {
            return (this.uploadPolicyField != null);
        }

        public bool IsSetUploadPolicySignature()
        {
            return (this.uploadPolicySignatureField != null);
        }

        public S3Storage WithAWSAccessKeyId(string AWSAccessKeyId)
        {
            this.AWSAccessKeyIdField = AWSAccessKeyId;
            return this;
        }

        public S3Storage WithBucket(string bucket)
        {
            this.bucketField = bucket;
            return this;
        }

        public S3Storage WithPrefix(string prefix)
        {
            this.prefixField = prefix;
            return this;
        }

        public S3Storage WithUploadPolicy(string uploadPolicy)
        {
            this.uploadPolicyField = uploadPolicy;
            return this;
        }

        public S3Storage WithUploadPolicySignature(string uploadPolicySignature)
        {
            this.uploadPolicySignatureField = uploadPolicySignature;
            return this;
        }

        [XmlElement(ElementName="AWSAccessKeyId")]
        public string AWSAccessKeyId
        {
            get
            {
                return this.AWSAccessKeyIdField;
            }
            set
            {
                this.AWSAccessKeyIdField = value;
            }
        }

        [XmlElement(ElementName="Bucket")]
        public string Bucket
        {
            get
            {
                return this.bucketField;
            }
            set
            {
                this.bucketField = value;
            }
        }

        [XmlElement(ElementName="Prefix")]
        public string Prefix
        {
            get
            {
                return this.prefixField;
            }
            set
            {
                this.prefixField = value;
            }
        }

        [XmlElement(ElementName="UploadPolicy")]
        public string UploadPolicy
        {
            get
            {
                return this.uploadPolicyField;
            }
            set
            {
                this.uploadPolicyField = value;
            }
        }

        [XmlElement(ElementName="UploadPolicySignature")]
        public string UploadPolicySignature
        {
            get
            {
                return this.uploadPolicySignatureField;
            }
            set
            {
                this.uploadPolicySignatureField = value;
            }
        }
    }
}

