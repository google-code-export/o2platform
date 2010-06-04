namespace Amazon.CloudWatch.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://monitoring.amazonaws.com/doc/2009-05-15/", IsNullable=false)]
    public class GetMetricStatisticsResult
    {
        private List<Datapoint> datapointsField;
        private string labelField;

        public bool IsSetDatapoints()
        {
            return (this.Datapoints.Count > 0);
        }

        public bool IsSetLabel()
        {
            return (this.labelField != null);
        }

        public GetMetricStatisticsResult WithDatapoints(params Datapoint[] list)
        {
            foreach (Datapoint datapoint in list)
            {
                this.Datapoints.Add(datapoint);
            }
            return this;
        }

        public GetMetricStatisticsResult WithLabel(string label)
        {
            this.labelField = label;
            return this;
        }

        [XmlElement(ElementName="Datapoints")]
        public List<Datapoint> Datapoints
        {
            get
            {
                if (this.datapointsField == null)
                {
                    this.datapointsField = new List<Datapoint>();
                }
                return this.datapointsField;
            }
            set
            {
                this.datapointsField = value;
            }
        }

        [XmlElement(ElementName="Label")]
        public string Label
        {
            get
            {
                return this.labelField;
            }
            set
            {
                this.labelField = value;
            }
        }
    }
}

