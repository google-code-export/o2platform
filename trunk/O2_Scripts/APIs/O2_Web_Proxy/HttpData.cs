using System;
using System.Text;
using System.IO;
using System.Net;
using O2.Kernel.ExtensionMethods;

namespace O2.XRules.Database.APIs 
{         
	public class RequestResponseData 
	{
		public HttpWebRequest  WebRequest 	 { get; set; }
		public HttpWebResponse WebResponse   { get; set; }
		public byte[]    	   ResponseBytes { get; set; }	
		public String    	   ResponseString { get; set; }	
		
		public override string ToString()
		{
			return "{0} : {1}".info(WebRequest.RequestUri, ResponseBytes.Length);
		}
	}
}
	