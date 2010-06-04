namespace Amazon.AutoScaling.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://autoscaling.amazonaws.com/doc/2009-05-15/", IsNullable=false)]
    public class DescribeLaunchConfigurationsRequest
    {
        private List<string> launchConfigurationNamesField;
        private decimal? maxRecordsField;
        private string nextTokenField;

        public bool IsSetLaunchConfigurationNames()
        {
            return (this.LaunchConfigurationNames.Count > 0);
        }

        public bool IsSetMaxRecords()
        {
            return this.maxRecordsField.HasValue;
        }

        public bool IsSetNextToken()
        {
            return (this.nextTokenField != null);
        }

        public DescribeLaunchConfigurationsRequest WithLaunchConfigurationNames(params string[] list)
        {
            foreach (string str in list)
            {
                this.LaunchConfigurationNames.Add(str);
            }
            return this;
        }

        public DescribeLaunchConfigurationsRequest WithMaxRecords(decimal maxRecords)
        {
            this.maxRecordsField = new decimal?(maxRecords);
            return this;
        }

        public DescribeLaunchConfigurationsRequest WithNextToken(string nextToken)
        {
            this.nextTokenField = nextToken;
            return this;
        }

        [XmlElement(ElementName="LaunchConfigurationNames")]
        public List<string> LaunchConfigurationNames
        {
            get
            {
                if (this.launchConfigurationNamesField == null)
                {
                    this.launchConfigurationNamesField = new List<string>();
                }
                return this.launchConfigurationNamesField;
            }
            set
            {
                this.launchConfigurationNamesField = value;
            }
        }

        [XmlElement(ElementName="MaxRecords")]
        public decimal MaxRecords
        {
            get
            {
                return this.maxRecordsField.GetValueOrDefault();
            }
            set
            {
                this.maxRecordsField = new decimal?(value);
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

