// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Threading;

namespace O2.DotNetWrappers.DotNet
{
    public class O2Thread
    {
        #region Delegates

        public delegate Thread FuncThread(); // they forgot to include this one :)
        public delegate void FuncVoid(); // they forgot to include this one :)
        public delegate void FuncVoidT1<T1>(T1 arg1);
        public delegate void FuncVoidT1T2<T1,T2>(T1 arg1,T2 arg2);
        public delegate void FuncVoidT1T2T3<T1, T2,T3>(T1 arg1, T2 arg2, T3 arg3);
        public delegate void FuncVoidT1T2T3T4<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);

        #endregion

        //var threadEnded = new AutoResetEvent(false);

        /*public static void staThreadSync(FuncVoid codeToExecute)
        {            
            var thread = staThread(codeToExecute);
            thread.Join();
            if (thread.IsAlive)
            {
            }
        }*/

        public static Thread staThread(FuncVoid codeToExecute)
        {
            var stackTrace = getCurrentStackTrace();    // used for cross thread debugging purposes
            var staThread = new Thread(() => codeToExecute());
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();            
            return staThread;
        }

       /* public static void mtaThreadSync(FuncVoid codeToExecute)
        {
            var thread = mtaThread(codeToExecute);            
            thread.Join();
            if (thread.IsAlive)
            {
            }
        }*/

        public static Thread mtaThread(FuncVoid codeToExecute)
        {
            return mtaThread("[O2 Mta Thread]", codeToExecute);
        }

        public static Thread mtaThread(string threadName, FuncVoid codeToExecute)
        {
            var stackTrace = getCurrentStackTrace();    // used for cross thread debugging purposes
            var mtaThread = new Thread(() => codeToExecute())
                                {
                                    Name = threadName
                                };
            mtaThread.SetApartmentState(ApartmentState.MTA);
            mtaThread.Start();
            return mtaThread;
        }
        
        public static Thread mtaThread(Semaphore semaphore, FuncVoid codeToExecute)
        {
            var stackTrace = getCurrentStackTrace();    // used for cross thread debugging purposes
            if (semaphore == null)
                return mtaThread(codeToExecute);
                // if no use the mtaThread function with no semaphore support
            else
            {
                var _mtaThread = new Thread(() =>
                                                {
                                                    semaphore.WaitOne();
                                                    codeToExecute();
                                                    semaphore.Release();
                                                });
                _mtaThread.SetApartmentState(ApartmentState.MTA);
                _mtaThread.Start();
                return _mtaThread;
            }
        }

        public static string getCurrentStackTrace()
        {
            return new System.Diagnostics.StackTrace().ToString();
        }
    }
}
