namespace Amazon
{
    using Amazon.AutoScaling;
    using Amazon.CloudFront;
    using Amazon.CloudWatch;
    using Amazon.EC2;
    using Amazon.ElasticLoadBalancing;
    using Amazon.ElasticMapReduce;
    using Amazon.RDS;
    using Amazon.S3;
    using Amazon.SimpleDB;
    using Amazon.SimpleNotificationService;
    using Amazon.SQS;
    using System;

    public static class AWSClientFactory
    {
        public static AmazonAutoScaling CreateAmazonAutoScalingClient(string awsAccessKey, string awsSecretAccessKey)
        {
            return new AmazonAutoScalingClient(awsAccessKey, awsSecretAccessKey);
        }

        public static AmazonAutoScaling CreateAmazonAutoScalingClient(string awsAccessKey, string awsSecretAccessKey, AmazonAutoScalingConfig config)
        {
            return new AmazonAutoScalingClient(awsAccessKey, awsSecretAccessKey, config);
        }

        public static AmazonCloudFront CreateAmazonCloudFrontClient(string awsAccessKey, string awsSecretAccessKey)
        {
            return new AmazonCloudFrontClient(awsAccessKey, awsSecretAccessKey);
        }

        public static AmazonCloudFront CreateAmazonCloudFrontClient(string awsAccessKey, string awsSecretAccessKey, AmazonCloudFrontConfig config)
        {
            return new AmazonCloudFrontClient(awsAccessKey, awsSecretAccessKey, config);
        }

        public static AmazonCloudWatch CreateAmazonCloudWatchClient(string awsAccessKey, string awsSecretAccessKey)
        {
            return new AmazonCloudWatchClient(awsAccessKey, awsSecretAccessKey);
        }

        public static AmazonCloudWatch CreateAmazonCloudWatchClient(string awsAccessKey, string awsSecretAccessKey, AmazonCloudWatchConfig config)
        {
            return new AmazonCloudWatchClient(awsAccessKey, awsSecretAccessKey, config);
        }

        public static AmazonEC2 CreateAmazonEC2Client(string awsAccessKey, string awsSecretAccessKey)
        {
            return new AmazonEC2Client(awsAccessKey, awsSecretAccessKey);
        }

        public static AmazonEC2 CreateAmazonEC2Client(string awsAccessKey, string awsSecretAccessKey, AmazonEC2Config config)
        {
            return new AmazonEC2Client(awsAccessKey, awsSecretAccessKey, config);
        }

        public static AmazonElasticLoadBalancing CreateAmazonElasticLoadBalancingClient(string awsAccessKey, string awsSecretAccessKey)
        {
            return new AmazonElasticLoadBalancingClient(awsAccessKey, awsSecretAccessKey);
        }

        public static AmazonElasticLoadBalancing CreateAmazonElasticLoadBalancingClient(string awsAccessKey, string awsSecretAccessKey, AmazonElasticLoadBalancingConfig config)
        {
            return new AmazonElasticLoadBalancingClient(awsAccessKey, awsSecretAccessKey, config);
        }

        public static AmazonElasticMapReduce CreateAmazonElasticMapReduceClient(string awsAccessKey, string awsSecretAccessKey)
        {
            return new AmazonElasticMapReduceClient(awsAccessKey, awsSecretAccessKey);
        }

        public static AmazonElasticMapReduce CreateAmazonElasticMapReduceClient(string awsAccessKey, string awsSecretAccessKey, AmazonElasticMapReduceConfig config)
        {
            return new AmazonElasticMapReduceClient(awsAccessKey, awsSecretAccessKey, config);
        }

        public static AmazonRDS CreateAmazonRDSClient(string awsAccessKey, string awsSecretAccessKey)
        {
            return new AmazonRDSClient(awsAccessKey, awsSecretAccessKey);
        }

        public static AmazonRDS CreateAmazonRDSClient(string awsAccessKey, string awsSecretAccessKey, AmazonRDSConfig config)
        {
            return new AmazonRDSClient(awsAccessKey, awsSecretAccessKey, config);
        }

        public static AmazonS3 CreateAmazonS3Client(string awsAccessKey, string awsSecretAccessKey)
        {
            return new AmazonS3Client(awsAccessKey, awsSecretAccessKey);
        }

        public static AmazonS3 CreateAmazonS3Client(string awsAccessKey, string awsSecretAccessKey, AmazonS3Config config)
        {
            return new AmazonS3Client(awsAccessKey, awsSecretAccessKey, config);
        }

        public static AmazonSimpleDB CreateAmazonSimpleDBClient(string awsAccessKey, string awsSecretAccessKey)
        {
            return new AmazonSimpleDBClient(awsAccessKey, awsSecretAccessKey);
        }

        public static AmazonSimpleDB CreateAmazonSimpleDBClient(string awsAccessKey, string awsSecretAccessKey, AmazonSimpleDBConfig config)
        {
            return new AmazonSimpleDBClient(awsAccessKey, awsSecretAccessKey, config);
        }

        public static AmazonSimpleNotificationService CreateAmazonSNSClient(string awsAccessKey, string awsSecretAccessKey)
        {
            return new AmazonSimpleNotificationServiceClient(awsAccessKey, awsSecretAccessKey);
        }

        public static AmazonSimpleNotificationService CreateAmazonSNSClient(string awsAccessKey, string awsSecretAccessKey, AmazonSimpleNotificationServiceConfig config)
        {
            return new AmazonSimpleNotificationServiceClient(awsAccessKey, awsSecretAccessKey, config);
        }

        public static AmazonSQS CreateAmazonSQSClient(string awsAccessKey, string awsSecretAccessKey)
        {
            return new AmazonSQSClient(awsAccessKey, awsSecretAccessKey);
        }

        public static AmazonSQS CreateAmazonSQSClient(string awsAccessKey, string awsSecretAccessKey, AmazonSQSConfig config)
        {
            return new AmazonSQSClient(awsAccessKey, awsSecretAccessKey, config);
        }
    }
}

