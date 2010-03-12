using System;
using System.Windows.Threading;
using O2.Kernel.ExtensionMethods;
//O2Ref:WindowsBase.dll
//O2Ref:System.Core.dll

namespace O2.Script
{
    public static class WPF_Threading_ExtensionMethods
    {    
    	public static object wpfInvoke<T,TRet>(this T source, Func<TRet> func) where T:DispatcherObject
    	{
    		try
			{            
        		return (TRet)source.Dispatcher.Invoke(func);
        	}			
            catch (Exception ex)
            {
                ex.log("in wpfInvoke");
                return null;			// because I want to return null in these case I can't use TRet since that would fo
            }
        	
    	}
    	
    	public static void wpfInvoke<T>(this T source, Action act) where T:DispatcherObject 
    	{
    		try
			{            
        		source.Dispatcher.Invoke(act);
        	}			
            catch (Exception ex)
            {
                ex.log("in wpfInvoke");
            }
    	}    	    	    	    	   
    }
}