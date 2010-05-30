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
using O2.API.AST.ExtensionMethods.CSharp;
using O2.Kernel;
using O2.DotNetWrappers.Windows;

namespace O2.External.SharpDevelop.AST
{
    public class CSharp_FastCompiler
    {
        internal int forceAstBuildDelay = 100;
        public string SourceCode { get; set; }						// I think there is a small race condition with the use of this variable
        public string SourceCodeFile { get; set; }
        //public string OriginalCodeSnippet { get; set; }	
        public bool CreatedFromSnipptet { get; set; }
        public List<string> ReferencedAssemblies { get; set; }
        public Dictionary<string, object> InvocationParameters { get; set; }
        public CompilationUnit CompilationUnit { get; set; }
        public List<string> ExtraSourceCodeFilesToCompile { get; set; }
        public AstDetails AstDetails { get; set; }
        public string AstErrors { get; set; }
        public bool generateDebugSymbols { get; set; }
        public string CompilationErrors { get; set; }        
        public CompilerResults CompilerResults { get; set; }
        public bool ExecuteInStaThread { get; set; }
        public bool ExecuteInMtaThread { get; set; }
        
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

        public System.Threading.ManualResetEvent FinishedCompilingCode { get;set;} 
		
        public CSharp_FastCompiler()
        {        
        	DebugMode = false;				// set to true to see details about each AstCreation and Compilation stage
        	InvocationParameters = new Dictionary<string, object>();
            ExtraSourceCodeFilesToCompile = new List<String>();
        	ReferencedAssemblies = getDefaultReferencedAssemblies();
            default_MethodName = "dynamicMethod";
            default_TypeName = "DynamicType";            
            generateDebugSymbols = false;
            //OriginalCodeSnippet = "";
            SourceCode = "";
            FinishedCompilingCode = new System.Threading.ManualResetEvent(true);
            // defaults

        }

        public List<string> getDefaultUsingStatements()
        {
            return new List<string>().add("System")
                                     .add("System.Drawing")
                                     .add("System.Windows.Forms")
                                     .add("System.Collections.Generic")
                                     .add("O2.Interfaces")
                                     .add("O2.Kernel")
                                     .add("O2.Kernel.ExtensionMethods")
                                     .add("O2.Views.ASCX.ExtensionMethods")
                                     .add("O2.Views.ASCX.CoreControls")
                                     .add("O2.Views.ASCX.classes.MainGUI")
                                     .add("O2.DotNetWrappers.ExtensionMethods")
                                     .add("O2.DotNetWrappers.Windows")
                                     .add("O2.DotNetWrappers.DotNet")
                                     .add("O2.DotNetWrappers.Network")
                                     .add("O2.External.IE.ExtensionMethods")
                                     .add("O2.XRules.Database.ExtensionMethods")
                                     //.add("O2.XRules.Database._Rules._Interfaces")
                                     //.add("O2.XRules.Database._Rules.APIs")
                                     //.add("O2.XRules.Database.O2Utils")
                                     .add("O2.External.SharpDevelop.ExtensionMethods")
                                     .add("O2.External.SharpDevelop.Ascx")
                //GraphSharp related
                                     .add("O2.Script")
                                     .add("GraphSharp.Controls")
                                     .add("O2.API.Visualization.ExtensionMethods")
                                     .add("O2.API.AST.Graph")
                                     .add("O2.API.AST.CSharp")
                                     .add("O2.API.AST.ExtensionMethods.CSharp")
                                     .add("O2.API.AST.ExtensionMethods")
                                     .add("WPF=System.Windows.Controls")
                                     .add("Media=System.Windows.Media")
                //Scanning AST Engine related
                                     .add("ICSharpCode.NRefactory")
                                     .add("ICSharpCode.NRefactory.Ast")
                                     .add("ICSharpCode.SharpDevelop.Dom");                
           

        }
		public List<string> getDefaultReferencedAssemblies()
        {
            return new List<string>().add("System.dll")
                                     .add("System.Drawing.dll")
                                     .add("System.Core.dll")
                                     .add("System.Windows.Forms.dll")
                                     .add("O2_Kernel.dll")
                                     .add("O2_Interfaces.dll")
                                     .add("O2_DotNetWrappers.dll")
                                     .add("O2_Views_Ascx.dll")
                                     .add("O2_External_IE.dll")
                                     .add("O2_XRules_Database.exe")
                                     .add("O2_External_SharpDevelop.dll")
                                     .add("O2SharpDevelop.dll")
                //GraphSharp related
                                     .add("O2_Api_Visualization.dll")
                                     .add("O2_Api_AST.dll")
                                     .add("QuickGraph.dll")
                                     .add("GraphSharp.dll")
                                     .add("GraphSharp.Controls.dll")
                                     .add("PresentationCore.dll")
                                     .add("PresentationFramework.dll")
                                     .add("WindowsBase.dll")
                                     .add("WindowsFormsIntegration.dll")
                                     .add("ICSharpCode.AvalonEdit.dll")
                // twitter related
                                     .add("Newtonsoft.Json.dll")
                                     .add("Dimebrain.TweetSharp.dll")
                //Linq to Xsd
                                     .add("System.Xml.dll")
                                     .add("System.Xml.Linq.dll")
                                     .add("O2_Misc_Microsoft_MPL_Libs.dll");                                     
        }

