namespace Amazon.RDS.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class ModifyDBParameterGroupRequest
    {
        private string DBParameterGroupNameField;
        private List<Parameter> parametersField;

        public bool IsSetDBParameterGroupName()
        {
            return (this.DBParameterGroupNameField != null);
        }

        public bool IsSetParameters()
        {
            return (this.Parameters.Count > 0);
        }

        public ModifyDBParameterGroupRequest WithDBParameterGroupName(string DBParameterGroupName)
        {
            this.DBParameterGroupNameField = DBParameterGroupName;
            return this;
        }

        public ModifyDBParameterGroupRequest WithParameters(params Parameter[] list)
        {
            foreach (Parameter parameter in list)
            {
                this.Parameters.Add(parameter);
            }
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

        [XmlElement(ElementName="Parameters")]
        public List<Parameter> Parameters
        {
            get
            {
                if (this.parametersField == null)
                {
                    this.parametersField = new List<Parameter>();
                }
                return this.parametersField;
            }
            set
            {
                this.parametersField = value;
            }
        }
    }
}

