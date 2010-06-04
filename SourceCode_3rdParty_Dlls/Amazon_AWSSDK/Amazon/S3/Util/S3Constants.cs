namespace Amazon.S3.Util
{
    using System;

    internal static class S3Constants
    {
        internal const string AmzAclHeader = "x-amz-acl";
        internal const string AmzDateHeader = "x-amz-date";
        internal const string AmzDeleteMarkerHeader = "x-amz-delete-marker";
        internal const string AmzId2Header = "x-amz-id-2";
        internal const string AmzMetadataDirectiveHeader = "x-amz-metadata-directive";
        internal const string AmzMfaHeader = "x-amz-mfa";
        internal const string AmzRequestIdHeader = "x-amz-request-id";
        internal const string AmzStorageClassHeader = "x-amz-storage-class";
        internal const string AmzVersionIdHeader = "x-amz-version-id";
        internal const string AuthorizationHeader = "Authorization";
        internal static readonly string[] BucketVersions = new string[] { "", "V1", "V2" };
        internal static readonly string[] CannedAcls = new string[] { "", "private", "public-read", "public-read-write", "authenticated-read", "bucket-owner-read", "bucket-owner-full-control" };
        internal const string RequestParam = "request";
        internal const string S3DefaultEndpoint = "s3.amazonaws.com";
        internal static readonly string[] StorageClasses = new string[] { "STANDARD", "REDUCED_REDUNDANCY" };
        internal static readonly string[] Verbs = new string[] { "GET", "HEAD", "PUT", "DELETE" };
        internal const string VersioningEnabled = "Enabled";
        internal const string VersioningOff = "Off";
        internal const string VersioningSuspended = "Suspended";

        internal static readonly string DeleteVerb = Verbs[3];
        internal static readonly string GetVerb = Verbs[0];
        internal static readonly string HeadVerb = Verbs[1];
        internal static readonly string[] LocationConstraints = new string[] { "", "EU", "us-west-1", "ap-southeast-1" };
        internal const int MaxBucketLength = 0x3f;
        internal static readonly long MaxS3ObjectSize = (5L * ((long) Math.Pow(2.0, 30.0)));
        internal static readonly string[] MetaDataDirectives = new string[] { "COPY", "REPLACE" };
        internal const int MinBucketLength = 3;
        internal const int PutObjectDefaultTimeout = 0x124f80;
        internal static readonly string PutVerb = Verbs[2];        
    }
}

