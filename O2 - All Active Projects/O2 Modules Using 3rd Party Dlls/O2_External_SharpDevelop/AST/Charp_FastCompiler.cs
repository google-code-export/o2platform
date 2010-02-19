using System;
using System.Reflection;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using ICSharpCode.NRefactory.Ast;
using O2.DotNetWrappers.DotNet;
//O2Ref:O2SharpDevelop.dll
using ICSharpCode.NRefactory;
//O2File:C:\O2\_XRules_Local\MiscTestss\extra.cs
using O2.DotNetWrappers.ExtensionMethods;
using O2.External.SharpDevelop.ExtensionMethods;
using O2.Kernel.ExtensionMethods;

namespace O2.External.SharpDevelop.AST
{
    public class CSharp_FastCompiler
    {
        public string SourceCode { get; set; }						// I think there is a small race condition with the use of this variable
        public List<string> ReferencedAssemblies { get; set; }
        public Dictionary<string, object> InvocationParameters { get; set; }
        public CompilationUnit CompilationUnit { get; set; }
        //public AstDetails AstDetails { get; set; }
        public string AstErrors { get; set; }
        public string CompilationErrors { get; set; }        
        public CompilerResults CompilerResults { get; set; }
        
        //public O2Timer executionTime { get; set; }
        
        public MethodInvoker onAstFail { get; set; }
        public MethodInvoker onAstOK { get; set; }
        public MethodInvoker onCompileFail { get; set; }
        public MethodInvoker onCompileOK { get; set; }        
        public MethodInvoker beforeSnippetAst { get; set; }
        public MethodInvoker beforeCompile { get; set; }

        public string default_MethodName {get; set;}
        public string default_TypeName { get; set; }

		public bool DebugMode {get ; set;}
		
		Stack<string> createAstStack = new Stack<string>();
		Stack<string> compileStack = new Stack<string>();
		
		bool creatingAst;
		bool compiling;
		
        public CSharp_FastCompiler()
        {        
        	DebugMode = true;				// set to true to see details about each AstCreation and Compilation stage
        	InvocationParameters = new Dictionary<string, object>();
        	ReferencedAssemblies = getDefaultReferencedAssemblies();
            default_MethodName = "dynamicMethod";
            default_TypeName = "DynamicType";                                   
            // defaults

        }

        //using 
        //using 
        //using 
        //using 
        //ref 


        public List<string> getDefaultUsingStatements()
        {
            return new List<string>().add("System")
                                     .add("System.Windows.Forms")
                                     .add("O2.Interfaces")
                                     .add("O2.Kernel")
                                     .add("O2.Kernel.ExtensionMethods")
                                     .add("O2.Views.ASCX.CoreControls")
                                     .add("O2.Views.ASCX.classes.MainGUI");
                                     //.add("O2.External.IE");
        }
		public List<string> getDefaultReferencedAssemblies()
        {
            return new List<string>().add("System.dll")
                                     .add("System.Core.dll")
                                     .add("System.Windows.Forms.dll")
                                     .add("O2_Kernel.dll")
                                     .add("O2_Interfaces.dll")
                                     .add("O2_DotNetWrappers.dll")
                                     .add("O2_Views_Ascx.dll");
                                     //.add("O2_External_IE.dll");
        }

		public Dictionary<string,object> getDefaultInvocationParameters()
		{
			return new Dictionary<string, object>();
		}
              
        public void compileSnippet(string codeSnippet)
        {
        	createAstStack.Clear();	
        	createAstStack.Push(codeSnippet);
        	compileSnippet();
        }
        public void compileSnippet()
        {      
        	if (creatingAst == false && createAstStack.Count > 0)
       		{
       			creatingAst = true;
       			var codeSnippet = createAstStack.Pop();
       			createAstStack.Clear();	
       			
        		O2Thread.mtaThread(
                	()=>{	
                			InvocationParameters = getDefaultInvocationParameters();
                			this.invoke(beforeSnippetAst);
	                		DebugMode.info("Compiling Source Snippet (Size: {0})", codeSnippet.size());        	
				            var sourceCode = createCSharpCodeWith_Class_Method_WithMethodText(codeSnippet);
				            if (sourceCode != null)
				                compileSourceCode(sourceCode);
				            creatingAst = false;
				            compileSnippet();
				        });
			}
        }

		public void compileSourceCode(string sourceCode)
        {
        	compileStack.Push(sourceCode);
        	compileSourceCode();
       	}
       	
