using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.DotNetWrappers.Network;

namespace O2.DotNetWrappers.ExtensionMethods
{
    public static class web_ExtensionMethods
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
                var uri  = new Uri(_string);
                return (uri != null);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
