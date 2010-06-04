namespace Amazon.RDS.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class Parameter
    {
        private string allowedValuesField;
        private string applyMethodField;
        private string applyTypeField;
        private string dataTypeField;
        private string descriptionField;
        private bool? isModifiableField;
        private string parameterNameField;
        private string parameterValueField;
        private string sourceField;

        public bool IsSetAllowedValues()
        {
            return (this.allowedValuesField != null);
        }

        public bool IsSetApplyMethod()
        {
            return (this.applyMethodField != null);
        }

        public bool IsSetApplyType()
        {
            return (this.applyTypeField != null);
        }

        public bool IsSetDataType()
        {
            return (this.dataTypeField != null);
        }

        public bool IsSetDescription()
        {
            return (this.descriptionField != null);
        }

        public bool IsSetIsModifiable()
        {
            return this.isModifiableField.HasValue;
        }

        public bool IsSetParameterName()
        {
            return (this.parameterNameField != null);
        }

        public bool IsSetParameterValue()
        {
            return (this.parameterValueField != null);
        }

        public bool IsSetSource()
        {
            return (this.sourceField != null);
        }

        public Parameter WithAllowedValues(string allowedValues)
        {
            this.allowedValuesField = allowedValues;
            return this;
        }

        public Parameter WithApplyMethod(string applyMethod)
        {
            this.applyMethodField = applyMethod;
            return this;
        }

        public Parameter WithApplyType(string applyType)
        {
            this.applyTypeField = applyType;
            return this;
        }

        public Parameter WithDataType(string dataType)
        {
            this.dataTypeField = dataType;
            return this;
        }

        public Parameter WithDescription(string description)
        {
            this.descriptionField = description;
            return this;
        }

        public Parameter WithIsModifiable(bool isModifiable)
        {
            this.isModifiableField = new bool?(isModifiable);
            return this;
        }

        public Parameter WithParameterName(string parameterName)
        {
            this.parameterNameField = parameterName;
            return this;
        }

        public Parameter WithParameterValue(string parameterValue)
        {
            this.parameterValueField = parameterValue;
            return this;
        }

        public Parameter WithSource(string source)
        {
            this.sourceField = source;
            return this;
        }

        [XmlElement(ElementName="AllowedValues")]
        public string AllowedValues
        {
            get
            {
                return this.allowedValuesField;
            }
            set
            {
                this.allowedValuesField = value;
            }
        }

        [XmlElement(ElementName="ApplyMethod")]
        public string ApplyMethod
        {
            get
            {
                return this.applyMethodField;
            }
            set
            {
                this.applyMethodField = value;
            }
        }

        [XmlElement(ElementName="ApplyType")]
        public string ApplyType
        {
            get
            {
                return this.applyTypeField;
            }
            set
            {
                this.applyTypeField = value;
            }
        }

        [XmlElement(ElementName="DataType")]
        public string DataType
        {
            get
            {
                return this.dataTypeField;
            }
            set
            {
                this.dataTypeField = value;
            }
        }

        [XmlElement(ElementName="Description")]
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        [XmlElement(ElementName="IsModifiable")]
        public bool IsModifiable
        {
            get
            {
                return this.isModifiableField.GetValueOrDefault();
            }
            set
            {
                this.isModifiableField = new bool?(value);
            }
        }

        [XmlElement(ElementName="ParameterName")]
        public string ParameterName
        {
            get
            {
                return this.parameterNameField;
            }
            set
            {
                this.parameterNameField = value;
            }
        }

        [XmlElement(ElementName="ParameterValue")]
        public string ParameterValue
        {
            get
            {
                return this.parameterValueField;
            }
            set
            {
                this.parameterValueField = value;
            }
        }

        [XmlElement(ElementName="Source")]
        public string Source
        {
            get
            {
                return this.sourceField;
            }
            set
            {
                this.sourceField = value;
            }
        }
    }
}

