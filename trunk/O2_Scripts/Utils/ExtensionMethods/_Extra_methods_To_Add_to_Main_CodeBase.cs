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

//O2File:ascx_ObjectViewer
//O2Ref:Ionic.Zip.dll
//O2Ref:O2_API_AST.dll

//O2File:_Extra_methods_Collections.cs
//O2File:_Extra_methods_Items.cs
//O2File:_Extra_methods_Reflection.cs
//O2File:_Extra_methods_WinForms_Controls.cs
//O2File:_Extra_methods_WinForms_DataGridView.cs
//O2File:_Extra_methods_WinForms_Misc.cs
//O2File:_Extra_methods_WinForms_TreeView.cs

namespace O2.XRules.Database.Utils
{		
	
	// Other extension method classes
	
	
	public static class _Extra_XElement_LinqXML_extensionMethods
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

		public static string add_xmlns(this string name, XElement xElement)				
		{
			return name.prepend_AttributeValue(xElement, "xmlns");
		}
		
		public static string prepend_AttributeValue(this string name, XElement xElement, string attributeName)
		{
			var xmlns =  xElement.attribute(attributeName).value();
			return "{" + xmlns + "}" + name; 
		}
		
		public static XElement innerXml(this XElement xElement, string value)
		{
			return xElement.value(value);			
		}
		
		public static XElement value(this XElement xElement, string value)
		{
			xElement.Value = value;
			return xElement;
		}
		
	}
	
	public static class _Extra_ConfigFiles_extensionMethods
	{
		// Config files (can't easily put this on the main
        public static Panel editLocalConfigFile(this string file)
        {
            var panel = O2Gui.open<Panel>("Editing local config file: {0}".format(file), 700, 300);
            return file.editLocalConfigFile(panel);
        }
	}
		
	public static class _Extra_Processes_ExtensionMethods
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
	 
	public static class _Extra_Int_ExtensionMethods
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
	public static class _Extra_RegistryKeyExtensionMethods
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
	public static class _Extra_Uri_ExtensionMethods
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
	public static class _Extra_DownloadFiles_ExtensionMethods
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
	
		
	public static class _Extra_O2Gui_ExtensionMethods
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
	
	
	public static class _Extra_XmlLinq_ExtensiomMethods
	{
		public static XAttribute value(this XAttribute xAttribute, string value)
		{	
			if (xAttribute.notNull())
				xAttribute.SetValue(value);
			return xAttribute;
		}		
	}
	
	public static class _Extra_Xml_XSD_ExtensionMethods
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

	public static class _Extra_XML_ExtensionMethods
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
	
	public static class _Extra_AppDomain_ExtensionMethods
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
	
		
	public static class _Extra_ListView_ExtensionMethods
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


	
	public static class _Extra_sourceCodeViewer_ExtensionMethods
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
	
	public static class _Extra_Misc_ExtensionMethods
	{
		public static T deserialize<T>(this string _string, bool fromDisk)
		{
			if (fromDisk && _string.fileExists())
				return _string.deserialize<T>();
			
			return (T)Serialize.getDeSerializedObjectFromString(_string, typeof(T));  
		}
		
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
		
		public static List<string> deleteFiles(this List<string> files)
		{
			foreach(var file in files)
				Files.deleteFile(file);
			return files;
		}
		
		public static List<string> filesContains(this List<string> files, string textToSearch)
		{
			return (from file in files
					where file.fileContents().contains(textToSearch)
					select file).toList();
		}
		
		public static List<string> filesContains_RegEx(this List<string> files, string regExToSearch)
		{
			return (from file in files
					where file.fileContents().regEx(regExToSearch)
					select file).toList();
		}
		
		public static string fromLines_getText(this List<string> lines)
		{
			return StringsAndLists.fromStringList_getText(lines);
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
		
		public static bool isGuid(this String guidValue)
		{
			try
			{
				new Guid(guidValue);
				return true;
			}
			catch
			{
				return false;
			}
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
	}
	
	
	public static class _Extra_ComObject_ExtensionMethods
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
	
	public static class _Extra_H2_ExtensionMethods
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
	
	public static class _Extra_Console_ExtensionMethods
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
	
	public static class _Extra_Zip_ExtensionMethods
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
	
	public static class _Extra_Compile_ExtensionMethods
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
    	