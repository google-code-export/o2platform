namespace Amazon.RDS.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class DeleteDBInstanceResponse
    {
        private Amazon.RDS.Model.DeleteDBInstanceResult deleteDBInstanceResultField;
        private Amazon.RDS.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDeleteDBInstanceResult()
        {
            return (this.deleteDBInstanceResultField != null);
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

        public DeleteDBInstanceResponse WithDeleteDBInstanceResult(Amazon.RDS.Model.DeleteDBInstanceResult deleteDBInstanceResult)
        {
            this.deleteDBInstanceResultField = deleteDBInstanceResult;
            return this;
        }

        public DeleteDBInstanceResponse WithResponseMetadata(Amazon.RDS.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="DeleteDBInstanceResult")]
        public Amazon.RDS.Model.DeleteDBInstanceResult DeleteDBInstanceResult
        {
            get
            {
                return this.deleteDBInstanceResultField;
            }
            set
            {
                this.deleteDBInstanceResultField = value;
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

