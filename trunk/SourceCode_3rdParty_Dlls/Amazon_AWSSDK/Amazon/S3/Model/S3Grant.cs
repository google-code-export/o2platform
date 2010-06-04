namespace Amazon.S3.Model
{
    using System;
    using System.Security;
    using System.Text;
    using System.Xml.Serialization;

    public class S3Grant
    {
        private S3Grantee grantee;
        private S3Permission permission;

        internal bool IsSetGrantee()
        {
            return (this.grantee != null);
        }

        internal bool IsSetPermission()
        {
            return (this.permission != S3Permission.INVALID);
        }

        public override string ToString()
        {
            return string.Concat(new object[] { "S3Grantee: ", this.grantee, " S3Permission: ", this.Permission });
        }

        internal string ToXML()
        {
            StringBuilder builder = new StringBuilder(0x400);
            builder.Append("<Grant>");
            if (this.Grantee.IsSetCanonicalUser())
            {
                builder.Append("<Grantee xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:type=\"CanonicalUser\">");
                builder.Append("<ID>").Append(SecurityElement.Escape(this.Grantee.CanonicalUser.First)).Append("</ID>");
                builder.Append("<DisplayName>").Append(SecurityElement.Escape(this.Grantee.CanonicalUser.Second)).Append("</DisplayName>");
                builder.Append("</Grantee>");
                builder.Append("<Permission>").Append(this.Permission).Append("</Permission>");
            }
            else if (this.Grantee.IsSetEmailAddress())
            {
                builder.Append("<Grantee xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:type=\"AmazonCustomerByEmail\">");
                builder.Append("<EmailAddress>").Append(SecurityElement.Escape(this.Grantee.EmailAddress)).Append("</EmailAddress>");
                builder.Append("</Grantee>");
                builder.Append("<Permission>").Append(this.Permission).Append("</Permission>");
            }
            else if (this.Grantee.IsSetURI())
            {
                builder.Append("<Grantee xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:type=\"Group\">");
                builder.Append("<URI>").Append(SecurityElement.Escape(this.Grantee.URI)).Append("</URI>");
                builder.Append("</Grantee>");
                builder.Append("<Permission>").Append(this.Permission).Append("</Permission>");
            }
            builder.Append("</Grant>");
            return builder.ToString();
        }

        public S3Grant WithGrantee(S3Grantee grantee)
        {
            this.grantee = grantee;
            return this;
        }

        public S3Grant WithPermission(S3Permission permission)
        {
            this.permission = permission;
            return this;
        }

        [XmlElement(ElementName="Grantee")]
        public S3Grantee Grantee
        {
            get
            {
                return this.grantee;
            }
            set
            {
                this.grantee = value;
            }
        }

        [XmlElement(ElementName="Permission")]
        public S3Permission Permission
        {
            get
            {
                return this.permission;
            }
            set
            {
                this.permission = value;
            }
        }
    }
}

