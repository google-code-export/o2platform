namespace Amazon.CloudWatch.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://monitoring.amazonaws.com/doc/2009-05-15/", IsNullable=false)]
    public class Metric
    {
        private List<Dimension> dimensionsField;
        private string measureNameField;
        private string namespaceValueField;

        public bool IsSetDimensions()
        {
            return (this.Dimensions.Count > 0);
        }

        public bool IsSetMeasureName()
        {
            return (this.measureNameField != null);
        }

        public bool IsSetNamespace()
        {
            return (this.namespaceValueField != null);
        }

        public Metric WithDimensions(params Dimension[] list)
        {
            foreach (Dimension dimension in list)
            {
                this.Dimensions.Add(dimension);
            }
            return this;
        }

        public Metric WithMeasureName(string measureName)
        {
            this.measureNameField = measureName;
            return this;
        }

        public Metric WithNamespace(string namespaceValue)
        {
            this.namespaceValueField = namespaceValue;
            return this;
        }

        [XmlElement(ElementName="Dimensions")]
        public List<Dimension> Dimensions
        {
            get
            {
                if (this.dimensionsField == null)
                {
                    this.dimensionsField = new List<Dimension>();
                }
                return this.dimensionsField;
            }
            set
            {
                this.dimensionsField = value;
            }
        }

        [XmlElement(ElementName="MeasureName")]
        public string MeasureName
        {
            get
            {
                return this.measureNameField;
            }
            set
            {
                this.measureNameField = value;
            }
        }

        [XmlElement(ElementName="Namespace")]
        public string Namespace
        {
            get
            {
                return this.namespaceValueField;
            }
            set
            {
                this.namespaceValueField = value;
            }
        }
    }
}

