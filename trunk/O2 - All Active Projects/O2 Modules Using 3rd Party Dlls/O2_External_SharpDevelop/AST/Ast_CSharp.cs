// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.CodeDom;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using O2.Kernel;
using O2.Kernel.Interfaces.O2Core;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.Views.ASCX;

using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.Ast;   

namespace O2.External.SharpDevelop.AST
{            
    public class Ast_CSharp
    {    
        private static IO2Log log = PublicDI.log;
    	
        public CompilationUnit compilationUnit {get; set;}
        public IParser parser {get; set;}
        public string sourceCodeFile {get ; set;}
        public AstDetails astDetails {get; set;}
        public string errors  {get; set;}
		
        SupportedLanguage language = SupportedLanguage.CSharp;
		
        public Ast_CSharp(string _sourceCodeFile)
        {    	
            createAst(_sourceCodeFile);
            mapAstDetails(parser.CompilationUnit);   
        }

        public Ast_CSharp(CompilationUnit unit)
        {
        	createAst("");
            mapAstDetails(unit);
        }
    	
        public void createAst(string _sourceCodeFile)
        {
            sourceCodeFile = _sourceCodeFile;    		
            parser = ParserFactory.CreateParser(language, new StringReader(sourceCodeFile));
            parser.Parse();			
            errors  = (parser.Errors.Count > 0)?  parser.Errors.ErrorOutput : "";                                     
        }
        
	    public void mapAstDetails(CompilationUnit unit)
	    {
	    	try
	    	{
	    		
	        	compilationUnit = unit;

	            astDetails = new AstDetails();	            
	            var specials = parser.Lexer.SpecialTracker.RetrieveSpecials();
	            
	            astDetails.mapSpecials(specials);
	            astDetails.rewriteCode_CSharp(compilationUnit, specials);
	            astDetails.rewriteCode_VBNet(compilationUnit, specials);	            
	
	            compilationUnit.AcceptVisitor(astDetails, null);
			}
			catch(Exception ex)
			{
				PublicDI.log.error("in mapAstDetails: {0}", ex.Message);
			}
	    }

        /*public List<String> UsingDeclarations
    	{
    		return astDetails.UsingDeclarations;
    	}
    	
    	public List<String> Types
    	{
    		get
    		{    		
    			return astDetails.Types
    		
    		}
    	}*/
    	    	    	    	    
    }
}