// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.CodeDom;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using O2.Interfaces.O2Core;
using O2.Kernel;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.Views.ASCX;

using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.Ast;
using System.Collections.Generic;   

namespace O2.External.SharpDevelop.AST
{            
    public class Ast_CSharp
    {    
        private static IO2Log log = PublicDI.log;
    	
        public CompilationUnit CompilationUnit {get; set;}
        public IParser Parser {get; set;}
        public string SourceCodeFile {get ; set;}
        public AstDetails AstDetails {get; set;}
        public string Errors  {get; set;}
        public List<ISpecial> extraSpecials { get; set; }
		
        SupportedLanguage language = SupportedLanguage.CSharp;

        public Ast_CSharp()
        {
            extraSpecials = new List<ISpecial>();
        }

        public Ast_CSharp(string _sourceCodeFile) 
            : this()
        {    	
            createAst(_sourceCodeFile);
            mapAstDetails(Parser.CompilationUnit);   
        }


        public Ast_CSharp(CompilationUnit unit)
            : this()
        {
            createAst("");
            mapAstDetails(unit);
        }
    
        public void createAst(string _sourceCodeFile)
        {
            SourceCodeFile = _sourceCodeFile;    		
            Parser = ParserFactory.CreateParser(language, new StringReader(SourceCodeFile));
            Parser.Parse();
            Errors = (Parser.Errors.Count > 0) ?  Parser.Errors.ErrorOutput : "";                                     
        }
        
	    public void mapAstDetails(CompilationUnit unit)
	    {
	    	try
	    	{	    		
	        	CompilationUnit = unit;

	            AstDetails = new AstDetails();	            
	            var specials = Parser.Lexer.SpecialTracker.RetrieveSpecials();
                specials.AddRange(extraSpecials);
                AstDetails.mapSpecials(specials);
                AstDetails.rewriteCode_CSharp(CompilationUnit, specials);
                AstDetails.rewriteCode_VBNet(CompilationUnit, specials);	            
	
	            CompilationUnit.AcceptVisitor(AstDetails, null);
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


        public void mapAstDetails()
        {
            mapAstDetails(this.CompilationUnit);
        }
    }
}