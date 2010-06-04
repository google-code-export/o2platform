namespace Amazon.RDS.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class CreateDBParameterGroupResult
    {
        private Amazon.RDS.Model.DBParameterGroup DBParameterGroupField;

        public bool IsSetDBParameterGroup()
        {
            return (this.DBParameterGroupField != null);
        }

        public CreateDBParameterGroupResult WithDBParameterGroup(Amazon.RDS.Model.DBParameterGroup DBParameterGroup)
        {
            this.DBParameterGroupField = DBParameterGroup;
            return this;
        }

        [XmlElement(ElementName="DBParameterGroup")]
        public Amazon.RDS.Model.DBParameterGroup DBParameterGroup
        {
            get
            {
                return this.DBParameterGroupField;
            }
            set
            {
                this.DBParameterGroupField = value;
            }
        }
    }
}

