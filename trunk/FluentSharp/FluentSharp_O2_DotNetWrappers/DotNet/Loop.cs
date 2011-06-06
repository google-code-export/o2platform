using System;
using System.Windows.Forms;
using FluentSharp.O2.DotNetWrappers.Windows;

namespace FluentSharp.O2.DotNetWrappers.DotNet
{
    public static class Loop
    {
        public static void nTimesWithDelay(int count, int delay, MethodInvoker methodInvoker)
        {
            nTimesWithDelay(count, delay, true, methodInvoker);
        }

        public static void nTimesWithDelay(int count, int delay, bool runInMtaThread, MethodInvoker methodInvoker)
        {
            if (runInMtaThread)
                O2Thread.mtaThread(() => nTimesWithDelay(count, delay, false, methodInvoker));
            else
                for (int i = 0; i < count; i++)
                {
                    methodInvoker();
                    Processes.Sleep(delay);
                }
        }

        public static void nTimes(int count, Action<int> methodInvoker)
        {
            for (int i = 0; i < count; i++)
                methodInvoker(i);
        }
    }

}
