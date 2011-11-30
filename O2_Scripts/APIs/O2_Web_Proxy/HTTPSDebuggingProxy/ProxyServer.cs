//based on the code from http://www.codeproject.com/KB/IP/HTTPSDebuggingProxy.aspx
//originally coded by @matt_mcknight  

// see O2_Web_Proxy.cs API for a way to consume this Proxy from O2
 
//O2File:ProxyCache.cs

//O2Ref:System.Configuration.dll 
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

using O2.Kernel.ExtensionMethods;
using O2.XRules.Database.APIs;
using O2.XRules.Database.Utils;

//O2File:HttpData.cs
//O2File:ProxyCache.cs
//O2File:_Extra_methods_Web.cs
//O2File:_Extra_methods_Items.cs

namespace HTTPProxyServer
{       
	
    public sealed class ProxyServer
    {
    	//O2 specific
    	public static Func<HttpWebRequest,string, bool> HandleWebRequestProxyCommands;
        public static Func<string, string> InterceptRemoteUrl;
        public static Func<Uri, bool> InterceptResponseHtml;    
        public static Func<Uri,string, string> HtmlContentReplace;
        public static Action<HttpWebRequest> InterceptWebRequest;
        public static Action<RequestResponseData> OnResponseReceived;
                
        public bool CaptureRequests { get; set; }
        public bool ProxyStarted { get; set; }
        
        public static ProxyCache proxyCache;

		//Original
        private static readonly ProxyServer _server = new ProxyServer();

        private static readonly int BUFFER_SIZE = 8192;
        private static readonly char[] semiSplit = new char[] { ';' };
        private static readonly char[] equalSplit = new char[] { '=' };
        private static readonly String[] colonSpaceSplit = new string[] { ": " };
        private static readonly char[] spaceSplit = new char[] { ' ' };
        private static readonly char[] commaSplit = new char[] { ',' };
        private static readonly Regex cookieSplitRegEx = new Regex(@",(?! )");
        private static X509Certificate2 _certificate;
        private static object _outputLockObj = new object();


        private TcpListener _listener;
        private Thread _listenerThread;
//        private Thread _cacheMaintenanceThread;
                
        public IPAddress ListeningIPInterface 	{ get ; set; } //DC                
        public Int32 ListeningPort 				{ get ; set; } //DC        

        private ProxyServer()
        {        	
        	setDefaultValues();
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        }
		
		
        public Boolean DumpHeaders { get; set; }
        public Boolean DumpPostData { get; set; }
        public Boolean DumpResponseData { get; set; }

        public static ProxyServer Server
        {
            get { return _server; }
        }
		
		public void setDefaultValues()
		{
			proxyCache = new ProxyCache();
			CaptureRequests = true;
			this.clearPastRequests();
			IPAddress ipAddress = IPAddress.Loopback;
			IPAddress.TryParse("127.0.0.1", out ipAddress);
			ListeningIPInterface = ipAddress;
			ListeningPort = 8081;
		}		        

        public bool Start(string certFilePath)
        {             	        	
            "..Starting Proxy Server using Certificate: {0}".info(certFilePath);
            try
            {                
                try
                {
                    _certificate = new X509Certificate2(certFilePath);
                }
                catch (Exception ex)
                {
                	"[ProxyServer] Start: Could not create the certificate from file from {0}. Exception: ".error(certFilePath, ex.Message);
                	return false;
                    //throw new ConfigurationErrorsException(String.Format("Could not create the certificate from file from {0}",certFilePath), ex);
                }
                
                "[ProxyServer] Starting listener on IP {0} and Port {1}".format(ListeningIPInterface, ListeningPort);
                _listener = new TcpListener(ListeningIPInterface, ListeningPort);                
                _listener.Start();
            }
            catch (Exception ex)
            {
            	"[ProxyServer] Start: {0}".error(ex.Message); 
                //Console.WriteLine(ex.Message);
                return false;
            }
			"[ProxyServer] listener started".info();
            _listenerThread = new Thread(new ParameterizedThreadStart(Listen));
                        
            //_cacheMaintenanceThread = new Thread(new ThreadStart(ProxyCache.CacheMaintenance));

            _listenerThread.Start(_listener);
//            _cacheMaintenanceThread.Start();
			this.ProxyStarted = true;
            return true;
        }

