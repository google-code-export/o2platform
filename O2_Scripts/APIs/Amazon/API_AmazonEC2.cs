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
using O2.XRules.Database.Utils;

using Amazon.S3.Model;
using Amazon.S3;
using Amazon;

//O2File:ISecretData.cs
//O2File:SecretData_ExtensionMethods.cs
//O2File:ascx_AskUserForLoginDetails.cs

//O2Ref:AWSSDK.dll

namespace O2.XRules.Database.APIs
{	
	public class API_AmazonEC2
	{			
		public ICredential Credential { get; set; }
		
		public API_AmazonEC2()
		{
			//todo (add lamda methods from 'Tool - Amazon EC2 Browser.h2')
		}
	}
}