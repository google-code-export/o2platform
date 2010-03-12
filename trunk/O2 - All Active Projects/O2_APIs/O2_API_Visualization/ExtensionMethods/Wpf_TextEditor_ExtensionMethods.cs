using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.AvalonEdit;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using O2.API.Visualization.Ascx;
using GraphSharp.Controls;
namespace O2.API.Visualization.ExtensionMethods
{
    public static class WPF_TextEditor_ExtensionMethods
    {
        public static TextEditor add_WpfTextEditor(this System.Windows.Forms.Control control)
        {
            return control.add_WPF_Control<TextEditor>();
        }

        public static TextEditor add_WpfTextEditor(this ContentControl control)
        {
            return control.add_Control<TextEditor>();            
        }

        public static TextEditor add_WpfTextEditor(this ElementHost control)
        {
            return control.add_Control<TextEditor>();
        }

        public static TextEditor add_WpfTextEditor(this ascx_Xaml_Host control)
        {
            return control.add_Control<TextEditor>();
        }

        public static TextEditor add_WpfTextEditor(this GraphLayout control)
        {
            return control.add<TextEditor>();
        }

        public static TextEditor set_text(this TextEditor textEditor, string text)
        { 
            return (TextEditor)textEditor.wpfInvoke(
                ()=>{
                        textEditor.Text = text;
                        return textEditor;
                });
        }
    }
}
