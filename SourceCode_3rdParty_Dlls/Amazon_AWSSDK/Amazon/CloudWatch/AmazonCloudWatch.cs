namespace Amazon.CloudWatch
{
    using Amazon.CloudWatch.Model;
    using System;

    public interface AmazonCloudWatch : IDisposable
    {
        GetMetricStatisticsResponse GetMetricStatistics(GetMetricStatisticsRequest request);
        ListMetricsResponse ListMetrics(ListMetricsRequest request);
    }
}

