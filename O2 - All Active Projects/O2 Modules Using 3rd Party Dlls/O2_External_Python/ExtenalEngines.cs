// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using O2.External.Python.CPython;
using O2.External.Python.IronPython;
using O2.External.Python.Jython;

namespace O2.External.Python
{
    public class ExternalEngines
    {
        public static void addCurrentPythonEnginesTo_ascx_SourceCodeEditor()
        {
            DI.reflection.loadAssembly("O2_External_SharpDevelop.dll");
            DI.reflection.invokeMethod_Static("ascx_SourceCodeEditor", "addExternalEngine",
                                              new object[]
                                                  {
                                                      "Jython",
                                                      (Func<string, DataReceivedEventHandler, Process>)
                                                      JythonExec.executePythonFile
                                                  });
            DI.reflection.invokeMethod_Static("ascx_SourceCodeEditor", "addExternalEngine",
                                              new object[]
                                                  {
                                                      "IronPython",
                                                      (Func<string, DataReceivedEventHandler, Process>)
                                                      IronPythonExec.executePythonFile
                                                  });
            DI.reflection.invokeMethod_Static("ascx_SourceCodeEditor", "addExternalEngine",
                                              new object[]
                                                  {
                                                      "CPython",
                                                      (Func<string, DataReceivedEventHandler, Process>)
                                                      CPythonExec.executePythonFile
                                                  });
        }
    }
}
