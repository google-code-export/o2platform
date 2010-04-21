﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.NRefactory.Ast;
using System.CodeDom;
using O2.API.AST.Visitors;
using O2.API.AST.ExtensionMethods;
using O2.API.AST.ExtensionMethods.CSharp;
using O2.DotNetWrappers.ExtensionMethods;
using ICSharpCode.SharpDevelop.Dom;
using O2.API.AST.CSharp;


namespace O2.API.AST.CSharp
{
    public class O2MappedAstData
    {    	
        public O2AstResolver O2AstResolver { get; set; }
        public MapAstToDom MapAstToDom { get; set; }
        public MapAstToNRefactory MapAstToNRefactory { get; set; }
        
		public Dictionary<string, GetAllINodes> FileToINodes { get; set; }
		public Dictionary<string, CompilationUnit> FileToCompilationUnit { get; set; }
		
        /*//from MapAstToDom
        public Dictionary<CompilationUnit, List<CodeNamespace>> CompilationUnitToNameSpaces { get; set; }
        public Dictionary<MethodDeclaration, CodeMemberMethod> MethodsAstToDom { get; set; }
        public Dictionary<CodeMemberMethod, MethodDeclaration> MethodsDomToAst { get; set; }
        public Dictionary<TypeDeclaration, CodeTypeDeclaration> TypesAstToDom { get; set; }
        public Dictionary<CodeTypeDeclaration, TypeDeclaration> TypesDomToAst { get; set; }

        //from MapAstToNRefactory
        public Dictionary<NRefactoryAST.CompilationUnit, ICompilationUnit> CompilationUnitToICompilationUnit { get; set; }
        public Dictionary<NRefactoryAST.TypeDeclaration, IClass> TypeDeclarationToIClass { get; set; }
        public Dictionary<IClass, NRefactoryAST.TypeDeclaration> IClassToTypeDeclaration { get; set; }
        public Dictionary<NRefactoryAST.MethodDeclaration, IMethod> MethodDeclarationToIMethod { get; set; }
        public Dictionary<IMethod, NRefactoryAST.MethodDeclaration> IMethodToMethodDeclaration { get; set; }
        public Dictionary<NRefactoryAST.ConstructorDeclaration, IMethod> ConstructorDeclarationToIMethod { get; set; }
        public Dictionary<IMethod, NRefactoryAST.ConstructorDeclaration> IMethodToConstructorDeclaration { get; set; }
        */
        public O2MappedAstData()
        {
            O2AstResolver = new O2AstResolver();
            MapAstToDom = new MapAstToDom();
            MapAstToNRefactory = new MapAstToNRefactory(O2AstResolver.myProjectContent);
			FileToINodes = new Dictionary<string, GetAllINodes>();            
			FileToCompilationUnit = new Dictionary<string,CompilationUnit>();            
        }

        public void loadFile(string fileOrCode)
        {
            var codeToLoad = "";
            var filePath = "";
            try
            {
                // resolve filename and get code to process                
                if (fileOrCode.fileExists())
                {
                    codeToLoad = fileOrCode.fileContents();
                    filePath = fileOrCode;
                }
                else
                {
                    codeToLoad = fileOrCode;
                    filePath = fileOrCode.save();
                }
                // get compilation unit
                var parser = codeToLoad.csharpAst();
                var compilationUnit = parser.CompilationUnit;
                //processCompilationUnit
                loadCompilationUnit(filePath, compilationUnit);
            }
            catch (Exception ex)
            {
                O2.Kernel.PublicDI.log.ex(ex,"in O2MappedAstData.LoadFile: " + filePath);
            }
		}                            
        

        public void loadCompilationUnit(string filePath, CompilationUnit compilationUnit)
        {        	
        	// map all INodes
	        FileToINodes.add(filePath,new GetAllINodes(compilationUnit));
            FileToCompilationUnit.add(filePath,compilationUnit);
            //Map AsT to DOM (System.DOM classes)
            MapAstToDom.loadCompilationUnit(compilationUnit);
            //Map AST to NRefactory (ICompilationUnit, IClass, IMethod)
            MapAstToNRefactory.loadCompilationUnit(compilationUnit);

			// update variable for CodeComplete
			O2AstResolver.setCurrentCompilationUnit(compilationUnit);                        
            var iCompilationUnit = MapAstToNRefactory.CompilationUnitToICompilationUnit[compilationUnit];            
            O2AstResolver.myProjectContent.UpdateCompilationUnit(null, iCompilationUnit, "");
        }

        // normalized data

        public List<CompilationUnit> compilationUnits()
        {
            return MapAstToDom.CompilationUnitToNameSpaces.Keys.ToList();
        }

        public List<TypeDeclaration> typeDeclarations()
        {
            return MapAstToDom.TypesAstToDom.Keys.ToList();
        }

        public List<MethodDeclaration> methodDeclarations()
        {
            return MapAstToDom.MethodsAstToDom.Keys.ToList();
        }

        public List<CodeMemberMethod> codeMemberMethods()
        {
            return MapAstToDom.MethodsAstToDom.Values.ToList();
        }

        public List<CodeTypeDeclaration> codeTypeDeclarations()
        {
            return MapAstToDom.TypesAstToDom.Values.ToList();
        }

        public List<CodeNamespace> codeNamespaces()
        {
            var codeNamespaces = new List<CodeNamespace>();
            foreach (var namespaces in MapAstToDom.CompilationUnitToNameSpaces.Values.ToList())
                codeNamespaces.AddRange(namespaces);
            return codeNamespaces;
        }

        public List<ICompilationUnit> iCompilationUnits()
        {
            return MapAstToNRefactory.CompilationUnitToICompilationUnit.Values.ToList();
        }

        public List<IClass> iClasses()
        {
            return MapAstToNRefactory.IClassToTypeDeclaration.Keys.ToList();
        }

        public List<IMethod> iMethods()
        {
            var methods = new List<IMethod>();
            methods.AddRange(MapAstToNRefactory.IMethodToMethodDeclaration.Keys.ToList());
            methods.AddRange(MapAstToNRefactory.IMethodToConstructorDeclaration.Keys.ToList());            
            return methods;
        }
    }
}
