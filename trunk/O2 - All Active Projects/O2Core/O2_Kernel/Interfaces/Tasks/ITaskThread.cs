using O2.Kernel.Interfaces.Tasks;

namespace O2.Kernel.Interfaces.Tasks
{
    public interface ITaskThread
    {
        void setTaskControl(ITaskControl _taskControl);
        void start();
        void wait(double secondsToWait);
        ITask getTask();
        bool isTaskCompleted();
    }
}