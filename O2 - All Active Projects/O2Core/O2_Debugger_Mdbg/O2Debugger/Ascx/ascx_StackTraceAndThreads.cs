// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Kernel.Interfaces.Messages;
using O2.Kernel;
using O2.DotNetWrappers.Windows;
using O2.Debugger.Mdbg.O2Debugger.Objects;

namespace O2.Debugger.Mdbg.O2Debugger.Ascx
{
    public partial class ascx_StackTraceAndThreads : UserControl
    {
        int itemId = 0;

        public ascx_StackTraceAndThreads()
        {
            InitializeComponent();
            onLoad();
        }

        bool runOnLoad = true;

        void onLoad()
        {
            if (runOnLoad && DesignMode == false)
            {
                PublicDI.o2MessageQueue.onMessages += ascx_CurrentFrameDetails_onMessages;
                runOnLoad = false;
            }
        }

        void ascx_CurrentFrameDetails_onMessages(IO2Message o2Message)
        {
            if (o2Message is IM_O2MdbgAction)
            {
                var o2MDbgAction = (IM_O2MdbgAction)o2Message;
                switch (o2MDbgAction.o2MdbgAction)
                {
                    case IM_O2MdbgActions.breakEvent:
                        showThreadsWithStackTrace();
                        //DI.o2MDbg.executeMDbgCommand(O2MDbgCommands.printLocalVariables());
                        //DI.o2MDbg.executeMDbgCommand("w");
                        break;
                }
            }
        }

        public void showThreadsWithStackTrace()
        {                        
            var threads = DI.o2MDbg.sessionData.Threads;
            tvExecutionArchive.add_Node(O2Forms.newTreeNode(itemId++.ToString(),"", 0, threads));
            showThreadDetails(threads);            
        }

        private void showThreadDetails(List<O2MDbgThread> threads)
        {
            PublicDI.log.debug("showThreadDetails.Start");
            tvThreadsAndStackTrace.clear();
            //var nodesToAdd = new TreeNodeCollection();
            foreach (var thread in threads)
            {
                var threadNode = new TreeNode(thread.Id.ToString() + " - " + thread.Number.ToString());
                var threadProperties = new TreeNode("Properties");
                var threadStack_FunctionsRef = new TreeNode("StackTrace_Functions");
                var threadStack_FilesRef = new TreeNode("StackTrace_FilesRef");
                var threadVariables = new TreeNode("Variables");
                // Properties
                threadNode.Nodes.Add(threadProperties);
                foreach (var property in DI.reflection.getProperties(thread))
                {
                    var noteText = String.Format("{0} = {1}", property.Name, DI.reflection.getProperty(property, thread));
                    threadProperties.Nodes.Add(noteText);
                }

                // threadStack_FilesRef
                threadNode.Nodes.Add(threadStack_FilesRef);
                foreach (var sourceCodeMapping in thread.sourceCodeMappings)
                    threadStack_FilesRef.Nodes.Add(sourceCodeMapping.FileName + " : " + sourceCodeMapping.Line);

                // threadStack_FunctionsRef
                threadNode.Nodes.Add(threadStack_FunctionsRef);
                foreach (var functionName in thread.stackTrace)
                    threadStack_FunctionsRef.Nodes.Add(functionName);                

                // Variables
                threadNode.Nodes.Add(threadVariables);
                foreach (var variable in thread.o2MDbgvariables)
                {
                    var nodeText = string.Format("{0} = {1}", variable.name, variable.value);
                    threadVariables.Nodes.Add(nodeText);
                }


                tvThreadsAndStackTrace.add_Node(threadNode);
            }
            tvThreadsAndStackTrace.expandAll();
            PublicDI.log.debug("showThreadDetails.End");
        }

        public void showFrameThreads()
        {
            tvThreadsAndStackTrace.Nodes.Add("asd");
            var threads = DI.o2MDbg.ActiveProcess.Threads;
            foreach (O2.Debugger.Mdbg.Debugging.MdbgEngine.MDbgThread thread in threads)
            {
              //  thread.
                var type = thread.GetType().FullName;
            }
        }

        private void tvExecutionArchive_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvExecutionArchive.SelectedNode != null && tvExecutionArchive.SelectedNode.Tag != null && tvExecutionArchive.SelectedNode.Tag is List<O2MDbgThread>)
            {
                var threads = (List<O2MDbgThread>)tvExecutionArchive.SelectedNode.Tag;
                if (threads.Count > 0)
                {
                    if (threads[0].sourceCodeMappings != null && threads[0].sourceCodeMappings.Count > 0)
                    {
                        var file = threads[0].sourceCodeMappings[0].FileName;
                        var lineNumber = threads[0].sourceCodeMappings[0].Line;
                        O2.Kernel.CodeUtils.O2Messages.fileOrFolderSelected(file, lineNumber);
                    }
                    showThreadDetails(threads);
                    O2Forms.SetFocusOnControl(this, 50);
                }
            }
        }        
    }
}
