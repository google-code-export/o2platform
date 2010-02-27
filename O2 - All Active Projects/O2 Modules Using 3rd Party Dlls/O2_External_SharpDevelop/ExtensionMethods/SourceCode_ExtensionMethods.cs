using System;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.ExtensionMethods;
using O2.External.SharpDevelop.Ascx;
using O2.External.SharpDevelop.AST;
using O2.Kernel;

namespace O2.External.SharpDevelop.ExtensionMethods
{
    public static class SourceCode_ExtensionMethods
    {
        public static ascx_SourceCodeEditor add_SourceCodeEditor(this GroupBox groupBox)
        {
            return (ascx_SourceCodeEditor) groupBox.invokeOnThread(
                                               () =>
                                                   {
                                                       var sourceCodeEditor = new ascx_SourceCodeEditor();
                                                       sourceCodeEditor.getObject_TextEditorControl().Document.
                                                           FormattingStrategy =
                                                           new DefaultFormattingStrategy();
                                                       sourceCodeEditor.Dock = DockStyle.Fill;
                                                       groupBox.Controls.Add(sourceCodeEditor);
                                                       return sourceCodeEditor;
                                                   });
        }

        public static ascx_SourceCodeEditor add_SourceCodeEditor(this Control control)
        {
            return (ascx_SourceCodeEditor)control.invokeOnThread(
                                              () =>
                                                  {
                                                      var sourceCodeEditor = new ascx_SourceCodeEditor();
                                                      sourceCodeEditor.getObject_TextEditorControl().Document.
                                                          FormattingStrategy = new DefaultFormattingStrategy();
                                                      sourceCodeEditor.Dock = DockStyle.Fill;
                                                      control.Controls.Add(sourceCodeEditor);
                                                      return sourceCodeEditor;
                                                  });
        }

        public static ascx_SourceCodeViewer add_SourceCodeViewer(this Control control)
        {
            return (ascx_SourceCodeViewer)control.add_Control(typeof(ascx_SourceCodeViewer));
        }

        public static TextEditorControl textEditorControl(this ascx_SourceCodeEditor sourceCodeEditor)
        {
            return sourceCodeEditor.getObject_TextEditorControl();
        }

        public static TextEditorControl textEditorControl(this ascx_SourceCodeViewer sourceCodeViewer)
        {
            return sourceCodeViewer.editor().getObject_TextEditorControl();
        }

        public static int caretLine(this ascx_SourceCodeEditor sourceCodeEditor)
        {
            return sourceCodeEditor.textEditorControl().ActiveTextAreaControl.Caret.Line;
        }

        public static void colorCodeForExtension(this ascx_SourceCodeEditor sourceCodeEditor, string extension)
        {
            var tecSourceCode = sourceCodeEditor.textEditorControl();
            var dummyFileName = string.Format("aaa.{0}", extension);
            tecSourceCode.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategyForFile(dummyFileName);
        }

        public static void showAstValueInSourceCode(this TextEditorControl textEditorControl, AstValue astValue)
        {

            PublicDI.log.error("{0} {1} - {2}", astValue.Text, astValue.StartLocation, astValue.EndLocation);
            
            var start = new TextLocation(astValue.StartLocation.X - 1,
                                                                astValue.StartLocation.Y - 1);
            var end = new TextLocation(astValue.EndLocation.X - 1, astValue.EndLocation.Y - 1);
            var selection = new DefaultSelection(textEditorControl.Document, start, end);
            textEditorControl.ActiveTextAreaControl.SelectionManager.SetSelection(selection);
            setCaretToCurrentSelection(textEditorControl);

        }

        public static void setCaretToCurrentSelection(this TextEditorControl textEditorControl)
        {
            var finalCaretPosition = textEditorControl.ActiveTextAreaControl.TextArea.SelectionManager.SelectionCollection[0].StartPosition;
            var tempCaretPosition = new TextLocation
            {
                X = finalCaretPosition.X,
                Y = finalCaretPosition.Y + 10
            };
            textEditorControl.ActiveTextAreaControl.Caret.Position = tempCaretPosition;
            textEditorControl.ActiveTextAreaControl.TextArea.ScrollToCaret();
            textEditorControl.ActiveTextAreaControl.Caret.Position = finalCaretPosition;
            textEditorControl.ActiveTextAreaControl.TextArea.ScrollToCaret();
        }

        public static ascx_SourceCodeViewer set_Text(this ascx_SourceCodeViewer sourceCodeViewer, string text)
        {
             // ToCheckOutLater: I don't really understand why I need to run this of a different thread 
            // (but when developing ascx_O2_Command_Line there was a case where I had an
            // "In setDocumentContents: GapTextBufferStategy is not thread-safe!"
            // error
            return (ascx_SourceCodeViewer)sourceCodeViewer.invokeOnThread(           
                () =>
                    {
                        sourceCodeViewer.setDocumentContents(text);
                        return sourceCodeViewer;
                    });
            
        }

        public static string get_Text(this ascx_SourceCodeViewer sourceCodeViewer)
        {
            return sourceCodeViewer.editor().getSourceCode();
        }

        public static void onTextChanged(this ascx_SourceCodeViewer sourceCodeViewer, Action<string> textChanged)
        {
            sourceCodeViewer.editor().eDocumentDataChanged += textChanged;
        }

        public static ascx_SourceCodeEditor editor(this ascx_SourceCodeViewer sourceCodeViewer)
        {
            return sourceCodeViewer.getSourceCodeEditor();
        }

        public static void allowCompile(this ascx_SourceCodeViewer sourceCodeViewer, bool value)
        {
            sourceCodeViewer.editor().allowCodeCompilation = value;
        }

        public static void astDetails(this ascx_SourceCodeViewer sourceCodeViewer, bool value)
        {
            sourceCodeViewer.editor()._ShowSearchAndAstDetails = value;
        }

        public static void vScroolBar_Enabled(this ascx_SourceCodeViewer sourceCodeViewer, bool value)
        {
            sourceCodeViewer.editor().getObject_TextEditorControl().ActiveTextAreaControl.VScrollBar.Enabled = value;
        }

        public static void hScroolBar_Enabled(this ascx_SourceCodeViewer sourceCodeViewer, bool value)
        {
            sourceCodeViewer.editor().getObject_TextEditorControl().ActiveTextAreaControl.HScrollBar.Enabled = value;
        }

        public static void set_ColorsForCSharp(this ascx_SourceCodeViewer sourceCodeViewer)
        {
            sourceCodeViewer.editor().setDocumentHighlightingStrategy("aa.cs");
        }

        public static IDocument document(this ascx_SourceCodeEditor sourceCodeEditor)
        {
            return sourceCodeEditor.getObject_TextEditorControl().Document;
        }

        public static IDocument document(this ascx_SourceCodeViewer sourceCodeViewer)
        {
            return sourceCodeViewer.editor().document();
        }

        public static void set_ColorsForCSharp(this ascx_SourceCodeEditor sourceCodeEditor)
        {
            sourceCodeEditor.setDocumentHighlightingStrategy("aa.cs");
        }

        public static void open(this ascx_SourceCodeEditor sourceCodeEditor, string fileToOpen)
        {
            sourceCodeEditor.loadSourceCodeFile(fileToOpen);
        }

        public static void open(this ascx_SourceCodeViewer sourceCodeViewer, string fileToOpen)
        {

            sourceCodeViewer.editor().loadSourceCodeFile(fileToOpen);
        }       
    }
}