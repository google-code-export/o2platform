namespace Amazon.RDS.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class ModifyDBParameterGroupResult
    {
        private string DBParameterGroupNameField;

        public bool IsSetDBParameterGroupName()
        {
            return (this.DBParameterGroupNameField != null);
        }

        public ModifyDBParameterGroupResult WithDBParameterGroupName(string DBParameterGroupName)
        {
            this.DBParameterGroupNameField = DBParameterGroupName;
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
    }
}

