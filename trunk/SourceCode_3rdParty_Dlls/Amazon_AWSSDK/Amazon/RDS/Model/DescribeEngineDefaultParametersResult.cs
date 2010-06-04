namespace Amazon.RDS.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class DescribeEngineDefaultParametersResult
    {
        private Amazon.RDS.Model.EngineDefaults engineDefaultsField;

        public bool IsSetEngineDefaults()
        {
            return (this.engineDefaultsField != null);
        }

        public DescribeEngineDefaultParametersResult WithEngineDefaults(Amazon.RDS.Model.EngineDefaults engineDefaults)
        {
            this.engineDefaultsField = engineDefaults;
            return this;
        }

        [XmlElement(ElementName="EngineDefaults")]
        public Amazon.RDS.Model.EngineDefaults EngineDefaults
        {
            get
            {
                return this.engineDefaultsField;
            }
            set
            {
                this.engineDefaultsField = value;
            }
        }
    }
}

