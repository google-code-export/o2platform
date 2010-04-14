// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using O2.Interfaces.O2Core;
using O2.Kernel;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Views.ASCX;
using O2.Views.ASCX.ExtensionMethods;
using O2.Views.ASCX.classes.MainGUI;

namespace O2.Script
{
    public class Rohit_GUI
    {    
    	private static IO2Log log = PublicDI.log;

		public O2Gui o2Gui {get;set;}
		
        public Rohit_GUI()
    	{    	    	
    	}
    	
    	public void openGUI()
    	{
    		o2Gui = createRohitGui();
    		o2Gui.show();
    	}
    	
    	public static O2Gui createRohitGui()
    	{
    		var rohitGui = new O2Gui(800, 500);
    		addMenu(rohitGui);
    		return rohitGui;	
    	}	    	 
    	
    	public static void addMenu(O2Gui targetGui)
    	{
    		var menu = targetGui.add_ContextMenu();
    		
    		// create top level menus
            var fileMenu = menu.add_MenuItem("File");
            var editMenu = menu.add_MenuItem("Edit");
            var wizardsMenu = menu.add_MenuItem("Wizards");
            var modulesMenu = menu.add_MenuItem("Modules");
            var helpMenu = menu.add_MenuItem("Help");
    		
    		// fileMenu menu items
    		fileMenu.add_MenuItem("New");
    		fileMenu.add_MenuItem("Open");
    		fileMenu.add_MenuItem("Save");
    		fileMenu.add_MenuItem("Import");
    		fileMenu.add_MenuItem("Exit");
    		
    		// editMenu menu items
    		editMenu.add_MenuItem("Cut");
    		editMenu.add_MenuItem("Copy");
    		editMenu.add_MenuItem("Paste");
    		editMenu.add_MenuItem("Configuration");    		
    		
    		// wizardsMenu menu items
    		fileMenu.add_MenuItem("XRules database - Backup existing rules");
    		fileMenu.add_MenuItem("XRules database - Sync via SVN");    		    	
    		
    		
    		// modulesMenu menu items
    		modulesMenu.add_MenuItem("Search");    		
    		modulesMenu.add_MenuItem("Log Viewer");
    		modulesMenu.add_MenuItem("Join Traces");
    		var findingsViewersMenu = modulesMenu.add_MenuItem("Findings Viewers");
    		var intermediateRepresentationMenu = modulesMenu.add_MenuItem("Intermediate Representation Viewers");
    		var scriptEditorsMenu = modulesMenu.add_MenuItem("Script Editors");
    		var technologySpringMVC = modulesMenu.add_MenuItem("Technology - Spring MVC");
    		var technologyStruts = modulesMenu.add_MenuItem("Technology - Struts");
    		var commercialToolAppScanSourceMenu = modulesMenu.add_MenuItem("Commercial Tool - AppScan Source (OunceLabs)");
    		var freeToolMicrosoftCatNetMenu = modulesMenu.add_MenuItem("Free Tool - Microsoft Cat.NET");
    		var otherControlsMenu = modulesMenu.add_MenuItem("Other Controls");
    		
    		// modulesMenu menu items items
    		findingsViewersMenu.add_MenuItem("Findings Viewer");
    		findingsViewersMenu.add_MenuItem("Findings Query");
    		findingsViewersMenu.add_MenuItem("Search Assessment Run");
    		findingsViewersMenu.add_MenuItem("View Assessment Run");
    		
    		intermediateRepresentationMenu.add_MenuItem("Cir Viewer");
    		
    		commercialToolAppScanSourceMenu.add_MenuItem("Rules Manager");
    		commercialToolAppScanSourceMenu.add_MenuItem("Scanner - Simple");
    		commercialToolAppScanSourceMenu.add_MenuItem("Scanner - Advanced");
    		
    		freeToolMicrosoftCatNetMenu.add_MenuItem("MSCatNet Scanner");
    		
    		otherControlsMenu.add_MenuItem("Browse SVN");
    		// helpMenu menu items    		    		
    	}
    }
}
