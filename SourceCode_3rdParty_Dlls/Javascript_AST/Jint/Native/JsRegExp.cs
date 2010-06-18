using System;
using System.Collections.Generic;
using System.Text;
using Jint.Delegates;
using System.Text.RegularExpressions;

namespace Jint.Native
{
    [Serializable]
    public class JsRegExp : JsObject
    {
        public bool IsGlobal { get { return this["global"].ToBoolean(); } }
        public bool IsIgnoreCase { get { return (value.Options & RegexOptions.IgnoreCase) == RegexOptions.IgnoreCase; } }
        public bool IsMultiline { get { return (value.Options & RegexOptions.Multiline) == RegexOptions.Multiline; } }

        public JsRegExp()
        {
        }

        private Regex value;
        private string pattern;

        public JsRegExp(string pattern) : this(pattern, false, false, false)
        {
        }

        public JsRegExp(string pattern, bool g, bool i, bool m)
        {
            if (pattern.Contains("$"))
            {
                pattern = Regex.Replace(pattern, @"(?=[^\\])\$", m ? "(?=\r|\n|\r\n)" : @"\z", RegexOptions.Compiled);
            }

            if (pattern.StartsWith("^") && m)
            {
                pattern = "(?!\r|\n|\r\n)" + pattern.Substring(1);
            }

            pattern = Regex.Replace(pattern, @"(?=[^\\])?\\(\d)", @"\k<$1>", RegexOptions.Compiled);

            RegexOptions options = RegexOptions.ECMAScript;

            if (m)
            {
                options |= RegexOptions.Multiline;
            }

            if(i)
            {
                options |= RegexOptions.IgnoreCase;
            }

            value = new Regex(pattern, options);
            this.pattern = pattern;
        }

        public string Pattern { get { return pattern; } }

        public override object Value
        {
            get
            {
                return value;
            }
        }

        public override string ToSource()
        {
            return "/" + value.ToString() + "/";
        }

        public override string ToString()
        {
            return "/" + value.ToString() + "/" + (IsGlobal ? "g" : String.Empty) + (IsIgnoreCase ? "i" : String.Empty) + (IsMultiline ? "m" : String.Empty);
        }

        public const string TYPEOF = "regexp";

        public override string Class
        {
            get { return TYPEOF; }
        }
    }
}
