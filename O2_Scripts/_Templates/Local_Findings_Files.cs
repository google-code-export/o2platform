// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using O2.Interfaces.O2Core;
using O2.Interfaces.O2Findings;
using O2.Interfaces.XRules;
using O2.Kernel;
using O2.DotNetWrappers.O2Findings;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;

namespace O2.XRules.v0_1
{
    public class LocalProjectStruts : KXRule
    {    
    	private static IO2Log log = PublicDI.log;    	    	
    	public static string ozasmtFileToLoad {get ; set;}
    	
		static LocalProjectStruts()
    	{
    	 	ozasmtFileToLoad = @"E:\O2\Demodata\wg.ozasmt";
    	}
    	
    	public LocalProjectStruts()
    	{
    		Name = "local rule execution";
    	}
    	    	    	    	    
    	[XRule(Name="Local Files")]
    	public List<IO2Finding> loadLocalFiles()
    	{
    		var o2Assessment = new O2Assessment(new O2AssessmentLoad_OunceV6(), ozasmtFileToLoad);
    		log.info("there are {0} findings loaded", o2Assessment.o2Findings.Count);
    		
    		// this will invoke one of the XRules directly
    		//return new O2.XRules.Database._Rules.XRule_Findings_Filter().onlyTraces(o2Assessment);    		
    		return new O2.XRules.Database._Rules.XRule_Findings_Filter().allFindings(o2Assessment.o2Findings);    		
    	}    	    
    }
}
