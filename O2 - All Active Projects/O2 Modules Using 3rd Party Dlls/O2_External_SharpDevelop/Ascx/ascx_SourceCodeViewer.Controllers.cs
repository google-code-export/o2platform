using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace O2.External.SharpDevelop.Ascx
{
    public partial class ascx_SourceCodeViewer
    {
        public ascx_SourceCodeEditor getSourceCodeEditor()
        {
            return sourceCodeEditor;
        }

        public void setDocumentContents(string documentContents)
        {
            sourceCodeEditor.setDocumentContents(documentContents,"aaa.cs");
        }
    }
}
