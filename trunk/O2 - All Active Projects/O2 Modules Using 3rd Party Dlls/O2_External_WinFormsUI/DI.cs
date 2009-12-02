using System;
using System.Collections.Generic;
using O2.External.WinFormsUI.Forms;
using O2.External.WinFormsUI.O2Environment;
using O2.Kernel;
using O2.Kernel.Interfaces.Messages;
using O2.Kernel.Interfaces.O2Core;
using O2.Kernel.InterfacesBaseImpl;

namespace O2.External.WinFormsUI
{
    internal class DI
    {
        static DI()
        {
            log = PublicDI.log; // _note that when the O2GuiWithDockPanel is create it will overide the PublicDI log with WinFormsUILog() object            
            reflection = PublicDI.reflection;
            config = PublicDI.config;
            o2MessageQueue = KO2MessageQueue.getO2KernelQueue();
            
            new O2MessagesHandler(); // set up O2Message hook

            sDefaultFileName_ReportBug_LogView = "ReportBug_LogView.Rtf";
            sDefaultFileName_ReportBug_ScreenShotImage = "ReportBug_ScreenShotImage.bmp";
            sEmailDefaultTextFromO2Gui = "enter message here";
            sEmailHost = "mail.ouncelabs.com";
            sEmailToSendBugReportsTo = "dinis.cruz@ouncelabs.com";
            sO2Website = "https://ounceopen.squarespace.com";
            LogViewerControlName = "O2 Logs";
            autoAddLogViewerToGui = true;
        }
        
        // DI objects
        public static KO2Config config { get; set;}        
        public static IO2MessageQueue o2MessageQueue { get; set; }
        public static IO2Log log;
        public static IReflection reflection;

        // local global vars
        public static Dictionary<String, O2DockContent> dO2LoadedO2DockContent  = new Dictionary<String, O2DockContent>();
        public static O2GuiWithDockPanel o2GuiWithDockPanel;
        public static bool o2GuiStandAloneFormMode;


        public static string sDefaultFileName_ReportBug_LogView { get; set; }
        public static string sDefaultFileName_ReportBug_ScreenShotImage { get; set; }
        public static string sEmailDefaultTextFromO2Gui { get; set; }
        public static string sEmailHost { get; set; }
        public static string sEmailToSendBugReportsTo { get; set; }
        public static string sO2Website { get; set; }

        public static string LogViewerControlName { get; set; }

        public static bool autoAddLogViewerToGui { get; set; }
     
    }
}