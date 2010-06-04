namespace Amazon.RDS.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class ModifyDBParameterGroupResponse
    {
        private Amazon.RDS.Model.ModifyDBParameterGroupResult modifyDBParameterGroupResultField;
        private Amazon.RDS.Model.ResponseMetadata responseMetadataField;

        public bool IsSetModifyDBParameterGroupResult()
        {
            return (this.modifyDBParameterGroupResultField != null);
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

        public ModifyDBParameterGroupResponse WithModifyDBParameterGroupResult(Amazon.RDS.Model.ModifyDBParameterGroupResult modifyDBParameterGroupResult)
        {
            this.modifyDBParameterGroupResultField = modifyDBParameterGroupResult;
            return this;
        }

        public ModifyDBParameterGroupResponse WithResponseMetadata(Amazon.RDS.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="ModifyDBParameterGroupResult")]
        public Amazon.RDS.Model.ModifyDBParameterGroupResult ModifyDBParameterGroupResult
        {
            get
            {
                return this.modifyDBParameterGroupResultField;
            }
            set
            {
                this.modifyDBParameterGroupResultField = value;
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

