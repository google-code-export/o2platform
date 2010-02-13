
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
            sourceCodeEditor.setDocumentContents(documentContents,"xyz.cs");
        }

        public void setDocumentContents(string documentContents, string file)
        {
            sourceCodeEditor.setDocumentContents(documentContents, file);
        }
    }
}
