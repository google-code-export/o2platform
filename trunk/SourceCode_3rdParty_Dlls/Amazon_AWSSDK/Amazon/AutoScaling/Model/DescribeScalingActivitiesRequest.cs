namespace Amazon.AutoScaling.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://autoscaling.amazonaws.com/doc/2009-05-15/", IsNullable=false)]
    public class DescribeScalingActivitiesRequest
    {
        private List<string> activityIdsField;
        private string autoScalingGroupNameField;
        private decimal? maxRecordsField;
        private string nextTokenField;

        public bool IsSetActivityIds()
        {
            return (this.ActivityIds.Count > 0);
        }

        public bool IsSetAutoScalingGroupName()
        {
            return (this.autoScalingGroupNameField != null);
        }

        public bool IsSetMaxRecords()
        {
            return this.maxRecordsField.HasValue;
        }

        public bool IsSetNextToken()
        {
            return (this.nextTokenField != null);
        }

        public DescribeScalingActivitiesRequest WithActivityIds(params string[] list)
        {
            foreach (string str in list)
            {
                this.ActivityIds.Add(str);
            }
            return this;
        }

        public DescribeScalingActivitiesRequest WithAutoScalingGroupName(string autoScalingGroupName)
        {
            this.autoScalingGroupNameField = autoScalingGroupName;
            return this;
        }

        public DescribeScalingActivitiesRequest WithMaxRecords(decimal maxRecords)
        {
            this.maxRecordsField = new decimal?(maxRecords);
            return this;
        }

        public DescribeScalingActivitiesRequest WithNextToken(string nextToken)
        {
            this.nextTokenField = nextToken;
            return this;
        }

        [XmlElement(ElementName="ActivityIds")]
        public List<string> ActivityIds
        {
            get
            {
                if (this.activityIdsField == null)
                {
                    this.activityIdsField = new List<string>();
                }
                return this.activityIdsField;
            }
            set
            {
                this.activityIdsField = value;
            }
        }

        [XmlElement(ElementName="AutoScalingGroupName")]
        public string AutoScalingGroupName
        {
            get
            {
                return this.autoScalingGroupNameField;
            }
            set
            {
                this.autoScalingGroupNameField = value;
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

