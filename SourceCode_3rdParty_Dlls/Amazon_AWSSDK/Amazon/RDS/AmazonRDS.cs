namespace Amazon.RDS
{
    using Amazon.RDS.Model;
    using System;

    public interface AmazonRDS : IDisposable
    {
        AuthorizeDBSecurityGroupIngressResponse AuthorizeDBSecurityGroupIngress(AuthorizeDBSecurityGroupIngressRequest request);
        CreateDBInstanceResponse CreateDBInstance(CreateDBInstanceRequest request);
        CreateDBParameterGroupResponse CreateDBParameterGroup(CreateDBParameterGroupRequest request);
        CreateDBSecurityGroupResponse CreateDBSecurityGroup(CreateDBSecurityGroupRequest request);
        CreateDBSnapshotResponse CreateDBSnapshot(CreateDBSnapshotRequest request);
        DeleteDBInstanceResponse DeleteDBInstance(DeleteDBInstanceRequest request);
        DeleteDBParameterGroupResponse DeleteDBParameterGroup(DeleteDBParameterGroupRequest request);
        DeleteDBSecurityGroupResponse DeleteDBSecurityGroup(DeleteDBSecurityGroupRequest request);
        DeleteDBSnapshotResponse DeleteDBSnapshot(DeleteDBSnapshotRequest request);
        DescribeDBInstancesResponse DescribeDBInstances(DescribeDBInstancesRequest request);
        DescribeDBParameterGroupsResponse DescribeDBParameterGroups(DescribeDBParameterGroupsRequest request);
        DescribeDBParametersResponse DescribeDBParameters(DescribeDBParametersRequest request);
        DescribeDBSecurityGroupsResponse DescribeDBSecurityGroups(DescribeDBSecurityGroupsRequest request);
        DescribeDBSnapshotsResponse DescribeDBSnapshots(DescribeDBSnapshotsRequest request);
        DescribeEngineDefaultParametersResponse DescribeEngineDefaultParameters(DescribeEngineDefaultParametersRequest request);
        DescribeEventsResponse DescribeEvents(DescribeEventsRequest request);
        ModifyDBInstanceResponse ModifyDBInstance(ModifyDBInstanceRequest request);
        ModifyDBParameterGroupResponse ModifyDBParameterGroup(ModifyDBParameterGroupRequest request);
        RebootDBInstanceResponse RebootDBInstance(RebootDBInstanceRequest request);
        ResetDBParameterGroupResponse ResetDBParameterGroup(ResetDBParameterGroupRequest request);
        RestoreDBInstanceFromDBSnapshotResponse RestoreDBInstanceFromDBSnapshot(RestoreDBInstanceFromDBSnapshotRequest request);
        RestoreDBInstanceToPointInTimeResponse RestoreDBInstanceToPointInTime(RestoreDBInstanceToPointInTimeRequest request);
        RevokeDBSecurityGroupIngressResponse RevokeDBSecurityGroupIngress(RevokeDBSecurityGroupIngressRequest request);
    }
}

