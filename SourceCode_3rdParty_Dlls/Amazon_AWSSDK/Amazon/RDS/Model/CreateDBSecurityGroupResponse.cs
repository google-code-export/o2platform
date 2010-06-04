namespace Amazon.RDS.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class CreateDBSecurityGroupResponse
    {
        private Amazon.RDS.Model.CreateDBSecurityGroupResult createDBSecurityGroupResultField;
        private Amazon.RDS.Model.ResponseMetadata responseMetadataField;

        public bool IsSetCreateDBSecurityGroupResult()
        {
            return (this.createDBSecurityGroupResultField != null);
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

        public CreateDBSecurityGroupResponse WithCreateDBSecurityGroupResult(Amazon.RDS.Model.CreateDBSecurityGroupResult createDBSecurityGroupResult)
        {
            this.createDBSecurityGroupResultField = createDBSecurityGroupResult;
            return this;
        }

        public CreateDBSecurityGroupResponse WithResponseMetadata(Amazon.RDS.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="CreateDBSecurityGroupResult")]
        public Amazon.RDS.Model.CreateDBSecurityGroupResult CreateDBSecurityGroupResult
        {
            get
            {
                return this.createDBSecurityGroupResultField;
            }
            set
            {
                this.createDBSecurityGroupResultField = value;
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

