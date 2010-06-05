using System.IO;
using System.Collections.Generic;
using System.Linq;
using O2.Interfaces.O2Core;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.Network;
using O2.DotNetWrappers.ExtensionMethods;
using HTMLparserLibDotNet20.O2ExtraCode;
using System.Xml.Linq;
using System;
using SharpSvn;
//using O2.Core.XRules.XRulesEngine;

namespace O2.Core.XRules.Classes
{
    public class SvnApi
    {
        public static string localRulesDatabase = @"C:\O2\O2Scripts_Database\_Scripts";
        public static string svnO2RootFolder = "http://o2platform.googlecode.com/svn/trunk/";		
        //public static string svnO2DatabaseRulesFolder = "http://o2platform.googlecode.com/svn/trunk/O2%20-%20All%20Active%20Projects/O2_XRules_Database/_Rules";
        public static string svnO2DatabaseRulesFolder = "http://o2platform.googlecode.com/svn/trunk/O2_Scripts/";

    	private static IO2Log log = PublicDI.log;

        public static void SyncLocalFolderWithO2XRulesDatabase()
        {
            try
            {
                "in SyncLocalFolderWithO2XRulesDatabase".debug();
                //var targetLocalDir = XRules_Config.PathTo_XRulesDatabase_fromO2;
                var targetLocalDir = localRulesDatabase;
                var o2XRulesSvn = svnO2DatabaseRulesFolder;

                "starting Sync with XRules Database to {0}".info(targetLocalDir);
                using (SvnClient client = new SvnClient())
                {
                    if (targetLocalDir.dirExists().isFalse() || targetLocalDir.pathCombine(".svn").dirExists().isFalse())
                    {
                        "First Sync, so doing an SVN Checkout to: {0}".info(targetLocalDir);
                        Files.deleteFolder(targetLocalDir, true);
                        client.CheckOut(new Uri(o2XRulesSvn), targetLocalDir);
                    }
                    else
                    {
                        "Local XRules folder exists, so doing an SVN Update".info();
                        client.Update(targetLocalDir);
                    }
                    "SVN Sync completed".debug();
                }
            }
            catch (Exception ex)
            {
                ex.log("in SvnApi.SyncLocalFolderWithO2XRulesDatabase");
            }
        }

        public class HttpMode
        {
            public static string getHtmlCode(string urlToFetch)
            {
                var urlContents = new Web().getUrlContents(urlToFetch);
                return urlContents;
            }

            public static List<SvnMappedUrl> getSvnMappedUrls(string urlToFetch, bool recursive)
            {
                log.debug("In getSvnMappedUrls: {0}", urlToFetch);
                if (recursive)
                {
                    var svnMappedUrls = getSvnMappedUrls(urlToFetch);
                    var childUrls = new List<SvnMappedUrl>();
                    foreach (var svnMappedUrl in svnMappedUrls)
                        if (svnMappedUrl.IsFile == false && svnMappedUrl.VirtualPath != "../")
                        {
                            //log.error("fetching sub dir: {0}", svnMappedUrl.VirtualPath);
                            childUrls.AddRange(getSvnMappedUrls(svnMappedUrl.FullPath, true));
                        }
                    svnMappedUrls.AddRange(childUrls);
                    return svnMappedUrls;
                }
                //else
                return getSvnMappedUrls(urlToFetch);
            }

            public static List<SvnMappedUrl> getSvnMappedUrls(string urlToFetch)
            {
                var svnMappedUrls = new List<SvnMappedUrl>();
                var codeToParse = new Web().getUrlContents(urlToFetch);
                if (codeToParse != "")
                {
                    var nodes = Majestic12ToXml.ConvertNodesToXml(codeToParse);
                    foreach (var element in nodes.OfType<XElement>().Descendants("li").Descendants("a"))
                    {
                        var virtualPath = element.Attribute("href").Value;
                        if (virtualPath != "../")
                            svnMappedUrls.Add(new SvnMappedUrl(urlToFetch, virtualPath, element.Value));
                    }
                }
                return svnMappedUrls;
            }

            public static void download(SvnMappedUrl svnMappedUrl, string baseUrl, string targetFolder)
            {
                if (svnMappedUrl.VirtualPath == "../")
                    return;
                var localVirtualPath = svnMappedUrl.FullPath.Replace(baseUrl, "");
                localVirtualPath = WebEncoding.urlDecode(localVirtualPath);
                log.debug("svn download: {0}", svnMappedUrl.FullPath);
                Files.checkIfDirectoryExistsAndCreateIfNot(targetFolder);
                if (svnMappedUrl.IsFile)
                {
                    var localfile = targetFolder.pathCombine(localVirtualPath);

                    //var fileContents = Web.getUrlContents(svnMappedUrl.FullPath);
                    //Files.WriteFileContent(localfile,fileContents);

                    new Web().downloadBinaryFile(svnMappedUrl.FullPath, localfile);

                    if (!File.Exists(localfile))
                        //        			log.info("contents saved to local file: {0}", localfile);        		
                        //        		else
                        log.info("could not create file: {0}", localfile);
                }
                else
                {
                    var localfolder = targetFolder.pathCombine(localVirtualPath);
                    log.debug("   ... remote target was a folder, so creating local folder: {0}", localfolder);
                    Files.checkIfDirectoryExistsAndCreateIfNot(localfolder);
                }

                //var codeToParse = Web.getUrlContents(urlToFetch);
            }

            public class SvnMappedUrl
            {
                public string BasePath { get; set; }
                public string VirtualPath { get; set; }
                public string Text { get; set; }
                public string FullPath { get; set; }
                public bool IsFile { get; set; }
                public string FileExtension { get; set; }

                public SvnMappedUrl(string basePath, string virtualPath, string text)
                {
                    BasePath = basePath;
                    VirtualPath = virtualPath;
                    Text = text;
                    if (basePath.Length > 0 && basePath[basePath.Length - 1] != '/' &&
                        virtualPath.Length > 0 && virtualPath[0] != '/')
                        FullPath = basePath + '/' + virtualPath;
                    else
                        FullPath = basePath + virtualPath;

                    if (virtualPath.Length > 0)
                        IsFile = virtualPath[virtualPath.Length - 1] != '/';

                    FileExtension = (IsFile) ? Path.GetExtension(virtualPath) : "";
                }

                public string getFileContents()
                {
                    if (IsFile)
                        return new Web().getUrlContents(FullPath);
                    return "";
                }
            }
        }
    }
    
}