        public void Stop()
        {
            "Stopping Proxy Server".info();
            this.ProxyStarted = false;
            _listener.Stop();

            //wait for server to finish processing current connections...

            _listenerThread.Abort();
//            _cacheMaintenanceThread.Abort();
            _listenerThread.Join();
            _listenerThread.Join();
        }

        private static void Listen(Object obj)
        {
            TcpListener listener = (TcpListener)obj;
            try
            {
                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    while (!ThreadPool.QueueUserWorkItem(new WaitCallback(ProxyServer.ProcessClient), client)) ;
                }
            }
            catch (ThreadAbortException) { }
            catch (SocketException) { }
        }


        private static void ProcessClient(Object obj)
        {
            TcpClient client = (TcpClient)obj;
            try
            {
                DoHttpProcessing(client);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ex.log();
            }
            finally
            {
                client.Close();
            }
        }		


        private static void DoHttpProcessing(TcpClient client)
        {                    	
            Stream clientStream = client.GetStream();
            Stream outStream = clientStream; //use this stream for writing out - may change if we use ssl
            SslStream sslStream = null;
            StreamReader clientStreamReader = new StreamReader(clientStream);
            //CacheEntry cacheEntry = null;
            MemoryStream cacheStream = null;

			Action setMonitor = 
				()=>{                        
			            if (Server.DumpHeaders || Server.DumpPostData || Server.DumpResponseData)
			            {
			                //make sure that things print out in order - NOTE: this is bad for performance
			                Monitor.TryEnter(_outputLockObj, TimeSpan.FromMilliseconds(-1.0));
			            }
			         };
			
			setMonitor();
            
            try
            {
                        
                //read the first line HTTP command
                String httpCmd = clientStreamReader.ReadLine();
                if (String.IsNullOrEmpty(httpCmd))
                {
                    clientStreamReader.Close();
                    clientStream.Close();
                    return;
                }
                //break up the line into three components
                String[] splitBuffer = httpCmd.Split(spaceSplit, 3);

                String method = splitBuffer[0];
                String remoteUri = splitBuffer[1];
                Version version = new Version(1, 0);

                HttpWebRequest webReq = null;
                HttpWebResponse response = null;
                var rawRequestHeaders = new StringBuilder();
                var rawResponseHeaders = new StringBuilder();
                byte[] requestPostBytes = null;
                
                int contentLen = 0;
                
                Action handleSLL_CONNECT_withCaller = 
                	()=>{
			                if (splitBuffer[0].ToUpper() == "CONNECT")
			                {
			                    //Browser wants to create a secure tunnel
			                    //instead = we are going to perform a man in the middle "attack"
			                    //the user's browser should warn them of the certification errors however.
			                    //Please note: THIS IS ONLY FOR TESTING PURPOSES - you are responsible for the use of this code
			                    remoteUri = "https://" + splitBuffer[1];
			                    while (!String.IsNullOrEmpty(clientStreamReader.ReadLine())) ;
			                    StreamWriter connectStreamWriter = new StreamWriter(clientStream);
			                    connectStreamWriter.WriteLine("HTTP/1.0 200 Connection established");
			                    connectStreamWriter.WriteLine(String.Format("Timestamp: {0}", DateTime.Now.ToString()));
			                    connectStreamWriter.WriteLine("Proxy-agent: matt-dot-net");
			                    connectStreamWriter.WriteLine();
			                    connectStreamWriter.Flush();
			                    
			                    connectStreamWriter.Close();   //see if it has side effects with ssl sites
			                }
						};

				Action handleSLL_CONNECT_withRemote = 
                	()=>{
			                if (splitBuffer[0].ToUpper() == "CONNECT")
			                {			                			
			                    sslStream = new SslStream(clientStream, false);
			                    try
			                    {
			                        //sslStream.AuthenticateAsServer(_certificate, false, SslProtocols.Tls | SslProtocols.Ssl3 | SslProtocols.Ssl2, true);
			                        sslStream.AuthenticateAsServer(_certificate, false, SslProtocols.Tls | SslProtocols.Ssl3 | SslProtocols.Ssl2, false);
			                    }
			                    catch (Exception ex)
			                    {
			                    	"[Proxy Server] in sslStream.AuthenticateAsServer: {0}".error(ex.Message);
			                    	"NOTE: this error is usually caused by running with UAC, start the script in non UAC".lineBefore().lineBeforeAndAfter().info();
			                    	//ex.log();
			                        //Console.WriteLine(ex.Message);
			                        sslStream.Close();
			                        clientStreamReader.Close();
			                        //connectStreamWriter.Close();
			                        clientStream.Close();
			                        return;
			                    }
			
			                    //HTTPS server created - we can now decrypt the client's traffic
			                    clientStream = sslStream;
			                    clientStreamReader = new StreamReader(sslStream);
			                    outStream = sslStream;
			                    //read the new http command.
			                    httpCmd = clientStreamReader.ReadLine();
			                    if (String.IsNullOrEmpty(httpCmd))
			                    {
			                        clientStreamReader.Close();
			                        clientStream.Close();
			                        sslStream.Close();
			                        return;
			                    }
			                    splitBuffer = httpCmd.Split(spaceSplit, 3);
			                    method = splitBuffer[0];
			                    remoteUri = remoteUri + splitBuffer[1];
			                }
		                };

				//handleSLL_CONNECT();
				handleSLL_CONNECT_withCaller();
				
				handleSLL_CONNECT_withRemote();
				                
				
				// O2 callback				
                if (InterceptRemoteUrl.notNull())						
                {                	
                    remoteUri = InterceptRemoteUrl(remoteUri);
				}
				
				
				
				Action createWebRequest =
					()=>{								
							//construct the web request that we are going to issue on behalf of the client.
			                webReq = (HttpWebRequest)HttpWebRequest.Create(remoteUri);
			                webReq.Method = method;
			                webReq.ProtocolVersion = version;
			
			                //read the request headers from the client and copy them to our request
			                contentLen = ReadRequestHeaders(clientStreamReader, webReq, rawRequestHeaders);
			                
			                webReq.Proxy = null;
			                webReq.KeepAlive = false;
			                webReq.AllowAutoRedirect = false;
			                webReq.AutomaticDecompression = DecompressionMethods.None;
						};
						
				createWebRequest();						
				
				if (HandleWebRequestProxyCommands.notNull() && HandleWebRequestProxyCommands(webReq,remoteUri) == false )			// O2 callback
				{
					"[ProxyServer] HandleWebRequestProxyCommands returned false, so dropping the request".info();
					return; 
				}
				//"WebRequest set with contentLen: {0}".info(contentLen);

                //String.Format("{0} {1} HTTP/{2}", webReq.Method, webReq.RequestUri.AbsoluteUri, webReq.ProtocolVersion).debug();
                if(Server.DumpHeaders)
                {
                    Console.WriteLine(String.Format("{0} {1} HTTP/{2}",webReq.Method,webReq.RequestUri.AbsoluteUri, webReq.ProtocolVersion));
                    DumpHeaderCollectionToConsole(webReq.Headers);
                }

                if (InterceptWebRequest.notNull())
                    InterceptWebRequest(webReq);
                            
/*                //using the completed request, check our cache
                if (method.ToUpper() == "GET")
                    cacheEntry = ProxyCache.GetData(webReq);
                else 
*/                
                if (method.ToUpper() == "POST")
                {
                    char[] postBuffer = new char[contentLen];
                    int bytesRead;
                    int totalBytesRead = 0;
                    StreamWriter sw = new StreamWriter(webReq.GetRequestStream());
                    var postData = new StreamWriter(new MemoryStream());
                    while (totalBytesRead < contentLen && (bytesRead = clientStreamReader.ReadBlock(postBuffer, 0, contentLen)) > 0)
                    {
                        totalBytesRead += bytesRead;
                        sw.Write(postBuffer, 0, bytesRead);
                        
                        postData.Write(postBuffer, 0, bytesRead);
                        //"totalBytesRead: {0}".error(totalBytesRead);
                        //if (ProxyServer.Server.DumpPostData)
                        //    Console.Write(postBuffer, 0, bytesRead);
                    }              
                    postData.Flush();
                    requestPostBytes = (postData.BaseStream as MemoryStream).ToArray();

                    sw.Close();
                }
                
				Action getResponse = 
					()=>{
							webReq.Timeout = 15000;
		
		                    try
		                    {
		                        response = (HttpWebResponse)webReq.GetResponse();
		                    }
		                    catch (WebException webEx)
		                    {
		                        response = webEx.Response as HttpWebResponse;
		                        webEx.log();
		                    }
						};
						
				Action handleResponse = 
					()=>{											                        
	                        StreamWriter myResponseWriter = new StreamWriter(outStream);
	                        Stream responseStream = response.GetResponseStream();
	                        	                        
	                        if (false) // disable cache for now
	                        {
		                        if (proxyCache.hasMapping(webReq.RequestUri.AbsolutePath))
		                        {
		                        	"Cache Hit for: {0}".error(webReq.RequestUri.AbsolutePath);
		                        	
		                        	var cacheObject = proxyCache.requestMappings()[webReq.RequestUri.AbsolutePath];
		                        	
		                        	WriteResponseStatus(cacheObject.WebResponse.StatusCode,
		                        					    cacheObject.WebResponse.StatusDescription, 
		                        					    myResponseWriter);
		                        					    
		                            WriteResponseHeaders(myResponseWriter, cacheObject.Response_Headers);
		                            /*var message = cacheObject.Response_String.replace("Web","Web_");
		                            var bytes = Encoding.ASCII.GetBytes(message);
		                            outStream.Write(bytes, 0 , bytes.Length);
		                            */
		                        	outStream.Write(cacheObject.Response_Bytes, 0, cacheObject.Response_Bytes.Length);
		                        	//"cacheObject.Response_String: {0}".info(cacheObject.Response_String);
		                        	
			                        responseStream.Close();
			                        outStream.Flush();
			                        
			                        responseStream.Close();
			                        response.Close();
			                        myResponseWriter.Close();		                            		                        
			                        return;
			                    }
			                    "no Cache Hit for: {0}".debug(webReq.RequestUri.AbsolutePath);
		                   	}
							List<Tuple<String,String>> responseHeaders = ProcessResponse(response, rawResponseHeaders);	                        
	                        try
	                        {	                        	
	                            //send the response status and response headers
	                            WriteResponseStatus(response.StatusCode,response.StatusDescription, myResponseWriter);
	                            WriteResponseHeaders(myResponseWriter, responseHeaders);
	
	                            //DateTime? expires = null;
	                            //CacheEntry entry = null;
/*	                            Boolean canCache = (sslStream == null && ProxyCache.CanCache(response.Headers, ref expires));
	                            if (canCache)
	                            {
	                                entry = ProxyCache.MakeEntry(webReq, response,responseHeaders, expires);
	                                if (response.ContentLength > 0)
	                                    cacheStream = new MemoryStream(entry.ResponseBytes);
	                            }
*/	
	                            Byte[] buffer;
	                            if (response.ContentLength > 0)
	                                buffer = new Byte[response.ContentLength];
	                            else
	                                buffer = new Byte[BUFFER_SIZE];
	
	                            int bytesRead;
	                            
	                            //new System.IO.().Wr
	                            
								//var bytesReceived = new List<byte>();
								var memoryStream = new MemoryStream();
								var binaryWriter = new  BinaryWriter(memoryStream);
								
								
	                            while ((bytesRead = responseStream.Read(buffer, 0, buffer.Length)) > 0)
	                            {	
	                            	binaryWriter.Write(buffer, 0, bytesRead);	                            	
	
	                            }
	                            binaryWriter.Flush();
	                            
	                            if (memoryStream.Length >  Int32.MaxValue)
	                            	"[ProxyServer]: memoryStream.Length >  Int32.MaxValue".error();
	                            	
	                            outStream.Write(memoryStream.ToArray(), 0, (int)memoryStream.Length);
	                            
	                            	                            
                            	var responseString = (response.ContentEncoding == "gzip")
                            							? memoryStream.ToArray().gzip_Decompress()
                            							: memoryStream.ToArray().ascii();                            							                            	
                            	                            	
                            	var requestResponseData = proxyCache.add(webReq, response, memoryStream.ToArray(), responseString);
                            	
                            	requestResponseData.Request_Headers_Raw = rawRequestHeaders.str();                            	
                            	requestResponseData.Response_Headers_Raw = rawResponseHeaders.str();                            	
                            	requestResponseData.Response_Headers = responseHeaders;
                            	
                            	if (requestPostBytes.notNull())
                            	{
                            		requestResponseData.Request_PostBytes = requestPostBytes; 
                    				requestResponseData.Request_PostString = requestPostBytes.ascii();
                    			}
                            	
	                            if (OnResponseReceived.notNull()) 	                            
	                            	OnResponseReceived(requestResponseData);//webReq, response,memoryStream.ToArray().ascii());//UTF8Encoding.UTF8.GetString(memoryStream.ToArray(), 0, (int)memoryStream.Length));	                            
	                            		
	                            responseStream.Close();
	                            
	                            /*if (cacheStream != null)
	                            {
	                                cacheStream.Flush();
	                                cacheStream.Close();
	                            }*/
	
	                            outStream.Flush();
/*	                            if (canCache)
	                                ProxyCache.AddData(entry);
*/	                                
	                        }
	                        catch (Exception ex)
	                        {
	                            Console.WriteLine(ex.Message);
	                            ex.log("handleResponse_noCache");
	                        }
	                        finally
	                        {
	                            responseStream.Close();
	                            response.Close();
	                            myResponseWriter.Close();
	                        }		                    
						};
						
						
				Action handleResponse_noCache =
					()=>{								
							//Console.WriteLine(String.Format("ThreadID: {2} Requesting {0} on behalf of client {1}", webReq.RequestUri, client.Client.RemoteEndPoint.ToString(), Thread.CurrentThread.ManagedThreadId));
		                    
		                    getResponse();
		                	if (response != null)
		                		handleResponse();
		                	else
		                		"[ProxyServer] Response was null".error();
						};
				
/*				Action handleResponse_viaCache = 
					()=>{
							"CACHE SHOULD BE DISABLED (for now)".error();
		                    //serve from cache
		                    StreamWriter myResponseWriter = new StreamWriter(outStream);
		                    try
		                    {
		                        WriteResponseStatus(cacheEntry.StatusCode, cacheEntry.StatusDescription, myResponseWriter);
		                        WriteResponseHeaders(myResponseWriter, cacheEntry.Headers);
		                        if (cacheEntry.ResponseBytes != null)
		                        {
		                            outStream.Write(cacheEntry.ResponseBytes, 0, cacheEntry.ResponseBytes.Length);
		                            if (ProxyServer.Server.DumpResponseData)
		                                Console.Write(UTF8Encoding.UTF8.GetString(cacheEntry.ResponseBytes));
		                        }
		                        myResponseWriter.Close();
		                    }
		                    catch (Exception ex)
		                    {
		                        Console.WriteLine(ex.Message);
		                        ex.log("handleResponse_viaCache");
		                    }
		                    finally
		                    {
		                        myResponseWriter.Close();
		                    }
						};
				
                if ( true || cacheEntry == null)		//DCz: Disabling cache for now
                {
                    handleResponse_noCache();
                }
                else
                {
                	handleResponse_viaCache();
                }
                */
                handleResponse_noCache();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ex.log();
            }
            finally
            {
                if (Server.DumpHeaders || Server.DumpPostData || Server.DumpResponseData)
                {
                    //release the lock
                    Monitor.Exit(_outputLockObj);
                }

                clientStreamReader.Close();
                clientStream.Close();
                if (sslStream != null)
                    sslStream.Close();
                outStream.Close();
                if (cacheStream != null)
                    cacheStream.Close();
            }

        }

