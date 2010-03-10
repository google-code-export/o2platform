// This code was based on the CSharp Editor Example with Code Completion created by Daniel Grunwald
using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.DotNet;
using O2.Views.ASCX.classes.MainGUI;

using ICSharpCode;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Gui;
using ICSharpCode.SharpDevelop.Dom.CSharp;
using ICSharpCode.TextEditor.Gui.CompletionWindow;
using ICSharpCode.SharpDevelop.Dom;
using ICSharpCode.SharpDevelop.Dom.NRefactoryResolver;

using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.Ast;
using CSharpEditor;

using O2.External.SharpDevelop.ExtensionMethods;
using O2.Kernel;

namespace O2.External.SharpDevelop.Ascx
{
    public class O2CodeCompletion : ICompletionDataProvider
    {        	
        public TextEditorControl textEditor;
        //public TextEditorControl textEditorToGrabCodeFrom;      // (Was not really working) to support behind the scenes AST building
        public List<string> extraSourceCodeToProcess;
		public LanguageProperties CurrentLanguageProperties = LanguageProperties.CSharp;
		public ProjectContentRegistry pcRegistry;
        public DefaultProjectContent myProjectContent;
		public ParseInformation parseInformation = new ParseInformation();
		public ICompilationUnit lastCompilationUnit;
		public string DummyFileName = "edited.cs";
		public ImageList SmallIcons;
    	public Form HostForm { get; set; }
    	public CodeCompletionWindow codeCompletionWindow;
    	public Action<string> statusMessage;
    	
    	public O2CodeCompletion(TextEditorControl _textEditor)
    		: this(_textEditor,(text)=>text.info())
    	{
    	}
    	public O2CodeCompletion(TextEditorControl _textEditor, Action<string> status)
    	{    	
    		_textEditor.invokeOnThread(
    			()=>{
			    		textEditor = _textEditor;
                        //textEditorToGrabCodeFrom = _textEditor; // (wasn't working) by default these are the same
                        extraSourceCodeToProcess = new List<string>();
			    		//if (statusMessage!=null)
				    	statusMessage = status ; // (text)=>{ text.info();};		
				    	statusMessage("Loading Icons");
			    		loadIcons();
			    		statusMessage("Settinp Environment");
			    		setupEnvironment();
		    		});
    	}
    	    	
    	
    	public void setupEnvironment()
    	{
    		textEditor.ActiveTextAreaControl.TextArea.KeyEventHandler += TextAreaKeyEventHandler;						
			textEditor.Disposed += CloseCodeCompletionWindow;  // When the editor is disposed, close the code completion window
			
			//set up the ToolTipRequest event
			textEditor.ActiveTextAreaControl.TextArea.ToolTipRequest += OnToolTipRequest;
			
			this.pcRegistry = new ProjectContentRegistry();
			this.myProjectContent = new DefaultProjectContent();
			this.myProjectContent.Language = this.CurrentLanguageProperties;
            pcRegistry.ActivatePersistence(Path.Combine(PublicDI.config.O2TempDir,
                                                        "CSharpCodeCompletion"));
			// static parse current code thread
			startParseCodeThread();
			// add references
			
			startAddReferencesThread();         	
    	}
    	
