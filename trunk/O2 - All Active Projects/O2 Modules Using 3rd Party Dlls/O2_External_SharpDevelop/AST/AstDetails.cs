using System.Collections.Generic;
using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.Ast;
using ICSharpCode.NRefactory.PrettyPrinter;
using ICSharpCode.NRefactory.Visitors;
using O2.Kernel;

namespace O2.External.SharpDevelop.AST
{
    public class AstDetails : AbstractAstVisitor
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
                    //PublicDI.log.info(((Comment)special).CommentText);
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
}