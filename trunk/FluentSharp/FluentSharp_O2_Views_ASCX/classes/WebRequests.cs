// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using FluentSharp.O2.DotNetWrappers.DotNet;
using FluentSharp.O2.DotNetWrappers.ExtensionMethods;
using FluentSharp.O2.DotNetWrappers.Windows;
using FluentSharp.O2.DotNetWrappers.Zip;
using FluentSharp.O2.Interfaces.Views;
using FluentSharp.O2.Kernel.CodeUtils;
using FluentSharp.O2.Views.ASCX.CoreControls;

namespace FluentSharp.O2.Views.ASCX.classes
{
    public class WebRequests
    {
        public static void downloadFileUsingAscxDownload(String sFileToDownload,
                                                         Callbacks.dMethod_String downloadDemoFileCallback)
        {
            if (String.IsNullOrEmpty(sFileToDownload))
                DI.log.error("in downloadFileUsingAscxDownload: No file provided");
            else
            {
                string sTargetFile = Path.Combine(DI.config.O2TempDir, Path.GetFileName(sFileToDownload));
                downloadFileUsingAscxDownload(sFileToDownload, sTargetFile, downloadDemoFileCallback);
            }
        }

        public static void downloadFileUsingAscxDownload(string sFileToDownload, String sTargetFileOrFolder,
                                                         Callbacks.dMethod_String dCallbackWhenCompleted)
        {
            O2Thread.mtaThread(
                () =>
                    {
                        var windowTitle = string.Format("{0} : {1}", "Download File", sFileToDownload);
                        O2Messages.openControlInGUISync(typeof (ascx_DownloadFile), O2DockState.Float, windowTitle);

                        O2Messages.getAscx(windowTitle,
                                           guiControl =>
                                               {
                                                   if (guiControl != null && guiControl is ascx_DownloadFile)
                                                   {
                                                       //var downloadFile = (IControl_DownloadFile)guiControl;
                                                       //O2Messages.openAscxGui(typeof (ascx_DownloadFile), O2DockState.Float, "Download File");
                                                       var adfDownloadFile = (ascx_DownloadFile) guiControl;
                                                       // Exec.openNewWindowWithControl("DownloadFile");
                                                       adfDownloadFile.invokeOnThread(
                                                           delegate
                                                               {
                                                                   adfDownloadFile.setDownloadDetails(sFileToDownload,sTargetFileOrFolder);
                                                                   adfDownloadFile.setAutoCloseOnDownload(true);
                                                                   adfDownloadFile.setCallBackWhenCompleted(dCallbackWhenCompleted);
                                                                   adfDownloadFile.downloadFile();
                                                                   return null;
                                                               });

                                                   }
                                               });
                    });
        }

        
    }
}

