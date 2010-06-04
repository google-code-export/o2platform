namespace Amazon.RDS.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class RestoreDBInstanceToPointInTimeResponse
    {
        private Amazon.RDS.Model.ResponseMetadata responseMetadataField;
        private Amazon.RDS.Model.RestoreDBInstanceToPointInTimeResult restoreDBInstanceToPointInTimeResultField;

        public bool IsSetResponseMetadata()
        {
            return (this.responseMetadataField != null);
        }

        public bool IsSetRestoreDBInstanceToPointInTimeResult()
        {
            return (this.restoreDBInstanceToPointInTimeResultField != null);
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

        public RestoreDBInstanceToPointInTimeResponse WithResponseMetadata(Amazon.RDS.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        public RestoreDBInstanceToPointInTimeResponse WithRestoreDBInstanceToPointInTimeResult(Amazon.RDS.Model.RestoreDBInstanceToPointInTimeResult restoreDBInstanceToPointInTimeResult)
        {
            this.restoreDBInstanceToPointInTimeResultField = restoreDBInstanceToPointInTimeResult;
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

        [XmlElement(ElementName="RestoreDBInstanceToPointInTimeResult")]
        public Amazon.RDS.Model.RestoreDBInstanceToPointInTimeResult RestoreDBInstanceToPointInTimeResult
        {
            get
            {
                return this.restoreDBInstanceToPointInTimeResultField;
            }
            set
            {
                this.restoreDBInstanceToPointInTimeResultField = value;
            }
        }
    }
}

