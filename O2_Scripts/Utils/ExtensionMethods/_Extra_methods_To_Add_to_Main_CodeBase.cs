// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using  System.Collections.Specialized;
using System.Linq;
using System.Xml.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Win32;
using O2.Interfaces.O2Core;
using O2.Interfaces.O2Findings;
using O2.Kernel;
using O2.Kernel.Objects;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.O2Findings;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.Network;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.H2Scripts;
using O2.Views.ASCX;
using O2.Views.ASCX.CoreControls;
using O2.Views.ASCX.classes.MainGUI;
using O2.Views.ASCX.ExtensionMethods;
using O2.External.SharpDevelop.AST;
using O2.External.SharpDevelop.ExtensionMethods;
using O2.External.SharpDevelop.Ascx;
using O2.API.AST.CSharp;
using O2.API.AST.ExtensionMethods;
using O2.API.AST.ExtensionMethods.CSharp;

using ICSharpCode.TextEditor;
using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.Ast; 
using ICSharpCode.SharpDevelop.Dom;
using ICSharpCode.SharpDevelop.Dom.CSharp;
using System.CodeDom;
using O2.Views.ASCX.O2Findings;
using O2.Views.ASCX.DataViewers;
using System.Security.Cryptography;

//O2Ref:O2_API_AST.dll

//O2File:ascx_ObjectViewer.cs

namespace O2.XRules.Database.Utils
{

		[Serializable]
	public  class NameValueItems : List<Item> 
	{
		
	}
	
	[Serializable]
	public  class Items : List<Item> 
	{
		
	}
	
	[Serializable]
	public  class Item : NameValuePair<string,string>
	{
		public Item()
		{}
		
		public Item(string key, string value) : base(key,value)
		{
			
		}
	}
	
	[Serializable]
	public  class NameValuePair<T,K>
	{
		[XmlAttribute]
		public T Key {get;set;}
		[XmlAttribute]
		public K Value {get;set;}
		
		public NameValuePair()
		{}
		
		public NameValuePair(T key, K value)
		{
			Key = key;
			Value = value;
		}
	}

	public static class NameValuePair_ExtensionMethods
	{
	
		public static List<Item> add(this List<Item> list, string key, string value)
		{
			list.Add(new Item(key,value));
			return list;
		}
		
		public static List<NameValuePair<T,K>> add<T,K>(this List<NameValuePair<T,K>> list, T key, K value)
		{
			list.Add(new NameValuePair<T,K>(key,value));
			return list;
		}
	}
	
	public static class Reflection_ExtensionMethods
	{
		//Reflection
		public static T property<T>(this object _object, string propertyName)
		{						
			if (_object.notNull())
			{
				var result = _object.property(propertyName);
				if (result.notNull())
					return (T)result;
			}
			return default(T);
		}
		
		public static object property(this object _object, string propertyName, object value)
		{
			var propertyInfo = PublicDI.reflection.getPropertyInfo(propertyName, _object.type());  
			if (propertyInfo.isNull())
				"Could not find property {0} in type {1}".error(propertyName, _object.type());  
			else
			{
				PublicDI.reflection.setProperty(propertyInfo, _object,value);    
		//		"set {0}.{1} = {2}".info(_object.type(),propertyName, value);
			}
			return _object;

		}
		
		public static MethodInfo method(this Type type, string name)
		{
			foreach(var method in type.methods())
			{				
				if (method.Name == name)
					return method;
			}
			return null;
		}
		
		public static List<System.Attribute> attributes(this List<MethodInfo> methods)
		{
			return (from method in methods 
					from attribute in method.attributes()
					select attribute).toList();
		}
		public static List<System.Attribute> attributes(this MethodInfo method)
		{
			return PublicDI.reflection.getAttributes(method);
		}
		
		public static List<MethodInfo> withAttribute(this Assembly assembly, string attributeName)
		{
			return assembly.methods().withAttribute(attributeName);
		}
		
		public static List<MethodInfo> withAttribute(this List<MethodInfo> methods, string attributeName)
		{ 
			return (from method in methods 
					from attribute in method.attributes()		  
					where attributeName == (attribute.TypeId as Type).Name.remove("Attribute")
					select method).toList();						
		}

		
		public static string signature(this MethodInfo methodInfo)
		{
			return "mscorlib".assembly()
							 .type("RuntimeMethodInfo")
							 .invokeStatic("ConstructName",methodInfo)
							 .str();
		}
		
		public static object enumValue(this Type enumType, string value)
		{
			return enumType.enumValue<object>(value);
		}
		public static T enumValue<T>(this Type enumType, string value)
		{
			var fieldInfo = (FieldInfo) enumType.field(value);
			if (fieldInfo.notNull())
				return (T)fieldInfo.GetValue(enumType);
			return default(T);
		}
		
