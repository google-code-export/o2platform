namespace Amazon.CloudFront.Model
{
    using System;

    public class GetStreamingDistributionInfoRequest : CloudFrontRequest
    {
        public GetStreamingDistributionInfoRequest WithId(string id)
        {
            base.distId = id;
            return this;
        }

        public override string Id
        {
            get
            {
                return base.distId;
            }
            set
            {
                base.distId = value;
            }
        }
    }
}

