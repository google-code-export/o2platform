namespace Amazon.AutoScaling.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://autoscaling.amazonaws.com/doc/2009-05-15/", IsNullable=false)]
    public class DescribeScalingActivitiesResult
    {
        private List<Activity> activitiesField;
        private string nextTokenField;

        public bool IsSetActivities()
        {
            return (this.Activities.Count > 0);
        }

        public bool IsSetNextToken()
        {
            return (this.nextTokenField != null);
        }

        public DescribeScalingActivitiesResult WithActivities(params Activity[] list)
        {
            foreach (Activity activity in list)
            {
                this.Activities.Add(activity);
            }
            return this;
        }

        public DescribeScalingActivitiesResult WithNextToken(string nextToken)
        {
            this.nextTokenField = nextToken;
            return this;
        }

        [XmlElement(ElementName="Activities")]
        public List<Activity> Activities
        {
            get
            {
                if (this.activitiesField == null)
                {
                    this.activitiesField = new List<Activity>();
                }
                return this.activitiesField;
            }
            set
            {
                this.activitiesField = value;
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

