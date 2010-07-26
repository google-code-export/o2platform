// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using System.Xml;
using System.Drawing;
using System.Threading;
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
using O2.Views.ASCX;
using O2.External.SharpDevelop.AST;
using O2.External.SharpDevelop.ExtensionMethods;
using O2.External.SharpDevelop.Ascx;
//using O2.External.IE.ExtensionMethods;
//using O2.External.IE.Wrapper;
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
	public static class ExtraMethodsToAddToO2CodeBase_String
	{
		public static bool isInt(this string value)
		{
			int a =0;
			return Int32.TryParse(value, out a);
		}
	}
	
    public static class ExtraMethodsToAddToO2CodeBase
    {
    	public static string md5Hash(this Bitmap bitmap)
    	{    	
    		try
    		{
    			if (bitmap.isNull())
    				return null;
    			//based on code snippets from http://dotnet.itags.org/dotnet-c-sharp/85838/
				using (MemoryStream strm = new MemoryStream())
				{
					var image = new Bitmap(bitmap);
					bitmap.Save(strm, System.Drawing.Imaging.ImageFormat.Bmp);
					strm.Seek(0, 0);
					byte[] bytes = strm.ToArray();
					MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
					byte[] hashed = md5.TransformFinalBlock(bytes, 0, bytes.Length);					
					string hash = BitConverter.ToString(hashed).ToLower();										
					md5.Clear();
					image.Dispose();					
					return hash;
				} 
			} 
			catch(Exception ex)
			{
				ex.log("in bitmap.md5Hash");
				return "";
			}
		}
		public static bool isNotEqualTo(this Bitmap bitmap1, Bitmap bitmap2)
		{
			return bitmap1.isEqualTo(bitmap2).isFalse();
		}
		public static bool isEqualTo(this Bitmap bitmap1, Bitmap bitmap2 )
		{
			var md5Hash1 = bitmap1.md5Hash();
			var md5Hash2 = bitmap2.md5Hash();
			if (md5Hash1.valid() && md5Hash2.valid())
				return md5Hash1 == md5Hash2;
			else
				"in Bitmap.isEqualTo at least one of the calculated MD5 Hashes was not valid".error();
			return false;
		}
    }   
    
    public static class ExtraMethodsToAddToO2CodeBase_Colections
    {
    	public static List<T> keys<T, T1>(this Dictionary<T, T1> dictionary)
        {
            if (dictionary.notNull())
            	return dictionary.Keys.toList();
            return new List<T>();
        }
    }
    
    public static class ExtraMethodsToAddToO2CodeBase_Serialize_ExtensionMethods
    {
    	public static string serialize(this object _object, bool serializeToFile)
    	{
    		if (serializeToFile)
    			return _object.serialize();
    		return Serialize.createSerializedXmlStringFromObject(_object);
    	}
    }
        
    public static class ExtraMethodsToAddToO2CodeBase_TreeView
    {
    	public static TreeNode select(this TreeView treeView, string text)
    	{
    		foreach(var treeNode in treeView.nodes())
    			if (treeNode.get_Text()==text)
    				return treeNode.selected();
    		return null;
    	}
    	
    }
    
    public static class ExtraMethodsToAddToO2CodeBase_O2_API_AST
    {
    	public static List<INode> iNodes(this List<TypeDeclaration> typeDeclarations)
    	{
    		var iNodes = new List<INode>();
    		foreach(var typeDeclaration in typeDeclarations)
    			iNodes.AddRange(typeDeclaration.iNodes());
    		return iNodes;
    	}
    	
    	public static List<T> iNodes<T>(this List<INode> iNodes)
    		where T : INode
    	{
    		var iNodesInT = new List<T>();
    		foreach(var iNode in iNodes)
    			iNodesInT.AddRange(iNode.iNodes<T>());
    		return iNodesInT;    		
    	}
    	
    	public static List<FieldDeclaration> fields(this List<INode> iNodes)
    	{
    		return iNodes.iNodes<FieldDeclaration>();
    	}
    	
    	public static List<FieldDeclaration> fields(this List<INode> iNodes, string nameToFind)
    	{
    		return(from fieldDeclaration in iNodes.fields()
    			   from name in fieldDeclaration.names()
    			   where name == nameToFind
    			   select fieldDeclaration).toList();    		
    	}
    	
    	public static List<TypeReference> types(this List<FieldDeclaration> fieldDeclarations)
    	{
    		return (from fieldDeclaration in fieldDeclarations    			    
    			    select fieldDeclaration.TypeReference).toList();
    	}
    	
    	public static List<string> names(this List<FieldDeclaration> fieldDeclarations)
    	{
    		return (from fieldDeclaration in fieldDeclarations
    			    from field in fieldDeclaration.Fields
    			    select field.Name).toList();
    	}
    	
    	public static List<string> names(this FieldDeclaration fieldDeclaration)
    	{
    		return (from field in fieldDeclaration.Fields
    			    select field.Name).toList();
    	}
    	
    	public static List<VariableDeclaration> variables(this List<FieldDeclaration> fieldDeclarations)
    	{
    		return (from fieldDeclaration in fieldDeclarations
    			    from field in fieldDeclaration.Fields
    			    select field).toList();
    	}
    	
    	public static Dictionary<string, string> values(this List<FieldDeclaration> fieldDeclarations)
    	{
    		var values = new Dictionary<string, string>();
    		foreach(var variable in fieldDeclarations.variables())
    		{
    			if (variable.Initializer is PrimitiveExpression)
    				values.add(variable.Name,(variable.Initializer as PrimitiveExpression).StringValue);
    			else
    				values.add(variable.Name,variable.Initializer.str());
    		}
    		return values;
    	}
    	
    	
    	public static Dictionary<string, VariableDeclaration> filtered_ByName(this List<FieldDeclaration> fieldDeclarations)
    	{
    		var filtered_ByName = new Dictionary<string, VariableDeclaration>();
    		foreach(var fieldDeclaration in fieldDeclarations)
    			foreach(var variable in fieldDeclaration.Fields)    			
    				filtered_ByName.add(variable.Name, variable);
    		return filtered_ByName;
    	}
    	
    }
        
	public static class ExtraMethodsToAddToO2CodeBase_ascx_TableList
	{
		public static ascx_TableList afterSelect(this ascx_TableList tableList, Action<List<ListViewItem>> callback)
		{	
			tableList.getListViewControl().SelectedIndexChanged +=
		 		(sender,e)=>{
		 						if(callback.notNull())
		 						{
		 							var selectedItems = new List<ListViewItem>();		 						
		 							foreach(ListViewItem item in tableList.getListViewControl().SelectedItems)
		 								selectedItems.Add(item);
		 							callback(selectedItems);
		 						}
		 			    	};
			return tableList;
		}
	}
    

	public static class ExtraMethodsToAddToO2CodeBase_Button
	{
		public static List<Button> buttons(this Control control)
		{
			return control.controls<Button>(true);
		}
		public static Button button(this Control control, string text)
		{
			foreach(var button in control.buttons())
				if (button.get_Text() == text)
					return button;
			return null;
		}
	}
	public static class ExtraMethodsToAddToO2CodeBase_WinformControls
	{
		public static Control control<T>(this T hostControl, string controlName, bool recursive)
			where T : Control
		{
			var controls = hostControl.controls(recursive);
			foreach(var control in controls)				
   				if (control.typeName() == controlName)
					return control;
    		return null;			
		}
		
	}
}
    	
		
		