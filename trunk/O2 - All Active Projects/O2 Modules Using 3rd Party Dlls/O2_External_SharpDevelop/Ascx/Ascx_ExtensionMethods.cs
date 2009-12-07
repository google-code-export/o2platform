using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.TextEditor.Document;

namespace O2.External.SharpDevelop.Ascx
{
    public static class Ascx_ExtensionMethods
    {
        public static ascx_SourceCodeEditor addSourceCodeEditor(this GroupBox groupBox)
        {
            var sourceCodeEditor = new ascx_SourceCodeEditor();
            sourceCodeEditor.getObject_TextEditorControl().Document.FormattingStrategy = new DefaultFormattingStrategy();
            sourceCodeEditor.Dock = DockStyle.Fill;
            groupBox.Controls.Add(sourceCodeEditor);
            return sourceCodeEditor;
        }
    }
}
