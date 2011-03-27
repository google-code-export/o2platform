// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Text;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Views.ASCX.ExtensionMethods;
using O2.Views.ASCX.classes.MainGUI;

//O2File:_Extra_methods_To_Add_to_Main_CodeBase.cs
using O2.XRules.Database.Utils;

namespace O2.XRules.Database.Utils
{
    public class test_ascx_ObjectViewer : ContainerControl
    {       		
		public static void launchGui()
		{						
			var _object = new List<object>().add("aaa")
											.add(PublicDI.config)
											.add(AppDomain.CurrentDomain );
			_object.Add("123".wrapOnList().add("456").add("789"));
			
			_object.showObject();
			
		}
	}

    public class ascx_ObjectViewer : ContainerControl
    {       		
		public Object RootObject { get;set; }
		public bool showMethods { get;set; }
		public bool showPropertyAndFieldInfo { get;set; }
		public TreeView treeView;
		
        public ascx_ObjectViewer()
    	{
    		//showMethods = true;
    		//showPropertyAndFieldInfo = true;
    		this.Width = 300;
    		this.Height = 300;    		
    		buildGui();
    	}
 
    	public void buildGui()
    	{
    		var topPanel = this.add_Panel();
    		var propertyGrid = topPanel.add_GroupBox("").add_PropertyGrid();  
			treeView = propertyGrid.parent().insert_Left<Panel>().add_TreeView().sort();
			treeView.splitterDistance(300);
			var toStringValue = propertyGrid.parent().insert_Below<Panel>(150).add_GroupBox("ToString Value:").add_TextArea(); 
			var optionsPanel = treeView.insert_Below<Panel>(50);
			optionsPanel.add_CheckBox("Show Methods",0,0,
				(value)=>{
							showMethods = value;
							refresh();
						 });
			optionsPanel.add_CheckBox("Show Property and Field info",25,0,
				(value)=>{
							showPropertyAndFieldInfo = value;
							refresh();
						 })
						.autoSize();
			
			//using System.Collections 														
						
			treeView.afterSelect<object>( 
				(selectedObject) => {
										var treeNode = treeView.selected();
										if (treeNode.get_Tag().notNull())// && tag.str() != treeNode.get_Text())
										{								
											propertyGrid.show(selectedObject);
											var toString = selectedObject.str();
											if (toString == "System.__ComObject")
												toString += " : {0}".format(selectedObject.comTypeName());
											toStringValue.set_Text(toString); 
											
											propertyGrid.parent().set_Text(selectedObject.typeFullName()); 
											if (treeNode.nodes().size() ==0)
											{									
												addObjectPropertyAndFields(treeNode, selectedObject);
											}
										}
										
								  	});
		}								  
			
		public void addObjectPropertyAndFields (TreeNode targetNode, object targetObject)
		{
			if (targetObject is String)  // skip strings
				return;			
			
			if (targetObject is IEnumerable)
			{				
				foreach(var item in (targetObject as IEnumerable))
					targetNode.add_Node(item);
				targetNode.expand();						
			}
			else
			{
				//var objectNode = targetNode.add_Node(targetObject.str(), targetObject);				
				targetNode.add_Node("properties",null).add_Nodes(targetObject.type().properties(), 
														    (item)=> item.Name, 
														    (item)=> PublicDI.reflection.getProperty(item,targetObject),   
														    (item)=>false);																	   
												 
				targetNode.add_Node("fields",null).add_Nodes(targetObject.type().fields(), 
														    (item)=> item.Name, 
														    (item)=> targetObject.field(item.Name), //PublicDI.reflection.getField(item,_object),   
														    (item)=>false);
				targetNode.set_Text("{0}             ({1} properties {2} fields)"
					.format(targetNode.get_Text(),
							targetNode.nodes()[1].nodes().size(),
							targetNode.nodes()[0].nodes().size()));														    
			}														    
			if (showPropertyAndFieldInfo)
			{
				targetNode.add_Node("_PropertyInfo(s)",null).add_Nodes(targetObject.type().properties(), 
													    (item)=> item.Name);
				targetNode.add_Node("_FieldInfo(s)",null).add_Nodes(targetObject.type().fields(), 
													    (item)=> item.Name);													    
			}
			if (showMethods)
			{
				targetNode.add_Node("MethodInfo(s)",null).add_Nodes(targetObject.type().methods(), 
													    (item)=> item.Name);
			}
			
		}
		
		public void addFirstObject(object targetObject)
		{
			var objectNode = treeView.rootNode().add_Node(targetObject.str(), targetObject);
			addObjectPropertyAndFields(objectNode,targetObject);
			objectNode.expand();
			treeView.selectFirst();
		}
		
		public void show(object _object)
		{
			viewObject(_object);
		}
		
		public void viewObject(object _object)
		{
			RootObject = _object;
			addFirstObject(_object);
		}
		
		public void refresh()
		{
			treeView.clear();
			addFirstObject(RootObject);
		}
	}
	
	public static class ascx_ObjectViewer_ExtensionMethods
	{
		public static T details<T>(this T _object)
		{
			return _object.showObject();
		}
		
		public static T objectDetails<T>(this T _object)
		{
			return _object.showObject();
		}
		
		public static T viewObject<T>(this T _object)
		{
			return _object.showObject();
		}
		
		public static T showObject<T>(this T _object)
		{
			if (_object.isNull())
				"in showObject object provided was null".error();
			else
			{
				var formTitle = "Object Viewer = {0}".format(_object.type());
				var objectViewer = O2Gui.open<ascx_ObjectViewer>(formTitle, 800,400);			
				objectViewer.show(_object);
			}
			return _object;
		}
	}				 
}
