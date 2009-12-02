using System;
using O2.Kernel.CodeUtils;
using O2.Kernel.Interfaces.Views;

namespace O2.Kernel.Interfaces.Messages
{
    public enum IM_GUIActions 
    {
        isAscxGuiAvailable,
        openControlInGui,
        getGuiAscx,
        executeOnAscx,
        closeAscxParent,
        setAscxDockStateAndOpenIfNotAvailable
    }
    public interface IM_GUIAction : IO2Message
    {
        IM_GUIActions GuiAction { get; set; }
        Type controlType { get; set; }
        string controlTypeString { get; set; }        
        O2DockState o2DockState { get; set; }
        string controlName { get; set; }
        string targetMethod { get; set; }
        string[] methodParameters { get; set; }         // forcing the parameters to be strings so they work ok across WCF
        Callbacks.dMethod_Object returnDataCallback { get; set; }
    }
}
