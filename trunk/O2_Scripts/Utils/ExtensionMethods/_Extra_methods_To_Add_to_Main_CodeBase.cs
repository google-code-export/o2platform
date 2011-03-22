// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using System.Xml;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Win32;
using O2.Interfaces.O2Core;
using O2.Interfaces.O2Findings;
using O2.Kernel;
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

namespace O2.XRules.Database.Utils
{
	public static class ExtraMethodsToAddToO2CodeBase_IO
	{
		//Reflection
		
		public static MethodInfo method(this Type type, string name)
		{
			foreach(var method in type.methods())
			{				
				if (method.Name == name)
					return method;
			}
			return null;
		}
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
	
	//TreeNode
	
	public static class TreeNode_ExtensionMethods
	{
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
}
    	