		//Array		
		
		public static Array createArray<T>(this Type arrayType,  params T[] values)			
		{
			try
			{
				if (values.isNull())
					return  Array.CreateInstance (arrayType,0);	
					
				var array =  Array.CreateInstance (arrayType,values.size());	
				
				if (values.notNull())
					for(int i=0 ; i < values.size() ; i ++)
						array.SetValue(values[i],i);
				return array;								
			}
			catch(Exception ex)
			{
				ex.log("in Array.createArray");
			}
			return null;
		}
		
		
		
		//WebServices SOAP methods
		public static List<MethodInfo> webService_SoapMethods(this Assembly assembly)
		{
			var soapMethods = new List<MethodInfo >(); 
			foreach(var type in assembly.types())
				soapMethods.AddRange(type.webService_SoapMethods());
			return soapMethods;
					
		}
		public static List<MethodInfo> webService_SoapMethods(this object _object)
		{
			Type type = (_object is Type) 	
							? (Type)_object
							: _object.type();				
			var soapMethods = new List<MethodInfo >(); 
			foreach(var method in type.methods())
				foreach(var attribute in method.attributes())
					if (attribute.typeFullName() == "System.Web.Services.Protocols.SoapDocumentMethodAttribute")
						soapMethods.Add(method);
			return soapMethods;
		}
		
		public static Items property_Values_AsStrings(this object _object)
		{		
			var propertyValues_AsStrings = new Items();
			foreach(var property in _object.type().properties())				
				propertyValues_AsStrings.add(property.Name.str(), _object.property(property.Name).str());
			return propertyValues_AsStrings;
		}

	}
	public static class XElement_LinqXML_extensionMethods
	{
		public static XElement parent(this XElement element)
		{
			return element.Parent;
		}
		
		public static XElement element(this XElement elementToSearch, string name, bool createIfNotExistant)
		{
			var foundElement = elementToSearch.element(name);
			if (foundElement.notNull())
				return foundElement;
			return createIfNotExistant 
					? elementToSearch.add_Element(name)
					: null;
		}
		
		public static XElement add_Element(this XElement rootElement, string text)
		{
			var newElement = new XElement(text);
			rootElement.Add(newElement);
			return newElement;
		}
		
		public static XAttribute add_Attribute(this XElement rootElement, string text, object value)
		{
			var newAttribute = new XAttribute(text.xName(), value);
			rootElement.Add(newAttribute);
			return newAttribute;
		}
		
		public static XAttribute attribute(this XElement elementToSearch, string name, bool createIfNotExistant)
		{
			return elementToSearch.attribute(name, 	createIfNotExistant, null);
		}
		
		public static XAttribute attribute(this XElement elementToSearch, string name, bool createIfNotExistant, object value)
		{
			var foundAttribute = elementToSearch.attribute(name);
			if (foundAttribute.notNull())
				return foundAttribute;
			return createIfNotExistant 
					? elementToSearch.add_Attribute(name, value ?? "")
					: null;
		}
	}
	public static class ConfigFiles_extensionMethods
	{
		// Config files (can't easily put this on the main
        public static Panel editLocalConfigFile(this string file)
        {
            var panel = O2Gui.open<Panel>("Editing local config file: {0}".format(file), 700, 300);
            return file.editLocalConfigFile(panel);
        }
		
		// new one
		public static T resizeFormToControlSize<T>(this T control, Control controlToSync)
			where T : Control
		{
			if (controlToSync.notNull())
			{
				var parentForm = control.parentForm();
				if (parentForm.notNull())
				{
					var top = controlToSync.PointToScreen(System.Drawing.Point.Empty).Y;
					var left = controlToSync.PointToScreen(System.Drawing.Point.Empty).X;
					var width = controlToSync.Width;
					var height = controlToSync.Height;
					"Setting parentForm location to {0}x{1} : {2}x{3}".info(top, left, width, height);
					parentForm.Top = top;
					parentForm.Left = left;
					parentForm.Width = width;
					parentForm.Height = height;
				}
			}
			return control;
		}
		
		public static string saveImageFromClipboardToFile(this object _object)
		{
			var clipboardImagePath = _object.saveImageFromClipboard();
			if (clipboardImagePath.fileExists())
			{
				var fileToSave = O2Forms.askUserForFileToSave(PublicDI.config.O2TempDir,"*.jpg");
				if (fileToSave.valid())
				{
					Files.MoveFile(clipboardImagePath, fileToSave);
					"Clipboard Image saved to: {0}".info(fileToSave);
				}
			}
			return clipboardImagePath;
		}						
		
		//Label

		public static Label autoSize(this Label label, bool value)
		{
			label.invokeOnThread(
				()=>{						
						label.AutoSize = value;
					});
			return label;
		}
		