/*

using System;
using System.Xml;
using System.IO;
using System.Net;
using System.Windows.Forms;
using ounceLabs.F1.xsd.SavedAssessment;
using System.Collections.Generic;
using ounceLabs.F1.classes;
using ounceLabs.F1.classes.f1;
using ounceLabs.F1.ascx.ounce;
 
class webAutomationTest // change this to a unique name
{	
	
	
	
	public static void downloadJavaSourceCodeFromAddress()
	{
		//String sTargetUrl = @"http://www.remotescriptguru.com/springapp/jsp/showSource.jsp?dir=/src/..";		
		String sTargetUrl = @"http://www.remotescriptguru.com/springapp/jsp/showSource.jsp?dir=/src/../WEB-INF/lib";		
		DI.log.debug("Downloading all files from : {0}",sTargetUrl);
		loadPageAndInvokeCallback(sTargetUrl, javaFileDownloadCallback);
	}
	
	public static void javaFileDownloadCallback(webAutomation.O2HtmlPage hpHtmlPage)
	{
		String sTargetFolder = @"E:\_Java_Apps\Bamboo";
		String sTextToFilterOut = @"http://www.remotescriptguru.com/springapp/jsp/showSource.jsp?name=";
		foreach(webAutomation.O2Link lLink in hpHtmlPage.lLinks)
		{
			if (lLink.sHref.IndexOf(sTextToFilterOut) > -1)
			{
				String sFilteredHref = lLink.sHref.Replace(sTextToFilterOut,"");				
				String sTargetFile = getTargetFilePathAndCreateDirectoriesIfRequired(sTargetFolder, sFilteredHref);				
				String sTargetLink = lLink.sHref.Replace("name=","name=/src/..");
				DI.log.debug("Saving  {0} --> {1}", sFilteredHref, sTargetFile);		
				String sFileContent = getUrlContents(sTargetLink,true);
				if (sTargetFile.IndexOf(".jar")>-1)
				{
					DI.log.error("Index of <pre>:{0}  of PK: {1}",sFileContent.IndexOf("<pre>"), sFileContent.IndexOf("PK") );
					String sJarFileContents = sFileContent.Substring(sFileContent.IndexOf("PK"));
					sJarFileContents = sJarFileContents.Substring(0,sJarFileContents.IndexOf(@"</pre>"));
					//sJarFileContents = System.Web.HttpUtility.HtmlDecode(sJarFileContents);
					files.WriteFileContent(sTargetFile,sJarFileContents);

				}
				else
				{				
					if (sFileContent.IndexOf("<pre>") == -1 )
						files.WriteFileContent(sTargetFile,sFileContent);
					else
					{
						String sHtmlCode = sFileContent.Substring(sFileContent.IndexOf("<pre>") + 5);
						sHtmlCode = sHtmlCode.Substring(0,sHtmlCode.IndexOf(@"</pre>"));
						sHtmlCode = System.Web.HttpUtility.HtmlDecode(sHtmlCode);
	//					DI.log.debug("{0}" , sHtmlCode);															
						files.WriteFileContent(sTargetFile,sHtmlCode);
					}
				}
			}
			return;
		}
		DI.log.debug("Completed processing page");
	}
	
	private static String getTargetFilePathAndCreateDirectoriesIfRequired(String sTarget, String sPathToFileToProcess)
	{		
		String[] sSplitedText = sPathToFileToProcess.Split('/');	
		foreach(String sItem in sSplitedText)
		{
			sTarget = Path.Combine(sTarget,sItem);
			if (sItem.IndexOf(".") > -1)  // means it is the file name			
				return sTarget;
			else							
				if (false == Directory.Exists(sTarget))
				{
					DI.log.info("creating directory: {0}",sTarget);
					Directory.CreateDirectory(sTarget);
				}			
		}
		return sTarget;
	}
	
	public static void loadPageAndInvokeCallback(
		String sTargetUrl, 
		Callbacks.dMethod_HtmlPage dCallbackToProcessLoadedPage)
	{
		
	
		f1Messages.sendMessage("open WebAutomation");
		ascx_WebAutomation awaWebAutomation = (ascx_WebAutomation)f1Messages.sendMessage("WebAutomation.ascx_WebAutomation");
		// register callback (called when the page is loaded and O2HtmlPage object is populated)
		//awaWebAutomation.eWebPageLoadedAndProcess = callback_AfterPageLoad;   // this will clear all previous callbacks
		awaWebAutomation.eWebPageLoadedAndProcess = dCallbackToProcessLoadedPage;
		//load inital target			
		awaWebAutomation.processUrl(sTargetUrl);
		

		//fTargetForm.Text = "aaa";
		
	//	DI.log.info("Type {0}", o.GetType()); 
	}
	
/*	public static void callback_AfterPageLoad(Object oHtmlPage)
	{
		if (oHtmlPage.GetType().Name == "O2HtmlPage")
		{
			webAutomation.O2HtmlPage hpHtmPage = (webAutomation.O2HtmlPage)oHtmlPage;
			foreach(webAutomation.O2Link lLink in hpHtmPage.lLinks)
				DI.log.debug("Link: {0} : {1}", lLink.sText, lLink.sHref);
		}
		
	}* /
}
*/
