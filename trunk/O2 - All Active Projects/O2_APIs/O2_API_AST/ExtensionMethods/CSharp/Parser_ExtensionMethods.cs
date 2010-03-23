using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.NRefactory;
using System.IO;
using O2.DotNetWrappers.ExtensionMethods;

namespace O2.API.AST.ExtensionMethods.CSharp
{
    public static class Parser_ExtensionMethods
    {        
        public static IParser csharpAst(this string csharpCodeOrFile)
        {
            var language = SupportedLanguage.CSharp;
            var codeToParse = (csharpCodeOrFile.fileExists()) ? csharpCodeOrFile.fileContents() : csharpCodeOrFile;

            var parser = ParserFactory.CreateParser(language, new StringReader(codeToParse));

            parser.Parse();
            return parser;
        }

    }
}
