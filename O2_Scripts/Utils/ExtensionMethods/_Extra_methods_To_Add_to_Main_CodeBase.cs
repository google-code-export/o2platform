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
using System.Security.Cryptography;

//O2Ref:O2_API_AST.dll

namespace O2.XRules.Database.Utils
{
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
}
    	
		
		