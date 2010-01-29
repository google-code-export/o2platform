// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using O2.Kernel;
using O2.Kernel.Interfaces.O2Core;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.Views.ASCX;
using O2.DotNetWrappers.Network;
using O2.Views.ASCX.classes.MainGUI;
using O2.External.SharpDevelop.Ascx;

using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.Ast;
using ICSharpCode.NRefactory.PrettyPrinter;


//O2_File:C:\O2\O2 - All Active Projects\O2 - All Active Projects\O2Core\O2_DotNetWrappers\Network\Web.cs
//O2_File:C:\O2\O2 - All Active Projects\O2 - All Active Projects\O2Core\O2_Views_ASCX\classes\MainGUI\WinForms.cs
//O2_File:C:\O2\O2 - All Active Projects\O2 - All Active Projects\O2Core\O2_Views_ASCX\classes\MainGUI\O2Gui.cs
//O2_File:extra.cs
//O2_File:c:\O2\O2 - All Active Projects\O2 - All Active Projects\O2 Modules Using 3rd Party Dlls\O2_External_SharpDevelop\AST\AstTreeView.cs



namespace O2.Script
{
    public class Main
    {    
    	private static IO2Log log = PublicDI.log;
		
        public static void openAscx()
		{
			var sourceCodeAST = (ascx_View_SourceCode_AST)WinForms.showAscxInForm(
				typeof(ascx_View_SourceCode_AST), 
				"View SourceCode AST", 
				700, 
				500);		
							
			sourceCodeAST.loadFile(testFileToUse());
		//	sourceCodeAST.showAST();
		}
		
		private static string testFileToUse()
		{
			var testFile = @"http://o2platform.googlecode.com/svn/trunk/O2 - All Active Projects/O2_XRules_Database/_Rules/_Samples/HelloWorld.cs";
			return Web.checkIfFileExistsAndDownloadIfNot("HelloWorld.cs", testFile);
		}
	}
	
	public class ascx_View_SourceCode_AST : UserControl
	{
		ascx_SourceCodeEditor sourceCodeEditor;
		TabControl tabControl;
		TreeView ast_TreeView;
		TreeView usingDeclarations_TreeView;
        TreeView types_TreeView;
        TreeView methods_TreeView;
        TreeView fields_TreeView;
        TreeView properties_TreeView;
        TreeView comments_TreeView;
		ascx_SourceCodeEditor rewritenCSharpCode_SourceCodeEditor;
		ascx_SourceCodeEditor rewritenVBNet_SourceCodeEditor;
		
		public ascx_View_SourceCode_AST()
		{
			createGUI();					
		}			
		
		public void createGUI()
		{
			var splitControl = this.add_SplitContainer(
        						true, 		//setOrientationToHorizontal
        						true,		// setDockStyleoFill
        						true);		// setBorderStyleTo3D)
        	var topGroupBox = splitControl.Panel1.add_GroupBox("SourceCode");        	        	
        	
            //var bottomGroupBox = splitControl.Panel2.add_GroupBox("Ast");
            
        	sourceCodeEditor = topGroupBox.add_SourceCodeEditor();
        	
        	tabControl = splitControl.Panel2.add_TabControl();
        	        	
        	ast_TreeView = tabControl.add_Tab("AST").add_TreeView();
        	usingDeclarations_TreeView = tabControl.add_Tab("Using Declarations").add_TreeView();
        	types_TreeView = tabControl.add_Tab("Types").add_TreeView();;        	
        	methods_TreeView = tabControl.add_Tab("Methods").add_TreeView();;
        	fields_TreeView = tabControl.add_Tab("Fields").add_TreeView();;
        	properties_TreeView = tabControl.add_Tab("Properties").add_TreeView();;
        	comments_TreeView = tabControl.add_Tab("Comments").add_TreeView();;
        	rewritenCSharpCode_SourceCodeEditor = tabControl.add_Tab("Re-writen Code : CSharp").add_SourceCodeEditor();        	
        	rewritenVBNet_SourceCodeEditor = tabControl.add_Tab("Re-writen Code : VB.Net").add_SourceCodeEditor();;       
        	
        	sourceCodeEditor.eDocumentDataChanged += updateView;    
        	
        	usingDeclarations_TreeView.AfterSelect += showInSourceCode;
        	types_TreeView.AfterSelect += showInSourceCode;
        	methods_TreeView.AfterSelect += showInSourceCode;
        	fields_TreeView.AfterSelect += showInSourceCode;
        	properties_TreeView.AfterSelect += showInSourceCode;
        	comments_TreeView.AfterSelect += showInSourceCode;
 		}
		
		
		// move to TextEditor control
		public void showInSourceCode(Object sender,TreeViewEventArgs e)
		{
			var treeNoteTag = e.Node.Tag;
			if (treeNoteTag is AstValue)
			{
				var astValue = (AstValue)treeNoteTag;
				PublicDI.log.error("{0} {1} - {2}", astValue.Text, astValue.StartLocation, astValue.EndLocation);
				
				var textEditorControl = sourceCodeEditor.getObject_TextEditorControl();
				var start = new ICSharpCode.TextEditor.TextLocation(astValue.StartLocation.X-1,astValue.StartLocation.Y-1);
				var end = new ICSharpCode.TextEditor.TextLocation(astValue.EndLocation.X-1,astValue.EndLocation.Y-1);
				var selection = new ICSharpCode.TextEditor.Document.DefaultSelection(textEditorControl.Document,start, end);
				textEditorControl.ActiveTextAreaControl.SelectionManager.SetSelection(selection);	
				setCaretToCurrentSelection(textEditorControl);
			}			
		}
		
