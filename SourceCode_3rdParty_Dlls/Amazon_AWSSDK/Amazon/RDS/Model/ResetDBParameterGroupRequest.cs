namespace Amazon.RDS.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class ResetDBParameterGroupRequest
    {
        private string DBParameterGroupNameField;
        private List<Parameter> parametersField;
        private bool? resetAllParametersField;

        public bool IsSetDBParameterGroupName()
        {
            return (this.DBParameterGroupNameField != null);
        }

        public bool IsSetParameters()
        {
            return (this.Parameters.Count > 0);
        }

        public bool IsSetResetAllParameters()
        {
            return this.resetAllParametersField.HasValue;
        }

        public ResetDBParameterGroupRequest WithDBParameterGroupName(string DBParameterGroupName)
        {
            this.DBParameterGroupNameField = DBParameterGroupName;
            return this;
        }

        public ResetDBParameterGroupRequest WithParameters(params Parameter[] list)
        {
            foreach (Parameter parameter in list)
            {
                this.Parameters.Add(parameter);
            }
            return this;
        }

        public ResetDBParameterGroupRequest WithResetAllParameters(bool resetAllParameters)
        {
            this.resetAllParametersField = new bool?(resetAllParameters);
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

        [XmlElement(ElementName="ResetAllParameters")]
        public bool ResetAllParameters
        {
            get
            {
                return this.resetAllParametersField.GetValueOrDefault();
            }
            set
            {
                this.resetAllParametersField = new bool?(value);
            }
        }
    }
}

