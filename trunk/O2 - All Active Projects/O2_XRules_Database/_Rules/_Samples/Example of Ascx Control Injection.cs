// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
//O2Tag_OnlyAddReferencedAssemblies
//O2Ref:System.dll
//O2Ref:System.Windows.Forms.dll
//O2Ref:O2_Kernel.dll
//O2Ref:O2_Interfaces.dll
//O2Ref:O2_DotNetWrappers.dll
//O2Ref:O2_Views_ASCX.dll
using System;
using System.Windows.Forms;
using O2.Kernel;
using O2.Interfaces.O2Core;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Views.ASCX.ExtensionMethods;

namespace O2.Script
{
    public class ascx_GuiTest: UserControl
    {    
    	private static IO2Log log = PublicDI.log;

        public static void startControl()
    	{       		
			var control = (ascx_GuiTest)typeof(ascx_GuiTest).showAsForm(
				"PoC for SplitContainer Control Injection", 400, 400);		    					
    	}    	
    	
    	public ascx_GuiTest()
    	{    		    		
    		this.Load+= (sender, e) => buildGui();      // fire this on the onLoad event so that the ParentForm is already loaded    		
    		
    		//buildGui();  								// usually just doing this works, but in this case we need the Parent form size to be set
        }
		    
        private void buildGui()
        {
        	var textBox = this.add_TextBox(true);        	        	        	
        	var distance = 100;
        	var border3D = false;        	
        	textBox.injectControl_Top(new Label().set_Text("Top"), distance, border3D);
        	textBox.injectControl_Bottom(new Label().set_Text("Bottom") , distance,border3D);
			textBox.injectControl_Left(new Label().set_Text("Left"), distance , border3D);
        	textBox.injectControl_Right(new Label().set_Text("Right"), distance , border3D);
        	textBox.Select();
     	}   
    	    	    	    	    
    }
        
}
