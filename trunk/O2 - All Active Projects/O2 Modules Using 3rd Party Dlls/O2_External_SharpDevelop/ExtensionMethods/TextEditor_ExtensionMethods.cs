using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.TextEditor;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.ExtensionMethods;

namespace O2.External.SharpDevelop.ExtensionMethods
{
    public static class TextEditor_ExtensionMethods
    {
        public static TextEditorControl open(this TextEditorControl textEditorControl, string sourceCodeFile)
        {
            return (TextEditorControl)textEditorControl.invokeOnThread(
                () =>
                {
                    if (sourceCodeFile.fileExists())
                        textEditorControl.LoadFile(sourceCodeFile);
                    else
                    {
                        textEditorControl.SetHighlighting("C#");
                        textEditorControl.Document.TextContent = sourceCodeFile;
                    }
                    return textEditorControl;
                });
        }
        public static TextArea textArea(this TextEditorControl textEditorControl)
        {
            return textEditorControl.ActiveTextAreaControl.TextArea;
        }

        public static string get_Text(this TextEditorControl textEditorControl)
        {            
            return (string)textEditorControl.textArea().invokeOnThread(() => textEditorControl.textArea().Text);
        }
    }
}
