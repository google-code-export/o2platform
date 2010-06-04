namespace Amazon.RDS.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class CreateDBParameterGroupResponse
    {
        private Amazon.RDS.Model.CreateDBParameterGroupResult createDBParameterGroupResultField;
        private Amazon.RDS.Model.ResponseMetadata responseMetadataField;

        public bool IsSetCreateDBParameterGroupResult()
        {
            return (this.createDBParameterGroupResultField != null);
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

        public CreateDBParameterGroupResponse WithCreateDBParameterGroupResult(Amazon.RDS.Model.CreateDBParameterGroupResult createDBParameterGroupResult)
        {
            this.createDBParameterGroupResultField = createDBParameterGroupResult;
            return this;
        }

        public CreateDBParameterGroupResponse WithResponseMetadata(Amazon.RDS.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="CreateDBParameterGroupResult")]
        public Amazon.RDS.Model.CreateDBParameterGroupResult CreateDBParameterGroupResult
        {
            get
            {
                return this.createDBParameterGroupResultField;
            }
            set
            {
                this.createDBParameterGroupResultField = value;
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