        private static List<Tuple<String,String>> ProcessResponse(HttpWebResponse response, StringBuilder rawResponseHeaders)
        {
            String value=null;
            String header=null;
            List<Tuple<String, String>> returnHeaders = new List<Tuple<String, String>>();
            foreach (String s in response.Headers.Keys)
            {
            	rawResponseHeaders.AppendLine("{0}: {1}".format(s, response.Headers[s]));
            	
                if (s.ToLower() == "set-cookie")				//DCz: it looks like this will not cause multiple set-cookies to fail
                {
                    header = s;
                    value = response.Headers[s];
                }
                else
                    returnHeaders.Add(new Tuple<String, String>(s, response.Headers[s]));
            }
            
            //if (!String.IsNullOrWhiteSpace(value))
            if (!String.IsNullOrEmpty(value))
            {
                response.Headers.Remove(header);
                String[] cookies = cookieSplitRegEx.Split(value);
                foreach (String cookie in cookies)
                    returnHeaders.Add(new Tuple<String, String>("Set-Cookie", cookie));

            }
            returnHeaders.Add(new Tuple<String, String>("X-Proxied-By", "O2-Platform-Proxy")); //"matt-dot-net proxy"));
            return returnHeaders;
        }

        private static void WriteResponseStatus(HttpStatusCode code, String description, StreamWriter myResponseWriter)
        {
            String s = String.Format("HTTP/1.0 {0} {1}", (Int32)code, description);
            myResponseWriter.WriteLine(s);
            if(ProxyServer.Server.DumpHeaders)
                Console.WriteLine(s);
        }

