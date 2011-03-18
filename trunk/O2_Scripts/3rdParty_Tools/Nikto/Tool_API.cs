﻿// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Windows.Forms;
using System.Reflection;
using System.Text;
using O2.Interfaces.O2Core;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.Network;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.Zip;
using O2.Views.ASCX.classes.MainGUI;
using O2.Views.ASCX.ExtensionMethods;
using O2.XRules.Database.Utils;
//O2File:_Extra_methods_To_Add_to_Main_CodeBase.cs

namespace O2.XRules.Database.APIs
{
	// this calls exists to simplify the creation of new Tool APIs
	public class Tool_API
    {   
    	public string ToolName {get; set;}
    	public string Version {get; set;}    	
    	public string VersionWebDownload {get;set;} 
    	public string Install_File {get;set;}
    	public Uri Install_Uri {get;set;}
    	public string Install_Dir {get;set;}     	    
    	public string DownloadedInstallerFile {get; set;}
    	
    	public string  toolsDir = PublicDI.config.O2TempDir.pathCombine("..\\_ToolsOrAPIs").createDir();
    	public string  localDownloadsDir = PublicDI.config.O2TempDir.pathCombine("..\\_O2Downloads").createDir();
    	public string  s3DownloadFolder = "http://s3.amazonaws.com/O2_Downloads/";
    	public Process Install_Process {get;set;}
    	
    	//public bool Instaled { get { return isInstalled(); } }
    	
    	public Tool_API()
    	{
    		ToolName = "";
    		Install_File = "";    		
    		Install_Dir = "";
    		Version = "";
    		VersionWebDownload = "";    		
    	}    	    	    	    	
    	 
    	public void config(string toolName, string version, string installFile)
    	{
    		ToolName = toolName;
    		Version = version;
    		Install_File = installFile;
    		Install_Dir = toolsDir.pathCombine(toolName);
    	}
    	
    	public virtual bool isInstalled()
    	{
    		return isInstalled(true);
    	}
    	
    	public virtual bool isInstalled(bool showLogMessage)
    	{
    		if (Install_Dir.dirExists())
    		{
    			"{0} tool is installed because installation dir was found: {0}".debug(ToolName, Install_Dir);
    			return true;
    		}
    		if (showLogMessage)
    			"{0} tool is NOT installed because installation dir was NOT found: {0}".debug(ToolName, Install_Dir);
    		return false;
    	}    	    	
    	    	
    	public virtual bool installFromMsi_Local(string pathToMsi)
    	{    		
    		if (isInstalled(false).isFalse() && pathToMsi.fileExists())    							
				Processes.startProcess(pathToMsi).WaitForExit();
			return isInstalled();
    	}
    	
    	public virtual bool startInstaller_FromMsi_Web()
    	{    		
    		return install((msiFile)=> startInstaller_FromMsi_Local(msiFile));
    	}
    	
    	public virtual bool startInstaller_FromMsi_Local(string pathToMsi)
    	{    		
    		if (isInstalled(false).isFalse() && pathToMsi.fileExists())    							
				Install_Process = Processes.startProcess(pathToMsi);
			return isInstalled();
    	}
    	
    	//this will just download the installer (with the file in 
    	public virtual string installerFile()
    	{    		
    		DownloadedInstallerFile = download(Install_File);
    		if (DownloadedInstallerFile.fileExists())
    			return DownloadedInstallerFile;    		
    		"[Tool_API] could not download Install_File: {0}".error(DownloadedInstallerFile);
    		return null;    		
    	}
    	
    	public virtual bool installFromMsi_Web(string url)
    	{
    		VersionWebDownload = url;
    		return installFromMsi_Web();
    	}
    	
    	public virtual bool installFromExe_Web()
    	{
    		return installFromMsi_Web();
    	}
    	
    	public virtual bool installFromMsi_Web()
    	{    		
    		return install((msiFile)=> installFromMsi_Local(msiFile));
    	}    	    	
    	
