using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Views.ASCX.classes;
using HTMLparserLibDotNet20.O2ExtraCode;
using System.Xml.Linq;

namespace O2.Core.XRules.Classes
{
    public class SvnApi
    {
        public static string getHtmlCode(string urlToFetch)
        {
            var urlContents = WebRequests.getUrlContents(urlToFetch);
            return urlContents;
        }

        public static List<SvnMappedUrl> getSvnMappedUrls(string urlToFetch)
        {
            var svnMappedUrls = new List<SvnMappedUrl>();
            var codeToParse = WebRequests.getUrlContents(urlToFetch);
            if (codeToParse != "")
            {
                //			var link = Majestic12ToXml.ConvertNodesToXml(new byte[]{});
                var nodes = Majestic12ToXml.ConvertNodesToXml(codeToParse);                
                foreach (var element in nodes.OfType<XElement>().Descendants("li").Descendants("a"))
                {
                    //				log.info("element: {0}",element);
                    //				log.debug("    href = {0} , name = {1}", element.Attribute("href"), element.Value);
                    svnMappedUrls.Add(new SvnMappedUrl(urlToFetch, element.Attribute("href").Value, element.Value));
                }
            }
            return svnMappedUrls;
        }
    }

    public class SvnMappedUrl
    {
        public string BasePath { get; set; }
        public string VirtualPath { get; set; }
        public string Text { get; set; }
        public string FullPath { get; set; }
        public bool IsFile { get; set; }

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
        }

        public string getFileContents()
        {
            if (IsFile)
                return WebRequests.getUrlContents(FullPath);
            return "";
        }
    }
}
