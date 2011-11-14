
using FluentSharp.O2.DotNetWrappers.DotNet;

namespace V2.O2.External.SharpDevelop.Ascx
{
    public partial class ascx_SourceCodeViewer
    {
        public ascx_SourceCodeEditor getSourceCodeEditor()
        {
            return sourceCodeEditor;
        }

        public void setDocumentContents(string documentContents)
        {
            O2Thread.mtaThread(() => sourceCodeEditor.setDocumentContents(documentContents, "xyz.cs"));
        }

        public void setDocumentContents(string documentContents, string file)
        {
            // ToCheckOutLater: I don't really understand why I need to run this of a different thread 
            // (but when developing ascx_O2_Command_Line there was a case where I had an
            // "In setDocumentContents: GapTextBufferStategy is not thread-safe!"
            // error
            O2Thread.mtaThread(() => sourceCodeEditor.setDocumentContents(documentContents, file));
        }
    }
}
