using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.Integration;
using System.Windows;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using System.Windows.Markup;
using System.Windows.Controls;

namespace O2.API.Visualization.ExtensionMethods
{
    public static class Xaml_ExtensionMethods
    {
        public static ElementHost add_XamlViewer(this System.Windows.Forms.Control control)
        {
            return control.add_WPF_Host();
        }

        public static UIElement showXaml(this ElementHost elementHost, string xamlCode)
        {
            return (UIElement)elementHost.invokeOnThread(
                () =>
                {
                    try
                    {
                        if (xamlCode.valid())
                        {
                            ParserContext parserContext = new ParserContext();
                            parserContext.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
                            parserContext.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
                            UIElement childElement = null;
                            var xamlObject = XamlReader.Parse(xamlCode, parserContext);
                            if (xamlObject is UIElement)
                            {
                                var frame = new Frame();
                                frame.Content = (UIElement)xamlObject;
                                childElement = frame;
                                elementHost.Child = childElement;
                                "Xaml code loaded into ElementHost".info();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        "in showXaml: {0}".format(ex.Message).error();		// trying to display the InnerException was trowing an error
                        //ex.log();
                    }
                    return null;
                });
        }
    }
}
