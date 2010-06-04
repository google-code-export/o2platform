namespace Amazon.S3.Model
{
    using System;
    using System.Text;
    using System.Xml.Serialization;

    public class S3Grantee
    {
        private Tuple<string, string> canonicalUser;
        private string emailAddress;
        private string uri;

        private bool Equals(S3Grantee grantee)
        {
            if (grantee.IsSetCanonicalUser() && this.IsSetCanonicalUser())
            {
                return grantee.canonicalUser.Equals(this.canonicalUser);
            }
            if (grantee.IsSetEmailAddress() && this.IsSetEmailAddress())
            {
                return grantee.EmailAddress.Equals(this.EmailAddress);
            }
            return ((grantee.IsSetURI() && this.IsSetURI()) && grantee.URI.Equals(this.URI));
        }

        public override bool Equals(object obj)
        {
            return (((obj != null) && (base.GetType() == obj.GetType())) && this.Equals((S3Grantee) obj));
        }

        public override int GetHashCode()
        {
            if (this.IsSetCanonicalUser())
            {
                return (this.canonicalUser.First.GetHashCode() ^ this.canonicalUser.Second.GetHashCode());
            }
            if (this.IsSetEmailAddress())
            {
                return this.EmailAddress.GetHashCode();
            }
            if (this.IsSetURI())
            {
                return this.URI.GetHashCode();
            }
            return base.GetHashCode();
        }

        internal bool IsSetCanonicalUser()
        {
            return (((this.canonicalUser != null) && !string.IsNullOrEmpty(this.CanonicalUser.First)) && !string.IsNullOrEmpty(this.CanonicalUser.Second));
        }

        internal bool IsSetEmailAddress()
        {
            return !string.IsNullOrEmpty(this.emailAddress);
        }

        internal bool IsSetURI()
        {
            return (this.uri != null);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            if (this.IsSetEmailAddress())
            {
                builder.Append("EmailAddress:" + this.EmailAddress);
            }
            if (this.IsSetURI())
            {
                builder.Append("URI:" + this.URI);
            }
            if (this.IsSetCanonicalUser())
            {
                builder.Append("ID:" + this.canonicalUser.First + " DisplayName:" + this.canonicalUser.Second);
            }
            return builder.ToString();
        }

        public S3Grantee WithCanonicalUser(string id, string displayName)
        {
            this.canonicalUser = new Tuple<string, string>(id, displayName);
            return this;
        }

        public S3Grantee WithEmailAddress(string emailAddress)
        {
            this.emailAddress = emailAddress;
            return this;
        }

        public S3Grantee WithURI(string uri)
        {
            this.uri = uri;
            return this;
        }

        [XmlElement(ElementName="CanonicalUser")]
        public Tuple<string, string> CanonicalUser
        {
            get
            {
                if (this.canonicalUser == null)
                {
                    this.canonicalUser = new Tuple<string, string>("", "");
                }
                return this.canonicalUser;
            }
            set
            {
                this.canonicalUser = value;
            }
        }

        [XmlElement(ElementName="EmailAddress")]
        public string EmailAddress
        {
            get
            {
                return this.emailAddress;
            }
            set
            {
                this.emailAddress = value;
            }
        }

        [XmlElement(ElementName="URI")]
        public string URI
        {
            get
            {
                return this.uri;
            }
            set
            {
                this.uri = value;
            }
        }
    }
}

