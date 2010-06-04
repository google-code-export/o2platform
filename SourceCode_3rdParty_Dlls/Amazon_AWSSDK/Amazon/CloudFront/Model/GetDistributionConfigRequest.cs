namespace Amazon.CloudFront.Model
{
    using System;

    public class GetDistributionConfigRequest : CloudFrontRequest
    {
        public GetDistributionConfigRequest WithId(string id)
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

