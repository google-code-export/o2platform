// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Reflection;
using System.Text;
using O2.Interfaces.O2Core;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Views.ASCX;
using O2.Views.ASCX.classes.MainGUI;

namespace O2.Views.ASCX.DataViewers
{
    public class ascx_ShowInfo : UserControl
    {      
    	public PropertyGrid propertyGrid;    	    	
 
 		/*public void test() 		
 		{
 			var info = O2Gui.open<ascx_ShowInfo>("methods");
 			info.show(info.type().methods_public());
 		}*/
        public ascx_ShowInfo()
    	{    	
    		this.Width = 400;
    		this.Height = 300;
    		buildGui();
    	}
    	
    	public PropertyGrid buildGui()
    	{
            return (PropertyGrid)this.invokeOnThread(
    			()=>{
    		            this.clear();
    		            propertyGrid = this.add_Control<PropertyGrid>();    		
                        return propertyGrid;
                });
    	}

        public void show(object _object)
        {
            buildGui();
            if (_object is IEnumerable)
            {
                this.invokeOnThread(
                () =>
                {
                    var treeView = this.add_TreeView();
                    propertyGrid.insert_Left(treeView, 200);
                    var textBox = this.add_TextBox(true);
                    treeView.insert_Above(textBox, 20);
                    //config
                    textBox.ScrollBars = ScrollBars.None;
                    textBox.Multiline = false;

                    ((SplitContainer)textBox.Parent.Parent).Panel1MinSize = 20;
                    ((SplitContainer)textBox.Parent.Parent).SplitterDistance = 20;

                    //events
                    treeView.afterSelect<object>((tag) => showInPropertyGrid(tag));
                    textBox.onTextChange(
                        (text) => treeView.add_Nodes((IEnumerable)_object, true, text));
                    // populate treeview
                    var contextMenu = treeView.add_ContextMenu();
                    contextMenu.add("Copy To Clipboard: Selected Node Text", (item) => { treeView.SelectedNode.Text.toClipboard(); });
                    treeView.add_Nodes((IEnumerable)_object);
                });
            }
            else
            {
                showInPropertyGrid(_object);
            }
        }
    	    	    	
    	public void showInPropertyGrid(object _object)
    	{
    	//	if (_object is Control)
    	//		((Control)_object).invokeOnThread(()=> propertyGrid.show(_object));
    	//	else                
    			propertyGrid.show(_object);
    	}
    	
    	
    	    	    	    	    
    }
}
