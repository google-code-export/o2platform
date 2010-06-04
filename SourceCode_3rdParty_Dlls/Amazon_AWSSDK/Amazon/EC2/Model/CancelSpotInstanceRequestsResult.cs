namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class CancelSpotInstanceRequestsResult
    {
        private List<Amazon.EC2.Model.CancelledSpotInstanceRequest> cancelledSpotInstanceRequestField;

        public bool IsSetCancelledSpotInstanceRequest()
        {
            return (this.CancelledSpotInstanceRequest.Count > 0);
        }

        public CancelSpotInstanceRequestsResult WithCancelledSpotInstanceRequest(params Amazon.EC2.Model.CancelledSpotInstanceRequest[] list)
        {
            foreach (Amazon.EC2.Model.CancelledSpotInstanceRequest request in list)
            {
                this.CancelledSpotInstanceRequest.Add(request);
            }
            return this;
        }

        [XmlElement(ElementName="CancelledSpotInstanceRequest")]
        public List<Amazon.EC2.Model.CancelledSpotInstanceRequest> CancelledSpotInstanceRequest
        {
            get
            {
                if (this.cancelledSpotInstanceRequestField == null)
                {
                    this.cancelledSpotInstanceRequestField = new List<Amazon.EC2.Model.CancelledSpotInstanceRequest>();
                }
                return this.cancelledSpotInstanceRequestField;
            }
            set
            {
                this.cancelledSpotInstanceRequestField = value;
            }
        }
    }
}

