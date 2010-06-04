namespace Amazon.RDS.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class ModifyDBInstanceResponse
    {
        private Amazon.RDS.Model.ModifyDBInstanceResult modifyDBInstanceResultField;
        private Amazon.RDS.Model.ResponseMetadata responseMetadataField;

        public bool IsSetModifyDBInstanceResult()
        {
            return (this.modifyDBInstanceResultField != null);
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

        public ModifyDBInstanceResponse WithModifyDBInstanceResult(Amazon.RDS.Model.ModifyDBInstanceResult modifyDBInstanceResult)
        {
            this.modifyDBInstanceResultField = modifyDBInstanceResult;
            return this;
        }

        public ModifyDBInstanceResponse WithResponseMetadata(Amazon.RDS.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="ModifyDBInstanceResult")]
        public Amazon.RDS.Model.ModifyDBInstanceResult ModifyDBInstanceResult
        {
            get
            {
                return this.modifyDBInstanceResultField;
            }
            set
            {
                this.modifyDBInstanceResultField = value;
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

