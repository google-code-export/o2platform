namespace Amazon.RDS.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class AuthorizeDBSecurityGroupIngressResponse
    {
        private Amazon.RDS.Model.AuthorizeDBSecurityGroupIngressResult authorizeDBSecurityGroupIngressResultField;
        private Amazon.RDS.Model.ResponseMetadata responseMetadataField;

        public bool IsSetAuthorizeDBSecurityGroupIngressResult()
        {
            return (this.authorizeDBSecurityGroupIngressResultField != null);
        }

        public bool IsSetResponseMetadata()
        {
            return (this.responseMetadataField != null);
        }

        public string ToXML()
        {
            StringBuilder sb = new StringBuilder(0x400);
            XmlSerializer serializer = new XmlSerializer(base.GetType());
            using (StringWriter writer = new StringWriter(sb))
            {
                serializer.Serialize((TextWriter) writer, this);
            }
            return sb.ToString();
        }

        public AuthorizeDBSecurityGroupIngressResponse WithAuthorizeDBSecurityGroupIngressResult(Amazon.RDS.Model.AuthorizeDBSecurityGroupIngressResult authorizeDBSecurityGroupIngressResult)
        {
            this.authorizeDBSecurityGroupIngressResultField = authorizeDBSecurityGroupIngressResult;
            return this;
        }

        public AuthorizeDBSecurityGroupIngressResponse WithResponseMetadata(Amazon.RDS.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="AuthorizeDBSecurityGroupIngressResult")]
        public Amazon.RDS.Model.AuthorizeDBSecurityGroupIngressResult AuthorizeDBSecurityGroupIngressResult
        {
            get
            {
                return this.authorizeDBSecurityGroupIngressResultField;
            }
            set
            {
                this.authorizeDBSecurityGroupIngressResultField = value;
            }
        }

        [XmlElement(ElementName="ResponseMetadata")]
        public Amazon.RDS.Model.ResponseMetadata ResponseMetadata
        {
            get
            {
                return this.responseMetadataField;
            }
            set
            {
                this.responseMetadataField = value;
            }
        }
    }
}