        private static void WriteResponseHeaders(StreamWriter myResponseWriter, List<Tuple<String,String>> headers)
        {
            if (headers != null)
            {
                foreach (Tuple<String,String> header in headers)
                    myResponseWriter.WriteLine(String.Format("{0}: {1}", header.Item1,header.Item2));
            }
            myResponseWriter.WriteLine();
            myResponseWriter.Flush();

            if (Server.DumpHeaders)
                DumpHeaderCollectionToConsole(headers);
        }

        private static void DumpHeaderCollectionToConsole(WebHeaderCollection headers)
        {
            foreach (String s in headers.AllKeys)
                Console.WriteLine(String.Format("{0}: {1}", s,headers[s]));
            Console.WriteLine();
        }

        private static void DumpHeaderCollectionToConsole(List<Tuple<String,String>> headers)
        {
            foreach (Tuple<String,String> header in headers)
                Console.WriteLine(String.Format("{0}: {1}", header.Item1,header.Item2));
            Console.WriteLine();
        }

        private static int ReadRequestHeaders(StreamReader sr, HttpWebRequest webReq, StringBuilder rawRequestHeaders)
        {        	
            String httpCmd;
            int contentLen = 0;
            do
            {
                httpCmd = sr.ReadLine();
                if (String.IsNullOrEmpty(httpCmd))
                    return contentLen;
                rawRequestHeaders.AppendLine(httpCmd);
                String[] header = httpCmd.Split(colonSpaceSplit, 2, StringSplitOptions.None);
                switch (header[0].ToLower())
                {
                    case "host":
                        //webReq.Host = header[1];
                        break;
                    case "user-agent":
                        webReq.UserAgent = header[1];
                        break;
                    case "accept":
                        webReq.Accept = header[1];
                        break; 
                    case "referer":
                        webReq.Referer = header[1];
                        break;
                    case "cookie":
                        webReq.Headers["Cookie"] = header[1];
                        break;
                    case "proxy-connection":
                    case "connection":
                    case "keep-alive":
                        //ignore these
                        break;
                    case "content-length":
                        int.TryParse(header[1], out contentLen);
                        break;
                    case "content-type":
                        webReq.ContentType = header[1];
                        break;
                    case "if-modified-since":
                        String[] sb = header[1].Trim().Split(semiSplit);
                        DateTime d;
                        if (DateTime.TryParse(sb[0], out d))
                            webReq.IfModifiedSince = d;
                        break;
                    default:
                        try
                        {
                            webReq.Headers.Add(header[0], header[1]);
                        }
                        catch (Exception ex)
                        {
                            String.Format("Could not add header {0}.  Exception message:{1}".error(header[0], ex.Message));
                            ex.log();
                        }
                        break;
                }
            } while (!String.IsNullOrEmpty(httpCmd)); //(!String.IsNullOrWhiteSpace(httpCmd));
            return contentLen;
        }
    }                
    
    public static class ProxyServer_ExtensionMethods_Misc
    {
    	public static ProxyServer clearPastRequests(this ProxyServer proxyServer)
    	{
    		if (ProxyCache.Requests.notNull())
    		{
    			"[ProxyServer] clearing PastRequests object".info();
    			ProxyCache.Requests.Clear();
    		}
    		else    		
	    		ProxyCache.Requests = new List<RequestResponseData>();
	    	return proxyServer;
    	}
    	
    	public static List<RequestResponseData> requests(this ProxyServer proxyServer)
    	{
    		return ProxyCache.Requests;
    	}
    }
    
}
