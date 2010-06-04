namespace Amazon.AutoScaling.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://autoscaling.amazonaws.com/doc/2009-05-15/", IsNullable=false)]
    public class Activity
    {
        private string activityIdField;
        private string causeField;
        private string descriptionField;
        private DateTime? endTimeField;
        private decimal? progressField;
        private DateTime? startTimeField;
        private string statusCodeField;
        private string statusMessageField;

        public bool IsSetActivityId()
        {
            return (this.activityIdField != null);
        }

        public bool IsSetCause()
        {
            return (this.causeField != null);
        }

        public bool IsSetDescription()
        {
            return (this.descriptionField != null);
        }

        public bool IsSetEndTime()
        {
            return this.endTimeField.HasValue;
        }

        public bool IsSetProgress()
        {
            return this.progressField.HasValue;
        }

        public bool IsSetStartTime()
        {
            return this.startTimeField.HasValue;
        }

        public bool IsSetStatusCode()
        {
            return (this.statusCodeField != null);
        }

        public bool IsSetStatusMessage()
        {
            return (this.statusMessageField != null);
        }

        public Activity WithActivityId(string activityId)
        {
            this.activityIdField = activityId;
            return this;
        }

        public Activity WithCause(string cause)
        {
            this.causeField = cause;
            return this;
        }

        public Activity WithDescription(string description)
        {
            this.descriptionField = description;
            return this;
        }

        public Activity WithEndTime(DateTime endTime)
        {
            this.endTimeField = new DateTime?(endTime);
            return this;
        }

        public Activity WithProgress(decimal progress)
        {
            this.progressField = new decimal?(progress);
            return this;
        }

        public Activity WithStartTime(DateTime startTime)
        {
            this.startTimeField = new DateTime?(startTime);
            return this;
        }

        public Activity WithStatusCode(string statusCode)
        {
            this.statusCodeField = statusCode;
            return this;
        }

        public Activity WithStatusMessage(string statusMessage)
        {
            this.statusMessageField = statusMessage;
            return this;
        }

        [XmlElement(ElementName="ActivityId")]
        public string ActivityId
        {
            get
            {
                return this.activityIdField;
            }
            set
            {
                this.activityIdField = value;
            }
        }

        [XmlElement(ElementName="Cause")]
        public string Cause
        {
            get
            {
                return this.causeField;
            }
            set
            {
                this.causeField = value;
            }
        }

        [XmlElement(ElementName="Description")]
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        [XmlElement(ElementName="EndTime")]
        public DateTime EndTime
        {
            get
            {
                return this.endTimeField.GetValueOrDefault();
            }
            set
            {
                this.endTimeField = new DateTime?(value);
            }
        }

        [XmlElement(ElementName="Progress")]
        public decimal Progress
        {
            get
            {
                return this.progressField.GetValueOrDefault();
            }
            set
            {
                this.progressField = new decimal?(value);
            }
        }

        [XmlElement(ElementName="StartTime")]
        public DateTime StartTime
        {
            get
            {
                return this.startTimeField.GetValueOrDefault();
            }
            set
            {
                this.startTimeField = new DateTime?(value);
            }
        }

        [XmlElement(ElementName="StatusCode")]
        public string StatusCode
        {
            get
            {
                return this.statusCodeField;
            }
            set
            {
                this.statusCodeField = value;
            }
        }

        [XmlElement(ElementName="StatusMessage")]
        public string StatusMessage
        {
            get
            {
                return this.statusMessageField;
            }
            set
            {
                this.statusMessageField = value;
            }
        }
    }
}

