// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using O2.Rnd.Views.Wpf.GuiHelpers;

namespace O2.wpfGUI.xaml_windows
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1
    {
        public Window1()
        {
            InitializeComponent();
            validateNewAppDomainText();
        }

        private void btOpenO2Gui_Click(object sender, RoutedEventArgs e)
        {
            //O2AppDomainFactory o2AppDomainFactory = AppDomainCentral.getO2CoreLib();
            //if (o2AppDomainFactory != null)
            //    o2AppDomainFactory.openO2Gui();           
        }

        private void btRefreshAppDomainList_Click(object sender, RoutedEventArgs e)
        {
            //O2Wpf.populateWpfListBoxWithO2AppDomainFactoryObjectsWithCurrentAppDomains(lbCurrentAppDomains);
            //currentAppDomains.Items.Add("New Item");
        }

        private void lbCurrentAppDomains_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
        }

        private void currentAppDomains_Drop(object sender, DragEventArgs e)
        {
        }

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //  if (e.NewValue > 20)
            Opacity = e.NewValue;
            //this.
        }

        private void lbCurrentAppDomains_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*         if (lbCurrentAppDomains.SelectedItem != null && lbCurrentAppDomains.SelectedItem is O2Proxy)
            {
                lbAssemblies.Items.Clear();
                var o2Proxy = (O2Proxy) lbCurrentAppDomains.SelectedItem;
                foreach (string assembly in o2Proxy.getAssemblies(false))
                    lbAssemblies.Items.Add(assembly);
            }*/
        }

        private void lbAssemblies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
/*            if (lbAssemblies.SelectedItem != null)
                try
                {
                    lbTypes.Items.Clear();
                    var o2Proxy = (O2Proxy) lbCurrentAppDomains.SelectedItem;

                    List<string> typesViaProxy = o2Proxy.getTypes(lbAssemblies.SelectedItem.ToString());
                    if (typesViaProxy.Count == 0)
                        lbTypes.Items.Add("There were no types retrived from the O2Proxy, check logs for errors");
                    else
                        foreach (string type in typesViaProxy)
                            lbTypes.Items.Add(type);
                }
                catch (Exception ex)
                {
                    lbTypes.Items.Add("error: " + ex.Message);
                }

            /*      if (lbAssemblies.SelectedItem != null && lbAssemblies.SelectedItem is Assembly)
            {
                foreach (var type in Reflection.getTypes((Assembly)lbAssemblies.SelectedItem))
                    lbTypes.Items.Add(type);
            }*/

        }

        private void lbTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*         if (lbTypes.SelectedItem != null)
                try
                {
                    lbStaticMethods.Items.Clear();
                    var o2Proxy = (O2Proxy) lbCurrentAppDomains.SelectedItem;

                    List<string> staticMethodsViaProxy = o2Proxy.getMethods(lbAssemblies.SelectedItem.ToString(),
                                                                            lbTypes.SelectedItem.ToString(),
                                                                            cbOnlyShowStaticMethods.IsChecked.Value,
                                                                            cbOnlyShowMethodsWithNoParameters.IsChecked.
                                                                                Value);
                    if (staticMethodsViaProxy.Count == 0)
                        lbStaticMethods.Items.Add(
                            "There were no static methods retrived from the O2Proxy, check logs for errors");
                    else
                        foreach (string type in staticMethodsViaProxy)
                            lbStaticMethods.Items.Add(type);
                }
                catch (Exception ex)
                {
                    lbStaticMethods.Items.Add("error: " + ex.Message);
                }


            /*if (lbTypes.SelectedItem != null && lbTypes.SelectedItem is Type)
            {
                foreach (var type in Reflection.getMethods((Type)lbTypes.SelectedItem))
                    lbStaticMethods.Items.Add(type);
            }*/
        }

        private void lbStaticMethods_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void btCreateAppDomain_Click(object sender, RoutedEventArgs e)
        {
            /*          var O2AppDomainFactory = new O2AppDomainFactory(tbNewAppDomainName.Text, tbNewAppDomainBaseDirectory.Text);
            O2AppDomainFactory.load("nunit.core", @"C:\Program Files\Nunit\bin\nunit.core.dll", true);
            O2AppDomainFactory.load("nunit.core.extensions", @"C:\Program Files\Nunit\bin\nunit.core.extensions.dll",
                                    true);
            O2AppDomainFactory.load("nunit.core.interfaces", @"C:\Program Files\Nunit\bin\nunit.core.interfaces.dll",
                                    true);
            O2AppDomainFactory.load("loadtest-assembly", @"C:\Program Files\Nunit\bin\loadtest-assembly.dll", true);
            btRefreshAppDomainList_Click(null, null);*/
        }

        private void tbNewAppDomainName_TextChanged(object sender, TextChangedEventArgs e)
        {
            /*          if (tbNewAppDomainBaseDirectory != null)
                tbNewAppDomainBaseDirectory.Text = Path.Combine(O2Proxy_Config.O2TempDir_AppDomainProxy,
                                                                tbNewAppDomainName.Text);
   * */
        }

        private void tbNewAppDomainBaseDirectory_TextChanged(object sender, TextChangedEventArgs e)
        {
            validateNewAppDomainText();
        }


        private void validateNewAppDomainText()
        {
            if (lbNewAppDomainErrorMessage != null)
            {
                lbNewAppDomainErrorMessage.Content = "";
                if (tbNewAppDomainName.Text == "")
                    lbNewAppDomainErrorMessage.Content += "Value required for AppDomain Name. " + Environment.NewLine;
                if (tbNewAppDomainBaseDirectory.Text == "")
                    lbNewAppDomainErrorMessage.Content += "Value required for BaseDirectory Name. " +
                                                          Environment.NewLine;
                //if (tbNewAppDomainBaseDirectory.Text != "" && !Directory.Exists(tbNewAppDomainBaseDirectory.Text))
                //    lbNewAppDomainErrorMessage.Content += "Directory Name provioded doesn't exist " + Environment.NewLine;

                lbNewAppDomainErrorMessage.Visibility = (lbNewAppDomainErrorMessage.Content.ToString() != "")
                                                            ? Visibility.Visible
                                                            : Visibility.Hidden;
                btCreateAppDomain.Visibility = (lbNewAppDomainErrorMessage.Content.ToString() == "")
                                                   ? Visibility.Visible
                                                   : Visibility.Hidden;
                //btCreateAppDomain. = 
            }
        }

        private void btInvokeStaticMethod_Click(object sender, RoutedEventArgs e)
        {
            /*         if (lbStaticMethods.SelectedItem != null)
            {
                var o2Proxy = (O2Proxy) lbCurrentAppDomains.SelectedItem;
                string assembly = lbAssemblies.SelectedItem.ToString();
                string type = lbTypes.SelectedItem.ToString();
                string method = lbStaticMethods.SelectedItem.ToString();
                tbInvocationResult.Text = string.Format(" ... invoking method : {0} {1} {2} ()", assembly, type, method);
                object result = o2Proxy.staticInvocation(assembly, type, method, new object[0]);
                if (result != null)
                    tbInvocationResult.Text = result.ToString();
                else
                    tbInvocationResult.Text += Environment.NewLine + "result = null";
            }
   */ 
        }
    }
}