		public static Label text_Center(this Label label)			
		{			
			label.invokeOnThread(
				()=>{						
						label.autoSize(false);
						label.TextAlign = ContentAlignment.MiddleCenter;
					});
			return label;
		}				
		//Control (Font)			
		
		
		public static T size<T>(this T control, int value)
			where T : Control
		{
			return control.textSize(value);
		}
		
		public static T size<T>(this T control, string value)
			where T : Control
		{
			return control.textSize(value.toInt());
		}
		
		public static T font<T>(this T control, string fontFamily, string size)
			where T : Control
		{
			return control.font(fontFamily, size.toInt());
		}
		
		public static T font<T>(this T control, string fontFamily, int size)
			where T : Control
		{
			return control.font(new FontFamily(fontFamily), size);
		}
		
		public static T font<T>(this T control, FontFamily fontFamily, string size)
			where T : Control
		{
			return control.font(fontFamily, size.toInt());
		}
		
		public static T font<T>(this T control, FontFamily fontFamily, int size)
			where T : Control
		{
			if (control.isNull())
				return null;
			control.invokeOnThread(
				()=>{
						if (fontFamily.isNull())
							fontFamily = control.Font.FontFamily;
						control.Font = new Font(fontFamily, size);
					});
			return control;
		}
		
		public static T font<T>(this T control, string fontFamily)
			where T : Control
		{
			return control.fontFamily(fontFamily);
		}
		
		public static T fontFamily<T>(this T control, string fontFamily)
			where T : Control
		{
			control.invokeOnThread(
				()=> control.Font = new Font(new FontFamily(fontFamily), control.Font.Size));			
			return control;
		}
		
		public static T textSize<T>(this T control, int value)
			where T : Control
		{
			control.invokeOnThread(
				()=> control.Font = new Font(control.Font.FontFamily, value));			
			return control;
		}
		
		public static T font_bold<T>(this T control)		// just bold() conficts with WPF version
			where T : Control
		{
			control.invokeOnThread(
				()=> control.Font = new Font( control.Font, control.Font.Style | FontStyle.Bold ));
			return control;
		}
		
		public static T font_italic<T>(this T control)
			where T : Control
		{
			control.invokeOnThread(
				()=> control.Font = new Font( control.Font, control.Font.Style | FontStyle.Italic ));
			return control;
		}
		
		//CheckBox
		public static CheckBox checkedChanged(this CheckBox checkBox, Action<bool> action)
		{
			checkBox.invokeOnThread(
				()=> checkBox.CheckedChanged+= (sender,e) => {action(checkBox.value());});
			return checkBox;
		}
		//ListBox
		
		public static ListBox add_ListBox(this Control control)
		{
			return control.add_Control<ListBox>();
		}
		
		public static ListBox add_Item(this ListBox listBox, object item)
		{
			return listBox.add_Items(item);
		}
		
		public static ListBox add_Items(this ListBox listBox, params object[] items)
		{
			return (ListBox)listBox.invokeOnThread(
				()=>{
						listBox.Items.AddRange(items);
						return listBox;
					});					
		}
		
		public static object selectedItem(this ListBox listBox)
		{
			return (object)listBox.invokeOnThread(
				()=>{	
						return listBox.SelectedItem;	
					});
		}
		
		public static T selectedItem<T>(this ListBox listBox)
		{			
			var selectedItem = listBox.selectedItem();
			if (selectedItem is T) 
				return (T) selectedItem;
			return default(T);					
		}
		
		public static ListBox select(this ListBox listBox, int selectedIndex)
		{
			return (ListBox)listBox.invokeOnThread(
				()=>{
						if (listBox.Items.size() > selectedIndex)
							listBox.SelectedIndex = selectedIndex;
						return listBox;
					});					
		}
		
		public static ListBox selectFirst(this ListBox listBox)
		{
			return listBox.select(0);
		}
		
		
		//processes
		
		
		
		public static string startProcess_getConsoleOut(this string processExe, string arguments)
		{
			return Processes.startProcessAsConsoleApplicationAndReturnConsoleOutput(processExe, arguments);
		}
		
		public static Process startProcess(this string processExe, Action<string> onDataReceived)
		{
			return processExe.startProcess("", onDataReceived);
		}
		
		public static Process startProcess(this string processExe, string arguments, Action<string> onDataReceived)
		{
			return Processes.startProcessAndRedirectIO(processExe, arguments, onDataReceived);			
		}
		
		public static Process startProcess(this string processExe, string arguments)
		{
			return Processes.startProcess(processExe, arguments);
		}
		
		public static Process startProcess(this string processExe)
		{
			return Processes.startProcess(processExe);
		}
		
		public static Process close(this Process process)
		{
			return process.stop();
		}
		
