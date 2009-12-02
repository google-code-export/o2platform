using O2.Kernel.Interfaces.Tasks;

namespace O2.Kernel.Interfaces.Tasks
{
    public class TaskEvents
    {
        #region Delegates

        public delegate void TaskEvent_ResultsObject(object resultsObject);

        public delegate void TaskEvent_StatusChanged(ITask task, TaskStatus taskStatus);

        #endregion
    }
}