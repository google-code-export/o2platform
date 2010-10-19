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
		// Config files
        public static Panel editLocalConfigFile(this string file)
        {
            var panel = O2Gui.open<Panel>("Editing local config file: {0}".format(file), 700, 300);
            return file.editLocalConfigFile(panel);
        }

		//DateTime extensionMethods
		
		public static string safeFileName(this DateTime dateTime)
		{
			return Files.getSafeFileNameString(dateTime.str()); 
		}
		
		// ASCX TextBox
		
		public static TextBox allowTabs(this TextBox textBox)
		{
			return textBox.acceptsTab();
		}
		public static TextBox acceptsTab(this TextBox textBox)
		{
			return textBox.acceptsTab(true);
		}
		public static TextBox acceptsTab(this TextBox textBox, bool value)
		{
			textBox.invokeOnThread(()=> textBox.AcceptsTab = value);
			return textBox;
		}
		// Collections Dictionary<string,string>
		
		
		//Processes ExtensionMethods API				
		
		
	
		// Controls ExtensionMethods
		
		public static Form opacity(this Form form, double value)			
		{
			form.invokeOnThread(
				()=>{
						form.Opacity = value;
					});
			return form;
		}
		
		public static TextBox add_TextBox(this Control control, string labelText, string defaultTextBoxText)
		{
			return control.add_Label(labelText).top(3)
						  .append_TextBox(defaultTextBoxText)
						  .align_Right(control);;
		}
		
		public static TextBox add_TextBox(this Control control, int top, string labelText, string defaultTextBoxText)
		{
			return control.add_Label(labelText).top(top+3)
						  .append_TextBox(defaultTextBoxText)
						  .align_Right(control);;
		}
		
		public static CheckBox add_CheckBox(this Control control, int top, string checkBoxText)
		{
			return control.add_CheckBox(top, 0, checkBoxText);
		}
		
		public static CheckBox add_CheckBox(this Control control, int top,int left, string checkBoxText)
		{			
			return control.add_CheckBox(checkBoxText, top, left,(value)=> {})
						  .autoSize();
		}
		
		public static Button add_Button(this Control control, int top, string buttonText)
		{
			return control.add_Button(top, 0, buttonText);
		}
		
		public static Button add_Button(this Control control, int top,int left, string buttonText)
		{
			return control.add_Button(buttonText, top, left);
		}
		
		
		//PropertyGrid
		
		public static PropertyGrid toolBarVisible(this PropertyGrid propertyGrid, bool value)
		{
			propertyGrid.invokeOnThread(()=>propertyGrid.ToolbarVisible = value);
		
			return propertyGrid;
		}
		
		public static PropertyGrid helpVisible(this PropertyGrid propertyGrid, bool value)
		{
			propertyGrid.invokeOnThread(()=>propertyGrid.HelpVisible = value);		
			return propertyGrid;
		}
		
		public static PropertyGrid sort_Alphabetical(this PropertyGrid propertyGrid)
		{
			propertyGrid.invokeOnThread(()=>propertyGrid.PropertySort = PropertySort.Alphabetical);		
			return propertyGrid;
		}
		
		public static PropertyGrid sort_Categorized(this PropertyGrid propertyGrid)
		{
			propertyGrid.invokeOnThread(()=>propertyGrid.PropertySort = PropertySort.Categorized);		
			return propertyGrid;
		}
		
		public static PropertyGrid sort_CategorizedAlphabetical(this PropertyGrid propertyGrid)
		{
			propertyGrid.invokeOnThread(()=>propertyGrid.PropertySort = PropertySort.CategorizedAlphabetical);		
			return propertyGrid;
		}
		
		
		// ascx_Directory
		public static ascx_Directory processDroppedObjects(this ascx_Directory directory, bool value)
		{
			directory.invokeOnThread(()=>directory._ProcessDroppedObjects = value);
			return directory;
		}
		
		public static ascx_Directory handleDrop(this ascx_Directory directory, bool value)
		{
			directory.invokeOnThread(()=>directory._HandleDrop = value);
			return directory;
		}					
		
		// ascx_SourceCodeEditor ExtensionMethods
		public static ascx_SourceCodeEditor editScript(this string scriptOrFile)
		{
			if (scriptOrFile.fileExists().isFalse())
			{
				if (scriptOrFile.local().valid())				
					scriptOrFile= scriptOrFile.local();				
				else
				{					
					var h2Script = new H2(scriptOrFile);
					scriptOrFile = PublicDI.config.getTempFileInTempDirectory(".h2");
					h2Script.save(scriptOrFile);
				}
			}			
			return O2Gui.open<Panel>(scriptOrFile.fileName(),800,400)
			     		.add_SourceCodeEditor()
			     		.open(scriptOrFile);
		}
						
		// Objects
		
		public static Form lastFormLoaded(this string dummyString)
		{
			return dummyString.lastWindowShown();
		}
		public static Form lastWindowShown(this string dummyString)
		{
			return dummyString.applicationWinForms().Last();
		}
		
		public static Exception log(this Exception ex)
		{
			ex.log("");
			return ex;
		}
		
		// ascx_TableList in O2.Views.ASCX.DataViewers
		public static ascx_TableList title(this ascx_TableList tableList, string title)
		{
			tableList.invokeOnThread(()=> tableList._Title = title );
			return tableList;
			
		}
		
		public static ascx_TableList show(this ascx_TableList tableList, object targetObject)	
		{			
			if (tableList.notNull() && targetObject.notNull())
			{
				tableList.clearTable();
				tableList.title("{0}".format(targetObject.typeFullName()));  
				tableList.add_Columns("name","value"); 
				foreach(var property in PublicDI.reflection.getProperties(targetObject))
					tableList.add_Row(property.Name, targetObject.prop(property.Name).str());
				tableList.makeColumnWidthMatchCellWidth();					
			}
			return tableList;
		}


		//screenshots
		
		public static string saveImageFromClipboard(this object _object)
		{
			var sync = new AutoResetEvent(false);
			string savedImage = null;
			O2Thread.staThread(
				()=>{
						var bitmap = new Control().fromClipboardGetImage();  
						if (bitmap.notNull())
						{
							savedImage = bitmap.save();
							savedImage.toClipboard(); 
							"Image in clipboard was saved to: {0}".info(savedImage);
						}
						sync.Set();							
					});
					
			sync.WaitOne(2000);
			
			return savedImage;
		}
		
		// we need to do this because the clipboard can only be accessed from an STA thread
		public static string clipboardText_Get(this object _object)
		{
			var sync = new AutoResetEvent(false);
			string clipboardText = null;
			O2Thread.staThread(
				()=>{
						clipboardText = O2Forms.getClipboardText();						
						sync.Set();							
					});					
			sync.WaitOne(2000);		
			return clipboardText;
		}
		// poing existing toClipboard(this string _) to this
		public static string clipboardText_Set(this string newClipboardText)
		{
			var sync = new AutoResetEvent(false);			
			O2Thread.staThread(
				()=>{
						O2Forms.setClipboardText(newClipboardText);
						sync.Set();							
					});					
			sync.WaitOne(2000);		
			return newClipboardText;
		}
	}	   
}
    	