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
using System.Collections.Specialized;
using System.Linq;
using System.Xml.Linq;
using System.Reflection; 
using System.Text;
using System.ComponentModel;
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
using O2.DotNetWrappers.Zip;
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

using Ionic.Zip;
//O2Ref:Ionic.Zip.dll
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
		public string this[string key] 
		{
			get
			{
				foreach(var item in this)
					if (item.Key == key)
						return item.Value;
				return null;
					//return new Item(value,value);
			}	
		}
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
		
		public override string ToString()
		{
			return Key.str();
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
	
	
		#region tuples

    public class Tuple<T>
    {
        public Tuple(T first)
        {
            First = first;
        }

        public T First { get; set; }
    }

    public class Tuple<T, T2> : Tuple<T>
    {
        public Tuple(T first, T2 second)
            : base(first)
        {
            Second = second;
        }

        public T2 Second { get; set; }
    }

    public class Tuple<T, T2, T3> : Tuple<T, T2>
    {
        public Tuple(T first, T2 second, T3 third)
            : base(first, second)
        {
            Third = third;
        }

        public T3 Third { get; set; }
    }

    public class Tuple<T, T2, T3, T4> : Tuple<T, T2, T3>
    {
        public Tuple(T first, T2 second, T3 third, T4 fourth)
            : base(first, second, third)
        {
            Fourth = fourth;
        }

        public T4 Fourth { get; set; }
    }

    #endregion

	
	
	// Other extension method classes
	
	public static class Reflection_ExtensionMethods
	{
		//Reflection				
		public static object field<T>(this object _object, string fieldName)
		{
			var type = typeof(T);  
			return _object.field(type, fieldName);
		}
		
		public static object field(this object _object, Type type, string fieldName)
		{			
			"**** type:{0}".error(type.typeName());
			var fieldInfo =  (FieldInfo)type.field(fieldName);
			return PublicDI.reflection.getFieldValue(fieldInfo, type);
		}
		
		public static List<ConstructorInfo> ctors(this Type type)
		{
			return type.GetConstructors(System.Reflection.BindingFlags.NonPublic | 
									    System.Reflection.BindingFlags.Public | 
									    System.Reflection.BindingFlags.Instance).toList();
		}
		


		
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
		
		public static List<string> names(this List<PropertyInfo> properties)
		{
			return (from property in properties
					select property.Name).toList();
		}
		
		public static List<object> propertyValues(this object _object)
		{
			var propertyValues = new List<object>();
			var names = _object.type().properties().names();
			foreach(var name in names)
				propertyValues.Add(_object.prop(name));
			return propertyValues;
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
		
		public static List<AssemblyName> referencedAssemblies(this Assembly assembly)
		{
			return assembly.GetReferencedAssemblies().toList();
		}
		
		public static Assembly assembly(this AssemblyName assemblyName)
		{
			return assemblyName.str().assembly();
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
					if (attribute.typeFullName() == "System.Web.Services.Protocols.SoapDocumentMethodAttribute" ||
					    attribute.typeFullName() == "System.Web.Services.Protocols.SoapRpcMethodAttribute")
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
		
		//LinkLabel
		
		public static List<LinkLabel> links(this Control control)
		{
			return control.controls<LinkLabel>(true);
		}
		
		public static LinkLabel link(this Control control, string text)
		{
			foreach(var link in control.links())
				if (link.get_Text() == text)
					return link;
			return null;
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
		public static CheckBox append_CheckBox(this Control control, string text, Action<bool> action)
		{
			return control.append_Control<CheckBox>()
						  .set_Text(text)
						  .autoSize()
						  .onChecked(action);
		}
		public static CheckBox onClick(this CheckBox checkBox, Action<bool> action)
		{
			return checkBox.onChecked(action);
		}
		
		public static CheckBox onChecked(this CheckBox checkBox, Action<bool> action)
		{
			return checkBox.checkedChanged(action);
		}
		public static CheckBox checkedChanged(this CheckBox checkBox, Action<bool> action)
		{
			checkBox.invokeOnThread(
				()=> checkBox.CheckedChanged+= (sender,e) => {action(checkBox.value());});
			return checkBox;
		}
		//WebBrowser
		
		public static WebBrowser onNavigated(this WebBrowser webBrowser, Action<string> callback)
		{
			webBrowser.invokeOnThread(()=> webBrowser.Navigated+= (sender,e)=> callback(e.Url.str()));
			return webBrowser;													
		}
		
		public static WebBrowser add_NavigationBar(this WebBrowser webBrowser)
		{
			var navigationBar = webBrowser.insert_Above(20).add_TextBox("url","");
			webBrowser.onNavigated((url)=> navigationBar.set_Text(url));
			navigationBar.onEnter((text)=>webBrowser.open(text));
			return webBrowser;
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
		
		
		//TabControl
		
		public static TabControl remove_Tab(this TabControl tabControl, string text)
		{
			var tabToRemove = tabControl.tab(text);
			if (tabToRemove.notNull())
				tabControl.remove_Tab(tabToRemove);
			return tabControl;
		}
		
		public static bool has_Tab(this TabControl tabControl, string text)
		{
			return tabControl.tab(text).notNull();
		}
		
		public static TabPage tab(this TabControl tabControl, string text)
		{
			foreach(var tab in tabControl.tabs())
				if (tab.get_Text() == text)
					return tab;
			return null;
		}
		public static List<TabPage> tabs(this TabControl tabControl)
		{
			return tabControl.tabPages();
		}
		
		public static List<TabPage> tabPages(this TabControl tabControl)
		{
			return (List<TabPage>)tabControl.invokeOnThread(
									()=>{
											var tabPages = new List<TabPage>();
											foreach(TabPage tabPage in tabControl.TabPages)
												tabPages.Add(tabPage);
											return tabPages;											
										});
		}
		
		
	}
	
	public static class TextBox_ExtensionMethods
	{
		public static TextBox add_TextBox(this Control control, string text)
		{
			var textBox = control.add_TextBox();
			textBox.set_Text(text);
			return textBox;
		}
		
		public static TextBox add_TextArea(this Control control, string text)
		{
			var textBox = control.add_TextArea();
			textBox.set_Text(text);
			return textBox;
		}
				
	}
	
	public static class Processes_ExtensionMethods
	{		
		public static string startProcess_getConsoleOut(this string processExe)
		{
			return processExe.startProcess_getConsoleOut("");
		}
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
		
		public static Process executeH2_as_Admin_askUserBefore(this string scriptName)
		{
			if ("It looks like your current account doesn't have the rights to run this script, do you want to try running this script with full priviledges?".askUserQuestion())				
				return scriptName.executeH2_as_Admin();
			return null;
		}
		
		public static Process executeH2_as_Admin(this string scriptToExecute)
		{
			var process = new Process();
			process.StartInfo.FileName  = PublicDI.config.CurrentExecutableDirectory.pathCombine("O2_XRules_Database.exe");
			process.StartInfo.Arguments = "\"{0}\"".format(scriptToExecute);
			process.StartInfo.Verb = "runas";
			process.Start();
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
		
		public static Panel popupWindow(this string title)
		{
			return title.showAsForm();
		}		
		
		public static Panel popupWindow(this string title, int width, int height)
		{
			return title.showAsForm(width, height);
		}
			
		public static Panel createForm(this string title)			
		{
			return title.showAsForm();
		}
		
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
		public static TreeView add_TreeView_with_PropertyGrid<T>(this T control)
			where T : Control
		{
			return control.add_TreeView_with_PropertyGrid(true);
		}
		
		public static TreeView add_TreeView_with_PropertyGrid<T>(this T control, bool insertBelow)
			where T : Control
		{			
			var treeView = control.clear().add_TreeView();				
			var targetPanel = (insertBelow) ? treeView.insert_Below() : treeView.insert_Right();
			var propertyGrid = targetPanel.add_PropertyGrid().helpVisible(false);	 	
			treeView.showSelected(propertyGrid);;
			return treeView;
		}
		
		// this is one of O2's weirdest bugs in the .NET Framework, but there are cases where 
		// a treeview only has 1 node and it is not shown
		public static TreeView applyPathFor_1NodeMissingNodeBug(this TreeView treeView)
		{
			if (treeView.nodes().size()==1)
			{
				var firstNode = treeView.nodes()[0];
				firstNode.set_Text(firstNode.get_Text() + "");	
			}
			return treeView;
		}
		
		public static TreeView collapse(this TreeView treeView)
		{
			if (treeView.notNull())
				treeView.invokeOnThread(()=> treeView.CollapseAll());
			return treeView;
		}
		
		public static TreeView showSelected(this TreeView treeView, PropertyGrid propertyGrid)			
		{
			return treeView.showSelected<object>(propertyGrid);
		}
		
		public static TreeView showSelected<T>(this TreeView treeView, PropertyGrid propertyGrid)			
		{
			return treeView.afterSelect<T>((item)=> propertyGrid.show(item));			
		}
		
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
		
		//IEnumerable<T> collection, bool addDummyNode
		public static TreeView add_Nodes<T>(this TreeView treeView, IEnumerable<T> collection, bool addDummyNode)
		{
				
			treeView.rootNode().add_Nodes(collection, (item)=> item.str() ,(item)=> item,(item)=> addDummyNode);			
			return treeView;
		}
		
		public static TreeNode add_Nodes<T>(this TreeNode treeNode, IEnumerable<T> collection, bool addDummyNode)
		{
			return treeNode.add_Nodes(collection, (item)=> item.str() ,(item)=> item, (item)=> addDummyNode);						
		}
		
		//IEnumerable<T> collection, Func<T, bool> getAddDummyNode
		public static TreeView add_Nodes<T>(this TreeView treeView, IEnumerable<T> collection, Func<T, bool> getAddDummyNode)
		{
			treeView.rootNode().add_Nodes(collection, (item)=> item.str() ,(item)=> item, getAddDummyNode);			
			return treeView;
		}
		
		public static TreeNode add_Nodes<T>(this TreeNode treeNode, IEnumerable<T> collection, Func<T, bool> getAddDummyNode)
		{
			return treeNode.add_Nodes(collection, (item)=> item.str() ,(item)=> item, getAddDummyNode);						
		}
		
		//IEnumerable<T> collection, Func<T,string> getNodeName)
		public static TreeView add_Nodes<T>(this TreeView treeView, IEnumerable<T> collection, Func<T,string> getNodeName)
		{
			treeView.rootNode().add_Nodes(collection, getNodeName,(item)=> item,(item)=> false);			
			return treeView;
		}
				
		public static TreeNode add_Nodes<T>(this TreeNode treeNode, IEnumerable<T> collection, Func<T,string> getNodeName)
		{
			return treeNode.add_Nodes(collection, getNodeName, (item)=> item, (item)=> false);			
		}
		
		
		//IEnumerable<T> collection, Func<T,string> getNodeName, getColor)
		public static TreeView add_Nodes<T>(this TreeView treeView, IEnumerable<T> collection, Func<T,string> getNodeName, Func<T, Color> getColor)
		{
			return treeView.add_Nodes<T>(collection, getNodeName, (item)=> false, getColor);
		}
		
		public static TreeNode add_Nodes<T>(this TreeNode treeNode, IEnumerable<T> collection, Func<T,string> getNodeName, Func<T, Color> getColor)
		{
			return treeNode.add_Nodes<T>(collection, getNodeName, (item)=> false, getColor);
		}
		
		//IEnumerable<T> collection, Func<T,string> getNodeName, addDummyNode, getColor)
		public static TreeView add_Nodes<T>(this TreeView treeView, IEnumerable<T> collection, Func<T,string> getNodeName, Func<T, bool> getAddDummyNode, Func<T, Color> getColor)
		{
			return treeView.add_Nodes<T>(collection, getNodeName, (item)=>item, getAddDummyNode, getColor);
		}
		
		public static TreeNode add_Nodes<T>(this TreeNode treeNode, IEnumerable<T> collection, Func<T,string> getNodeName, Func<T, bool> getAddDummyNode, Func<T, Color> getColor)
		{
			return treeNode.add_Nodes<T>(collection, getNodeName, (item)=>item, getAddDummyNode, getColor);
		}
		
		//IEnumerable<T> collection, Func<T,string> getNodeName, getTagValue, addDummyNode, getColor)
		public static TreeView add_Nodes<T>(this TreeView treeView, IEnumerable<T> collection, Func<T,string> getNodeName, Func<T,object> getTagValue,  Func<T, bool> getAddDummyNode, Func<T, Color> getColor)
		{
			treeView.rootNode().add_Nodes(collection, getNodeName,getTagValue, getAddDummyNode, getColor);
			return treeView;
		}
				
		public static TreeNode add_Nodes<T>(this TreeNode treeNode, IEnumerable<T> collection, Func<T,string> getNodeName, Func<T,object> getTagValue, Func<T, bool> getAddDummyNode, Func<T, Color> getColor)
		{
			foreach(var item in collection)
			{
				var newNode = Ascx_ExtensionMethods.add_Node(treeNode,getNodeName(item), getTagValue(item), getAddDummyNode(item));
				newNode.color(getColor(item));
			}
			return treeNode;
		}
		
		//IEnumerable<T> collection, Func<T,string> getNodeName, bool addDummyNode
		public static TreeView add_Nodes<T>(this TreeView treeView, IEnumerable<T> collection, Func<T,string> getNodeName, bool addDummyNode)
		{
			treeView.rootNode().add_Nodes(collection, getNodeName, (item)=> item,(item)=> addDummyNode);			
			return treeView;
		}
				
		public static TreeNode add_Nodes<T>(this TreeNode treeNode, IEnumerable<T> collection, Func<T,string> getNodeName, bool addDummyNode)
		{
			return treeNode.add_Nodes(collection, getNodeName, (item)=> item,(item)=> addDummyNode);			
		}
		
		//Func<T,string> getNodeName, Func<T, object> getTagValue, Func<T,bool> getAddDummyNode
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
		
		public static TreeView add_File(this TreeView treeView, string file)
		{
			return treeView.add_Files(file.wrapOnList());
		}
				
		
		public static List<string> texts(this List<TreeNode> treeNodes)
		{
			return (from treeNode in treeNodes
					select treeNode.get_Text()).toList();
		}
		
		public static List<T> tags<T>(this TreeView treeView)
		{
			return treeView.nodes().tags<T>();
		}
		
		public static List<T> tags<T>(this List<TreeNode> treeNodes)
		{
			return (from treeNode in treeNodes
					where Ascx_ExtensionMethods.get_Tag(treeNode) is T
					select (T)Ascx_ExtensionMethods.get_Tag(treeNode)).toList();
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
		
		public static SplitContainer splitContainerFixed(this Control control)
		{
			return control.splitContainer().isFixed(true);
		}
		
		public static SplitContainer @fixed(this SplitContainer splitContainer, bool value)
		{
			return 	splitContainer.isFixed(value);
		}
		
		public static SplitContainer isFixed(this SplitContainer splitContainer, bool value)
		{
			splitContainer.invokeOnThread(()=> splitContainer.IsSplitterFixed = value);
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
		
		
		public static DataGridView onDoubleClick<T>(this DataGridView dataGridView, Action<T> callback)
		{
			dataGridView.onDoubleClick(
				(dataGridViewRow)=>{
										if (dataGridViewRow.Tag.notNull() && dataGridViewRow.Tag is T)
											callback((T)dataGridViewRow.Tag);
								   });
			return dataGridView;
		}						
		
		public static DataGridView onDoubleClick(this DataGridView dataGridView, Action<DataGridViewRow> callback)
		{
			dataGridView.invokeOnThread(
				()=>{
						dataGridView.DoubleClick+= 
							(sender,e)=>{
											if (dataGridView.SelectedRows.size() == 1)
											{
												var selectedRow = dataGridView.SelectedRows[0];
												callback(selectedRow);
											}
										 };
					});
			return dataGridView;		
		}
		
		public static DataGridView afterSelect<T>(this DataGridView dataGridView, Action<T> callback)
		{
			dataGridView.afterSelect(
				(dataGridViewRow)=>{
										if (dataGridViewRow.Tag.notNull() && dataGridViewRow.Tag is T)
											callback((T)dataGridViewRow.Tag);
								   });
			dataGridView.onDoubleClick<T>(callback);					   
			return dataGridView;
		}
		
		public static DataGridView afterSelect(this DataGridView dataGridView, Action<DataGridViewRow> callback)
		{
			dataGridView.invokeOnThread(
				()=>{
						dataGridView.SelectionChanged+= 
							(sender,e)=>{
											if (dataGridView.SelectedRows.size() == 1)
											{
												var selectedRow = dataGridView.SelectedRows[0];
												callback(selectedRow);
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
		
		
		public static DataGridView show(this DataGridView dataGridView, object _object)
		{
			dataGridView.Tag = _object;	
			dataGridView.remove_Columns();
			if(_object is IEnumerable)
			{
				var list = (_object as IEnumerable); 
				var first = list.first();  
				var names = first.type().properties().names(); 
				dataGridView.add_Columns(names);
				foreach(var item in list)
				{
					var rowId = dataGridView.add_Row(item.propertyValues().ToArray()); 
					dataGridView.get_Row(rowId).Tag = item;										
				}
			}
			else
			{
				var names = _object.type().properties().names(); 
				dataGridView.add_Column("Property name",150); 
				dataGridView.add_Column("Property value");
				foreach(var name in names)
					dataGridView.add_Row(name, _object.prop(name));												
			}		
			return dataGridView;
		 }

		
	}
	
	public static class List_ExtensionMethods
	{
		public static List<T> where<T>(this List<T> list, Func<T,bool> query)
		{
			return list.Where<T>(query).toList();
		}
		
		public static T first<T>(this List<T> list, Func<T,bool> query)
		{
			var results = list.Where<T>(query).toList();
			if (results.size()>0)
				return results.First();
			return default(T);
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
		
		public static ascx_TableList afterSelect_get_Cell(this ascx_TableList tableList, int rowNumber, Action<string> callback)
		{
			tableList.afterSelect(
				(selectedRows)=>{			
						if (selectedRows.size()==1)
						{
						 	var selectedRow = selectedRows[0]; 
						 	var values = selectedRow.values();
						 	if (values.size() > rowNumber)
						 		callback(values[rowNumber]);
						}
					});
			return tableList;
		}
						
		public static ascx_TableList afterSelect_set_Cell(this ascx_TableList tableList, int rowNumber, TextBox textBox)
		{
			return tableList.afterSelect_get_Cell(rowNumber,(value)=> textBox.set_Text(value));			
		}
		
		public static ascx_TableList afterSelect_get_Row(this ascx_TableList tableList, Action<ListViewItem> callback)
		{
			tableList.afterSelect(
				(selectedRows)=>{			
						if (selectedRows.size()==1)
						 	callback(selectedRows[0]);						
					});
			return tableList;
		}
		
		public static ascx_TableList afterSelect_get_RowIndex(this ascx_TableList tableList, Action<int> callback)
		{
			tableList.afterSelect(
				(selectedRows)=>{			
						if (selectedRows.size()==1)
						 	callback(selectedRows[0].Index);						
					});
			return tableList;
		}
		
		public static ascx_TableList afterSelect<T>(this ascx_TableList tableList, List<T> items, Action<T> callback)
		{
			tableList.afterSelect(
				(selectedRows)=>{			
						if (selectedRows.size()==1)
						{
							var index = selectedRows[0].Index;							
							if (index < items.size())
						 		callback(items[index]);						
						}
					});
			return tableList;
		}
		
		public static ascx_TableList selectFirst(this ascx_TableList tableList)
		{
			return (ascx_TableList)tableList.invokeOnThread(
				()=>{
						var listView = tableList.getListViewControl();
						listView.SelectedIndices.Clear();
						listView.SelectedIndices.Add(0);
						return tableList;
					});
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
	
	public static class Xml_XSD_ExtensionMethods
	{
		//replace current xml_CreateCSharpFile with this one (inside O2.External.SharpDevelop.ExtensionMethods)
		public static string xmlCreateCSharpFile_Patched(this string xmlFile)
		{
			var csharpFile = "{0}.cs".format(xmlFile); //xmlFile.replace(".xml",".cs");
			return xmlFile.xmlCreateCSharpFile_Patched(csharpFile);
		}
		
		public static string xmlCreateCSharpFile_Patched(this string xmlFile, string csharpFile)
		{
			var xsdFile = "{0}.xsd".format(xmlFile) ;// xmlFile.replace(".xml",".xsd");
			return xmlFile.xmlCreateCSharpFile_Patched(xsdFile, csharpFile);
		}
		
		public static string xmlCreateCSharpFile_Patched(this string xmlFile, string xsdFile, string csharpFile)
		{								
			if (xsdFile.dirExists())
				xsdFile = xsdFile.pathCombine("{0}.xsd".format(xmlFile.fileName()));
			if (csharpFile.dirExists())
				csharpFile = csharpFile.pathCombine("{0}.cs".format(xmlFile.fileName()));
				
			xmlFile.xmlCreateXSD().saveAs(xsdFile);
			if (xsdFile.fileExists())
			{
				"Created XSD for Xml File: {0}".info(xmlFile);	 
				var tempCSharpFile = xsdFile.xsdCreateCSharpFile();
				tempCSharpFile.fileContents()
					          .insertBefore("//O2Ref:O2_Misc_Microsoft_MPL_Libs.dll".line())
					   	      .saveAs(csharpFile);				
				if (csharpFile.fileExists())
				{
					"Created CSharpFile for Xml File: {0}".info(csharpFile);	
					if (tempCSharpFile != csharpFile)
						File.Delete(tempCSharpFile);
				}	
				return csharpFile;
			}
			return null;	
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
		
		public static T insert_LogViewer<T>(this T control)
			where T : Control
		{
			control.insert_Below(100)
				   .add_LogViewer();
			return control;
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
		
		public static T white<T>(this T control)
			where T : Control
		{
			return control.backColor(Color.White);
		}
		
		public static T pink<T>(this T control)
			where T : Control
		{
			return control.backColor(Color.LightPink);
		}
		
		public static T azure<T>(this T control)
			where T : Control
		{
			return control.backColor(Color.Azure);
		}
		
		
		
	}
	
	public static class sourceCodeViewer_ExtensionMethods
	{

		public static ascx_SourceCodeEditor editor(this ascx_SourceCodeEditor codeEditor)
		{
			return codeEditor;
		}
		
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
		
		public static ascx_SourceCodeViewer open(this ascx_SourceCodeViewer codeViewer, string file , int line)
		{
			codeViewer.editor().open(file, line);
			return codeViewer;
		}
		
		public static ascx_SourceCodeEditor open(this ascx_SourceCodeEditor codeEditor, string file , int line)
		{
			codeEditor.open(file);
			codeEditor.gotoLine(line);
			return codeEditor;
		}
		
		public static ascx_SourceCodeViewer gotoLine(this ascx_SourceCodeViewer codeViewer, int line)
		{
			codeViewer.editor().gotoLine(line);
			return codeViewer;
		}
		
		public static ascx_SourceCodeViewer gotoLine(this ascx_SourceCodeViewer codeViewer, int line, int showLinesBelow)
		{
			codeViewer.editor().gotoLine(line,showLinesBelow);
			return codeViewer;
		}
		
		public static ascx_SourceCodeEditor gotoLine(this ascx_SourceCodeEditor codeEditor, int line, int showLinesBelow)
		{
			if (line>0)
			{
				codeEditor.caret_Line(line,-showLinesBelow);			
				codeEditor.caret_Line(line,showLinesBelow);						
				codeEditor.gotoLine(line);
			}
			return codeEditor;
		}
	}
	
	public static class Misc_ExtensionMethods
	{
		public static string fileName_WithoutExtension(this string filePath)
		{
			return Path.GetFileNameWithoutExtension(filePath);
		}
		public static string upperCaseFirstLetter(this string _string)
		{
			if (_string.valid())
			{
				return _string[0].str().upper() + _string.subString(1); 
			}
			return _string;
		}
		public static List<string> lines(this string text, bool removeEmptyLines)
		{
			if (removeEmptyLines)
				return text.lines();
			return text.fixCRLF()
					   .Split(new string[] { Environment.NewLine }, System.StringSplitOptions.None )
					   .toList();
		}
		public static string subString(this string _string, int startPosition)
		{
			if (_string.size() < startPosition)
				return "";
			return _string.Substring(startPosition);
		}
		
		public static string subString(this string _string,int startPosition, int size)
		{
			var subString = _string.subString(startPosition);
			if (subString.size() < size)
				return subString;
			return subString.Substring(0,size);
		}
		
		public static string subString_After(this string _string, string _stringToFind)
		{
			var index = _string.IndexOf(_stringToFind);
			if (index >0)
			{
				return _string.subString(index + _stringToFind.size());
			}
			return "";
		}
		
		
		public static string add_RandomLetters(this string _string)
		{
			return _string.add_RandomLetters(10);
		}
		
		public static string add_RandomLetters(this string _string, int count)
		{
			return "{0}_{1}".format(_string,count.randomLetters());
		}
		
		public static string file(this string folder, string virtualFilePath)
		{
			var mappedFile = folder.pathCombine(virtualFilePath);
			if (mappedFile.fileExists())
				return mappedFile;
			return null;
		}
		
		public static List<string> files(this List<string> folders)
		{
			return folders.files("*.*");
		}
		
		public static List<string> files(this List<string> folders, string filter)
		{
			return folders.files(filter,false);
		}
		
		public static List<string> files(this List<string> folders, string filter, bool recursive)
		{
			var files = new List<string>();
			foreach(var folder in folders)
				files.AddRange(folder.files(filter, recursive));
			return files;
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
		
		public static bool toBool(this string _string)
		{
			try
			{
				if (_string.valid())
					return bool.Parse(_string);				
			}
			catch(Exception ex)
			{
				"in toBool, failed to convert provided value ('{0}') into a boolean: {2}".format(_string, ex.Message);				
			}
			return false;
		}
		
		
		// so that it is automatically available in the O2 Scriping environment (was in public static class ascx_ObjectViewer_ExtensionMethods)
		public static void details<T>(this T _object)
		{
			O2Thread.mtaThread(()=>_object.showObject());
		}
		
		
		public static List<string> add_If_Not_There(this List<string> list, string item)
		{
			if (item.notNull())
				if (list.contains(item).isFalse())
					list.add(item);
			return list;
		}
		public static string join(this List<string> list)
		{
			return list.join(",");
		}
		
		public static string join(this List<string> list, string separator)
		{
			if (list.size()==1)
				return list[0];
			if (list.size() > 1)
				return list.Aggregate((a,b)=> "{0} {1} {2}".format(a,separator,b));
			return "";
		}
	}
	public static class Dictionary_ExtensionMethods
	{
		public static Dictionary<string,string> toStringDictionary(this string targetString, string rowSeparator, string keySeparator)
		{
			var stringDictionary = new Dictionary<string,string>();
			try
			{
				foreach(var row in targetString.split(rowSeparator))
				{
					if(row.valid())
					{
						var splittedRow = row.split(keySeparator);
						if (splittedRow.size()!=2)
							"[toStringDictionary] splittedRow was not 2: {0}".error(row);
						else
						{
							if (stringDictionary.hasKey(splittedRow[0]))
								"[toStringDictionary] key already existed in the collection: {0}".error(splittedRow[0]);		
							else
								stringDictionary.Add(splittedRow[0], splittedRow[1]);
						}
					}
				}
			}
			catch(Exception ex)
			{
				"[toStringDictionary] {0}".error(ex.Message);
			}
			return stringDictionary;
		}
	}
	
	public static class IEnumerable_ExtensionMethods
	{
		public static bool isIEnumerable(this object list)
		{
			return list.notNull() && list is IEnumerable;
		}
		
		public static int count(this object list)
		{
			if (list.isIEnumerable())
				return (list as IEnumerable).count();
			return -1;
		}
		
		public static int size(this IEnumerable list)
		{
			return list.count();
		}
		public static int count(this IEnumerable list)
		{			
			var count = 0;
			if (list.notNull())
				foreach(var item in list)
					count++;
			return count;
		}
		
		public static object first(this IEnumerable list)
		{
			if(list.notNull())
				foreach(var item in list)
					return item;
			return null;
		}
		
		public static T first<T>(this IEnumerable<T> list)
		{
			try
			{
				if (list.notNull())
					return list.First();
			}
			catch(Exception ex)
			{	
				if (ex.Message != "Sequence contains no elements")
					"[IEnumerable.first] {0}".error(ex.Message);
			}
			return default(T);
		}
		
		public static T last<T>(this IEnumerable<T> list)
		{
			try
			{
				if (list.notNull())
					return list.Last();
			}
			catch(Exception ex)
			{	
				if (ex.Message != "Sequence contains no elements")
					"[IEnumerable.first] {0}".error(ex.Message);
			}
			return default(T);
		}
		
		public static object last(this IEnumerable list)
		{
			object lastItem = null;
			if(list.notNull())
				foreach(var item in list)
					lastItem= item;
			return lastItem;
		}
		
/*		public static List<T> selectMany<T,T1>(this IEnumerable<T> list)
		{
			if (list.notNull()) 
				list.SelectMany<T,T1>((a)=> a);
			return new List<T>();
		}*/
		
		/*public static List<T> toList<T>(this IEnumerable list)
		{
			return list.Cast<T>().toList();
		}*/
	}
	
	public static class ComObject_ExtensionMethods
	{
		//the results of this are not consistent
		public static TreeView showInfo_ComObject(this  object _rootObject)
		{
			var treeView = O2Gui.open<Panel>("showInfo_ComObject",400,400).add_TreeView();
			var propertyGrid = treeView.insert_Below().add_PropertyGrid();
			
			Action<TreeNode, object> add_Object =
				(treeNode,_object)=>{
									treeNode.clear();									
									//treeNode.add_Node(_object.str(), _object, true);
									Ascx_ExtensionMethods.add_Node(treeNode,_object.str(), _object, true);
								  };
			Action<TreeNode, IEnumerable> add_Objects = 
				(treeNode,items)=>{
									treeNode.clear();
									foreach(var item in items)
										//treeNode.add_Node(item.str(), item, true);
										Ascx_ExtensionMethods.add_Node(treeNode, item.str(), item, true);
								  };
			
			 
			treeView.beforeExpand<object>(
				(treeNode, _object)=>{		
										if (_object is String)
											treeNode.add_Node(_object); 
										else
										{
											if (_object is IEnumerable)
												add_Objects(treeNode, _object as IEnumerable);
											else
												foreach(PropertyDescriptor property in TypeDescriptor.GetProperties(_object))
													treeNode.add_Node(property.Name.str(), property.GetValue(_object),true);
										}
									 });
			
			treeView.afterSelect<object>(
				(_object)=> propertyGrid.show(_object));
				
			if(_rootObject is IEnumerable)
				add_Objects(treeView.rootNode(), _rootObject as IEnumerable);  
			else
				add_Object(treeView.rootNode(), _rootObject);  
			return treeView;
		}
	}
	
	public static class H2_ExtensionMethods
	{
		public static string scriptSourceCode(this string file)
		{
			if (file.extension(".h2"))
				return file.h2_SourceCode();
			return file.fileContents();
		}
		public static string h2_SourceCode(this string file)
		{
			if (file.extension(".h2"))
			{
				"return source code of H2 file".info();
				return H2.load(file).SourceCode;
			}
			return null;
		}			
	}	
	
	public static class Console_ExtensionMethods
	{
		public static MemoryStream capture_Console(this string firstLine)
		{
			var memoryStream = new MemoryStream();
			memoryStream.capture_Console();  
			Console.WriteLine(firstLine);
			return memoryStream;
		}
		public static MemoryStream capture_Console(this MemoryStream memoryStream)
		{
			return memoryStream.capture_ConsoleOut()
							   .capture_ConsoleError(); 
		}
		public static MemoryStream capture_ConsoleOut(this MemoryStream memoryStream)
		{
			var streamWriter = new StreamWriter(memoryStream);
			System.Console.SetOut(streamWriter);
			streamWriter.AutoFlush = true;
			return memoryStream;
		}
		
		public static MemoryStream capture_ConsoleError(this MemoryStream memoryStream)
		{
			var streamWriter = new StreamWriter(memoryStream);
			System.Console.SetError(streamWriter);
			streamWriter.AutoFlush = true;
			return memoryStream;
		}
		
		public static string readToEnd(this MemoryStream memoryStream)
		{
			memoryStream.Position =0;
			return new StreamReader(memoryStream).ReadToEnd();
		}
	}
	
	public static class Zip_ExtensionMethods
	{	
		public static string zip_File(this string filesToZip)
		{
			return filesToZip.zip_File(".zip".tempFile());
		}
		
		public static string zip_Folder(this string filesToZip)
		{
			return filesToZip.zip_Folder(".zip".tempFile());
		}
		
		public static string zip_Files(this List<string> filesToZip)
		{
			return filesToZip.zip_Files(".zip".tempFile());
		}
		
		public static string zip_Files(this List<string> filesToZip, string targetZipFile)//, string baseFolder)
		{		
			"Creating ZipFile with {0} files to {1}".info(filesToZip.size(), targetZipFile);
			if (targetZipFile.fileExists())
				Files.deleteFile(targetZipFile);
            var zpZipFile = new ZipFile(targetZipFile);
            foreach(var fileToZip in filesToZip)
            {            	
            	{
            		zpZipFile.AddFile(fileToZip);            
            	}
            	//catch(Exception ex)
            	{
            	//	"[zip_Files] {0} in file {1}".error(ex.Message, fileToZip);
            	}
            }
            zpZipFile.Save();
            zpZipFile.Dispose();
            return targetZipFile;        
		}
	}
	
	public static class Compile_ExtensionMethods
	{
		public static string compileIntoDll_inFolder(this string fileToCompile, string targetFolder)
		{
				"Compiling file: {0} ".debug(fileToCompile);
				//var fileToCompile = currentFolder.pathCombine(file + ".cs");
				var filenameWithoutExtension = fileToCompile.fileName_WithoutExtension();
				var compiledDll = targetFolder.pathCombine(filenameWithoutExtension + ".dll");
				var mainClass = "";
				if (fileToCompile.fileExists().isFalse()) 
					"could not find file to compile: {0}".error(fileToCompile);  
				else
				{ 
					var assembly = new CompileEngine().compileSourceFiles(new List<string> {fileToCompile}, 
																		  mainClass, 
																		  filenameWithoutExtension);
					if (assembly.isNull()) 
						"no compiled assembly object created for: {0}".error(fileToCompile);
					else
					{ 
						Files.Copy(assembly.Location, compiledDll);
						"Copied: {0} to {1}".info(assembly.Location, compiledDll);
						if (compiledDll.fileExists().isFalse())
							"compiled file not created in: {0}".error(compiledDll);
						else
							return compiledDll;
					}
				}  
				return null;
			}

	}
}
    	