		public Dictionary<string,object> getDefaultInvocationParameters()
		{
			return new Dictionary<string, object>();
		}
              
        public void compileSnippet(string codeSnippet)
        {
            try
            {
                FinishedCompilingCode.Reset();
                createAstStack = new Stack<string>();
          //      createAstStack.Clear();
                if (createAstStack.Count == 0)
                    creatingAst = false;
                createAstStack.Push(codeSnippet);
                compileSnippet();
            }
            catch (Exception ex)
            {
                ex.log("in compileSnippet");
            }
        }
        public void compileSnippet()
        {
            O2Thread.mtaThread(
                () =>
                {
                    if (creatingAst == false && createAstStack.Count > 0)
                    {
                        creatingAst = true;
                        var codeSnippet = createAstStack.Pop();
                        this.sleep(forceAstBuildDelay, DebugMode);            // wait a bit to allow more entries to be cleared from the stack
                        if (createAstStack.Count > 0)
                            codeSnippet = createAstStack.Pop();

                        createAstStack.Clear();

                        InvocationParameters = getDefaultInvocationParameters();
                        this.invoke(beforeSnippetAst);
                        DebugMode.info("Compiling Source Snippet (Size: {0})", codeSnippet.size());
                        var sourceCode = createCSharpCodeWith_Class_Method_WithMethodText(codeSnippet);
                        if (sourceCode != null)
                            compileSourceCode(sourceCode, CreatedFromSnipptet);
                        else
                            FinishedCompilingCode.Set();
                        creatingAst = false;
                        compileSnippet();
                    }
                });
        }

        private void compileSourceCode(string sourceCode, bool compiledFromSnipptet)
        {
            compiledFromSnipptet = compiledFromSnipptet;
            compileStack.Push(sourceCode);
            compileSourceCode();
        }

		public void compileSourceCode(string sourceCode)
        {
            if (sourceCode.valid().isFalse())
            {
                "in CSharp_FastCompiler,compileSourceCode, provided sourceCode code was empty".error();
                if (onCompileFail !=null)
                    onCompileFail();
            }
            else
            {
                // we need to do make sure we include any extra references included in the code
                var astCSharp = new Ast_CSharp(sourceCode);
                mapCodeO2References(astCSharp);
                compileSourceCode(sourceCode, false);
            }
       	}
       	
