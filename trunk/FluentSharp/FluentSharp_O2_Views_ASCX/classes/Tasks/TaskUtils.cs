// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FluentSharp.O2.Interfaces.Tasks;
using FluentSharp.O2.Kernel.CodeUtils;
using FluentSharp.O2.Views.ASCX;
using FluentSharp.O2.Views.ASCX.classes.Tasks;
using FluentSharp.O2.Views.ASCX.classes.TasksWrappers;
using FluentSharp.O2.Views.ASCX.Tasks;

namespace FluentSharp.O2.Views.ASCX.classes.Tasks
{
    public class TaskUtils
    {

        public static void unzipFileAndInvokeCallback(string assessmentFile, FlowLayoutPanel taskHostControl,
                                                      Callbacks.dMethod_String callbackToInvokeForEachUnzippedFile)
        {
            executeTask(new Task_Unzip(assessmentFile, DI.config.TempFolderInTempDirectory),
                        taskHostControl,
                        resultObject =>
                            {
                                if (resultObject is List<string>)
                                    foreach (string item in (List<string>)resultObject)
                                        callbackToInvokeForEachUnzippedFile(item);
                            });
        }


        public static void executeTask(ITask task, TaskEvents.TaskEvent_StatusChanged onTaskStatusChange)
        {
            executeTask(task, null, onTaskStatusChange);
        }

        public static void executeTask(ITask task, Control controlToHostTask,
                                       TaskEvents.TaskEvent_StatusChanged onTaskStatusChange)
        {
            if (controlToHostTask != null && controlToHostTask.InvokeRequired)
                controlToHostTask.Invoke(new EventHandler(delegate
                                                              {
                                                                  executeTask(task, controlToHostTask,
                                                                              onTaskStatusChange);
                                                              }));
            else
            {
                task.onTaskStatusChange += onTaskStatusChange;
                var taskControl = new ascx_Task(new TaskThread(task));
                if (controlToHostTask != null)
                    controlToHostTask.Controls.Add(taskControl);
                taskControl.startTask();
            }
        }

        public static void executeTask(ITask task, TaskEvents.TaskEvent_ResultsObject onTaskExecutionCompletion)
        {
            executeTask(task, null, onTaskExecutionCompletion);
        }

        public static void executeTask(ITask task, Control controlToHostTask,
                                       TaskEvents.TaskEvent_ResultsObject onTaskExecutionCompletion)
        {
            if (controlToHostTask != null && controlToHostTask.InvokeRequired)
                controlToHostTask.Invoke(new EventHandler(delegate
                                                              {
                                                                  executeTask(task, controlToHostTask,
                                                                              onTaskExecutionCompletion);
                                                              }));
            else
            {
                task.onTaskExecutionCompletion += onTaskExecutionCompletion;

                var taskControl = new ascx_Task(new TaskThread(task));
                if (controlToHostTask != null)
                    controlToHostTask.Controls.Add(taskControl);
                taskControl.startTask();
            }
        }
    }
}
