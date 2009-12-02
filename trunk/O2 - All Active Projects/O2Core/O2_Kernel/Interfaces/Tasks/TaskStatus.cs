namespace O2.Kernel.Interfaces.Tasks
{
    public enum TaskStatus
    {
        No_Task_Defined,
        Ready,
        Initializing_Objects,
        Executing,
        Waiting,
        Uploading_Data_To_Remote_Server,
        Waiting_For_Remote_Server_Response,
        Downloading_Data_From_Remote_Server,
        Completed_OK,
        Completed_Failed
    }
}