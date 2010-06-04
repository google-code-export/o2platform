namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class CancelSpotInstanceRequestsRequest
    {
        private List<string> spotInstanceRequestIdField;

        public bool IsSetSpotInstanceRequestId()
        {
            return (this.SpotInstanceRequestId.Count > 0);
        }

        public CancelSpotInstanceRequestsRequest WithSpotInstanceRequestId(params string[] list)
        {
            foreach (string str in list)
            {
                this.SpotInstanceRequestId.Add(str);
            }
            return this;
        }

        [XmlElement(ElementName="SpotInstanceRequestId")]
        public List<string> SpotInstanceRequestId
        {
            get
            {
                if (this.spotInstanceRequestIdField == null)
                {
                    this.spotInstanceRequestIdField = new List<string>();
                }
                return this.spotInstanceRequestIdField;
            }
            set
            {
                this.spotInstanceRequestIdField = value;
            }
        }
    }
}

