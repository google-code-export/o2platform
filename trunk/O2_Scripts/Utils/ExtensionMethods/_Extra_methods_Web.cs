// Tshis file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Net;
using System.IO;
using System.Text;
using System.Linq;
using System.IO.Compression;
using System.Collections;
using System.Collections.Generic;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.Views.ASCX.CoreControls;
using O2.Views.ASCX.classes.MainGUI;
using O2.Views.ASCX.ExtensionMethods;

using System.Runtime.Serialization.Json;

//O2Ref:System.ServiceModel.Web.dll
//O2Ref:System.Runtime.Serialization.dll

namespace O2.XRules.Database.Utils
{	
	public static class _Extra_Web_ExtensionMethods_Uri
	{
		public static string pathNoQuery(this Uri uri)
		{
			return uri.AbsoluteUri.remove(uri.Query);
		}
	}
	
	public static class _Extra_Web_ExtensionMethods_Http
	{
		public static string html(this string url)
		{
			return url.get_Html();
		}
		public static string get_Html(this string url)
		{
			if (url.isUri())
				return url.uri().get_Html();
			"in get_Html, url provided was not a valid URI: {0}".error(url);
			return null;
		}
		
		public static string get_Html(this Uri url)	// this is a better way to represent it
		{
			return url.getHtml();
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
	
	public static class _Extra_Web_ExtensionMethods_GZip
	{
	
		public static byte[] gzip_Compress(this string _string)
		{
			var bytes = Encoding.ASCII.GetBytes (_string);
			var outputStream = new MemoryStream();
			using (var gzipStream = new GZipStream(outputStream,CompressionMode.Compress))			
				gzipStream.Write(bytes, 0, bytes.size());			
			return outputStream.ToArray();
		}
		
		public static string gzip_Decompress(this byte[] bytes)
		{
			var inputStream = new MemoryStream();
			inputStream.Write(bytes, 0, bytes.Length);
			inputStream.Position = 0;
			var outputStream = new MemoryStream();
			using (var gzipStream= new GZipStream(inputStream,CompressionMode.Decompress))
			{
			    byte[] buffer = new byte[4096];
			    int numRead;
			    while ((numRead = gzipStream.Read(buffer, 0, buffer.Length)) != 0)			    
			        outputStream.Write(buffer, 0, numRead);			    
			}	
			return outputStream.ToArray().ascii();
		}
		
		public static string json_Serialize<T>(T _object)
	    {
	        var serializer = new DataContractJsonSerializer(_object.type());
	        var memoryStream = new MemoryStream();
	        serializer.WriteObject(memoryStream, _object);
	        var serializedObject = Encoding.Default.GetString(memoryStream.ToArray());
	        memoryStream.Dispose();
	        return serializedObject;
	    }

	    public static T json_Deserialize<T>(string json)
	    {
	        T obj = Activator.CreateInstance<T>();
	        MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
	        System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(obj.GetType());
	        obj = (T)serializer.ReadObject(ms);
	        ms.Close();
	        ms.Dispose();
	        return obj;
	    }
	}
}    	