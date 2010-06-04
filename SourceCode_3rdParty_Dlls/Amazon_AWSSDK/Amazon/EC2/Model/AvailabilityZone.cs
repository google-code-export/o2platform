namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class AvailabilityZone
    {
        private List<string> messageField;
        private string regionNameField;
        private string zoneNameField;
        private string zoneStateField;

        public bool IsSetMessage()
        {
            return (this.Message.Count > 0);
        }

        public bool IsSetRegionName()
        {
            return (this.regionNameField != null);
        }

        public bool IsSetZoneName()
        {
            return (this.zoneNameField != null);
        }

        public bool IsSetZoneState()
        {
            return (this.zoneStateField != null);
        }

        public AvailabilityZone WithMessage(params string[] list)
        {
            foreach (string str in list)
            {
                this.Message.Add(str);
            }
            return this;
        }

        public AvailabilityZone WithRegionName(string regionName)
        {
            this.regionNameField = regionName;
            return this;
        }

        public AvailabilityZone WithZoneName(string zoneName)
        {
            this.zoneNameField = zoneName;
            return this;
        }

        public AvailabilityZone WithZoneState(string zoneState)
        {
            this.zoneStateField = zoneState;
            return this;
        }

        [XmlElement(ElementName="Message")]
        public List<string> Message
        {
            get
            {
                if (this.messageField == null)
                {
                    this.messageField = new List<string>();
                }
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }

        [XmlElement(ElementName="RegionName")]
        public string RegionName
        {
            get
            {
                return this.regionNameField;
            }
            set
            {
                this.regionNameField = value;
            }
        }

        [XmlElement(ElementName="ZoneName")]
        public string ZoneName
        {
            get
            {
                return this.zoneNameField;
            }
            set
            {
                this.zoneNameField = value;
            }
        }

        [XmlElement(ElementName="ZoneState")]
        public string ZoneState
        {
            get
            {
                return this.zoneStateField;
            }
            set
            {
                this.zoneStateField = value;
            }
        }
    }
}

