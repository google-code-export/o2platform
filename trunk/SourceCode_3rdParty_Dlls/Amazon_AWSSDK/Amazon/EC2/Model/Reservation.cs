namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class Reservation
    {
        private List<string> groupNameField;
        private string ownerIdField;
        private string requesterIdField;
        private string reservationIdField;
        private List<Amazon.EC2.Model.RunningInstance> runningInstanceField;

        public bool IsSetGroupName()
        {
            return (this.GroupName.Count > 0);
        }

        public bool IsSetOwnerId()
        {
            return (this.ownerIdField != null);
        }

        public bool IsSetRequesterId()
        {
            return (this.requesterIdField != null);
        }

        public bool IsSetReservationId()
        {
            return (this.reservationIdField != null);
        }

        public bool IsSetRunningInstance()
        {
            return (this.RunningInstance.Count > 0);
        }

        public Reservation WithGroupName(params string[] list)
        {
            foreach (string str in list)
            {
                this.GroupName.Add(str);
            }
            return this;
        }

        public Reservation WithOwnerId(string ownerId)
        {
            this.ownerIdField = ownerId;
            return this;
        }

        public Reservation WithRequesterId(string requesterId)
        {
            this.requesterIdField = requesterId;
            return this;
        }

        public Reservation WithReservationId(string reservationId)
        {
            this.reservationIdField = reservationId;
            return this;
        }

        public Reservation WithRunningInstance(params Amazon.EC2.Model.RunningInstance[] list)
        {
            foreach (Amazon.EC2.Model.RunningInstance instance in list)
            {
                this.RunningInstance.Add(instance);
            }
            return this;
        }

        [XmlElement(ElementName="GroupName")]
        public List<string> GroupName
        {
            get
            {
                if (this.groupNameField == null)
                {
                    this.groupNameField = new List<string>();
                }
                return this.groupNameField;
            }
            set
            {
                this.groupNameField = value;
            }
        }

        [XmlElement(ElementName="OwnerId")]
        public string OwnerId
        {
            get
            {
                return this.ownerIdField;
            }
            set
            {
                this.ownerIdField = value;
            }
        }

        [XmlElement(ElementName="RequesterId")]
        public string RequesterId
        {
            get
            {
                return this.requesterIdField;
            }
            set
            {
                this.requesterIdField = value;
            }
        }

        [XmlElement(ElementName="ReservationId")]
        public string ReservationId
        {
            get
            {
                return this.reservationIdField;
            }
            set
            {
                this.reservationIdField = value;
            }
        }

        [XmlElement(ElementName="RunningInstance")]
        public List<Amazon.EC2.Model.RunningInstance> RunningInstance
        {
            get
            {
                if (this.runningInstanceField == null)
                {
                    this.runningInstanceField = new List<Amazon.EC2.Model.RunningInstance>();
                }
                return this.runningInstanceField;
            }
            set
            {
                this.runningInstanceField = value;
            }
        }
    }
}

