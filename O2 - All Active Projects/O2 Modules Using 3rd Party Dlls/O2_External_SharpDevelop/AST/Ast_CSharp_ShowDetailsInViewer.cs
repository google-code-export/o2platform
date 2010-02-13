using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.DotNetWrappers.Windows;
using O2.External.SharpDevelop.Ascx;
using O2.Kernel.ExtensionMethods;

namespace O2.External.SharpDevelop.AST
{
    public class Ast_CSharp_ShowDetailsInViewer
    {
        private TabControl tabControl;
        private ascx_SourceCodeEditor sourceCodeEditor;

        public Ast_CSharp_ShowDetailsInViewer(ascx_SourceCodeEditor _sourceCodeEditor)
        {
            sourceCodeEditor = _sourceCodeEditor;            
            tabControl = (TabControl)sourceCodeEditor.field("tcSourceInfo");
        }

        public void populateTabControl()
        {
            /*
            ast_TreeView = tabControl.add_Tab("AST").add_TreeView();
            usingDeclarations_TreeView = tabControl.add_Tab("Using Declarations").add_TreeView();
            types_TreeView = tabControl.add_Tab("Types").add_TreeView();
            methods_TreeView = tabControl.add_Tab("Methods").add_TreeView();
            fields_TreeView = tabControl.add_Tab("Fields").add_TreeView();
            properties_TreeView = tabControl.add_Tab("Properties").add_TreeView();
            comments_TreeView = tabControl.add_Tab("Comments").add_TreeView();
            */ 
        }
    }
}
