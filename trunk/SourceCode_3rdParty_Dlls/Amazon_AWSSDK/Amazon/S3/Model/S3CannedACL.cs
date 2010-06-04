namespace Amazon.S3.Model
{
    using System;

    public enum S3CannedACL
    {
        NoACL,
        Private,
        PublicRead,
        PublicReadWrite,
        AuthenticatedRead,
        BucketOwnerRead,
        BucketOwnerFullControl
    }
}