		public static Process closeInNSeconds(this Process process, int seconds)
		{
			O2Thread.mtaThread(
				()=>{
						process.sleep(seconds*1000);
						"Closing Process:{0}".info(process.ProcessName);
						process.stop();
					});
			return process;
		}
	}
	
	public static class Int_ExtensionMethods
	{
		public static Action loop(this int count , Action action)
		{
			return count.loop(500,action);
		}
		
		public static Action loop(this int count , int delay,  Action action)
		{
			"Executing provided action for {0} times with a delay of {1} milliseconds".info(count);
			for(var i=0 ; i < count ; i ++)
			{
				action();
				if (delay > 0)
					count.sleep(delay);
			}
			return action;
		}
		
	}
	//REGISTRY
	public static class RegistryKeyExtensionMethods
    {    
    	public static string makeDomainTrusted(this string rootDomain, string subDomain)
    	{
			try
			{				
				var ieKeysLocation = @"Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\";
				//var domainsKeyLocation =  ieKeysLocation + "Domains";
				var domainsKeyLocation =  ieKeysLocation + "EscDomains";			    
				var trustedSiteZone = 0x2;
				RegistryKey currentUserKey = Registry.CurrentUser; 
				currentUserKey.getOrCreateSubKey(domainsKeyLocation, rootDomain, false); 
				currentUserKey.createSubDomainKeyAndValue(domainsKeyLocation, rootDomain, subDomain, "http",trustedSiteZone); 
				currentUserKey.createSubDomainKeyAndValue(domainsKeyLocation, rootDomain, subDomain, "https",trustedSiteZone); 
				var message = "Added as truted the domain: {1}.{0}".format(rootDomain,subDomain);
				return message;
			}
			catch(Exception ex)
			{
				ex.log("in makeDomainTrusted");
				return ex.Message;
			}
		}
    
        public static RegistryKey getOrCreateSubKey(this RegistryKey registryKey, string parentKeyLocation, string key, bool writable)
        {
            string keyLocation = string.Format(@"{0}\{1}", parentKeyLocation, key);
            RegistryKey foundRegistryKey = registryKey.OpenSubKey(keyLocation, writable);
            return foundRegistryKey ?? registryKey.createSubKey(parentKeyLocation, key);
        }

        public static RegistryKey createSubKey(this RegistryKey registryKey, string parentKeyLocation, string key)
        {
            RegistryKey parentKey = registryKey.OpenSubKey(parentKeyLocation, true); //must be writable == true
            if (parentKey == null) 
            	 throw new NullReferenceException(string.Format("Missing parent key: {0}", parentKeyLocation)); 
            RegistryKey createdKey = parentKey.CreateSubKey(key);
            if (createdKey == null) 
            	throw new Exception(string.Format("Key not created: {0}", key));
            return createdKey;
        }
        
        //IE Specific
        public static void createSubDomainKeyAndValue(this RegistryKey currentUserKey, string domainsKeyLocation, string domain, 
        											   string subDomainKey, string subDomainValue, int zone)
        {
            RegistryKey subdomainRegistryKey = currentUserKey.getOrCreateSubKey(string.Format(@"{0}\{1}", domainsKeyLocation, domain), subDomainKey, true);
            object objSubDomainValue = subdomainRegistryKey.GetValue(subDomainValue);
            if (objSubDomainValue == null || Convert.ToInt32(objSubDomainValue) != zone)            
                subdomainRegistryKey.SetValue(subDomainValue, zone, RegistryValueKind.DWord);           
        }
	}   
	
	//Download file
	public static class Uri_ExtensionMethods
	{
		public static Uri append(this Uri uri, string virtualPath)
		{
			try
			{
				return new Uri(uri, virtualPath);
			}
			catch
			{
				return null;
			}
		}
	}
	public static class DownloadFiles_ExtensionMethods
	{		
		public static string download(this string fileToDownload)
		{
			return fileToDownload.uri().download();
		}
		
		public static string download(this Uri uri)
		{
			return uri.downloadFile();
		}
		public static string downloadFile(this Uri uri)
		{
			if (uri.isNull())
				return null;
			var fileName = uri.Segments.Last();
			if (fileName.valid())
			{
				var targetFile = "".tempDir().pathCombine(fileName);
				Files.deleteFile(targetFile);
				return downloadFile(uri, targetFile);
			}
			else
				"Could not extract filename from provided uri: {0}".error(uri.str());
			return null;					
		}
		
		public static string download(this string fileToDownload, string targetFile)
		{
			return downloadFile(fileToDownload.uri(),targetFile);
		}
		
