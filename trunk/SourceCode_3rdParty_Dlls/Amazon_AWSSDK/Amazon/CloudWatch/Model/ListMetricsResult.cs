namespace Amazon.CloudWatch.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://monitoring.amazonaws.com/doc/2009-05-15/", IsNullable=false)]
    public class ListMetricsResult
    {
        private List<Metric> metricsField;
        private string nextTokenField;

        public bool IsSetMetrics()
        {
            return (this.Metrics.Count > 0);
        }

        public bool IsSetNextToken()
        {
            return (this.nextTokenField != null);
        }

        public ListMetricsResult WithMetrics(params Metric[] list)
        {
            foreach (Metric metric in list)
            {
                this.Metrics.Add(metric);
            }
            return this;
        }

        public ListMetricsResult WithNextToken(string nextToken)
        {
            this.nextTokenField = nextToken;
            return this;
        }

        [XmlElement(ElementName="Metrics")]
        public List<Metric> Metrics
        {
            get
            {
                if (this.metricsField == null)
                {
                    this.metricsField = new List<Metric>();
                }
                return this.metricsField;
            }
            set
            {
                this.metricsField = value;
            }
        }

        [XmlElement(ElementName="NextToken")]
        public string NextToken
        {
            get
            {
                return this.nextTokenField;
            }
            set
            {
                this.nextTokenField = value;
            }
        }
    }
}

