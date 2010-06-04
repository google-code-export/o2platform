namespace Amazon.RDS.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class ResetDBParameterGroupResponse
    {
        private Amazon.RDS.Model.ResetDBParameterGroupResult resetDBParameterGroupResultField;
        private Amazon.RDS.Model.ResponseMetadata responseMetadataField;

        public bool IsSetResetDBParameterGroupResult()
        {
            return (this.resetDBParameterGroupResultField != null);
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

        public ResetDBParameterGroupResponse WithResetDBParameterGroupResult(Amazon.RDS.Model.ResetDBParameterGroupResult resetDBParameterGroupResult)
        {
            this.resetDBParameterGroupResultField = resetDBParameterGroupResult;
            return this;
        }

        public ResetDBParameterGroupResponse WithResponseMetadata(Amazon.RDS.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="ResetDBParameterGroupResult")]
        public Amazon.RDS.Model.ResetDBParameterGroupResult ResetDBParameterGroupResult
        {
            get
            {
                return this.resetDBParameterGroupResultField;
            }
            set
            {
                this.resetDBParameterGroupResultField = value;
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