    	void loadIcons()    	
    	{    	
    		//System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(O2CodeCompletion));
    		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));    		
    		SmallIcons = new ImageList();
			SmallIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));    			
    	}
    	/*public O2CodeCompletion(Form hostForm)
    	{
    		HostForm = hostForm;
    	}*/
    
    
    	public void open(string fileToOpen)
    	{
    		textEditor.open(fileToOpen);
    		//DummyFileName = fileToOpen;				// DC  - check if this makes the diference
    	}
    	
    	public void startAddReferencesThread()
    	{
            O2Thread.mtaThread(
                () =>
                {
                    statusMessage("Loading MsCorlib");
                    this.myProjectContent.AddReferencedContent(this.pcRegistry.Mscorlib);
                    addReference("System.Windows.Forms");    
                    addReference("System");
                    addReference("System.Data");
                    addReference("System.Drawing");
                    addReference("System.Xml");
                    addReference("Microsoft.VisualBasic");
                    
                    //this.myProjectContent.AddReferencedContent(this.pcRegistry.SystemWindowsForms);
                    foreach (var reference in CompileEngine.getListOfO2AssembliesInExecutionDir())
                        addReference(reference);
                    /*addReference("O2SharpDevelop.dll");
                    addReference("O2_Kernel.dll");
                    addReference("O2_Views_ASCX.dll");
                    addReference("O2_Views_ASCX.dll");						
                    addReference("O2_External_IE.dll");
                    addReference("O2_DotNetWrappers.dll");
                    addReference("O2_Core_XRules.dll");
                    addReference("O2_Core_CIR.dll");
                    addReference("O2_External_SharpDevelop.dll");
                    addReference("O2_External_O2Mono.dll");*/
                                   
                    statusMessage("Dependencies loaded");
                });
    	}
    	
    	public void addReference(string referenceAssembly)
    	{
    		this.myProjectContent.add_Reference(this.pcRegistry, referenceAssembly, statusMessage);			
    	}
    
    	// this will regularly parse the current source code so that we have code completion for its methods 
		public void startParseCodeThread()
		{
			O2Thread.mtaThread(
				()=>{
						//"starting parseCodeThread".debug();
						while (!textEditor.IsDisposed)
			                {
			                    //this.parseSourceCode(this.textEditorToGrabCodeFrom.get_Text());
                                this.parseSourceCode(this.textEditor.get_Text());
                                foreach (var codeOrFile in extraSourceCodeToProcess)
                                    if (codeOrFile.isFile())
                                        this.parseSourceCode(codeOrFile.contents());
                                    else
                                        this.parseSourceCode(codeOrFile);
			                    this.sleep(2000,false);
			             //       "post sleep in  parseCodeThread".debug();
			                }
			                "LEAVING  parseCodeThread".debug();
			         });
		}
		
		
    	public void parseSourceCode(string code)
		{	
			if (false == code.valid())
				return;            
			 var textReader = new StringReader(code);
			 ICompilationUnit newCompilationUnit;
			 var supportedLanguage = SupportedLanguage.CSharp;
			 using (IParser p = ParserFactory.CreateParser(supportedLanguage, textReader))
                {
                    // we only need to parse types and method definitions, no method bodies
                    // so speed up the parser and make it more resistent to syntax
                    // errors in methods
                    p.ParseMethodBodies = false;                    
                    p.Parse();
                    newCompilationUnit = ConvertCompilationUnit(p.CompilationUnit);
                }
            // Remove information from lastCompilationUnit and add information from newCompilationUnit.
            myProjectContent.UpdateCompilationUnit(lastCompilationUnit, newCompilationUnit, DummyFileName);
            lastCompilationUnit = newCompilationUnit;
            parseInformation.SetCompilationUnit(newCompilationUnit);                
		}
		
		public ICompilationUnit ConvertCompilationUnit(CompilationUnit compilationUnit)
        {
            NRefactoryASTConvertVisitor converter;
            converter = new NRefactoryASTConvertVisitor(myProjectContent);
            compilationUnit.AcceptVisitor(converter, null);            
            return converter.Cu;
        }
        
        
        
		// Was part of the CodeCompletionKeyHandler file
		
		/// <summary>
		/// Return true to handle the keypress, return false to let the text area handle the keypress
		/// </summary>
		public bool TextAreaKeyEventHandler(char key)
		{
			if (codeCompletionWindow != null) {
				// If completion window is open and wants to handle the key, don't let the text area
				// handle it
				if (codeCompletionWindow.ProcessKeyEvent(key))
					return true;
			}
			if (key == '.') {
				ICompletionDataProvider completionDataProvider = this;//new CodeCompletionProvider(this);
				
				codeCompletionWindow = CodeCompletionWindow.ShowCompletionWindow(
					textEditor.ParentForm,					// The parent window for the completion window
					textEditor, 							// The text editor to show the window for
					DummyFileName,							// Filename - will be passed back to the provider
					completionDataProvider,					// Provider to get the list of possible completions
					key										// Key pressed - will be passed to the provider
				);
				
				if (codeCompletionWindow != null) {
					// ShowCompletionWindow can return null when the provider returns an empty list
					codeCompletionWindow.Closed += new EventHandler(CloseCodeCompletionWindow);
				}
			}
			return false;
		}
		
		void CloseCodeCompletionWindow(object sender, EventArgs e)
		{
			if (codeCompletionWindow != null) {
				codeCompletionWindow.Closed -= new EventHandler(CloseCodeCompletionWindow);
				codeCompletionWindow.Dispose();
				codeCompletionWindow = null;
			}
		}
		
		
		// was part of Tool Tip Provider
		
		void OnToolTipRequest(object sender, ToolTipRequestEventArgs e)
		{
            try
            {
                if (e.InDocument && !e.ToolTipShown)
                {
                    IExpressionFinder expressionFinder;
                    expressionFinder = new CSharpExpressionFinder(this.parseInformation);
                    ExpressionResult expression = expressionFinder.FindFullExpression(
                        textEditor.Text,
                        textEditor.Document.PositionToOffset(e.LogicalPosition));
                    if (expression.Region.IsEmpty)
                    {
                        expression.Region = new DomRegion(e.LogicalPosition.Line + 1, e.LogicalPosition.Column + 1);
                    }

                    var textArea = textEditor.ActiveTextAreaControl.TextArea;
                    NRefactoryResolver resolver = new NRefactoryResolver(this.myProjectContent.Language);
                    ResolveResult rr = resolver.Resolve(expression,
                                                        this.parseInformation,
                                                        textArea.MotherTextEditorControl.Text);
                    string toolTipText = GetText(rr);
                    if (toolTipText != null)
                    {
                        e.ShowToolTip(toolTipText);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.log("in OnToolTipRequest");
            }
		}
		
		static string GetText(ResolveResult result)
		{
			if (result == null) {
				return null;
			}
			if (result is MixedResolveResult)
				return GetText(((MixedResolveResult)result).PrimaryResult);
			IAmbience ambience = new CSharpAmbience();
			ambience.ConversionFlags = ConversionFlags.StandardConversionFlags | ConversionFlags.ShowAccessibility;
			if (result is MemberResolveResult) {
				return GetMemberText(ambience, ((MemberResolveResult)result).ResolvedMember);
			} else if (result is LocalResolveResult) {
				LocalResolveResult rr = (LocalResolveResult)result;
				ambience.ConversionFlags = ConversionFlags.UseFullyQualifiedTypeNames
					| ConversionFlags.ShowReturnType;
				StringBuilder b = new StringBuilder();
				if (rr.IsParameter)
					b.Append("parameter ");
				else
					b.Append("local variable ");
				b.Append(ambience.Convert(rr.Field));
				return b.ToString();
			} else if (result is NamespaceResolveResult) {
				return "namespace " + ((NamespaceResolveResult)result).Name;
			} else if (result is TypeResolveResult) {
				IClass c = ((TypeResolveResult)result).ResolvedClass;
				if (c != null)
					return GetMemberText(ambience, c);
				else
					return ambience.Convert(result.ResolvedType);
			} else if (result is MethodGroupResolveResult) {
				MethodGroupResolveResult mrr = result as MethodGroupResolveResult;
				IMethod m = mrr.GetMethodIfSingleOverload();
				if (m != null)
					return GetMemberText(ambience, m);
				else
					return "Overload of " + ambience.Convert(mrr.ContainingType) + "." + mrr.Name;
			} else {
				return null;
			}
		}
		
		static string GetMemberText(IAmbience ambience, IEntity member)
		{
			StringBuilder text = new StringBuilder();
			if (member is IField) {
				text.Append(ambience.Convert(member as IField));
			} else if (member is IProperty) {
				text.Append(ambience.Convert(member as IProperty));
			} else if (member is IEvent) {
				text.Append(ambience.Convert(member as IEvent));
			} else if (member is IMethod) {
				text.Append(ambience.Convert(member as IMethod));
			} else if (member is IClass) {
				text.Append(ambience.Convert(member as IClass));
			} else {
				text.Append("unknown member ");
				text.Append(member.ToString());
			}
			string documentation = member.Documentation;
			if (documentation != null && documentation.Length > 0) {
				text.Append('\n');
				text.Append(CodeCompletionData.XmlDocumentationToText(documentation));
			}
			return text.ToString();
		}
		
		
		// part of the CodeCompletionProvider   (public class CodeCompletionProvider : ICompletionDataProvider)
		
		
		public ImageList ImageList {
			get {
				return this.SmallIcons;				
			}
		}
		
		public string PreSelection {
			get {
				return null;
			}
		}
		
		public int DefaultIndex {
			get {
				return -1;
			}
		}
		
		public CompletionDataProviderKeyResult ProcessKey(char key)
		{
			if (char.IsLetterOrDigit(key) || key == '_') {
				return CompletionDataProviderKeyResult.NormalKey;
			} else {
				// key triggers insertion of selected items
				return CompletionDataProviderKeyResult.InsertionKey;
			}
		}
		
		/// <summary>
		/// Called when entry should be inserted. Forward to the insertion action of the completion data.
		/// </summary>
		public bool InsertAction(ICompletionData data, TextArea textArea, int insertionOffset, char key)
		{
			textArea.Caret.Position = textArea.Document.OffsetToPosition(insertionOffset);
			return data.InsertAction(textArea, key);
		}
		
		public ICompletionData[] GenerateCompletionData(string fileName, TextArea textArea, char charTyped)
		{
			// We can return code-completion items like this:
			
			//return new ICompletionData[] {
			//	new DefaultCompletionData("Text", "Description", 1)
			//};
			
			NRefactoryResolver resolver = new NRefactoryResolver(this.myProjectContent.Language);
			ResolveResult rr = resolver.Resolve(FindExpression(textArea),
			                                        this.parseInformation,
			                                        textArea.MotherTextEditorControl.Text);
			List<ICompletionData> resultList = new List<ICompletionData>();
			if (rr != null) {
				ArrayList completionData = rr.GetCompletionData(this.myProjectContent);
				if (completionData != null) {
					AddCompletionData(resultList, completionData);
				}
			}
			return resultList.ToArray();
		}
		
		/// <summary>
		/// Find the expression the cursor is at.
		/// Also determines the context (using statement, "new"-expression etc.) the
		/// cursor is at.
		/// </summary>
		ExpressionResult FindExpression(TextArea textArea)
		{
			IExpressionFinder finder;
			finder = new CSharpExpressionFinder(this.parseInformation);			
			ExpressionResult expression = finder.FindExpression(textArea.Document.TextContent, textArea.Caret.Offset);
			if (expression.Region.IsEmpty) {
				expression.Region = new DomRegion(textArea.Caret.Line + 1, textArea.Caret.Column + 1);
			}
			return expression;
		}
		
		void AddCompletionData(List<ICompletionData> resultList, ArrayList completionData)
		{
			// used to store the method names for grouping overloads
			Dictionary<string, CodeCompletionData> nameDictionary = new Dictionary<string, CodeCompletionData>();
			
			// Add the completion data as returned by SharpDevelop.Dom to the
			// list for the text editor
			foreach (object obj in completionData) {
				if (obj is string) {
					// namespace names are returned as string
					resultList.Add(new DefaultCompletionData((string)obj, "namespace " + obj, 5));
				} else if (obj is IClass) {
					IClass c = (IClass)obj;
					resultList.Add(new CodeCompletionData(c,this));
				} else if (obj is IMember) {
					IMember m = (IMember)obj;
					if (m is IMethod && ((m as IMethod).IsConstructor)) {
						// Skip constructors
						continue;
					}
					// Group results by name and add "(x Overloads)" to the
					// description if there are multiple results with the same name.
					
					CodeCompletionData data;
					if (nameDictionary.TryGetValue(m.Name, out data)) {
						data.AddOverload();
					} else {
						nameDictionary[m.Name] = data = new CodeCompletionData(m,this);
						resultList.Add(data);
					}
				} else {
					// Current ICSharpCode.SharpDevelop.Dom should never return anything else
					throw new NotSupportedException();
				}
			}
		}
		
    }
    
    public class CodeCompletionData : DefaultCompletionData, ICompletionData
	{
		O2CodeCompletion o2CodeCompletion;
		IMember member;
		IClass c;		
		CSharpAmbience csharpAmbience = new CSharpAmbience();
		
		public CodeCompletionData(IMember member, O2CodeCompletion _o2CodeCompletion)
			: base(member.Name, null, GetMemberImageIndex(member))
		{
			this.member = member;
			o2CodeCompletion =  _o2CodeCompletion;
		}
		
		public CodeCompletionData(IClass c, O2CodeCompletion _o2CodeCompletion)
			: base(c.Name, null, GetClassImageIndex(c))
		{
			this.c = c;
			o2CodeCompletion = _o2CodeCompletion;
		}
		
		int overloads = 0;
		
		public void AddOverload()
		{
			overloads++;
		}
		
		static int GetMemberImageIndex(IMember member)
		{
			// Missing: different icons for private/public member
			if (member is IMethod)
				return 1;
			if (member is IProperty)
				return 2;
			if (member is IField)
				return 3;
			if (member is IEvent)
				return 6;
			return 3;
		}
		
		static int GetClassImageIndex(IClass c)
		{
			switch (c.ClassType) {
				case ICSharpCode.SharpDevelop.Dom.ClassType.Enum:
					return 4;
				default:
					return 0;
			}
		}
		
		string description;
		
		// DefaultCompletionData.Description is not virtual, but we can reimplement
		// the interface to get the same effect as overriding.
		string ICompletionData.Description {
			get {
				if (description == null) {
					IEntity entity = (IEntity)member ?? c;
					description = GetText(entity);
					if (overloads > 1) {
						description += " (+" + overloads + " overloads)";
					}
					description += Environment.NewLine + XmlDocumentationToText(entity.Documentation);
				}
				return description;
			}
		}
		
		/// <summary>
		/// Converts a member to text.
		/// Returns the declaration of the member as C# or VB code, e.g.
		/// "public void MemberName(string parameter)"
		/// </summary>
		string GetText(IEntity entity)
		{
			return (string)o2CodeCompletion.textEditor.invokeOnThread(
				()=> {					
						IAmbience ambience = csharpAmbience;
						if (entity is IMethod)
							return ambience.Convert(entity as IMethod);
						if (entity is IProperty)
							return ambience.Convert(entity as IProperty);
						if (entity is IEvent)
							return ambience.Convert(entity as IEvent);
						if (entity is IField)
							return ambience.Convert(entity as IField);
						if (entity is IClass)
							return ambience.Convert(entity as IClass);
						// unknown entity:
						return entity.ToString();
					});
		}
		
		static public string XmlDocumentationToText(string xmlDoc)
		{
			System.Diagnostics.Debug.WriteLine(xmlDoc);
			StringBuilder b = new StringBuilder();
			try {
				using (XmlTextReader reader = new XmlTextReader(new StringReader("<root>" + xmlDoc + "</root>"))) {
					reader.XmlResolver = null;
					while (reader.Read()) {
						switch (reader.NodeType) {
							case XmlNodeType.Text:
								b.Append(reader.Value);
								break;
							case XmlNodeType.Element:
								switch (reader.Name) {
									case "filterpriority":
										reader.Skip();
										break;
									case "returns":
										b.AppendLine();
										b.Append("Returns: ");
										break;
									case "param":
										b.AppendLine();
										b.Append(reader.GetAttribute("name") + ": ");
										break;
									case "remarks":
										b.AppendLine();
										b.Append("Remarks: ");
										break;
									case "see":
										if (reader.IsEmptyElement) {
											b.Append(reader.GetAttribute("cref"));
										} else {
											reader.MoveToContent();
											if (reader.HasValue) {
												b.Append(reader.Value);
											} else {
												b.Append(reader.GetAttribute("cref"));
											}
										}
										break;
								}
								break;
						}
					}
				}
				return b.ToString();
			} catch (XmlException) {
				return xmlDoc;
			}
		}
	}
	
	
	public static class SharpdevelopExtensionMethods
    {
    	public static void add_Reference(this DefaultProjectContent projectContent, ProjectContentRegistry pcRegistry, string assemblyToLoad, Action<string> debugMessage)
    	{
    		debugMessage("Loading: {0}".format(assemblyToLoad));
    		//if (!assemblyToLoad.fileExists())
    		//	"file doesn't exist".error();    		
    		IProjectContent referenceProjectContent = pcRegistry.GetProjectContentForReference(assemblyToLoad, assemblyToLoad);
    		if (referenceProjectContent== null)
    			"referenceProjectContent was null".error();
    		else
    		{
				projectContent.AddReferencedContent(referenceProjectContent);
				if (referenceProjectContent is ReflectionProjectContent)
                    (referenceProjectContent as ReflectionProjectContent).InitializeReferences();
                else 
                	"something when wrong in DefaultProjectContent.add_Reference".error();
            }
    	}
    }
}
