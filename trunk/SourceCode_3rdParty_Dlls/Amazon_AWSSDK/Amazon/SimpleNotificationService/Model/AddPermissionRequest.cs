namespace Amazon.SimpleNotificationService.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sns.amazonaws.com/doc/2010-03-31/", IsNullable=false)]
    public class AddPermissionRequest
    {
        private List<string> actionNamesField;
        private List<string> AWSAccountIdsField;
        private string labelField;
        private string topicArnField;

        public bool IsSetActionNames()
        {
            return (this.ActionNames.Count > 0);
        }

        public bool IsSetAWSAccountIds()
        {
            return (this.AWSAccountIds.Count > 0);
        }

        public bool IsSetLabel()
        {
            return (this.labelField != null);
        }

        public bool IsSetTopicArn()
        {
            return (this.topicArnField != null);
        }

        public AddPermissionRequest WithActionNames(params string[] list)
        {
            foreach (string str in list)
            {
                this.ActionNames.Add(str);
            }
            return this;
        }

        public AddPermissionRequest WithAWSAccountIds(params string[] list)
        {
            foreach (string str in list)
            {
                this.AWSAccountIds.Add(str);
            }
            return this;
        }

        public AddPermissionRequest WithLabel(string label)
        {
            this.labelField = label;
            return this;
        }

        public AddPermissionRequest WithTopicArn(string topicArn)
        {
            this.topicArnField = topicArn;
            return this;
        }

        [XmlElement(ElementName="ActionNames")]
        public List<string> ActionNames
        {
            get
            {
                if (this.actionNamesField == null)
                {
                    this.actionNamesField = new List<string>();
                }
                return this.actionNamesField;
            }
            set
            {
                this.actionNamesField = value;
            }
        }

        [XmlElement(ElementName="AWSAccountIds")]
        public List<string> AWSAccountIds
        {
            get
            {
                if (this.AWSAccountIdsField == null)
                {
                    this.AWSAccountIdsField = new List<string>();
                }
                return this.AWSAccountIdsField;
            }
            set
            {
                this.AWSAccountIdsField = value;
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

        [XmlElement(ElementName="TopicArn")]
        public string TopicArn
        {
            get
            {
                return this.topicArnField;
            }
            set
            {
                this.topicArnField = value;
            }
        }
    }
}

