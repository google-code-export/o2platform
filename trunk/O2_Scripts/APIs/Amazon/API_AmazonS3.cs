// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Text;
using O2.Kernel;
using O2.Kernel.ExtensionMethods; 
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Views.ASCX.ExtensionMethods;
using O2.Views.ASCX.classes.MainGUI;
using O2.XRules.Database.Utils.O2;

using Amazon.S3.Model;
using Amazon.S3;
using Amazon;

//O2File:ISecretData.cs
//O2File:SecretData_ExtensionMethods.cs

//O2Ref:AWSSDK.dll

namespace O2.XRules.Database.APIs
{	
	public class API_AmazonS3
	{
	
		public string defaultCredentialsFile = @"C:\O2\_USERDATA\AmazonS3.xml";
		public ICredential Credential { get; set; }
		public AmazonS3 AmazonS3 { get; set; }
		
		public API_AmazonS3()
		{}
		
		public API_AmazonS3 login()
		{
			return login(defaultCredentialsFile);
		}
		
		public API_AmazonS3 login(string credentialsFile)
		{
			if (credentialsFile.fileExists())
    			this.Credential = credentialsFile.credential("AmazonS3");    		
    		return login(this.Credential);    		
    	}
 
    	public API_AmazonS3 login(string username, string password)
    	{
    		return login(new Credential(username, password));
    	}
 
    	public API_AmazonS3 login(ICredential credential)
    	{
    		AmazonS3 = AWSClientFactory.CreateAmazonS3Client(credential.username(), credential.password());
    		
    		return this;
    	}
	}
}