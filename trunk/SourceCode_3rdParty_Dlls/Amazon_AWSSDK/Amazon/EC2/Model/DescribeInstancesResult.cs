namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeInstancesResult
    {
        private List<Amazon.EC2.Model.Reservation> reservationField;

        public bool IsSetReservation()
        {
            return (this.Reservation.Count > 0);
        }

        public DescribeInstancesResult WithReservation(params Amazon.EC2.Model.Reservation[] list)
        {
            foreach (Amazon.EC2.Model.Reservation reservation in list)
            {
                this.Reservation.Add(reservation);
            }
            return this;
        }

        [XmlElement(ElementName="Reservation")]
        public List<Amazon.EC2.Model.Reservation> Reservation
        {
            get
            {
                if (this.reservationField == null)
                {
                    this.reservationField = new List<Amazon.EC2.Model.Reservation>();
                }
                return this.reservationField;
            }
            set
            {
                this.reservationField = value;
            }
        }
    }
}

