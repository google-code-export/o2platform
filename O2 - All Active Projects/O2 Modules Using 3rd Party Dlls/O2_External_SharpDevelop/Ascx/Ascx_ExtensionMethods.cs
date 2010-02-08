using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.TextEditor.Document;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;

namespace O2.External.SharpDevelop.Ascx
{
    public static class Ascx_ExtensionMethods
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
    }
}
