/* 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
*/
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;

namespace DotNetGuru.AspectDNG.Util {
	public abstract class SimpleRegex {
        private static StringDictionary m_Cache = new StringDictionary();

        public static bool IsMatch(object o, string pseudoRegex) {
            string pattern = m_Cache[pseudoRegex];
            if (pattern == null)
                m_Cache[pseudoRegex] = pattern = EscapePseudoRegex(pseudoRegex);
            return Regex.IsMatch(o.ToString(), pattern, RegexOptions.Compiled);
        }

        public static bool IsPreciseMatch(object o, string pseudoRegex) {
            return IsMatch(o, "^" + pseudoRegex + "$");
        }

        public static string EscapePseudoRegex(string exp) {
            StringBuilder sb = new StringBuilder(exp.Length);
            foreach(char c in exp){
                switch (c) {
                    case '*':
                        sb.Append(".*");
                        break;
                    case '[':
                        sb.Append("\\[");
                        break;
                    case ']':
                        sb.Append("\\]");
                        break;
                    case '(':
                        sb.Append("\\(");
                        break;
                    case ')':
                        sb.Append("\\)");
                        break;
                    case '{':
                        sb.Append("(");
                        break;
                    case '}':
                        sb.Append(")");
                        break;
                    case '.':
                        sb.Append("\\.");
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }
            return sb.ToString();
        }
    }
}