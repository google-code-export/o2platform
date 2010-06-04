namespace Amazon.SQS
{
    using Amazon.SQS.Model;
    using System;

    public interface AmazonSQS : IDisposable
    {
        AddPermissionResponse AddPermission(AddPermissionRequest request);
        ChangeMessageVisibilityResponse ChangeMessageVisibility(ChangeMessageVisibilityRequest request);
        CreateQueueResponse CreateQueue(CreateQueueRequest request);
        DeleteMessageResponse DeleteMessage(DeleteMessageRequest request);
        DeleteQueueResponse DeleteQueue(DeleteQueueRequest request);
        GetQueueAttributesResponse GetQueueAttributes(GetQueueAttributesRequest request);
        ListQueuesResponse ListQueues(ListQueuesRequest request);
        ReceiveMessageResponse ReceiveMessage(ReceiveMessageRequest request);
        RemovePermissionResponse RemovePermission(RemovePermissionRequest request);
        SendMessageResponse SendMessage(SendMessageRequest request);
        SetQueueAttributesResponse SetQueueAttributes(SetQueueAttributesRequest request);
    }
}