       	public void compileSourceCode()
       	{       
       		if (compiling == false && compileStack.Count > 0)
       		{
       			compiling = true;
       			var sourceCode = compileStack.Pop();
       			compileStack.Clear();						// remove all previous compile requests (since their source code is now out of date
            	O2Thread.mtaThread(
	                ()=>{	  
	                		//Files.setCurrentDirectoryToExecutableDirectory();                			                		
	                		Environment.CurrentDirectory = O2.Kernel.PublicDI.config.CurrentExecutableDirectory;;
	                		this.invoke(beforeCompile);
	                		DebugMode.info("Compiling Source Code (Size: {0})", sourceCode.size());            
	                		SourceCode = sourceCode;   
	                		var providerOptions = new Dictionary<string,string>(); 
							providerOptions.Add ("CompilerVersion", "v3.5");
		                    var csharpCodeProvider = new Microsoft.CSharp.CSharpCodeProvider(providerOptions);                    
		                    var compilerParams = new CompilerParameters {GenerateInMemory = true};
		                    
		                    foreach(var referencedAssembly in ReferencedAssemblies)
		                        compilerParams.ReferencedAssemblies.Add(referencedAssembly);		                        
		                        
		                    CompilerResults = csharpCodeProvider.CompileAssemblyFromSource(compilerParams, sourceCode);	                    		                    		                    
		                    if (CompilerResults.Errors.Count > 0 || CompilerResults.CompiledAssembly == null)
							{
								CompilationErrors = "";
								foreach(CompilerError error in CompilerResults.Errors)
								{
									//CompilationErrors.Add(CompilationErrors.line(error.ToString());
									CompilationErrors = CompilationErrors.line(String.Format("{0}::{1}::{2}::{3}::{4}", error.Line,
                                                            error.Column, error.ErrorNumber,
                                                            error.ErrorText, error.FileName));
								}
		                    	DebugMode.error("Compilation failed");
		                    	this.invoke(onCompileFail);
		                    }
		                    else
		                   	{
		                   		DebugMode.debug("Compilation was OK");
		                    	this.invoke(onCompileOK);
		                    }
		                    compiling = false;
		                    compileSourceCode();
	                	});
            }
        }

        /*public string getAstErrors(string sourceCode)
        {
            return new Ast_CSharp(sourceCode).Errors;
        }*/

        public string createCSharpCodeWith_Class_Method_WithMethodText(string code)
        {
            if (code.empty())
                return null;
			code = code.line();	// make sure there is an empty line at the end            
                      
            try
            {
            	var snippetParser = new SnippetParser(SupportedLanguage.CSharp);
                var parsedCode = snippetParser.Parse(code);                
				AstErrors = snippetParser.errors();
                CompilationUnit = new CompilationUnit();
                if (parsedCode is BlockStatement || parsedCode is CompilationUnit)
                {
                    if (parsedCode is BlockStatement)
                    {
                        // map parsedCode into a new type and method 

                        var blockStatement = (BlockStatement) parsedCode;
                        CompilationUnit.add_Type(default_TypeName)
                            .add_Method(default_MethodName, InvocationParameters, blockStatement);
                    }
                    else
                        CompilationUnit = (CompilationUnit) parsedCode;
                    // create sourceCode using Ast_CSharp & AstDetails		
                    var astCSharp = new Ast_CSharp(CompilationUnit);
                    astCSharp.AstDetails.mapSpecials(snippetParser.Specials);
                    // // keep this so that we can process the instructions included as comments  	                        

                    // add references included in the original source code file
                    mapCodeO2References(astCSharp);
                    SourceCode = astCSharp.AstDetails.CSharpCode;
                    DebugMode.debug("Ast parsing was OK");
                    this.invoke(onAstOK);
                    return SourceCode;
                }            
            }
            catch (Exception ex)
            {                            	
				DebugMode.error("in createCSharpCodeWith_Class_Method_WithMethodText:{0}", ex.Message);                
            }      
			DebugMode.error("Ast parsing Failed");
			this.invoke(onAstFail);
			return null;                
        }                
        
        public void mapCodeO2References(Ast_CSharp astCSharp)
        {        	              	
        	ReferencedAssemblies = getDefaultReferencedAssemblies();
        	var compilationUnit = astCSharp.CompilationUnit;
        	
        	var currentUsingDeclarations = new List<string>();
        	foreach(var usingDeclaration in astCSharp.AstDetails.UsingDeclarations)
        		currentUsingDeclarations.Add(usingDeclaration.Text);
        	
        	var defaultUsingStatements = getDefaultUsingStatements();
        	foreach(var usingStatement in defaultUsingStatements)
        		if (false == currentUsingDeclarations.Contains(usingStatement))
        			compilationUnit.add_Using(usingStatement);
        	
        	foreach(var comment in astCSharp.AstDetails.Comments)
        	{
        		comment.Text.starts("using ", (value) => astCSharp.CompilationUnit.add_Using(value));
        		comment.Text.starts("ref ", (value) => ReferencedAssemblies.Add(value));
        		comment.Text.starts("O2Ref:", (value) => ReferencedAssemblies.Add(value));
        	}
        		        	
			// reset the astCSharp.AstDetails object        	
        	astCSharp.mapAstDetails(astCSharp.CompilationUnit);        	        	
        }               
        
        public object executeFirstMethod()
        {
        	
        	var parametersValues = InvocationParameters.valuesArray();
        	return executeFirstMethod(parametersValues);
        }
        
        public object executeFirstMethod(params object[] parameters)
        {
        	var assembly = CompilerResults.CompiledAssembly;
        	if (assembly != null)
        	{
        		var methods = assembly.methods();
        		if (methods.Count >0)        		
        		{
        			this.error("XXXXX");
        			this.error(" - method: {0}", methods[0].parameters().size());
        			this.error(" - param: {0}", parameters.size());
        			if (methods[0].parameters().size() == parameters.size())
        				return methods[0].invoke(parameters);
        			else
        				return methods[0].invoke();
        		}
        	}
        	return null;
        }                
    }
}
