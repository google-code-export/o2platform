namespace Amazon.RDS.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class DescribeEventsResult
    {
        private List<Amazon.RDS.Model.Event> eventField;
        private string markerField;

        public bool IsSetEvent()
        {
            return (this.Event.Count > 0);
        }

        public bool IsSetMarker()
        {
            return (this.markerField != null);
        }

        public DescribeEventsResult WithEvent(params Amazon.RDS.Model.Event[] list)
        {
            foreach (Amazon.RDS.Model.Event event2 in list)
            {
                this.Event.Add(event2);
            }
            return this;
        }

        public DescribeEventsResult WithMarker(string marker)
        {
            this.markerField = marker;
            return this;
        }

        [XmlElement(ElementName="Event")]
        public List<Amazon.RDS.Model.Event> Event
        {
            get
            {
                if (this.eventField == null)
                {
                    this.eventField = new List<Amazon.RDS.Model.Event>();
                }
                return this.eventField;
            }
            set
            {
                this.eventField = value;
            }
        }

        [XmlElement(ElementName="Marker")]
        public string Marker
        {
            get
            {
                return this.markerField;
            }
            set
            {
                this.markerField = value;
            }
        }
    }
}

