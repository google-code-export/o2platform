namespace O2.Kernel.Interfaces.Tasks
{
    public interface ITaskControl
    {        

        void setProgressBarMaximum(int value);
        int getProgressBarMaximum();

        void setProgressBarValue(int value);
        void incProgressBarValue();

        void setStartLinkVisibleStatus(bool value);
        void startExecutionTimeCounterThread();
        

        void startTask();        
    }
}