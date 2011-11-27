 using System;
using System.Text;
using System.IO;
using System.Net;
using O2.Kernel.ExtensionMethods;

namespace O2.XRules.Database.APIs 
{       
  //Web Edited
	public class RequestResponseData 
	{
		public HttpWebRequest  WebRequest 	 { get; set; }
		public HttpWebResponse WebResponse   { get; set; }
		public byte[]    	   ResponseBytes { get; set; }	
		public String    	   ResponseString { get; set; }	
	}
}
	