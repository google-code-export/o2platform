namespace Amazon.RDS.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class RevokeDBSecurityGroupIngressResponse
    {
        private Amazon.RDS.Model.ResponseMetadata responseMetadataField;
        private Amazon.RDS.Model.RevokeDBSecurityGroupIngressResult revokeDBSecurityGroupIngressResultField;

        public bool IsSetResponseMetadata()
        {
            return (this.responseMetadataField != null);
        }

        public bool IsSetRevokeDBSecurityGroupIngressResult()
        {
            return (this.revokeDBSecurityGroupIngressResultField != null);
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

        public RevokeDBSecurityGroupIngressResponse WithResponseMetadata(Amazon.RDS.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        public RevokeDBSecurityGroupIngressResponse WithRevokeDBSecurityGroupIngressResult(Amazon.RDS.Model.RevokeDBSecurityGroupIngressResult revokeDBSecurityGroupIngressResult)
        {
            this.revokeDBSecurityGroupIngressResultField = revokeDBSecurityGroupIngressResult;
            return this;
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

        [XmlElement(ElementName="RevokeDBSecurityGroupIngressResult")]
        public Amazon.RDS.Model.RevokeDBSecurityGroupIngressResult RevokeDBSecurityGroupIngressResult
        {
            get
            {
                return this.revokeDBSecurityGroupIngressResultField;
            }
            set
            {
                this.revokeDBSecurityGroupIngressResultField = value;
            }
        }
    }
}

