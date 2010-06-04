namespace Amazon.CloudFront.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://cloudfront.amazonaws.com/doc/2010-03-01/", IsNullable=false)]
    public class ListDistributionsResponse : CloudFrontResponse
    {
        private List<CloudFrontDistribution> distributions = new List<CloudFrontDistribution>();
        private bool isTruncated;
        private string marker;
        private string maxItems;
        private string nextMarker;

        [XmlElement(ElementName="Distribution")]
        public List<CloudFrontDistribution> Distribution
        {
            get
            {
                return this.distributions;
            }
        }

        [XmlElement(ElementName="IsTruncated")]
        public bool IsTruncated
        {
            get
            {
                return this.isTruncated;
            }
            set
            {
                this.isTruncated = value;
            }
        }

        [XmlElement(ElementName="Marker")]
        public string Marker
        {
            get
            {
                return this.marker;
            }
            set
            {
                this.marker = value;
            }
        }

        [XmlElement(ElementName="MaxItems")]
        public string MaxItems
        {
            get
            {
                return this.maxItems;
            }
            set
            {
                this.maxItems = value;
            }
        }

        [XmlElement(ElementName="NextMarker")]
        public string NextMarker
        {
            get
            {
                return this.nextMarker;
            }
            set
            {
                this.nextMarker = value;
            }
        }
    }
}

