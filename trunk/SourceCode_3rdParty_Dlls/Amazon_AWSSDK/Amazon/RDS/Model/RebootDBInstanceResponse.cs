namespace Amazon.RDS.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class RebootDBInstanceResponse
    {
        private Amazon.RDS.Model.RebootDBInstanceResult rebootDBInstanceResultField;
        private Amazon.RDS.Model.ResponseMetadata responseMetadataField;

        public bool IsSetRebootDBInstanceResult()
        {
            return (this.rebootDBInstanceResultField != null);
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

        public RebootDBInstanceResponse WithRebootDBInstanceResult(Amazon.RDS.Model.RebootDBInstanceResult rebootDBInstanceResult)
        {
            this.rebootDBInstanceResultField = rebootDBInstanceResult;
            return this;
        }

        public RebootDBInstanceResponse WithResponseMetadata(Amazon.RDS.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="RebootDBInstanceResult")]
        public Amazon.RDS.Model.RebootDBInstanceResult RebootDBInstanceResult
        {
            get
            {
                return this.rebootDBInstanceResultField;
            }
            set
            {
                this.rebootDBInstanceResultField = value;
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

