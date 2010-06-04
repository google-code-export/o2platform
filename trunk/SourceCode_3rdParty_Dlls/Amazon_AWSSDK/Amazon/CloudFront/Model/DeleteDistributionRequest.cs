namespace Amazon.CloudFront.Model
{
    using System;

    public class DeleteDistributionRequest : CloudFrontRequest
    {
        public DeleteDistributionRequest WithETag(string etag)
        {
            base.etagHeader = etag;
            return this;
        }

        public DeleteDistributionRequest WithId(string id)
        {
            base.distId = id;
            return this;
        }

        public override string ETag
        {
            get
            {
                return base.etagHeader;
            }
            set
            {
                base.etagHeader = value;
            }
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

