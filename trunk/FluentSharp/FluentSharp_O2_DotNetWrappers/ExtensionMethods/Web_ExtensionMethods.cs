using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentSharp.O2.DotNetWrappers.Network;
using FluentSharp.O2.DotNetWrappers.Windows;
using FluentSharp.O2.Kernel.ExtensionMethods;
using System.Drawing;
using System.IO;


namespace FluentSharp.O2.DotNetWrappers.ExtensionMethods
{
    public static class Web_ExtensionMethods
    {

        #region get data (via web requests)

        public static string getHtml(this Uri uri, bool showDebugMessage)
        {
            showDebugMessage.ifInfo("Downloading html code for: {0}", uri.str());
            return uri.getHtml();
        }        

        public static string getUrlContents(this Uri uri)
        {
            return uri.getUrlContents(null);
        }

        public static string getUrlContents(this Uri uri, string cookies)
        {
            return new Web().getUrlContents(uri.str(), cookies, false);
        }

        public static string getHtml(this Uri uri)
        {
            return uri.getUrlContents();
        }

        public static string getHtmlAndSave(this Uri uri)
        {
            return uri.getHtmlAndSaveOnFolder("".o2Temp2Dir());
        }

        public static string getHtmlAndSaveOnFolder(this Uri uri, string targetFolder)
        {
            var html = uri.getHtml();
            if (html.valid())
            {
                var targeFileName = targetFolder.pathCombine(uri.fileNameFriendly());
                return html.save(targeFileName);
            }
            return "";
        }

        public static Bitmap getImageAsBitmap(this Uri uri)
        {
            var webClient = new System.Net.WebClient();
            var imageBytes = webClient.DownloadData(uri);
            "image size :{0}".info(imageBytes.size());
            var memoryStream = new MemoryStream(imageBytes);
            var bitmap = new Bitmap(memoryStream);
            return bitmap;
        }

        #endregion

        #region uri and web links

        public static Uri web(this string _string)
        {
            return _string.uri();
        }

        public static Uri link(this string _string)
        {
            return _string.uri();
        }

        public static Uri url(this string _string)
        {
            return _string.uri();
        }

        public static Uri uri(this string _string)
        {
            try
            {
                if (_string.isFile().isFalse())                    
                    _string = _string.StartsWith("http") ? _string : @"http://" + _string;
                return new Uri(_string);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool isUri(this string _string)
        {
            return _string.validUri();
        }

        public static bool validUri(this string _string)
        {
            try
            {
                if (_string.valid())
                {
                    var uri = new Uri(_string);
                    return (uri != null && uri.IsAbsoluteUri && uri.IsFile.isFalse());
                }
            }
            catch (Exception)
            {
                
            }
            return false;
        }

        public static string fileNameFriendly(this Uri uri)
        {
            return Files.getSafeFileNameString(uri.str());
        }

        public static bool exists(this Uri uri)
        {
            return new Web().httpFileExists(uri.str());
        }

        public static string hostUrl(this Uri uri)
        {
            return "{0}://{1}".format(uri.Scheme, uri.Host);
        }
        #endregion 
        
        #region encode/decode

        public static string urlEncode(this String stringToEncode)
        {
            return WebEncoding.urlEncode(stringToEncode);
        }

        public static string urlDecode(this String stringToEncode)
        {
            return WebEncoding.urlDecode(stringToEncode);
        }

        public static string htmlEncode(this String stringToEncode)
        {
            return WebEncoding.htmlEncode(stringToEncode);
        }

        public static string htmlDecode(this String stringToEncode)
        {
            return WebEncoding.htmlDecode(stringToEncode);
        }

        public static byte asciiByte(this char charToConvert)
        {
            try
            {
                return System.Text.ASCIIEncoding.ASCII.GetBytes(new char[] { charToConvert })[0];
            }
            catch
            {
                return default(byte);
            }            
        }

        public static byte[] asciiBytes(this string stringToConvert)
        {
            return System.Text.ASCIIEncoding.ASCII.GetBytes(stringToConvert);
        }

        public static string base64Encode(this string stringToEncode)
        {
            try
            {
                return System.Convert.ToBase64String(stringToEncode.asciiBytes());
            }
            catch (Exception ex)
            {
                ex.log("in base64Encode");
                return "";
            }
        }
        public static string base64Encode(this byte[] bytesToEncode)
        {
            try
            {
                return System.Convert.ToBase64String(bytesToEncode);
            }
            catch (Exception ex)
            {
                ex.log("in base64Encode");
                return "";
            }
        }

        public static string base64Decode(this string stringToDecode)
        {
            try
            {
                return System.Convert.FromBase64String(stringToDecode).ascii(); ;
            }
            catch (Exception ex)
            {
                ex.log("in base64Decode");
                return "";
            }
        }

        public static byte[] base64Decode_AsByteArray(this string stringToDecode)
        {
            try
            {
                return System.Convert.FromBase64String(stringToDecode);
            }
            catch (Exception ex)
            {
                ex.log("in base64Decode");
                return null;
            }
        }

        #endregion

        #region ping

        public static bool ping(this string address)
        {
            return new Ping().ping(address);
        }

        public static bool online(this object _object)
        {
            return new Ping().ping("www.google.com");
        }

        #endregion

        /*#region SSL

        
        #endregion*/
    }
}
