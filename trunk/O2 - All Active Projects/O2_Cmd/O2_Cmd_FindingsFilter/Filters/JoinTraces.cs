// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Cmd.FindingsFilter.Ascx;
using O2.DotNetWrappers.O2CmdShell;
using O2.External.WinFormsUI.Forms;
using O2.Interfaces.O2Findings;

namespace O2.Cmd.FindingsFilter.Filters
{
    public class JoinTraces
    {
    	private static void loadTestData()
    	{    		
    		var findingsToLoad = @"C:\O2\_tempDir\tmp1B11.tmp.ozasmt";
    	    var findingsFilterDevGui = (ascx_FindingsFilterDevGui)O2AscxGUI.getAscx("Findings Filter");
            if (findingsFilterDevGui!=null)
                findingsFilterDevGui.loadOzasmtFile(findingsToLoad);

    		
    	}
    	
        private static IEnumerable<IO2Finding> joinCompatibleSinksAndSources(IO2Assessment o2Assessment)
        {
        	loadTestData();
            if (o2Assessment == null)
                return null;
            O2Cmd.log.write("--> Executing Filter: Join Compatible Sinks and Sources");
            return from IO2Finding finding in o2Assessment.o2Findings
                   where finding.o2Traces.Count > 0
                   select finding;
        }
    }
}
