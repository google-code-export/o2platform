namespace Amazon.ElasticMapReduce
{
    using Amazon.ElasticMapReduce.Model;
    using System;

    public interface AmazonElasticMapReduce : IDisposable
    {
        AddJobFlowStepsResponse AddJobFlowSteps(AddJobFlowStepsRequest request);
        DescribeJobFlowsResponse DescribeJobFlows(DescribeJobFlowsRequest request);
        RunJobFlowResponse RunJobFlow(RunJobFlowRequest request);
        TerminateJobFlowsResponse TerminateJobFlows(TerminateJobFlowsRequest request);
    }
}

