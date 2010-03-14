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
using O2.API.Visualization.Xaml;
using O2.API.Visualization.ExtensionMethods;

namespace O2.API.Visualization.ExtensionMethods
{
    public static class WPF_TextEditor_ExtensionMethods
    {

        #region add_WpfTextEditor

        public static WpfTextEditor add_WpfTextEditor(this System.Windows.Forms.Control control)
        {
            return control.add_WPF_Control<WpfTextEditor>();
        }

        public static WpfTextEditor add_WpfTextEditor(this ContentControl control)
        {
            return control.add_Control<WpfTextEditor>();
        }

        public static WpfTextEditor add_WpfTextEditor(this ElementHost control)
        {
            return control.add_Control<WpfTextEditor>();
        }

        public static WpfTextEditor add_WpfTextEditor(this ascx_Xaml_Host control)
        {
            return control.add_Control<WpfTextEditor>();
        }

        public static WpfTextEditor add_WpfTextEditor(this GraphLayout control)
        {
            return control.add<WpfTextEditor>();
        }

        #endregion

        #region setters

        public static WpfTextEditor set_Text(this TextEditor textEditor, string text)
        {
            return (WpfTextEditor)textEditor.wpfInvoke(
                () =>
                {
                    textEditor.Text = text;
                    return textEditor;
                });
        }

        #endregion

        #region gui options

        public static WpfTextEditor html(this WpfTextEditor wpfTextEditor)
        {
            wpfTextEditor.SyntaxHighlighting = "Html";
            return wpfTextEditor;
        }

        public static WpfTextEditor csharp(this WpfTextEditor wpfTextEditor)
        {
            wpfTextEditor.SyntaxHighlighting = "C#";
            return wpfTextEditor;
        }

        #endregion
    }
}
