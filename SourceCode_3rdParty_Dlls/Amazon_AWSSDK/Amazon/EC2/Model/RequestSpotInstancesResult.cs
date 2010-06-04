namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class RequestSpotInstancesResult
    {
        private List<Amazon.EC2.Model.SpotInstanceRequest> spotInstanceRequestField;

        public bool IsSetSpotInstanceRequest()
        {
            return (this.SpotInstanceRequest.Count > 0);
        }

        public RequestSpotInstancesResult WithSpotInstanceRequest(params Amazon.EC2.Model.SpotInstanceRequest[] list)
        {
            foreach (Amazon.EC2.Model.SpotInstanceRequest request in list)
            {
                this.SpotInstanceRequest.Add(request);
            }
            return this;
        }

        [XmlElement(ElementName="SpotInstanceRequest")]
        public List<Amazon.EC2.Model.SpotInstanceRequest> SpotInstanceRequest
        {
            get
            {
                if (this.spotInstanceRequestField == null)
                {
                    this.spotInstanceRequestField = new List<Amazon.EC2.Model.SpotInstanceRequest>();
                }
                return this.spotInstanceRequestField;
            }
            set
            {
                this.spotInstanceRequestField = value;
            }
        }
    }
}

