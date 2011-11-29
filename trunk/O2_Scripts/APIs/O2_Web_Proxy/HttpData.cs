using System;
using System.Text;
using System.IO;
using System.Net;
using System.Collections.Generic;
using O2.Kernel.ExtensionMethods;

namespace O2.XRules.Database.APIs 
{         
	public class RequestResponseData 
	{
		public HttpWebRequest  WebRequest 	 { get; set; }
		public HttpWebResponse WebResponse   { get; set; }				
		
		public String    	   RequestHeaders_Raw { get; set; }	
		public String    	   ResponseHeaders_Raw { get; set; }			
		
		public String    	   RequestPostString { get; set; }			
		public String    	   ResponseString { get; set; }	
		
		public byte[]    	   RequestPostBytes;
		public byte[]    	   ResponseBytes;
		
		public override string ToString()
		{
			return "{0} : {1}".format(WebRequest.RequestUri, ResponseBytes.Length);
		}
		
		public string	RequestUri
		{
			get { return WebRequest.RequestUri.str(); } 
		}
	}
	
	
	public static class RequestResponseData_ExtensionMethods
	{
		public static RequestResponseData add(this  List<RequestResponseData> requests, 
													HttpWebRequest webRequest,
													HttpWebResponse webResponse,
													byte[] responseBytes,
													string responseString )
		{			
			var requestResponseData = new RequestResponseData()
            									{
            										WebRequest = webRequest, 
													WebResponse = webResponse, 
													ResponseBytes = responseBytes,
													ResponseString = responseString 
            									};	
			requests.Add(requestResponseData);
			return requestResponseData;
		}
	}
			                            	

	
}
	