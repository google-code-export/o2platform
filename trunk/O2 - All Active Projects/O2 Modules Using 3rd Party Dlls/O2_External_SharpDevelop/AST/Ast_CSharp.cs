// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.CodeDom;
using System.IO;
using System.Collections.Generic;
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
using ICSharpCode.NRefactory.Visitors;

using ICSharpCode.NRefactory.PrettyPrinter;


namespace O2.Script
{
	
	public class AstValue 
	{
		public string Text {get;set;}
		public object OriginalObject {get; set;}
		public Location StartLocation  {get; set;}
		public Location EndLocation  {get; set;}
		
		public AstValue(string text,  object originalObject  , Location startLocation , Location endLocation)
		{
			Text = text;
			OriginalObject= originalObject;
			StartLocation = startLocation;
			EndLocation = endLocation;
		}
	}
	
	public class AstDetails : ICSharpCode.NRefactory.Visitors.AbstractAstVisitor
	{
		public List<AstValue> UsingDeclarations = new List<AstValue>();
		public List<AstValue> Types = new List<AstValue>();
		public List<AstValue> Methods = new List<AstValue>();
		public List<AstValue> Fields = new List<AstValue>();
		public List<AstValue> Properties = new List<AstValue>();
		public List<AstValue> Comments = new List<AstValue>();
		public string CSharpCode {get; set;}
		public string VBNetCode {get; set;}
		
		public override object VisitUsingDeclaration(UsingDeclaration usingDeclaration, object data)
		{			
			foreach(var declaration in usingDeclaration.Usings)
				UsingDeclarations.Add(new AstValue(declaration.Name, usingDeclaration, usingDeclaration.StartLocation, usingDeclaration.EndLocation));
			return null;
		}
	

		public override object VisitTypeDeclaration(TypeDeclaration typeDeclaration, object data)
		{
			base.VisitTypeDeclaration(typeDeclaration, data); // visit methods			
			Types.Add(new AstValue(typeDeclaration.Name, typeDeclaration, typeDeclaration.StartLocation, typeDeclaration.EndLocation));
			return null;
		}
		
		public override object VisitMethodDeclaration(MethodDeclaration methodDeclaration, object data)
		{
			base.VisitMethodDeclaration(methodDeclaration, data); // visit methods
			Methods.Add(new AstValue(methodDeclaration.Name, methodDeclaration, methodDeclaration.StartLocation, methodDeclaration.EndLocation));
			return null;
		}
		
		public override object VisitFieldDeclaration(FieldDeclaration fieldDeclaration, object data)
		{
			base.VisitFieldDeclaration(fieldDeclaration, data); // visit methods
			foreach(var field in fieldDeclaration.Fields)
				Fields.Add(new AstValue(field.Name, fieldDeclaration, fieldDeclaration.StartLocation, fieldDeclaration.EndLocation));
			return null; 
		}
		public override object VisitPropertyDeclaration(PropertyDeclaration propertyDeclaration, object data)
		{
			base.VisitPropertyDeclaration(propertyDeclaration, data); // visit methods
			Properties.Add(new AstValue(propertyDeclaration.Name, propertyDeclaration, propertyDeclaration.BodyStart, propertyDeclaration.BodyEnd));
			return null; 
		}
 

		public void mapSpecials(IList<ISpecial> specials)
		{
			foreach(var special in specials)
			{
				if (special is Comment)	
				{
					Comments.Add(new AstValue(
									((Comment)special).CommentText, 
									special, 
									special.StartPosition, 
									special.EndPosition));				
					PublicDI.log.info(((Comment)special).CommentText);
								}
			}
		}	
		
		public void rewriteCode_CSharp(CompilationUnit unit, IList<ISpecial> specials)
		{
			var outputVisitor  = new CSharpOutputVisitor();    		
    		using (SpecialNodesInserter.Install(specials, outputVisitor)) {
				unit.AcceptVisitor(outputVisitor, null);
			}
			//codeTextBox.Text = outputVisitor.Text.Replace("\t", "  ");
    		CSharpCode = outputVisitor.Text;
    	}
    	
    	public void rewriteCode_VBNet(CompilationUnit unit, IList<ISpecial> specials)
    	{
    		var outputVisitor  = new VBNetOutputVisitor();    		
    		using (SpecialNodesInserter.Install(specials, outputVisitor)) {
				unit.AcceptVisitor(outputVisitor, null);
			}
			//codeTextBox.Text = outputVisitor.Text.Replace("\t", "  ");
    		VBNetCode = outputVisitor.Text;
    		
//    		PublicDI.log.debug(recreatedCode);
		}
	}
	
    public class Ast_CSharp
    {    
    	private static IO2Log log = PublicDI.log;
    	
    	public CompilationUnit compilationUnit {get; set;}
		public IParser parser {get; set;}
		public string sourceCodeFile {get ; set;}
		public AstDetails astDetails {get; set;}
		
		SupportedLanguage language = SupportedLanguage.CSharp;
		
        public Ast_CSharp(string _sourceCodeFile)
    	{    	
    		createAst(_sourceCodeFile);
    	}
    	
    	public void createAst(string _sourceCodeFile)
    	{
    		sourceCodeFile = _sourceCodeFile;
    		
			IParser parser = ParserFactory.CreateParser(language, new StringReader(sourceCodeFile));
			parser.Parse();
			compilationUnit = parser.CompilationUnit;
			astDetails = new AstDetails();
			
			var specials = parser.Lexer.SpecialTracker.RetrieveSpecials();
			astDetails.mapSpecials(specials);
			astDetails.rewriteCode_CSharp(compilationUnit, specials);
			astDetails.rewriteCode_VBNet(compilationUnit, specials);			
			
			compilationUnit.AcceptVisitor(astDetails, null);
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
