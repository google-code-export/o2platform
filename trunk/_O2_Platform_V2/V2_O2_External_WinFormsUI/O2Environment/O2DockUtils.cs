// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using FluentSharp.O2.DotNetWrappers.DotNet;
using FluentSharp.O2.DotNetWrappers.ExtensionMethods;
using V2.O2.External.WinFormsUI.Forms;
using FluentSharp.O2.Interfaces.Views;
using WeifenLuo.WinFormsUI.Docking;

namespace V2.O2.External.WinFormsUI.O2Environment
{
    public class O2DockUtils
    {
        public static bool setDockState(string name, O2DockState state)
        {
            return setDockState(name, getDockStateFromO2DockState(state));
        }

        public static bool setDockState(string name, DockState state)
        {
            if (DI.dO2LoadedO2DockContent.ContainsKey(name))
            {
                DI.dO2LoadedO2DockContent[name].dockContent.DockState = state;
                return true;
            }
            return false;
        }

        public static bool isControlAvailable(string name)
        {
            return DI.dO2LoadedO2DockContent.ContainsKey(name);
        }

        public static DockState getDockStateFromO2DockState(O2DockState o2DockState)
        {
            return (DockState) Enum.Parse(typeof (DockState), o2DockState.ToString());
        }

        public static void setDockContentState(string controlToConfigure, O2DockState o2DockState)
        {
            if (false == string.IsNullOrEmpty(controlToConfigure))
            {
                var o2DockContent = getO2DockContent(controlToConfigure);
                if (o2DockContent != null)
                    o2DockContent.dockContent.okThread(delegate
                                                           {
                                                               o2DockContent.dockContent.DockState =
                                                                   getDockStateFromO2DockState(o2DockState);
                                                           });
            }
        }

        public static void setDockContentPosition(string controlToConfigure)
        {
            {
                var o2DockContent = getO2DockContent(controlToConfigure);
                if (o2DockContent != null)
                {
                    o2DockContent.dockContent.Activate();
                }
            }
        }

        public static void addO2DockContentToDIGlobalVar(O2DockContent controlToLoad)
        {
            if (DI.dO2LoadedO2DockContent.ContainsKey(controlToLoad.name))
                DI.dO2LoadedO2DockContent[controlToLoad.name] = controlToLoad;
            else
            {
                DI.dO2LoadedO2DockContent.Add(controlToLoad.name, controlToLoad);                
                addControlToLoadedO2ModulesList(controlToLoad);
            }
        }

        private static void addControlToLoadedO2ModulesList(O2DockContent controlToLoad)
        {            
            if (DI.o2GuiStandAloneFormMode == false)
                if (DI.o2GuiWithDockPanel == null)
                    DI.log.error("in addControlToLoadedO2ModulesList , DI.o2GuiWithDockPanel == null");
                else
                    DI.o2GuiWithDockPanel.addControlToLoadedO2ModulesMenu(controlToLoad);
        }

        public static void removeO2DockContentFromDIGlobalVar(string controlToRemove)
        {            
            if (DI.dO2LoadedO2DockContent.ContainsKey(controlToRemove))
                DI.dO2LoadedO2DockContent.Remove(controlToRemove);
            else            
                DI.log.error("in removeO2DockContentFromDIGlobalVar, tried to remove a control that is not in DI.dO2LoadedO2DockContent: {0}", controlToRemove);            
        }

        public static O2DockContent getO2DockContent(string name)
        {
            if (DI.dO2LoadedO2DockContent.ContainsKey(name))
            {
                return DI.dO2LoadedO2DockContent[name];
            }
            return null;
        }
        
        public static Form getO2DockContentForm(string name)
        {
            if (DI.dO2LoadedO2DockContent.ContainsKey(name))
            {              
                return DI.dO2LoadedO2DockContent[name].dockContent;
            }
            return null;
        }

        public static Control getAscx(string name)
        {
            return getAscx(name, false /*logErrorOnFail*/);
        }

        public static Control getAscx(string name, bool logErrorOnFail)
        {
            if (DI.dO2LoadedO2DockContent.ContainsKey(name))
            {
                var o2DockContent = O2DockUtils.getO2DockContent(name);            
                o2DockContent.dockContent.invokeOnThread(() => o2DockContent.dockContent.Activate());
                return o2DockContent.control;
            }
            if (logErrorOnFail)
                DI.log.error("in O2DockUtils.getAscx, could not find registed Ascx: {0}", name);
            return null;
        }

        public static List<string> getListAvailableAscx()
        {
            var availabledAscx = new List<string>();
            availabledAscx.AddRange(DI.dO2LoadedO2DockContent.Keys);
            return availabledAscx;        
        }

        public static void setDocState(string parentControl, string childControl, DockStyle dockStyle)
        {
            if (DI.dO2LoadedO2DockContent.ContainsKey(parentControl) && DI.dO2LoadedO2DockContent.ContainsKey(childControl))
            {
                var parent = DI.dO2LoadedO2DockContent[parentControl].dockContent;
                var child = DI.dO2LoadedO2DockContent[childControl].dockContent;
                child.invokeOnThread(() =>
                                         {
                                             if (parent.Pane != null)
                                                 child.DockTo(parent.Pane, dockStyle, 0);
                                             else
                                                 DI.log.error("in O2DockUtils.setDocState, parent.Pane was null for parent: {0}", parent.Name ?? "[null value]");
                                         });
            }
            else
            {
                DI.log.error("in O2DockUtils.setDocState, could not find registed parent or child controls: {0} , {1}", parentControl, childControl);
            }
        }

        public static void setPaneHeight(string controlInPane, int newHeight)
        {
            if (DI.dO2LoadedO2DockContent.ContainsKey(controlInPane))
            {
                var targetPane = DI.dO2LoadedO2DockContent[controlInPane].dockContent.Pane;     
           
                targetPane.invokeOnThread(() =>
                                              {
                                                  //DI.dO2LoadedO2DockContent[controlInPane].dockContent.Height = 300;
                                                  /*targetPane.SetDockState(DockState.DockBottom);
                                                  var nestedPanes = targetPane.DockWindow.NestedPanes;
                                                  targetPane.DockWindow.NestedPanes[0].Height = 300;
                                                  targetPane.DockWindow.NestedPanes[1].Height = 300;
                                                  /*var height = targetPane.DockWindow.Height;
                                                  targetPane.DockWindow.Height = 300;
                                                  height = targetPane.DockWindow.Height;
                                                  //targetPane.DockWindow.Height = newHeight;
                                                //  targetPane.Float();
                                                  //targetPane.ParentForm.Height = newHeight + 500;  // this works
                                                  //targetPane.DockPanel.Height = 300;
                                                  //DI.dO2LoadedO2DockContent[controlInPane].dockContent.DockPanel.Height = 300;
                                                  */
                                              });
            }
            else
            {
                DI.log.error("in O2DockUtils.setPaneHeight, could not find the controlInPane{0} , {1}", controlInPane);
            }
            
        }

        public static void addMenuItemWithOnClickEvent(string menuItemName, O2Thread.FuncVoid onMenuItemClick)
        {
            
        }
      
    }
}