		public static string downloadFile(this Uri uri, string targetFile)
		{
			if (uri.isNull())
				return null;
			"Downloading file {0} to location:{1}".info(uri.str(), targetFile);
			if (targetFile.fileExists())		// don't download if file already exists
			{
				"File already existed, so skipping download".debug();
				return targetFile;
			}
			var sync = new System.Threading.AutoResetEvent(false); 
				var downloadControl = O2Gui.open<ascx_DownloadFile>("Downloading: {0}".format(uri.str()), 455  ,170 );							
				downloadControl.setAutoCloseOnDownload(true);							
				downloadControl.setCallBackWhenCompleted((file)=>	downloadControl.parentForm().close());
				downloadControl.onClosed(()=>sync.Set());
				downloadControl.setDownloadDetails(uri.str(), targetFile);							
				downloadControl.downloadFile();
			sync.WaitOne();					 	// wait for download complete or form to be closed
			if (targetFile.fileExists())		
				return targetFile;
			return null;
		}								
	}
	
		
	public static class O2Gui_ExtensionMethods
	{		
		//Control
		public static Panel showAsForm(this string title)			
		{
			return title.showAsForm<Panel>(600,400);
		}
		
		public static Panel showAsForm(this string title, int width, int height)			
		{
			return  O2Gui.open<Panel>(title, width,height);
		}
		
		public static T showAsForm<T>(this string title)
			where T : Control
		{
			return title.showAsForm<T>(600,400);
		}
		
		public static T showAsForm<T>(this string title, int width, int height)
			where T : Control
		{
			return (T) O2Gui.open<T>(title, width,height);
		}
		
	}
	
	//PropertyGrid
	public static class PropertyGrid_ExtensionMethods
	{
		public static PropertyGrid onValueChange(this PropertyGrid propertyGrid, Action callback)
		{
			propertyGrid.invokeOnThread(()=>propertyGrid.PropertyValueChanged+=(sender,e)=>callback() );
			return propertyGrid;
		}
	}
	
	//TreeNode & TreeView
	public static class TreeNode_and_TreeView_ExtensionMethods
	{
		public static TreeView allow_TreeNode_Edits(this TreeView treeView)
		{
			if (treeView.notNull())
				treeView.invokeOnThread(()=> treeView.LabelEdit = true);		
			return treeView;
		}
		
		public static TreeNode beginEdit(this TreeNode treeNode)
		{
			if (treeNode.notNull())
				treeNode.treeView().invokeOnThread(()=> treeNode.BeginEdit());
			return treeNode;
		}				
		
		public static TreeNode set_Tag(this TreeNode treeNode, object value)
		{
			return (TreeNode)treeNode.treeView().invokeOnThread(
				()=>{
						treeNode.Tag = value;
						return treeNode;
					});
		}
		
		public static TreeNode insert_TreeNode(this TreeView treeView, string text, object tag, int position)
		{
			return treeView.rootNode().insert_TreeNode(text,tag, position);
		}
		public static TreeNode insert_TreeNode(this TreeNode treeNode, string text, object tag, int position)
		{
			return (TreeNode)treeNode.treeView().invokeOnThread(
				()=>{
						var newNode = treeNode.Nodes.Insert(position, text);
						newNode.Tag = tag;
						return treeNode;
					});
		}
		
		//not working
		/*public static TreeView add_Nodes<T>(this TreeView treeView, params string[] nodes)
		{
			treeView.rootNode().add_Nodes(nodes);
			return treeView;
		}
		
		public static TreeNode add_Nodes<T>(this TreeNode treeNode, params string[] nodes)
		{
			Ascx_ExtensionMethods.add_Nodes(treeNode,nodes);			
			return treeNode;
		}*/
		
		public static TreeView add_Nodes<T>(this TreeView treeView, IEnumerable<T> collection, bool addDummyNode)
		{
				
			treeView.rootNode().add_Nodes(collection, (item)=> item.str() ,(item)=> item,(item)=> addDummyNode);			
			return treeView;
		}
		
		public static TreeNode add_Nodes<T>(this TreeNode treeNode, IEnumerable<T> collection, bool addDummyNode)
		{
			return treeNode.add_Nodes(collection, (item)=> item.str() ,(item)=> item, (item)=> addDummyNode);						
		}
		
		public static TreeView add_Nodes<T>(this TreeView treeView, IEnumerable<T> collection, Func<T, bool> getAddDummyNode)
		{
			treeView.rootNode().add_Nodes(collection, (item)=> item.str() ,(item)=> item, getAddDummyNode);			
			return treeView;
		}
		
		public static TreeNode add_Nodes<T>(this TreeNode treeNode, IEnumerable<T> collection, Func<T, bool> getAddDummyNode)
		{
			return treeNode.add_Nodes(collection, (item)=> item.str() ,(item)=> item, getAddDummyNode);						
		}
		
		public static TreeView add_Nodes<T>(this TreeView treeView, IEnumerable<T> collection, Func<T,string> getNodeName)
		{
			treeView.rootNode().add_Nodes(collection, getNodeName,(item)=> item,(item)=> false);			
			return treeView;
		}
				
