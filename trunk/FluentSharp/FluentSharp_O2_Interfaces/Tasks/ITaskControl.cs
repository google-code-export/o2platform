// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
namespace FluentSharp.O2.Interfaces.Tasks
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