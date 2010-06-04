namespace Amazon.CloudFront.Model
{
    using System;
    using System.Xml.Serialization;

    public class ListOriginAccessIdentitiesRequest : CloudFrontRequest
    {
        public ListOriginAccessIdentitiesRequest WithMarker(string marker)
        {
            base.reqMarker = marker;
            return this;
        }

        public ListOriginAccessIdentitiesRequest WithMaxItems(string maxItems)
        {
            base.reqMaxItems = maxItems;
            return this;
        }

        [XmlElement(ElementName="Marker")]
        public override string Marker
        {
            get
            {
                return base.reqMarker;
            }
            set
            {
                base.reqMarker = value;
            }
        }

        [XmlElement(ElementName="MaxItems")]
        public override string MaxItems
        {
            get
            {
                return base.reqMaxItems;
            }
            set
            {
                base.reqMaxItems = value;
            }
        }
    }
}

