namespace Amazon.S3.Model
{
    using System;
    using System.Xml.Serialization;

    public class SetACLRequest : S3Request
    {
        private S3AccessControlList acl;
        private string bucketName;
        private S3CannedACL cannedACL;
        private string key;
        private string versionId;

        internal bool IsSetACL()
        {
            return (this.acl != null);
        }

        internal bool IsSetBucketName()
        {
            return !string.IsNullOrEmpty(this.bucketName);
        }

        internal bool IsSetCannedACL()
        {
            return ((this.cannedACL > S3CannedACL.NoACL) && (this.cannedACL <= S3CannedACL.BucketOwnerFullControl));
        }

        internal bool IsSetKey()
        {
            return !string.IsNullOrEmpty(this.key);
        }

        internal bool IsSetVersionId()
        {
            return !string.IsNullOrEmpty(this.versionId);
        }

        public void RemoveCannedACL()
        {
            this.cannedACL = S3CannedACL.NoACL;
        }

        public SetACLRequest WithACL(S3AccessControlList acl)
        {
            this.acl = acl;
            return this;
        }

        public SetACLRequest WithBucketName(string bucketName)
        {
            this.bucketName = bucketName;
            return this;
        }

        public SetACLRequest WithCannedACL(S3CannedACL acl)
        {
            this.cannedACL = acl;
            return this;
        }

        public SetACLRequest WithKey(string key)
        {
            this.key = key;
            return this;
        }

        public SetACLRequest WithVersionId(string versionId)
        {
            this.versionId = versionId;
            return this;
        }

        [XmlElement(ElementName="ACL")]
        public S3AccessControlList ACL
        {
            get
            {
                return this.acl;
            }
            set
            {
                this.acl = value;
            }
        }

        [XmlElement(ElementName="BucketName")]
        public string BucketName
        {
            get
            {
                return this.bucketName;
            }
            set
            {
                this.bucketName = value;
            }
        }

        public S3CannedACL CannedACL
        {
            get
            {
                return this.cannedACL;
            }
            set
            {
                this.cannedACL = value;
            }
        }

        [XmlElement(ElementName="Key")]
        public string Key
        {
            get
            {
                return this.key;
            }
            set
            {
                this.key = value;
            }
        }

        [XmlElement(ElementName="VersionId")]
        public string VersionId
        {
            get
            {
                return this.versionId;
            }
            set
            {
                this.versionId = value;
            }
        }
    }
}

