using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Interfaces.CIR;

namespace O2.Core.CIR.CirUtils.DotNet
{
    public class DecompiledCode
    {
        public static void showDecompiledFunctionInSourceCodeViewer(ICirFunction cirFunctionToProcess, string viewerControlName)
        {
            DI.log.debug("Showing details for: {0}", cirFunctionToProcess.FunctionName);
       //     if (showFunctionsDecompiledCodeInSourceCodeViewerToolStripMenuItem.Checked)
       //     { 
             //   var sourceCodeViewer = 
            //}

/*                return O2Thread.mtaThread(() =>
                                          {
                                              O2Messages.openControlInGUISync(typeof (ascx_FindingsViewer), O2DockState.Float, windowTitle);
                                              O2Messages.getAscx(windowTitle, guiControl =>
                                                                                  {
                                                                                      if (guiControl != null && guiControl is ascx_FindingsViewer)
                                                                                      {
                                                                                          var findingsViewer = (ascx_FindingsViewer) guiControl;
                                                                                          findingsViewer.loadO2Findings(o2Findings);
                                                                                      }                                                        
                                                                                  });
                                          });            
 */ 
        }
    }
}