		public static TreeNode add_Nodes<T>(this TreeNode treeNode, IEnumerable<T> collection, Func<T,string> getNodeName)
		{
			return treeNode.add_Nodes(collection, getNodeName, (item)=> item, (item)=> false);			
		}
		
		public static TreeView add_Nodes<T>(this TreeView treeView, IEnumerable<T> collection, Func<T,string> getNodeName, bool addDummyNode)
		{
			treeView.rootNode().add_Nodes(collection, getNodeName, (item)=> item,(item)=> addDummyNode);			
			return treeView;
		}
				
		public static TreeNode add_Nodes<T>(this TreeNode treeNode, IEnumerable<T> collection, Func<T,string> getNodeName, bool addDummyNode)
		{
			return treeNode.add_Nodes(collection, getNodeName, (item)=> item,(item)=> addDummyNode);			
		}
		
		public static TreeView add_Nodes<T>(this TreeView treeView, IEnumerable<T> collection, Func<T,string> getNodeName, Func<T, object> getTagValue, Func<T,bool> getAddDummyNode)
		{
			treeView.rootNode().add_Nodes(collection, getNodeName, getTagValue, getAddDummyNode);
			return treeView;
		}
		
		public static TreeNode add_Nodes<T>(this TreeNode treeNode, IEnumerable<T> collection, Func<T,string> getNodeName, Func<T, object> getTagValue, Func<T,bool> addDummyNode)
		{
			foreach(var item in collection)
				Ascx_ExtensionMethods.add_Node(treeNode,getNodeName(item), getTagValue(item), addDummyNode(item));
			return treeNode;
		}
		
		//add Files to TreeView
		public static TreeView add_Files(this TreeView treeView, String folder)
		{
		return treeView.add_Files(folder, "*.*",true);
		}
		
		public static TreeView add_Files(this TreeView treeView, String folder, string filter)
		{
		return treeView.add_Files(folder, filter,true);
		}
		
		public static TreeView add_Files(this TreeView treeView, String folder, string filter, bool recursive)
		{
			return treeView.add_Files(folder.files(filter,recursive));
		}
		
		public static TreeView add_Files(this TreeView treeView, List<string> files)
		{
			return treeView.add_Nodes(files, (file)=>file.fileName());
		}
	}
	
	public static class SplitContainer_ExtensionMethods
	{
		public static T splitterDistance<T>(this T control, int distance)
			where T : Control
		{
			var splitContainer = control.splitContainer();
			if (splitContainer.notNull())
				Ascx_ExtensionMethods.splitterDistance(splitContainer,distance);
			return control;
		}
		
		
		//Split Container
		
		public static SplitContainer splitContainer(this Control control)
		{
			return control.parent<SplitContainer>();
		}
		
		public static SplitContainer splitterWidth(this SplitContainer splitContainer, int value)
		{
			splitContainer.invokeOnThread(()=> splitContainer.SplitterWidth = value);
			return splitContainer;
		}
	}
	
	public static class DataGridView_ExtensionMethods
	{
	
		public static DataGridView dataSource(this DataGridView dataGridView, System.Data.DataTable dataTable)
		{
			dataGridView.invokeOnThread(
				()=>{
						dataGridView.DataSource = dataTable;
					});
			return dataGridView;
		}
		
		public static DataGridView ignoreDataErrors(this DataGridView dataGridView)
		{
			return dataGridView.ignoreDataErrors(false);
		}
		
		public static DataGridView ignoreDataErrors(this DataGridView dataGridView, bool showErrorInLog)
		{
			dataGridView.invokeOnThread(
				()=>{
						dataGridView.DataError+= 
								(sender,e) => { 
													if (showErrorInLog)
														" dataGridView error: {0}".error(e.Context);
											  };
					});
			return dataGridView;		
		}
		
		public static DataGridView afterSelect(this DataGridView dataGridView, Action<DataGridViewRow> onSelect)
		{
			dataGridView.invokeOnThread(
				()=>{
						dataGridView.SelectionChanged+= 
							(sender,e)=>{
											if (dataGridView.SelectedRows.size() == 1)
											{
												var selectedRow = dataGridView.SelectedRows[0];
												onSelect(selectedRow);
											}
										 };
					});
			return dataGridView;		
		}
		
		public static DataGridView row_Height(this DataGridView dataGridView, int value)
		{
			dataGridView.invokeOnThread(()=>dataGridView.RowTemplate.Height = value);
			return dataGridView;
		}
		
		public static List<string> values(this DataGridViewRow dataViewGridRow)
		{
			return ( List<string>)dataViewGridRow.DataGridView.invokeOnThread(
				()=>{
						var values = new List<string>();
						foreach(var cell in dataViewGridRow.Cells)
							values.add(cell.property("Value").str());
						return values;
					});			
		}
	}
	
