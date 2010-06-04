namespace Amazon.RDS.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class CreateDBInstanceResponse
    {
        private Amazon.RDS.Model.CreateDBInstanceResult createDBInstanceResultField;
        private Amazon.RDS.Model.ResponseMetadata responseMetadataField;

        public bool IsSetCreateDBInstanceResult()
        {
            return (this.createDBInstanceResultField != null);
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

        public CreateDBInstanceResponse WithCreateDBInstanceResult(Amazon.RDS.Model.CreateDBInstanceResult createDBInstanceResult)
        {
            this.createDBInstanceResultField = createDBInstanceResult;
            return this;
        }

        public CreateDBInstanceResponse WithResponseMetadata(Amazon.RDS.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="CreateDBInstanceResult")]
        public Amazon.RDS.Model.CreateDBInstanceResult CreateDBInstanceResult
        {
            get
            {
                return this.createDBInstanceResultField;
            }
            set
            {
                this.createDBInstanceResultField = value;
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

