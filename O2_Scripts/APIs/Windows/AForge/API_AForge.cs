// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using O2.Interfaces.O2Core;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Views.ASCX;

namespace O2.XRules.Database.APIs
{
    public class API_AForge : Panel
    {    
    	
        public API_AForge()
    	{    	    	
			setup();			
		}  
		
		public API_AForge setup()
		{
			var videoCodec = "WMV3 "; 
			var videoWriter = new AVIWriter(videoCodec);   //WMV3  //DIB  
			return this;
		}
    	    	    	    	    
    }
}
