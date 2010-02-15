using System.Windows.Forms;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
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

        public static TextEditorControl control(this ascx_SourceCodeEditor sourceCodeEditor)
        {
            return sourceCodeEditor.getObject_TextEditorControl();
        }

        public static int caretLine(this ascx_SourceCodeEditor sourceCodeEditor)
        {
            return sourceCodeEditor.control().ActiveTextAreaControl.Caret.Line;
        }

        public static void colorCodeForExtension(this ascx_SourceCodeEditor sourceCodeEditor, string extension)
        {
            var tecSourceCode = sourceCodeEditor.control();
            var dummyFileName = string.Format("aaa.{0}", extension);
            tecSourceCode.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategyForFile(dummyFileName);
        }

        public static void showAstValueInSourceCode(this TextEditorControl textEditorControl, AstValue astValue)
        {

            PublicDI.log.error("{0} {1} - {2}", astValue.Text, astValue.StartLocation, astValue.EndLocation);
            
            var start = new ICSharpCode.TextEditor.TextLocation(astValue.StartLocation.X - 1,
                                                                astValue.StartLocation.Y - 1);
            var end = new ICSharpCode.TextEditor.TextLocation(astValue.EndLocation.X - 1, astValue.EndLocation.Y - 1);
            var selection = new ICSharpCode.TextEditor.Document.DefaultSelection(textEditorControl.Document, start, end);
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
    }
}