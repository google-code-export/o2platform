
using System;

namespace O2.Kernel.ExtensionMethods
{
    public static class String_ExtensionMethods
    {

        public static string str(this object _object)
        {
            return _object.ToString();
        }

        public static bool validString(this object _object)
        {
            return _object.str().valid();
        }

        public static bool eq(this string string1, string string2)
        {
            return (string1 == string2);
        }

        public static bool neq(this string string1, string string2)
        {
            return (string1 != string2);
        }

        public static bool contains(this string string1, string string2)
        {
            return string1.Contains(string2);
        }

        public static bool starts(this string string1, string string2)
        {
            return string1.StartsWith(string2);
        }

        public static void starts(this string textToSearch, string textToFind, Action<string> onMatch)
        {
            if (textToSearch.starts(textToFind))
            {
                var textForCallback = textToSearch.remove(textToFind);
                if (textForCallback.valid())
                    onMatch(textForCallback);
            }            
        }

        public static bool ends(this string string1, string string2)
        {
            return string1.EndsWith(string2);
        }

        public static bool valid(this string _string)
        {
            if (false == string.IsNullOrEmpty(_string))
                if (_string.Trim() != "")
                    return true;
            return false;
        }

        public static bool empty(this string _string)
        {
            return !(_string.valid());
        }

        public static string format(this string format, params object[] parameters)
        {
            return string.Format(format, parameters);
        }

        public static string remove(this string _string, params string[] stringsToRemove)
        {
            return _string.replace("", stringsToRemove);
        }

        public static string toSpace(this string _string, params string[] stringsToChange)
        {
            return _string.replace(" ", stringsToChange);
        }

        public static string replace(this string _string, string replaceString, params string[] stringsToProcess)
        {
            foreach (var stringToProcess in stringsToProcess)
                _string = _string.Replace(stringToProcess, replaceString);
            return _string;
        }

        public static int size(this string _string)
        {
            return _string.Length;
        }
        public static string line(this string firstString, string secondString)
        {
            return firstString.line() + secondString;
        }

        public static string line(this string firstString)
        {
            return firstString + Environment.NewLine;
        }

        public static int toInt(this string _string)
        {
            Int32 value;
            Int32.TryParse(_string, out value);
            return value;
        }   
    }
}