// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System; 
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.API.AST.CSharp;

namespace O2.API.AST.ExtensionMethods
{
    public static class O2CodeStreamTaintRules_ExtensionMethods
    {    
    	
    	public static O2CodeStreamTaintRules add_TaintPropagator(this O2CodeStreamTaintRules taintRules, string taintPropagator)
    	{    		
    		if (taintRules.TaintPropagators.Contains(taintPropagator).isFalse())
    			taintRules.TaintPropagators.Add(taintPropagator);
    		return taintRules;
    	}
    	
    	public static bool isTaintPropagator(this O2CodeStreamTaintRules taintRules, string taintPropagator)
    	{    		
    		return (taintRules.TaintPropagators.Contains(taintPropagator));
    	}    		    	    	    	    	    
    }
}
