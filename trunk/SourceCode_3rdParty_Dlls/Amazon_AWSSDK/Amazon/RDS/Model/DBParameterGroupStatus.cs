namespace Amazon.RDS.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class DBParameterGroupStatus
    {
        private string DBParameterGroupNameField;
        private string parameterApplyStatusField;

        public bool IsSetDBParameterGroupName()
        {
            return (this.DBParameterGroupNameField != null);
        }

        public bool IsSetParameterApplyStatus()
        {
            return (this.parameterApplyStatusField != null);
        }

        public DBParameterGroupStatus WithDBParameterGroupName(string DBParameterGroupName)
        {
            this.DBParameterGroupNameField = DBParameterGroupName;
            return this;
        }

        public DBParameterGroupStatus WithParameterApplyStatus(string parameterApplyStatus)
        {
            this.parameterApplyStatusField = parameterApplyStatus;
            return this;
        }

        [XmlElement(ElementName="DBParameterGroupName")]
        public string DBParameterGroupName
        {
            get
            {
                return this.DBParameterGroupNameField;
            }
            set
            {
                this.DBParameterGroupNameField = value;
            }
        }

        [XmlElement(ElementName="ParameterApplyStatus")]
        public string ParameterApplyStatus
        {
            get
            {
                return this.parameterApplyStatusField;
            }
            set
            {
                this.parameterApplyStatusField = value;
            }
        }
    }
}

