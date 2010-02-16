// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)

using System.Windows.Forms;
using O2.Interfaces.O2Core;
using O2.Kernel;
using O2.DotNetWrappers.Windows;
using O2.External.WinFormsUI.Forms;
using O2.Core.XRules.Ascx;
//O2Tag_AddReferenceFile:nunit.framework.dll
using NUnit.Framework;
//O2Tag_AddSourceFile:E:\O2\_SourceCode_O2\O2Core\O2_DotNetWrappers\Windows\O2Forms_ThreadSafe_ToolStrip.cs

namespace O2.UnitTests.Standalone
{
    [TestFixture]
    public class Test_O2Forms_ThreadSafe_ToolStrip
    {    
        private readonly static IO2Log log = PublicDI.log;    	    	
    	
    	    	
        /*[Test]
    	public void listCurrentItems()
    	{    		
    		var xrulesControl = (ascx_XRules_Editor)O2AscxGUI.getAscx("XRules Editor");
    		foreach(var item in O2Forms_ThreadSafe_ToolStrip.getItems(xrulesControl))
    			log.debug("   [i]   :   {0}", item);			
    	}*/
    	
        [Test]
        // this test expects that there is a current ascx_XRules_Editor loaded in the current GUI
        public void addTextBoxToPanel()
        {
            log.info("in  addTextBoxToPanel");
            var xrulesControl = (ascx_XRules_Editor)O2AscxGUI.getAscx("XRules Editor");
            Assert.That(xrulesControl != null);    		    	    	
            Assert.That(O2Forms_ThreadSafe_ToolStrip.hasToolStripControl(xrulesControl), "xrulesControl control did not contain a ToolStrip control");
    		
            var toolStripControl = O2Forms_ThreadSafe_ToolStrip.getToolStripControl(xrulesControl);   		
            Assert.That(toolStripControl != null , "toolStripControl was null");
    		
            var newTextBoxName = "TextBox To Add";
            var newTextBoxValue = "Content of temp textbox";
            Assert.That(false == O2Forms_ThreadSafe_ToolStrip.removeControl(xrulesControl,newTextBoxName), "TextBox To add should NOT BE there at this stage");
    		
            var newTextBoxControl = O2Forms_ThreadSafe_ToolStrip.addTextBox(xrulesControl,newTextBoxName, newTextBoxValue);
            Assert.That(newTextBoxControl != null, "newTextBoxControl was null");
    		
            var itemAdded = O2Forms_ThreadSafe_ToolStrip.getItem(xrulesControl, newTextBoxName);
            Assert.That(itemAdded != null, "itemAdded  == null");
            Assert.That(itemAdded.Text == newTextBoxValue, "itemAdded Text value should be: " + newTextBoxValue );
    		    		    	
            Assert.That(O2Forms_ThreadSafe_ToolStrip.removeControl(xrulesControl,newTextBoxName), "TextBox To add should BE there at this stage");    		
        }   
    	
        [Test]
        public void addTextBoxToPanelWithEvent()
        {
            var xrulesControl = (ascx_XRules_Editor)O2AscxGUI.getAscx("XRules Editor");
            Assert.That(xrulesControl != null);    		    	    	
    		
            var newLabelName = "lbOpenFile";
            var newLabelText = "Open File";
    		
            var newTextBoxName = "TextBox To Add";
            var newTextBoxText = "";
            KeyEventHandler onKeyUp = (sender, e) => 
                                          {
                                              if(e.KeyData == Keys.Enter)    	
                                              {
                                                  var selectedFile = 	((ToolStripTextBox)sender).Text;
                                                  PublicDI.log.info("Opening file: {0}", selectedFile);
                                                  xrulesControl.loadFile(selectedFile);
                                                  //O2Messages.fileOrFolderSelected(selectedFile);
                                              }    					
                                          };
    			
            // remove in case it is there
            O2Forms_ThreadSafe_ToolStrip.removeControl(xrulesControl,newTextBoxName);
            O2Forms_ThreadSafe_ToolStrip.removeControl(xrulesControl,newLabelName);
            // add new one Label
    		
            var newLabel = O2Forms_ThreadSafe_ToolStrip.addLabel(xrulesControl,newLabelName, newLabelText);
            Assert.That(newLabel != null, "newLabel was null");
            // add new one TextBox   		
            var newTextBox = O2Forms_ThreadSafe_ToolStrip.addTextBox(xrulesControl,newTextBoxName, newTextBoxText,onKeyUp);
            Assert.That(newTextBox != null, "newTextBox was null");
    		
            // remove controls added
            //O2Forms_ThreadSafe_ToolStrip.removeControl(xrulesControl,newTextBoxName);
            //O2Forms_ThreadSafe_ToolStrip.removeControl(xrulesControl,newLabelName);
    		
        }
    }
}