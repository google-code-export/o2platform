namespace Amazon.RDS.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class DescribeDBParametersResult
    {
        private string markerField;
        private List<Amazon.RDS.Model.Parameter> parameterField;

        public bool IsSetMarker()
        {
            return (this.markerField != null);
        }

        public bool IsSetParameter()
        {
            return (this.Parameter.Count > 0);
        }

        public DescribeDBParametersResult WithMarker(string marker)
        {
            this.markerField = marker;
            return this;
        }

        public DescribeDBParametersResult WithParameter(params Amazon.RDS.Model.Parameter[] list)
        {
            foreach (Amazon.RDS.Model.Parameter parameter in list)
            {
                this.Parameter.Add(parameter);
            }
            return this;
        }

        [XmlElement(ElementName="Marker")]
        public string Marker
        {
            get
            {
                return this.markerField;
            }
            set
            {
                this.markerField = value;
            }
        }

        [XmlElement(ElementName="Parameter")]
        public List<Amazon.RDS.Model.Parameter> Parameter
        {
            get
            {
                if (this.parameterField == null)
                {
                    this.parameterField = new List<Amazon.RDS.Model.Parameter>();
                }
                return this.parameterField;
            }
            set
            {
                this.parameterField = value;
            }
        }
    }
}

