namespace Amazon.AutoScaling.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://autoscaling.amazonaws.com/doc/2009-05-15/", IsNullable=false)]
    public class DescribeLaunchConfigurationsResult
    {
        private List<LaunchConfiguration> launchConfigurationsField;
        private string nextTokenField;

        public bool IsSetLaunchConfigurations()
        {
            return (this.LaunchConfigurations.Count > 0);
        }

        public bool IsSetNextToken()
        {
            return (this.nextTokenField != null);
        }

        public DescribeLaunchConfigurationsResult WithLaunchConfigurations(params LaunchConfiguration[] list)
        {
            foreach (LaunchConfiguration configuration in list)
            {
                this.LaunchConfigurations.Add(configuration);
            }
            return this;
        }

        public DescribeLaunchConfigurationsResult WithNextToken(string nextToken)
        {
            this.nextTokenField = nextToken;
            return this;
        }

        [XmlElement(ElementName="LaunchConfigurations")]
        public List<LaunchConfiguration> LaunchConfigurations
        {
            get
            {
                if (this.launchConfigurationsField == null)
                {
                    this.launchConfigurationsField = new List<LaunchConfiguration>();
                }
                return this.launchConfigurationsField;
            }
            set
            {
                this.launchConfigurationsField = value;
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

