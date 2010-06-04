namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class CancelledSpotInstanceRequest
    {
        private string spotInstanceRequestIdField;
        private string stateField;

        public bool IsSetSpotInstanceRequestId()
        {
            return (this.spotInstanceRequestIdField != null);
        }

        public bool IsSetState()
        {
            return (this.stateField != null);
        }

        public CancelledSpotInstanceRequest WithSpotInstanceRequestId(string spotInstanceRequestId)
        {
            this.spotInstanceRequestIdField = spotInstanceRequestId;
            return this;
        }

        public CancelledSpotInstanceRequest WithState(string state)
        {
            this.stateField = state;
            return this;
        }

        [XmlElement(ElementName="SpotInstanceRequestId")]
        public string SpotInstanceRequestId
        {
            get
            {
                return this.spotInstanceRequestIdField;
            }
            set
            {
                this.spotInstanceRequestIdField = value;
            }
        }

        [XmlElement(ElementName="State")]
        public string State
        {
            get
            {
                return this.stateField;
            }
            set
            {
                this.stateField = value;
            }
        }
    }
}

