namespace Amazon.RDS.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class ModifyDBInstanceResult
    {
        private Amazon.RDS.Model.DBInstance DBInstanceField;

        public bool IsSetDBInstance()
        {
            return (this.DBInstanceField != null);
        }

        public ModifyDBInstanceResult WithDBInstance(Amazon.RDS.Model.DBInstance DBInstance)
        {
            this.DBInstanceField = DBInstance;
            return this;
        }

        [XmlElement(ElementName="DBInstance")]
        public Amazon.RDS.Model.DBInstance DBInstance
        {
            get
            {
                return this.DBInstanceField;
            }
            set
            {
                this.DBInstanceField = value;
            }
        }
    }
}

