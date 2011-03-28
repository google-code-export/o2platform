// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Drawing;
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
			var _object = new List<object>().add(PublicDI.config)
											.add("aaa")											
											.add(AppDomain.CurrentDomain );
			_object.Add("789".wrapOnList().add("456").add("123")); 
			
			_object.showObject();
			
		}
	}

    public class ascx_ObjectViewer : ContainerControl
    {       		
		public Object RootObject { get;set; }
		public bool ShowMethods { get;set; }
		public bool ShowPropertyAndFieldInfo { get;set; }		
		public bool Sorted { get;set; }
		
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
    		try
    		{
	    		var topPanel = this.add_Panel();
	    		var propertyGrid = topPanel.add_GroupBox("").add_PropertyGrid();  
				treeView = propertyGrid.parent().insert_Left<Panel>().add_TreeView().sort();;				
				treeView.splitterDistance(300);
				var toStringValue = propertyGrid.parent().insert_Above<Panel>(100).add_GroupBox("ToString Value:").add_TextArea(); 
				var optionsPanel = treeView.insert_Below<Panel>(50);
				var objectName = toStringValue.parent().insert_Above<Panel>(20).add_TextBox("name","");
				optionsPanel.add_CheckBox("Show Methods",0,0,
					(value)=>{
								ShowMethods = value;
								refresh();
							 });
				optionsPanel.add_CheckBox("Show Property and Field info",25,0,
					(value)=>{
								ShowPropertyAndFieldInfo = value;
								refresh();
							 })
							.autoSize()
							.append_Link("refresh", ()=>refresh());
				
				optionsPanel.add_CheckBox("Sort Values",0,120, 
					(value)=>{								
								Sorted = value;
								try
								{
									treeView.sort(Sorted);  // throwing "Unable to cast object of type 'System.Boolean' to type 'System.Windows.Forms.TreeView'"
								}
								catch(Exception ex)
								{
									ex.log();
								}								
							 }).@checked(true);
							
				treeView.afterSelect<object>( 
					(selectedObject) => {
											var treeNode = treeView.selected();
											objectName.set_Text(treeNode.get_Text());
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
											else if (treeNode.nodes().size()==0)
											{
												propertyGrid.show(null);
												propertyGrid.parent().set_Text("[null value]"); 
												toStringValue.set_Text("[null value]");
												objectName.set_Text("");
												treeNode.color(Color.Red);
											}
											
									  	});
			}
			catch(Exception ex)
			{
				ex.log("in buildGui",true);
			}
		}								  
			
		public void addObjectPropertyAndFields (TreeNode targetNode, object targetObject)
		{			
			if (targetObject is String)  // skip strings
				return;			
			
			if (targetObject is IDictionary)
			{
				var dictionary = (IDictionary)targetObject;
				var index = 0;
				foreach(var key in dictionary.Keys)
				{						
					if (key is String)						
						targetNode.add_Node(key.str(), dictionary[key]);
					else
					{
						index++;
						var value =  dictionary[key];
						targetNode.add_Node("key_{0}: {1}".format(index,key.str()), key);
						targetNode.add_Node("value_{0}: {1}".format(index, value.str()), value);
					}			 
				}
				targetNode.expand();											
			}
			else 
				if (targetObject is IEnumerable)
				{				
					try
					{
						foreach(var item in (targetObject as IEnumerable))
							targetNode.add_Node(item);
						targetNode.expand();						
					}
					catch(Exception ex)
					{
						ex.log("in addObjectPropertyAndFields IEnumerable loop");
					}				
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
			if (ShowPropertyAndFieldInfo)
			{
				targetNode.add_Node("_PropertyInfo(s)",null).add_Nodes(targetObject.type().properties(), 
													    (item)=> item.Name);
				targetNode.add_Node("_FieldInfo(s)",null).add_Nodes(targetObject.type().fields(), 
													    (item)=> item.Name);													    
			}
			if (ShowMethods)
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
			if (_object.isNull())			
				"in ascx_ObjectViewer provided object was null".error();			
			else
			{				
				RootObject = _object;
				addFirstObject(_object);
			}
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
		
		public static T showDetails<T>(this T _object)
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
