namespace Amazon.ElasticLoadBalancing
{
    using Amazon.ElasticLoadBalancing.Model;
    using System;

    public interface AmazonElasticLoadBalancing : IDisposable
    {
        ConfigureHealthCheckResponse ConfigureHealthCheck(ConfigureHealthCheckRequest request);
        CreateAppCookieStickinessPolicyResponse CreateAppCookieStickinessPolicy(CreateAppCookieStickinessPolicyRequest request);
        CreateLBCookieStickinessPolicyResponse CreateLBCookieStickinessPolicy(CreateLBCookieStickinessPolicyRequest request);
        CreateLoadBalancerResponse CreateLoadBalancer(CreateLoadBalancerRequest request);
        DeleteLoadBalancerResponse DeleteLoadBalancer(DeleteLoadBalancerRequest request);
        DeleteLoadBalancerPolicyResponse DeleteLoadBalancerPolicy(DeleteLoadBalancerPolicyRequest request);
        DeregisterInstancesFromLoadBalancerResponse DeregisterInstancesFromLoadBalancer(DeregisterInstancesFromLoadBalancerRequest request);
        DescribeInstanceHealthResponse DescribeInstanceHealth(DescribeInstanceHealthRequest request);
        DescribeLoadBalancersResponse DescribeLoadBalancers(DescribeLoadBalancersRequest request);
        DisableAvailabilityZonesForLoadBalancerResponse DisableAvailabilityZonesForLoadBalancer(DisableAvailabilityZonesForLoadBalancerRequest request);
        EnableAvailabilityZonesForLoadBalancerResponse EnableAvailabilityZonesForLoadBalancer(EnableAvailabilityZonesForLoadBalancerRequest request);
        RegisterInstancesWithLoadBalancerResponse RegisterInstancesWithLoadBalancer(RegisterInstancesWithLoadBalancerRequest request);
        SetLoadBalancerPoliciesOfListenerResponse SetLoadBalancerPoliciesOfListener(SetLoadBalancerPoliciesOfListenerRequest request);
    }
}