       	public void compileSourceCode()
       	{
       	    O2Thread.mtaThread(
       	        () =>
       	            {
                        try
                        {

                            if (compiling == false && compileStack.Count > 0)
                            {
                                compiling = true;
                                FinishedCompilingCode.Reset();
                                compileExtraSourceCodeReferencesAndUpdateReferencedAssemblies();
                                this.sleep(forceAstBuildDelay, DebugMode);            // wait a bit to allow more entries to be cleared from the stack
                                var sourceCode = compileStack.Pop();
                                compileStack.Clear();
                                // remove all previous compile requests (since their source code is now out of date

                                //Files.setCurrentDirectoryToExecutableDirectory();                			                		
                                Environment.CurrentDirectory = Kernel.PublicDI.config.CurrentExecutableDirectory;
                                ;
                                this.invoke(beforeCompile);
                                DebugMode.info("Compiling Source Code (Size: {0})", sourceCode.size());
                                SourceCode = sourceCode;
                                var providerOptions = new Dictionary<string, string>();
                                providerOptions.Add("CompilerVersion", "v3.5");
                                var csharpCodeProvider = new Microsoft.CSharp.CSharpCodeProvider(providerOptions);
                                var compilerParams = new CompilerParameters
                                                         {
                                                             GenerateInMemory = !generateDebugSymbols,
                                                             IncludeDebugInformation = generateDebugSymbols
                                                         };

                                foreach (var referencedAssembly in ReferencedAssemblies)
                                    compilerParams.ReferencedAssemblies.Add(referencedAssembly);

                                CompilerResults = csharpCodeProvider.CompileAssemblyFromSource(compilerParams, sourceCode);

                                FinishedCompilingCode.Set();

                                if (CompilerResults.Errors.Count > 0 || CompilerResults.CompiledAssembly == null)
                                {
                                    CompilationErrors = "";
                                    foreach (CompilerError error in CompilerResults.Errors)
                                    {
                                        //CompilationErrors.Add(CompilationErrors.line(error.ToString());
                                        CompilationErrors =
                                            CompilationErrors.line(String.Format("{0}::{1}::{2}::{3}::{4}", error.Line,
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
                            }
                        }
                        catch (Exception ex)
                        {
                            ex.log("in compileSourceCode");
                            compiling = false;
                            FinishedCompilingCode.Set();
                        }
       	            });
        }

        // we need to use CompileEngine (which is slower but supports multiple file compilation 
        public void compileExtraSourceCodeReferencesAndUpdateReferencedAssemblies()
        {            
            if (ExtraSourceCodeFilesToCompile.size() > 0)
            {                        
                var assembly = new CompileEngine().compileSourceFiles(ExtraSourceCodeFilesToCompile);                
                if (assembly != null)
                {
                    ReferencedAssemblies.Add(assembly.Location);
                    generateDebugSymbols = true;                // if there are extra assemblies we can't generate the assembly in memory                    
                }
            }
        }
        /*public string getAstErrors(string sourceCode)
        {
            return new Ast_CSharp(sourceCode).Errors;
        }*/
        public string createCSharpCodeWith_Class_Method_WithMethodText(string code)
        {                        
            var parsedCode = TextToCodeMappings.tryToFixRawCode(code, tryToCreateCSharpCodeWith_Class_Method_WithMethodText);            
        
            if (parsedCode == null)
            {
                DebugMode.error("Ast parsing Failed");
                this.invoke(onAstFail);
            }
            return parsedCode;
        }

        public string tryToCreateCSharpCodeWith_Class_Method_WithMethodText(string code)
        {
            if (code.empty())
                return null;
			code = code.line();	// make sure there is an empty line at the end            
                      
            try
            {
                // handle special incudes in source code
                foreach(var originalLine in code.lines())
                    originalLine.starts("//include", (includeText) => 
                        {
                            if (includeText.fileExists())
                                code = code.Replace(originalLine, originalLine.line().add(includeText.contents()));
                        });  
            	var snippetParser = new SnippetParser(SupportedLanguage.CSharp);
                
                var parsedCode = snippetParser.Parse(code);
				AstErrors = snippetParser.errors();
                CompilationUnit = new CompilationUnit();

                if (parsedCode is BlockStatement || parsedCode is CompilationUnit)
                {
                    Ast_CSharp astCSharp;
                    if (parsedCode is BlockStatement)
                    {
                        // map parsedCode into a new type and method 

                        var blockStatement = (BlockStatement)parsedCode;
                        CompilationUnit.add_Type(default_TypeName)
                            .add_Method(default_MethodName, InvocationParameters, blockStatement);

                        astCSharp = new Ast_CSharp(CompilationUnit, snippetParser.Specials);
                        //astCSharp.AstDetails.mapSpecials();

                        // add references included in the original source code file
                        mapCodeO2References(astCSharp);

                        CreatedFromSnipptet = true;
                    }
                    else
                    {
                        CompilationUnit = (CompilationUnit)parsedCode;
                        if (CompilationUnit.Children.Count == 0)
                            return null;

                        astCSharp = new Ast_CSharp(CompilationUnit, snippetParser.Specials);
                        // add the comments from the original code                        

                        mapCodeO2References(astCSharp);
                        CreatedFromSnipptet = false;
                    }
                    
                    // create sourceCode using Ast_CSharp & AstDetails		
                    if(CompilationUnit.Children.Count > 0)
                    {                        
                        // reset the astCSharp.AstDetails object        	
                        astCSharp.mapAstDetails();
                        // add the comments from the original code
                        astCSharp.ExtraSpecials.AddRange(snippetParser.Specials);	                 

	                    SourceCode = astCSharp.AstDetails.CSharpCode;

                        //once we have the created SourceCode we need to create a new AST with it
                        var tempAstDetails = new Ast_CSharp(SourceCode).AstDetails;
                        //note we should try to add back the specials here (so that comments make it to the generated code
                        AstDetails = tempAstDetails;
	                    DebugMode.debug("Ast parsing was OK");
	                    this.invoke(onAstOK);
	                    return SourceCode;
                    }
                }            
            }
            catch (Exception ex)
            {                            	
				DebugMode.error("in createCSharpCodeWith_Class_Method_WithMethodText:{0}", ex.Message);                
            }      			
			return null;                
        }        

        public void mapCodeO2References(Ast_CSharp astCSharp)
        {            
            bool onlyAddReferencedAssemblies = false;
            generateDebugSymbols = false; // default to not generating debug symbols and creating the assembly only in memory
            ExtraSourceCodeFilesToCompile = new List<string>();        	
        	var compilationUnit = astCSharp.CompilationUnit;
            ReferencedAssemblies = new List<string>();

        	var currentUsingDeclarations = new List<string>();
        	foreach(var usingDeclaration in astCSharp.AstDetails.UsingDeclarations)
        		currentUsingDeclarations.Add(usingDeclaration.Text);
        	
        	
            foreach (var comment in astCSharp.AstDetails.Comments)
            {
                comment.Text.eq("O2Tag_OnlyAddReferencedAssemblies", () => onlyAddReferencedAssemblies = true);                
                comment.Text.starts("using ", false, value => astCSharp.CompilationUnit.add_Using(value));
                comment.Text.starts(new [] {"ref ", "O2Ref:"}, false,  value => ReferencedAssemblies.Add(value));
                comment.Text.starts(new[] { "include", "file ", "O2File:" }, false, value => ExtraSourceCodeFilesToCompile.Add(value));
                comment.Text.starts(new[] { "dir ", "O2Dir:" }, false, value => ExtraSourceCodeFilesToCompile.AddRange(value.files("*.cs",true))); 
               
                comment.Text.starts(new[] {"O2:debugSymbols",
                                        "generateDebugSymbols", 
                                        "debugSymbols"}, true, (value) => generateDebugSymbols = true);
                comment.Text.eq("StaThread", () => { ExecuteInStaThread = true; });
                comment.Text.eq("MtaThread", () => { ExecuteInMtaThread = true; });  
            }

            //resolve location of ExtraSourceCodeFilesToCompile

            resolveFileLocationsOfExtraSourceCodeFilesToCompile();                        

            //make sure the referenced assemblies are in the current execution directory
            foreach(var reference in ReferencedAssemblies)
                if (reference.fileExists())
                {
                    var expectedFile = PublicDI.config.CurrentExecutableDirectory.pathCombine(reference.fileName());
                    if (expectedFile.fileExists().isFalse())
                        Files.Copy(reference, expectedFile);
                }

            if (onlyAddReferencedAssemblies.isFalse())
            {
                foreach (var defaultRefAssembly in getDefaultReferencedAssemblies())
                    if (ReferencedAssemblies.Contains(defaultRefAssembly).isFalse())
                        ReferencedAssemblies.add(defaultRefAssembly);            
                foreach (var usingStatement in getDefaultUsingStatements())
                    if (false == currentUsingDeclarations.Contains(usingStatement))
                        compilationUnit.add_Using(usingStatement);
            }

        }

        public void resolveFileLocationsOfExtraSourceCodeFilesToCompile()
        {
            if (ExtraSourceCodeFilesToCompile.size() > 0)
            {
                string defaultLocalScriptsFolder = @"C:\O2\O2Scripts_Database\_Scripts";
                List<string> o2LocalScriptFiles = null;
                // try to resolve local file references
                try
                {
                    for (int i = 0; i < ExtraSourceCodeFilesToCompile.size(); i++)
                    {
                        var fileToResolve = ExtraSourceCodeFilesToCompile[i].trim();
                        if (fileToResolve.fileExists().isFalse())
                            if (SourceCodeFile.valid())
                            {
                                var resolvedFile = SourceCodeFile.directoryName().pathCombine(fileToResolve);
                                if (resolvedFile.fileExists())
                                    ExtraSourceCodeFilesToCompile[i] = resolvedFile;
                            }
                        if (fileToResolve.fileExists().isFalse())
                        {
                            if (o2LocalScriptFiles == null)
                            {
                                o2LocalScriptFiles = defaultLocalScriptsFolder.files(true, "*.cs");
                            }
                            foreach (var localScriptFile in o2LocalScriptFiles)
                            {     
                                if (localScriptFile.fileName().lower().starts(fileToResolve.lower()))
                                //if (fileToResolve.lower() == localScriptFile.fileName().lower())
                                {
                                    "in CSharp fast compiler, file reference '{0}' was mapped to local O2 Script file '{1}'".debug(fileToResolve, localScriptFile);
                                    ExtraSourceCodeFilesToCompile[i] = localScriptFile;
                                    break;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ex.log("in compileExtraSourceCodeReferencesAndUpdateReferencedAssemblies while resolving ExtraSourceCodeFilesToCompile");
                }
            }
        }

        public object executeFirstMethod()
        {        	
        	var parametersValues = InvocationParameters.valuesArray();
            var assembly = CompilerResults.CompiledAssembly;
            return assembly.executeFirstMethod(ExecuteInStaThread, ExecuteInMtaThread, parametersValues);        	
        }                       
        
        

        /*public string processedCode()
        {
            //if (OriginalCodeSnippet.valid())
            //    return OriginalCodeSnippet;
            return SourceCode;                    
        }*/

        public Location getGeneratedSourceCodeMethodLineOffset()
        {
            if (CreatedFromSnipptet == true && SourceCode.valid())
                //if (OriginalCodeSnippet != SourceCode)      // if they are the same it means that there is no offset                    
                    if (AstDetails.Methods.size() > 0)
                    {
                        return AstDetails.Methods[0].OriginalObject.firstLineOfCode();

                        /*var firstMethod = AstDetails.Methods.first<AstValue>();
                        if (firstMethod.StartLocation.Line != 0)
                        {

//                            var methodDeclaration = AstDetails.Methods.first<AstValue>().OriginalObject.cast<MethodDeclaration>();
                            var methodDeclaration = (MethodDeclaration)AstDetails.Methods[0].OriginalObject;
                            var firstInstruction = methodDeclaration.Body.Children[0].StartLocation; ;
//                            methodDeclaration.
                            //var aa = aaa.typeFullName();
                         //   return AstDetails.Methods.first<AstValue>().StartLocation;
                            //var location = AstDetails.Methods.first<AstValue>().StartLocation;
                            return new Location(firstInstruction.Column - 1, firstInstruction.Line - 1);
                        }*/
                    }
            return new Location(0, 0) ;
        }

        public void waitForCompilationComplete()
        {
            FinishedCompilingCode.WaitOne();
        }
    }
}
