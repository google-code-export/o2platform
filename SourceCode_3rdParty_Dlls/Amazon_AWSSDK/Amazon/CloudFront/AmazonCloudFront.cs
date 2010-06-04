namespace Amazon.CloudFront
{
    using Amazon.CloudFront.Model;
    using System;

    public interface AmazonCloudFront : IDisposable
    {
        CreateDistributionResponse CreateDistribution(CreateDistributionRequest request);
        CreateOriginAccessIdentityResponse CreateOriginAccessIdentity(CreateOriginAccessIdentityRequest request);
        CreateStreamingDistributionResponse CreateStreamingDistribution(CreateStreamingDistributionRequest request);
        DeleteDistributionResponse DeleteDistribution(DeleteDistributionRequest request);
        DeleteOriginAccessIdentityResponse DeleteOriginAccessIdentity(DeleteOriginAccessIdentityRequest request);
        DeleteStreamingDistributionResponse DeleteStreamingDistribution(DeleteStreamingDistributionRequest request);
        GetDistributionConfigResponse GetDistributionConfig(GetDistributionConfigRequest request);
        GetDistributionInfoResponse GetDistributionInfo(GetDistributionInfoRequest request);
        GetOriginAccessIdentityConfigResponse GetOriginAccessIdentityConfig(GetOriginAccessIdentityConfigRequest request);
        GetOriginAccessIdentityInfoResponse GetOriginAccessIdentityInfo(GetOriginAccessIdentityInfoRequest request);
        GetStreamingDistributionConfigResponse GetStreamingDistributionConfig(GetStreamingDistributionConfigRequest request);
        GetStreamingDistributionInfoResponse GetStreamingDistributionInfo(GetStreamingDistributionInfoRequest request);
        ListDistributionsResponse ListDistributions();
        ListDistributionsResponse ListDistributions(ListDistributionsRequest request);
        ListOriginAccessIdentitiesResponse ListOriginAccessIdentities();
        ListOriginAccessIdentitiesResponse ListOriginAccessIdentities(ListOriginAccessIdentitiesRequest request);
        ListStreamingDistributionsResponse ListStreamingDistributions();
        ListStreamingDistributionsResponse ListStreamingDistributions(ListStreamingDistributionsRequest request);
        SetDistributionConfigResponse SetDistributionConfig(SetDistributionConfigRequest request);
        SetOriginAccessIdentityConfigResponse SetOriginAccessIdentityConfig(SetOriginAccessIdentityConfigRequest request);
        SetStreamingDistributionConfigResponse SetStreamingDistributionConfig(SetStreamingDistributionConfigRequest request);
    }
}