	public static class ListView_ExtensionMethods
	{
		public static ascx_TableList show_In_ListView<T>(this IEnumerable<T> data)
		{
			return data.show_In_ListView("View data in List Viewer", 600,400);
		}
		public static ascx_TableList show_In_ListView<T>(this IEnumerable<T> data, string title, int width, int height)
		{
			return O2Gui.open<Panel>(title, width, height).add_TableList().show(data);
		}
		
		public static ascx_TableList columnsWidthToMatchControlSize(this ascx_TableList tableList)
		{		
			tableList.parent().widthAdd(1);		// this trick forces it (need to find how to invoke it directly
			return tableList;
		}
	}
	
	public static class XmlLinq_ExtensiomMethods
	{
		public static XAttribute value(this XAttribute xAttribute, string value)
		{	
			if (xAttribute.notNull())
				xAttribute.SetValue(value);
			return xAttribute;
		}		
	}
	
	public static class XML_ExtensionMethods
	{		
		public static List<XmlAttribute> add_XmlAttribute(this List<XmlAttribute> xmlAttributes, string name, string value)
		{
			var xmlDocument = (xmlAttributes.size() > 0) 
									?  xmlAttributes[0].OwnerDocument
									: new XmlDocument();						
			var newAttribute = xmlDocument.CreateAttribute(name);
			newAttribute.Value = value;
			xmlAttributes.add(newAttribute);
			return xmlAttributes;
		}		
	}
	
	public static class AppDomain_ExtensionMethods
	{
		public static string executeScriptInSeparateAppDomain(this string scriptToExecute)
		{
			return scriptToExecute.executeScriptInSeparateAppDomain(true, false);
		}
		
		public static string executeScriptInSeparateAppDomain(this string scriptToExecute, bool showLogViewer, bool openScriptGui)
		{
			var appDomainName = 12.randomLetters();
			var o2AppDomain =  new O2AppDomainFactory(appDomainName);
			o2AppDomain.load("O2_XRules_Database"); 	
			o2AppDomain.load("O2_Kernel");
			o2AppDomain.load("O2_DotNetWrappers");
			
			var o2Proxy =  (O2Proxy)o2AppDomain.getProxyObject("O2Proxy");
			if (o2Proxy.isNull())
			{
				"in executeScriptInSeparateAppDomain, could not create O2Proxy object".error();
				return null;
			}
			o2Proxy.InvokeInStaThread = true;
			if (showLogViewer)
				o2Proxy.executeScript( "O2Gui.open<Panel>(\"Util - LogViewer\", 400,140).add_LogViewer();");
			if (openScriptGui)
				o2Proxy.executeScript("O2Gui.open<Panel>(\"Script editor\", 700, 300)".line() + 
 	  								  "		.add_Script(false)".line() + 
									  " 	.onCompileExecuteOnce()".line() + 
									  " 	.Code = \"hello\";".line() + 
									  "//O2File:Scripts_ExtensionMethods.cs");
			o2Proxy.executeScript(scriptToExecute);
			PublicDI.log.showMessageBox("Click OK to close the '{0}' AppDomain (and close all open windows)".format(appDomainName));										
			o2AppDomain.unLoadAppDomain();
			return scriptToExecute;
		}
		
		public static O2Proxy executeScript(this O2Proxy o2Proxy, string scriptToExecute)
		{
			o2Proxy.staticInvocation("O2_External_SharpDevelop","FastCompiler_ExtensionMethods","executeSourceCode",new object[]{ scriptToExecute });						
			return o2Proxy;
		}
		
		public static string execute_InScriptEditor_InSeparateAppDomain(this string scriptToExecute)
		{
			var script_Base64Encoded = scriptToExecute.base64Encode();
			var scriptLauncher = "O2Gui.open<Panel>(\"Script editor\", 700, 300)".line() + 
 	  								  "		.add_Script(false)".line() + 
									  " 	.onCompileExecuteOnce()".line() + 
									  " 	.Code = \"{0}\".base64Decode();".line().format(script_Base64Encoded) + 
									  "//O2File:Scripts_ExtensionMethods.cs";
			scriptLauncher.executeScriptInSeparateAppDomain(false,false);
			return scriptLauncher;									  
		}
		
		public static string localExeFolder(this string fileName)
		{
			var mappedFile = PublicDI.config.CurrentExecutableDirectory.pathCombine(fileName);
			return (mappedFile.fileExists())
						? mappedFile
						: null;					
		}
	}
	
		
	public static class WinFormControls_ExtensionMethods
	{
		public static List<Control> add_1x1(this Control control, Action<Control> buildPanel1,  Action<Control> buildPanel2)
		{
			var controls = control.add_1x1();
			buildPanel1(controls[0].add_Panel());
			buildPanel2(controls[1].add_Panel());
			return controls;
		}
		// insert_...()
		public static Panel insert_Left(this Control control)
		{
			return control.insert_Left(control.width()/2);			
		}
		
