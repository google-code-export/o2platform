namespace Amazon.SQS.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://queue.amazonaws.com/doc/2009-02-01/", IsNullable=false)]
    public class AddPermissionRequest
    {
        private List<string> actionNameField;
        private List<string> AWSAccountIdField;
        private string labelField;
        private string queueUrlField;

        public bool IsSetActionName()
        {
            return (this.ActionName.Count > 0);
        }

        public bool IsSetAWSAccountId()
        {
            return (this.AWSAccountId.Count > 0);
        }

        public bool IsSetLabel()
        {
            return (this.labelField != null);
        }

        public bool IsSetQueueUrl()
        {
            return (this.queueUrlField != null);
        }

        public AddPermissionRequest WithActionName(params string[] list)
        {
            foreach (string str in list)
            {
                this.ActionName.Add(str);
            }
            return this;
        }

        public AddPermissionRequest WithAWSAccountId(params string[] list)
        {
            foreach (string str in list)
            {
                this.AWSAccountId.Add(str);
            }
            return this;
        }

        public AddPermissionRequest WithLabel(string label)
        {
            this.labelField = label;
            return this;
        }

        public AddPermissionRequest WithQueueUrl(string queueUrl)
        {
            this.queueUrlField = queueUrl;
            return this;
        }

        [XmlElement(ElementName="ActionName")]
        public List<string> ActionName
        {
            get
            {
                if (this.actionNameField == null)
                {
                    this.actionNameField = new List<string>();
                }
                return this.actionNameField;
            }
            set
            {
                this.actionNameField = value;
            }
        }

        [XmlElement(ElementName="AWSAccountId")]
        public List<string> AWSAccountId
        {
            get
            {
                if (this.AWSAccountIdField == null)
                {
                    this.AWSAccountIdField = new List<string>();
                }
                return this.AWSAccountIdField;
            }
            set
            {
                this.AWSAccountIdField = value;
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

        [XmlElement(ElementName="QueueUrl")]
        public string QueueUrl
        {
            get
            {
                return this.queueUrlField;
            }
            set
            {
                this.queueUrlField = value;
            }
        }
    }
}

