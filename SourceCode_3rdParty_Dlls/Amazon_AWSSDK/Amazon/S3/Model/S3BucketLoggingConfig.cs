namespace Amazon.S3.Model
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml.Serialization;

    public class S3BucketLoggingConfig
    {
        private List<S3Grant> grantList = new List<S3Grant>();
        private string targetBucketName;
        private string targetPrefix = "";

        public void AddGrant(S3Grantee grantee, S3Permission permission)
        {
            S3Grant item = new S3Grant();
            item.WithGrantee(grantee);
            item.WithPermission(permission);
            this.Grants.Add(item);
        }

        internal bool IsSetGrants()
        {
            return (this.Grants.Count > 0);
        }

        internal bool IsSetTargetBucketName()
        {
            return !string.IsNullOrEmpty(this.targetBucketName);
        }

        internal bool IsSetTargetPrefix()
        {
            return (this.targetPrefix != null);
        }

        public void RemoveGrant(S3Grantee grantee)
        {
            List<S3Grant> list = new List<S3Grant>();
            foreach (S3Grant grant in this.Grants)
            {
                if (grant.Grantee.Equals(grantee))
                {
                    list.Add(grant);
                }
            }
            foreach (S3Grant grant2 in list)
            {
                this.Grants.Remove(grant2);
            }
        }

        public void RemoveGrant(S3Grantee grantee, S3Permission permission)
        {
            foreach (S3Grant grant in this.Grants)
            {
                if (grant.Grantee.Equals(grantee) && (grant.Permission == permission))
                {
                    this.Grants.Remove(grant);
                    break;
                }
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(0x400);
            builder.Append("<BucketLoggingStatus xmlns=\"http://s3.amazonaws.com/doc/2006-03-01/\">");
            if ((this.IsSetTargetBucketName() && this.IsSetTargetPrefix()) && this.IsSetGrants())
            {
                builder.Append("<LoggingEnabled>");
                builder.Append("<TargetBucket>" + this.TargetBucketName + "</TargetBucket>");
                builder.Append("<TargetPrefix>" + this.TargetPrefix + "</TargetPrefix>");
                builder.Append("<TargetGrants>");
                foreach (S3Grant grant in this.Grants)
                {
                    builder.Append(grant.ToXML());
                }
                builder.Append("</TargetGrants>");
                builder.Append("</LoggingEnabled>");
            }
            builder.Append("</BucketLoggingStatus>");
            return builder.ToString();
        }

        public S3BucketLoggingConfig WithGrants(params S3Grant[] args)
        {
            foreach (S3Grant grant in args)
            {
                this.Grants.Add(grant);
            }
            return this;
        }

        public S3BucketLoggingConfig WithTargetBucketName(string targetBucketName)
        {
            this.targetBucketName = targetBucketName;
            return this;
        }

        public S3BucketLoggingConfig WithTargetPrefix(string targetPrefix)
        {
            this.targetPrefix = targetPrefix;
            return this;
        }

        [XmlElement(ElementName="Grants")]
        public List<S3Grant> Grants
        {
            get
            {
                return this.grantList;
            }
        }

        [XmlElement(ElementName="TargetBucketName")]
        public string TargetBucketName
        {
            get
            {
                return this.targetBucketName;
            }
            set
            {
                this.targetBucketName = value;
            }
        }

        [XmlElement(ElementName="TargetPrefix")]
        public string TargetPrefix
        {
            get
            {
                return this.targetPrefix;
            }
            set
            {
                this.targetPrefix = value;
            }
        }
    }
}

