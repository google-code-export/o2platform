namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class RunInstancesResult
    {
        private Amazon.EC2.Model.Reservation reservationField;

        public bool IsSetReservation()
        {
            return (this.reservationField != null);
        }

        public RunInstancesResult WithReservation(Amazon.EC2.Model.Reservation reservation)
        {
            this.reservationField = reservation;
            return this;
        }

        [XmlElement(ElementName="Reservation")]
        public Amazon.EC2.Model.Reservation Reservation
        {
            get
            {
                return this.reservationField;
            }
            set
            {
                this.reservationField = value;
            }
        }
    }
}