    	public virtual bool installFromZip_Web()
    	{
    		if (Install_Dir.valid().isFalse())
    		{
    			"Install_Dir is not set, aborting installation".error();
    			return false;
    		}
    		Action<string> onDownload = 
    			(zipFile)=>{
    							if (zipFile.fileExists())    							
    								new zipUtils().unzipFile(zipFile,Install_Dir);                               
    					   };
    		
    		return install(onDownload);
    			
    	}
    	
    	public virtual bool installFromWeb_Jar()
    	{
    		if (Install_Dir.valid().isFalse())
    		{
    			"Install_Dir is not set, aborting installation".error();
    			return false;
    		}
    		Action<string> onDownload = 
    			(jarFile)=>{
    							if (jarFile.fileExists())    							
    							{
    								Install_Dir.createDir();
    								Files.MoveFile(jarFile, Install_Dir);    								
    							}
    					   };    		
    		return install(onDownload);
    	}
    	
    	public bool install(Action<string> onDownload)
    	{
    		if (Install_File.valid().isFalse())
    		{
    			"in Install for Tool {0} , a valid InstallFile must be provided: {1}".error(ToolName, Install_File);
    			return false;
    		}
    		"Installing: {0}".debug(ToolName);
    		if (isInstalled())    		
    			return true;
    		DownloadedInstallerFile = download(Install_File);
			if (DownloadedInstallerFile.fileExists())   
                	onDownload(DownloadedInstallerFile);                	    		
    		if (isInstalled())
            {
            	"{0} installed ok".info(Version);
            	return true;
            }
            "There was a problem installing the {0}".error(Version);
            return false;
    	}
    	
    	public string download(string file)
    	{
    		"downloading file: {0}".info(file);    	    		
    		var localPath = localDownloadsDir.pathCombine(file);
    		if (localPath.fileExists())
    		{
    			"found previously downloaded copy: {0}".info(localPath);
    			return localPath;
    		}
    		
    		var s3Path = "{0}{1}".format(s3DownloadFolder, file);    		
    		if (new Web().httpFileExists(s3Path))
    		{
    			"found file at S3: {0}".info(s3Path);
    			var downloadedFile =  download(s3Path.uri());
    			//"S3 file downloaded to: {0}".info(downloadedFile);
    			return downloadedFile;
    		}    		
    		return download(Install_Uri);
    	}
    		
    	public string download(Uri uri)
    	{
    		"Downloading from Uri: {0}".info(uri);
    		if (uri.isNull())
    			return null;
    		VersionWebDownload = uri.str();
    		
    		//var localFilePath = PublicDI.config.O2TempDir.pathCombine(CurrentVersionZipFile);    	
    		//"downloading file {0} from {1} to {2}".info(CurrentVersionZipFile, CurrentVersionWebDownload,localFilePath);
    		"downloading file {0}".info(VersionWebDownload);
    		    		    	
            //if (new Web().httpFileExists(VersionWebDownload))            
            //{
              //  var downloadedFile =  new Web().downloadBinaryFile(VersionWebDownload);                
              var downloadedFile = VersionWebDownload.download();
                if (downloadedFile.fileExists())
                {
                	var targetFile = localDownloadsDir.pathCombine(Install_File);
                	"Copying file: {0}".info(targetFile);
                	Files.Copy(downloadedFile, targetFile);
                	"Deleting file: {0}".info(targetFile);
                	Files.deleteFile(downloadedFile);                	
                	if (targetFile.fileExists())
                		return targetFile;
                }
            //}
            return null;            
    	}
    	
    	public virtual bool unInstall()
    	{
    		if (Install_Dir.valid() && Install_Dir.dirExists())
    		{
    			"Uninstalling tool {0} by deleting folder {1}".debug(ToolName, Install_Dir);
    			Files.deleteFolder(Install_Dir,true);
    			return isInstalled().isFalse();
    		}
    		return false;
    	}
    }
}