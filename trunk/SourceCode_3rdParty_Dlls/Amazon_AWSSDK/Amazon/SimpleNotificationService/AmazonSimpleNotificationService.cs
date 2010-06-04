namespace Amazon.SimpleNotificationService
{
    using Amazon.SimpleNotificationService.Model;
    using System;

    public interface AmazonSimpleNotificationService : IDisposable
    {
        AddPermissionResponse AddPermission(AddPermissionRequest request);
        ConfirmSubscriptionResponse ConfirmSubscription(ConfirmSubscriptionRequest request);
        CreateTopicResponse CreateTopic(CreateTopicRequest request);
        DeleteTopicResponse DeleteTopic(DeleteTopicRequest request);
        GetTopicAttributesResponse GetTopicAttributes(GetTopicAttributesRequest request);
        ListSubscriptionsResponse ListSubscriptions(ListSubscriptionsRequest request);
        ListSubscriptionsByTopicResponse ListSubscriptionsByTopic(ListSubscriptionsByTopicRequest request);
        ListTopicsResponse ListTopics(ListTopicsRequest request);
        PublishResponse Publish(PublishRequest request);
        RemovePermissionResponse RemovePermission(RemovePermissionRequest request);
        SetTopicAttributesResponse SetTopicAttributes(SetTopicAttributesRequest request);
        SubscribeResponse Subscribe(SubscribeRequest request);
        UnsubscribeResponse Unsubscribe(UnsubscribeRequest request);
    }
}

