namespace Amazon.AutoScaling
{
    using Amazon.AutoScaling.Model;
    using System;

    public interface AmazonAutoScaling : IDisposable
    {
        CreateAutoScalingGroupResponse CreateAutoScalingGroup(CreateAutoScalingGroupRequest request);
        CreateLaunchConfigurationResponse CreateLaunchConfiguration(CreateLaunchConfigurationRequest request);
        CreateOrUpdateScalingTriggerResponse CreateOrUpdateScalingTrigger(CreateOrUpdateScalingTriggerRequest request);
        DeleteAutoScalingGroupResponse DeleteAutoScalingGroup(DeleteAutoScalingGroupRequest request);
        DeleteLaunchConfigurationResponse DeleteLaunchConfiguration(DeleteLaunchConfigurationRequest request);
        DeleteTriggerResponse DeleteTrigger(DeleteTriggerRequest request);
        DescribeAutoScalingGroupsResponse DescribeAutoScalingGroups(DescribeAutoScalingGroupsRequest request);
        DescribeLaunchConfigurationsResponse DescribeLaunchConfigurations(DescribeLaunchConfigurationsRequest request);
        DescribeScalingActivitiesResponse DescribeScalingActivities(DescribeScalingActivitiesRequest request);
        DescribeTriggersResponse DescribeTriggers(DescribeTriggersRequest request);
        SetDesiredCapacityResponse SetDesiredCapacity(SetDesiredCapacityRequest request);
        TerminateInstanceInAutoScalingGroupResponse TerminateInstanceInAutoScalingGroup(TerminateInstanceInAutoScalingGroupRequest request);
        UpdateAutoScalingGroupResponse UpdateAutoScalingGroup(UpdateAutoScalingGroupRequest request);
    }
}