		public void setCaretToCurrentSelection(ICSharpCode.TextEditor.TextEditorControl textEditorControl)
		{
			var finalCaretPosition = textEditorControl.ActiveTextAreaControl.TextArea.SelectionManager.SelectionCollection[0].StartPosition;
			var tempCaretPosition = new ICSharpCode.TextEditor.TextLocation();
			tempCaretPosition.X = finalCaretPosition.X;
			tempCaretPosition.Y = finalCaretPosition.Y + 10;
			textEditorControl.ActiveTextAreaControl.Caret.Position = tempCaretPosition;				
			textEditorControl.ActiveTextAreaControl.TextArea.ScrollToCaret();
			textEditorControl.ActiveTextAreaControl.Caret.Position = finalCaretPosition;				
			textEditorControl.ActiveTextAreaControl.TextArea.ScrollToCaret();
		}
		
		public void loadFile(string fileToLoad)
		{				
			PublicDI.log.info("Loading file into SourceCode Editor: {0}", fileToLoad);
			sourceCodeEditor.loadSourceCodeFile(fileToLoad);
		}
				
		
		public void updateView(string sourceCode)
		{	
			var ast = new Ast_CSharp(sourceCode);
			ast_TreeView.show_Ast(ast);
			types_TreeView.show_List(ast.astDetails.Types, "Text");
			usingDeclarations_TreeView.show_List(ast.astDetails.UsingDeclarations,"Text");
			methods_TreeView.show_List(ast.astDetails.Methods,"Text");
			fields_TreeView.show_List(ast.astDetails.Fields,"Text");
			properties_TreeView.show_List(ast.astDetails.Properties,"Text");
			comments_TreeView.show_List(ast.astDetails.Comments,"Text");
			
			rewritenCSharpCode_SourceCodeEditor.setDocumentContents(ast.astDetails.CSharpCode, ".cs");
			rewritenVBNet_SourceCodeEditor.setDocumentContents(ast.astDetails.VBNetCode, ".vb");		
			
			/*foreach(var line in StringsAndLists.fromTextGetLines(sourceCode))
				methods_TreeView.add_Node(line);*/
			
			/*astTreeView.clear();
			//var sourceCode = sourceCodeEditor.getSourceCode();
			var language = SupportedLanguage.CSharp;
			IParser parser = ParserFactory.CreateParser(language, new StringReader(sourceCode));
			parser.Parse();
			var unit = parser.CompilationUnit;
			astTreeView.add_Node(new CollectionNode("CompilationUnit", unit.Children));
			*/
					
/*			FieldDeclaration fd = new FieldDeclaration( null,
														new TypeReference("TypeRef.Test"),
			                                            Modifiers.Private);
			unit.Children.Insert(0,fd);
			
			UsingDeclaration ud = new UsingDeclaration("O2.Kernel.BBB");//,new TypeReference("AAAA.BBB"));
			unit.Children.Insert(0,ud);*/
			
	
			
			//IList<ISpecial> savedSpecialsList = parser.Lexer.SpecialTracker.RetrieveSpecials();
			
			//showDetailsOfSpecialsList(savedSpecialsList);
						
			//showRecreatedCode(unit,savedSpecialsList);
			//foreach(var line in StringsAndLists.fromTextGetLines(sourceCode))
			//	astTreeView.add_Node(line);
		}
    	    	
    	/*public void showDetailsOfSpecialsList(IList<ISpecial> savedSpecialsList)    	
    	{
    		foreach(var special in savedSpecialsList)
    		{
    			PublicDI.log.info("[{0} {1} -> {2}", special.GetType().Name, special.StartPosition, special.EndPosition);
    			if (special is Comment)
    				PublicDI.log.info("  CommentText = {0}", ((Comment)special).CommentText);
    		}
    	}
    	
    	public void showRecreatedCode(CompilationUnit unit, IList<ISpecial> savedSpecialsList)
    	{
    		var outputVisitor  = new CSharpOutputVisitor();    		
    		using (SpecialNodesInserter.Install(savedSpecialsList, outputVisitor)) {
				unit.AcceptVisitor(outputVisitor, null);
			}
			//codeTextBox.Text = outputVisitor.Text.Replace("\t", "  ");
    		var recreatedCode = outputVisitor.Text;
    		PublicDI.log.debug(recreatedCode);
    	}
		*/
		
	}
}
