using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.NRefactory.Ast;
using ICSharpCode.SharpDevelop.Dom;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.API.AST.ExtensionMethods;
using O2.API.AST;
using O2.API.AST.CSharp;
//O2File:C:\O2\_XRules_Local\ExtensionMethods\extra_WebAutomation.cs
//using O2.Script;

namespace O2.API.AST.CSharp_temp2
{

	public class O2MethodStream_V2
	{
		public O2MappedAstData O2MappedAstData { get; set;}
		public List<IMethod> IMethods {get;set;}
		
		
		public O2MethodStream_V2(O2MappedAstData o2MappedAstData)
		{
			O2MappedAstData = o2MappedAstData;
			IMethods = new List<IMethod>();
		}
	}
	
    public class O2MethodStream
    {
    	public O2MappedAstData O2MappedAstData { get; set;}
        public LinkedList<O2MethodNode> Path { get; set; }
        public List<O2MethodEdge> O2MethodEdges { get; set; }
        public List<O2MethodNode> O2MethodNodes { get; set; }
        public List<INode> INodes { get; set; }
        //public List<O2CodeStreamPath> O2CodeStreamPaths {get;set;}
        public Dictionary<IMethod, O2MethodNode> IMethodToO2MethodNode { get; set; }
        //Dictionary<MethodDeclaration, Path> Mapped_MethodDeclarations  {get;set;}
        
        public O2MethodStream(O2MappedAstData o2MappedAstData)
        {
        	O2MappedAstData = o2MappedAstData;
        	Path = new LinkedList<O2MethodNode>();
        	O2MethodEdges = new List<O2MethodEdge>();
        	O2MethodNodes = new List<O2MethodNode>();
        	INodes = new List<INode>();
        	IMethodToO2MethodNode = new Dictionary<IMethod, O2MethodNode>();
        }
    }

    public class O2MethodEdge
    {
        public O2MethodNode Source { get; set; }
        public MemberReferenceExpression MemberReferenceExpression {get;set;}        
        public O2MethodNode Target { get; set; }
    }

    public class O2MethodNode
    {
        public INode INode {get;set;}        
        public MethodDeclaration MethodDeclaration {get;set;}
        public IMethod IMethod {get;set;}
        public string File {get;set;}
        public string Signature {get;set;}
        public string SourceCode {get;set;}

        /*public O2MethodNode(INode iNode)
        {
            INode = iNode;
        }*/
        
        public O2MethodNode(O2MappedAstData o2MappedAstData, IMethod iMethod)
        {        	
        	populateData(o2MappedAstData,iMethod);
        }
      	
      	public O2MethodNode populateData(O2MappedAstData o2MappedAstData, IMethod iMethod)
      	{
      		IMethod = iMethod;        		        	
        	/*MethodDeclaration = o2MappedAstData.methodDeclaration(iMethod);
        	INode = MethodDeclaration;
        	File = o2MappedAstData.file(iMethod);
        	SourceCode = o2MappedAstData.sourceCode(iMethod);
        	Signature = iMethod.fullName();*/
      		return this;
      	}
        
        public override string ToString()
        {
        	return Signature ?? base.ToString();
        }
        
    }
}
