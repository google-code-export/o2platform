using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.DotNetWrappers.Network;
using O2.DotNetWrappers.Windows;
using O2.Kernel.ExtensionMethods;


namespace O2.DotNetWrappers.ExtensionMethods
{
    public static class Web_ExtensionMethods
    {  
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
            return WebEncoding.urlEncode(stringToEncode);
        }

        public static string htmlDecode(this String stringToEncode)
        {
            return WebEncoding.urlEncode(stringToEncode);
        }

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

        public static bool exists(this Uri uri)
        {
            return new Web().httpFileExists(uri.str());
        }

        public static string getUrlContents(this Uri uri)
        {
            return uri.getUrlContents(null);
        }

        public static string getUrlContents(this Uri uri, string cookies)
        {
            return new Web().getUrlContents(uri.str(), cookies,false);
        }

        public static string getHtml(this Uri uri)
        {
            return uri.getUrlContents();
        }

        public static string fileNameFriendly(this Uri uri)
        {
            return Files.getSafeFileNameString(uri.str());
        }

        public static string getHtmlAndSave(this Uri uri)
        {
            return uri.getHtmlAndSaveOnFolder(uri.o2Temp2Dir());
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


    }
}
