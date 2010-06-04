namespace Amazon.S3
{
    using Amazon.S3.Model;
    using System;

    public interface AmazonS3 : IDisposable
    {
        CopyObjectResponse CopyObject(CopyObjectRequest request);
        DeleteBucketResponse DeleteBucket(DeleteBucketRequest request);
        DeleteObjectResponse DeleteObject(DeleteObjectRequest request);
        DisableBucketLoggingResponse DisableBucketLogging(DisableBucketLoggingRequest request);
        EnableBucketLoggingResponse EnableBucketLogging(EnableBucketLoggingRequest request);
        GetACLResponse GetACL(GetACLRequest request);
        GetBucketLocationResponse GetBucketLocation(GetBucketLocationRequest request);
        GetBucketLoggingResponse GetBucketLogging(GetBucketLoggingRequest request);
        GetBucketVersioningResponse GetBucketVersioning(GetBucketVersioningRequest request);
        GetObjectResponse GetObject(GetObjectRequest request);
        GetObjectMetadataResponse GetObjectMetadata(GetObjectMetadataRequest request);
        string GetPreSignedURL(GetPreSignedUrlRequest request);
        ListBucketsResponse ListBuckets();
        ListBucketsResponse ListBuckets(ListBucketsRequest request);
        ListObjectsResponse ListObjects(ListObjectsRequest request);
        ListVersionsResponse ListVersions(ListVersionsRequest request);
        PutBucketResponse PutBucket(PutBucketRequest request);
        PutObjectResponse PutObject(PutObjectRequest request);
        SetACLResponse SetACL(SetACLRequest request);
        SetBucketVersioningResponse SetBucketVersioning(SetBucketVersioningRequest request);
    }
}