		public static Panel insert_Right(this Control control)
		{
			return control.insert_Right<Panel>(control.width()/2);
		}
		
		public static Panel insert_Above(this Control control)
		{			
			return control.insert_Above<Panel>(control.height()/2);
		}
		
		public static Panel insert_Below(this Control control)
		{
			return control.insert_Below<Panel>(control.height()/2);
		}		
		// insert_...(width)
		public static Panel insert_Left(this Control control, int width)
		{			
			var panel = control.insert_Left<Panel>(width); 
			panel.splitterDistance(width); 				// to deal with bug in insert_Left<Panel>
			return panel;
		}
		
		public static Panel insert_Right(this Control control, int width)
		{
			return control.insert_Right<Panel>(width);
		}
		
		public static Panel insert_Above(this Control control, int width)
		{
			return control.insert_Above<Panel>(width);
		}
		
		public static Panel insert_Below(this Control control, int width)
		{
			return control.insert_Below<Panel>(width);
		}
		// insert_...(title)
		public static Panel insert_Left(this Control control, string title)
		{
			return control.insert_Left(control.width()/2, title);
		}
		
		public static Panel insert_Right(this Control control, string title)
		{
			return control.insert_Right(control.width()/2, title);
		}
		
		public static Panel insert_Above(this Control control, string title)
		{
			return control.insert_Above(control.height()/2, title);
		}
		
		public static Panel insert_Below(this Control control, string title)
		{
			return control.insert_Below(control.height()/2, title);
		}
		// insert_...(width, title)
		public static Panel insert_Left(this Control control, int width, string title)
		{
			return control.insert_Left<Panel>(width).add_GroupBox(title).add_Panel();
		}
		
		public static Panel insert_Right(this Control control, int width, string title)
		{
			return control.insert_Right<Panel>(width).add_GroupBox(title).add_Panel();
		}
		
		public static Panel insert_Above(this Control control, int width, string title)
		{
			return control.insert_Above<Panel>(width).add_GroupBox(title).add_Panel();
		}
		
		public static Panel insert_Below(this Control control, int width, string title)
		{
			return control.insert_Below<Panel>(width).add_GroupBox(title).add_Panel();
		}
		
		
		
	}
	
	public static class sourceCodeViewer_ExtensionMethods
	{
		public static ascx_SourceCodeViewer onTextChange(this ascx_SourceCodeViewer codeViewer, Action<string> callback)
		{
			codeViewer.editor().onTextChange(callback);
			return codeViewer;
		}
		
		public static ascx_SourceCodeEditor onTextChange(this ascx_SourceCodeEditor codeEditor, Action<string> callback)
		{
			codeEditor.invokeOnThread(
				()=>{
						codeEditor.eDocumentDataChanged+= callback;
					});
			return codeEditor;
		}
	}
	
	public static class Misc_ExtensionMethods
	{
		public static string add_RandomLetters(this string _string)
		{
			return _string.add_RandomLetters(10);
		}
		
		public static string add_RandomLetters(this string _string, int count)
		{
			return "{0}_{1}".format(_string,count.randomLetters());
		}
		
		public static string ascii(this int _int)
		{
			try
			{				
				return ((char)_int).str();					
			}
			catch(Exception ex)
			{
				ex.log();
				return "";
			}
		}
		
		public static Guid next(this Guid guid)
		{
			return guid.next(1);
		}
		
		public static Guid next(this Guid guid, int incrementValue)
		{			
			var guidParts = guid.str().split("-");
			var lastPartNextNumber = Int64.Parse(guidParts[4], System.Globalization.NumberStyles.AllowHexSpecifier);
			lastPartNextNumber+= incrementValue;
			var lastPartAsString = lastPartNextNumber.ToString("X12");
			var newGuidString = "{0}-{1}-{2}-{3}-{4}".format(guidParts[0],guidParts[1],guidParts[2],guidParts[3],lastPartAsString);
			return new Guid(newGuidString); 					
		}
		
		public static Guid emptyGuid(this Guid guid)
		{
			return Guid.Empty;
		}
		
		public static Guid newGuid(this string guidValue)
		{
			return Guid.NewGuid();
		}
		
		public static Guid guid(this string guidValue)
		{
			if (guidValue.inValid())
				return Guid.Empty;			
			return new Guid(guidValue);		
		}
		
		// so that it is automatically available in the O2 Scriping environment (was in public static class ascx_ObjectViewer_ExtensionMethods)
		public static void details<T>(this T _object)
		{
			O2Thread.mtaThread(()=>_object.showObject());
		}
		
		
		
	}
}
    	