using System;
using O2.Kernel.CodeUtils;
using O2.Kernel.Interfaces.Messages;
using O2.Kernel.Interfaces.Views;

namespace O2.Kernel.InterfacesBaseImpl
{
    //[Serializable]
    class KM_GUIAction : KO2Message, IM_GUIAction
    {
        public IM_GUIActions GuiAction { get; set; }
        public Type controlType { get; set; }
        public string controlTypeString { get; set; }
        public O2DockState o2DockState { get; set; }
        public string controlName { get; set; }
        public string targetMethod { get; set; }
        public string[] methodParameters { get; set; }

        public Callbacks.dMethod_Object returnDataCallback { get; set; }


        public static KM_GUIAction openControlInGui(Type _controlType)
        {
            return openControlInGui(_controlType, O2DockState.Float);
        }
        public static KM_GUIAction openControlInGui(Type _controlType, O2DockState _o2DockState)
        {
            return openControlInGui(_controlType, _o2DockState, _controlType.Name);
        }

        public static KM_GUIAction openControlInGui(Type _controlType, O2DockState _o2DockState, string _controlName)
        {
            var kmGuiAction = new KM_GUIAction
                                  {
                                      GuiAction = IM_GUIActions.openControlInGui,
                                      messageGUID = new Guid(),
                                      messageText = "KM_OpenControlInGUI",
                                      controlType = _controlType,
                                      o2DockState = _o2DockState,
                                      controlName = _controlName
                                  };
            return kmGuiAction;
        }

        public static KM_GUIAction getGuiAscx(string ascxToGet, Callbacks.dMethod_Object _returnDataCallback)
        {
            var kmGuiAction = new KM_GUIAction
                                  {
                                      GuiAction = IM_GUIActions.getGuiAscx,
                                      controlName = ascxToGet,
                                        returnDataCallback = _returnDataCallback
                                  };
            return kmGuiAction;
        }

        public static KM_GUIAction isAscxGuiAvailable()
        {
            var kmGuiAction = new KM_GUIAction
            {
                GuiAction = IM_GUIActions.isAscxGuiAvailable                
            };
            return kmGuiAction;            
        }

        internal static KM_GUIAction executeOnAscx(string ascxToExecuteMethod, string targetMethod, string[] methodParameters)
        {
            var kmGuiAction = new KM_GUIAction
            {
                GuiAction = IM_GUIActions.executeOnAscx,
                controlName = ascxToExecuteMethod,
                targetMethod = targetMethod,
                methodParameters = methodParameters
            };
            return kmGuiAction;  
        }

        internal static KM_GUIAction closeAscxParent(string targetAscxControl)
        {
            var kmGuiAction = new KM_GUIAction
            {
                GuiAction = IM_GUIActions.closeAscxParent,
                controlName = targetAscxControl,                
            };
            return kmGuiAction;  
        }

        internal static KM_GUIAction setAscxDockStateAndOpenIfNotAvailable(string typeOfControl, O2DockState _o2DockState, string _controlName)
        {
            var kmGuiAction = new KM_GUIAction
                                  {
                                      GuiAction = IM_GUIActions.setAscxDockStateAndOpenIfNotAvailable,
                                      controlTypeString = typeOfControl,
                                      o2DockState = _o2DockState,
                                      controlName = _controlName
            };
            return kmGuiAction;  
        }
    }
}