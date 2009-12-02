using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using O2.External.IKVM.IKVM;

namespace O2.External.IKVM
{
    public class IKVMEngine
    {
        public static void addCurrentIKVMEnginesTo_ascx_SourceCodeEditor()
        {
            DI.reflection.loadAssembly("O2_External_SharpDevelop.dll");
            DI.reflection.invokeMethod_Static("ascx_SourceCodeEditor", "addExternalEngine",
                                              new object[]
                                                  {
                                                      "IKVM",                                                      
                                                     (Func<string, DataReceivedEventHandler, Process>)
                                                      JavaExec.executeJavaFile
                                                  });
        }
    }
}
