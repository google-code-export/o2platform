using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Kernel.Interfaces.Rules;

namespace O2.Rules.OunceLabs.DataLayer_OunceV6
{
    public class MiscUtils_OunceV6
    {
        public static int getIdForSuportedLanguage(SupportedLanguage language)
        {
            switch (language)
            {
                case SupportedLanguage.Cpp:
                    return 1;
                case SupportedLanguage.Java:
                    return 2;
                case SupportedLanguage.DotNet:
                    return 3;
                case SupportedLanguage.ASP_VB6:
                    return 4;
                case SupportedLanguage.Cobol:
                    return 5;
                default:
                    DI.log.error("in MySqlRules_OunceV6.getIdForSuportedLanguage, unsupported language: {0}",
                                 language.ToString());
                    return -1;
            }
        }

        public static SupportedLanguage getSupportedLanguageFromLanguageName(string languageName)
        {
            switch (languageName)
            {
                case "C++":
                    return SupportedLanguage.Cpp;
                case "Java":
                    return SupportedLanguage.Java;
                case "DotNet":
                    return SupportedLanguage.DotNet;
                case "ASP Classic/VB6":
                    return SupportedLanguage.ASP_VB6;
                case "Cobol":
                    return SupportedLanguage.Cobol;
                default:
                    return SupportedLanguage.DotNet; // default to DotNet
            }
        }

        public static List<string> getStringListWithSupportedLanguages()
        {
            return new List<string> {"C++", "Java", "DotNet", "ASP Classic/VB6", "COBOL"};
        }
    }
}
