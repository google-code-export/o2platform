// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Reflection;
using System.Text;
using O2.Interfaces.O2Core;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.DotNet;
using O2.Views.ASCX;
using O2.Views.ASCX.classes.MainGUI;
using O2.External.SharpDevelop.AST;
using O2.External.SharpDevelop.ExtensionMethods; 
using O2.XRules.Database._Rules._Interfaces;
using WatiN.Core;
using SHDocVw;
 
//O2Ref:WatiN.Core.1x.dll
//O2Ref:Interop.SHDocVw.dll
//O2File:WatiN_IE.cs
//O2File:..\..\Utils\_Misc_UI_Controls\ascx_CaptchaQuestion.cs
//O2File:..\..\Utils\_Misc_UI_Controls\ascx_AskUserForLoginDetails.cs
 
namespace O2.Script
{
    public static class WatiN_IE_ExtensionMethods
    {    
    	//WatIN ExtensionMethods
 
    	public static WatiN_IE ie(this string url)
    	{
    		int top = 0;
    		int left = 900;    		
    		return url.ie(top, left);
    	}
 
    	public static WatiN_IE ie(this string url, int top, int left)
    	{
    		int width = 385;
    		int height = 500;
    		return url.ie(top, left, width, height);
    	}
 
    	public static WatiN_IE ie(this string url, int top, int left, int width, int height)
    	{    		
    		var ie = new WatiN_IE();
    		ie.createIEObject(url, top, left, width, height);
			return ie;						
    	}
 
    	public static WatiN_IE ie(this O2.External.IE.Wrapper.O2BrowserIE o2BrowserIE)
 		{ 			
			return (o2BrowserIE as System.Windows.Forms.WebBrowser).ie();
		}
 
		public static WatiN_IE ie(this System.Windows.Forms.WebBrowser webBrowser)
 		{ 			
			return new WatiN_IE(webBrowser);						
		}
 
    	// uri & url
 
    	public static Uri uri(this WatiN_IE watinIe)
    	{
    		return watinIe.IE.Uri;
    	}
 
 
    	public static string url(this WatiN_IE watinIe)
    	{
    		return watinIe.uri().str();
    	}
 
    	public static string title(this WatiN_IE watinIe)
    	{
    		return watinIe.IE.Title;
    	}
 
    	public static bool title(this WatiN_IE watinIe, string title)
    	{
    		return watinIe.IE.Title == title;
    	}
 
    	public static string processId(this WatiN_IE watinIe)
    	{
    		return watinIe.IE.ProcessID.str();
    	}
    	// region close 
 
    	public static WatiN_IE close(this WatiN_IE watinIe)
    	{
    		"closing WatIN InternetExplorer Process".info();
    		watinIe.close();
    		//watinIe.Close();
    		return watinIe;
    	}
 
    	public static WatiN_IE closeInNSeconds(this WatiN_IE watinIe, int seconds)
    	{
    		if (seconds > 60)
    		{
    			"in WatiN_IE closeInNSeconds, provided value bigger than 60 secs, so changing the delay (before close) to 60".error();
    			seconds = 60;
    		}
    		"IE instance will be closed in {0} seconds".info(seconds);
    		O2Thread.mtaThread( 
			()=>{ 
					watinIe.wait(5000);  
					watinIe.close();   
				}); 
			return watinIe;
    	}
 
 
    	// internet explorer
 
    	public static InternetExplorerClass internetExplorer(this WatiN_IE watinIe)
    	{
    		return watinIe.InternetExplorer;
    	}
 
		public static WatiN_IE open(this WatiN_IE watinIe, string url)
		{
			return watinIe.open(url,0);
		}
 
		public static WatiN_IE open(this WatiN_IE watinIe, string url, int miliseconds)
    	{
    		"[WatIN] open: {0}".info(url);
    		watinIe.execute(
    			()=>{
    					watinIe.IE.GoTo(url);
    					watinIe.wait(miliseconds);
    				});
    		return watinIe;
    	}
 
    	public static WatiN_IE wait(this WatiN_IE watinIe)
    	{
    		return watinIe.wait(1000);
    	}
 
    	public static WatiN_IE wait(this WatiN_IE watinIe, int miliseconds)
    	{
    		if (miliseconds > 0)
    			watinIe.sleep(miliseconds);
    		return watinIe;
    	}
 
    	public static WatiN_IE waitNSeconds(this WatiN_IE watinIe, int seconds)
    	{
    		if (seconds > 0)
    			watinIe.sleep(seconds* 1000);
    		return watinIe;
    	}
 
 
    	public static T wait<T>(this T element, int miliseconds)
    		where T : Element
    	{
    		if (miliseconds > 0)
    			element.sleep(miliseconds);
    		return element;
    	}
 
 
    	// WatiN Image Extension Methods
 
    	public static WatiN.Core.Image image(this WatiN_IE watinIe, string name)
    	{
    		foreach(var image in watinIe.images())
    			if (image.id() == name)//|| link.text() == name)
    				return image;
    		"in WatiN_IE could not find Image with name:{0}".error(name ?? "[null value]");
    		return null;    				
    	}
 
    	public static List<WatiN.Core.Image> images(this WatiN_IE watinIe)
    	{
    		return (from image in watinIe.IE.Images
    				select image).toList();
    	}
 
    	public static Uri uri(this WatiN.Core.Image image)
    	{
			return (image != null)
					? image.Uri
					: null;
    	}
 
    	public static string url(this WatiN.Core.Image image)
    	{
			return (image != null)
					? image.Uri.str()
					: "";
    	}
 
    	public static string src(this WatiN.Core.Image image)
    	{
			return (image != null)
					? image.Src
					: "";
    	}
 
    	// WatiN Link Extension methods    	
 
    	public static Link link(this WatiN_IE watinIe, string name)
    	{
    		foreach(var link in watinIe.links())
    			if (link.id() == name || link.text() == name)
    				return link;
    		"in WatiN_IE could not find Link with name:{0}".error(name ?? "[null value]");
    		return null;    				
    	}
 
    	public static List<Link> links(this WatiN_IE watinIe)
    	{
    		return (from link in watinIe.IE.Links
    				select link).toList();
    	}
 
    	public static string url(this Link link)
    	{
			return (link != null)
					? link.Url
					: "";
    	}
 
    	public static Link click(this Link link)
    	{
    		return link.click(0);    		
    	}    	   	
 
    	public static Link click(this Link link, int miliseconds)
    	{
    		if (link != null)
    		{
    			link.Click();
    			link.wait(miliseconds); 
    		}
    		return link;
    	}
 
 
    	public static List<string> texts(this List<Link> links)
    	{
    		return (from link in links 
    				select link.text()).toList();
    	}
 
    	public static List<string> urls(this List<Link> links)
    	{
    		return (from link in links 
    				select link.url()).toList();
    	}
 
    	// WatiN Button Extension methods
 
    	public static WatiN.Core.Button button(this WatiN_IE watinIe, string name)
    	{
    		foreach(var button in watinIe.buttons())
    			if (button.id() == name || button.value() == name)
    				return button;
    		"in WatiN_IE could not find Button with name:{0}".error(name ?? "[null value]");
    		return null;    				
    	}
 
    	public static List< WatiN.Core.Button> buttons(this WatiN_IE watinIe)
    	{
    		return (from button in watinIe.IE.Buttons
    				select button).toList();
    	}
 
 
    	public static List<string> texts(this List<WatiN.Core.Button> buttons)
    	{
    		return (from button in buttons 
    				select button.text()).toList();
    	}
 
    	public static List<string> values(this List<WatiN.Core.Button> buttons)
    	{
    		return (from button in buttons 
    				select button.value()).toList();
    	}
 
    	public static List<string> ids(this List<WatiN.Core.Button> buttons)
    	{
    		return (from button in buttons 
    				select button.id()).toList();
    	}
 
    	public static List<string> names(this List<WatiN.Core.Button> buttons)
    	{
    		return buttons.ids();
    	}
 
		public static string value(this WatiN.Core.Button button)
    	{    		
    		return (button != null)
    					? button.Value
    					: "";
    	}
 
    	public static WatiN.Core.Button click(this WatiN.Core.Button button)
    	{    		
    		if (button != null)
    			button.Click();
    		return button;
    	}
 
    	// WatiN SelectLists Extension methods    	
 
    	public static SelectList selectList(this WatiN_IE watinIe, string name)
    	{    		
    		foreach(var selectList in watinIe.selectLists())
    			if (selectList.id() == name)
    				return selectList;
    		"in WatiN_IE could not find SelectList with name:{0}".error(name ?? "[null value]");
    		return null;    				
    	}
 
    	public static List<SelectList> selectLists(this WatiN_IE watinIe)
    	{
    		return (from selectList in watinIe.IE.SelectLists
    				select selectList).toList();
    	}
 
    	public static string id(this SelectList selectList)
    	{
    		return (selectList != null)
    					? selectList.Id
    					: "";
    	}
 
    	public static List<string> ids(this List<SelectList> selectLists)
    	{
    		return (from selectList in selectLists 
    				select selectList.id()).toList();
    	}
 
    	public static List<Option> options(this SelectList selectList)
    	{
    		return (from option in selectList.Options 
    				select option).toList();
    	}
 
    	public static Option select(this Option option)
    	{
    		try
    		{
    			if (option != null)
					option.Select();
			}
			catch(Exception ex)
			{
				ex.log("in Option select");
			}
			return option;
    	}
 
    	public static SelectList select(this SelectList selectList, int index)
    	{
    		var options = selectList.options();
    		if (index < options.size())
    			options[index].select();
    		return selectList;
    	}
    	// WatiN CheckBox Extension methods    	
 
    	public static WatiN.Core.CheckBox checkBox(this WatiN_IE watinIe, string name)
    	{
    		//watinIe.textFields();   // after some events 
    		foreach(var checkBox in watinIe.checkBoxes())
    			if (checkBox.id() == name) // || checkBox.title() == name)
    				return checkBox;
    		"in WatiN_IE could not find CheckBox with name:{0}".error(name ?? "[null value]");
    		return null;    				
    	}
 
    	public static List<WatiN.Core.CheckBox> checkBoxes(this WatiN_IE watinIe)
    	{
    		return (from checkBox in watinIe.IE.CheckBoxes
    				select checkBox).toList();
    	}
 
    	public static string id(this WatiN.Core.CheckBox checkBox)
    	{
    		return (checkBox != null)
    					? checkBox.Id
    					: "";
    	}
 
    	public static List<string> ids(this List<WatiN.Core.CheckBox> checkBoxes)
    	{
    		return (from checkBox in checkBoxes 
    				select checkBox.id()).toList();
    	}
 
    	public static bool value(this WatiN.Core.CheckBox checkBox)
    	{    		
    		return (checkBox != null)
    					? checkBox.Checked
    					: false;
    	}
 
    	public static List<bool> values(this List<WatiN.Core.CheckBox> checkBoxes)
    	{
    		return (from checkBox in checkBoxes 
    				select checkBox.value()).toList();
    	}
 
    	public static WatiN.Core.CheckBox value(this WatiN.Core.CheckBox checkBox, bool value)
    	{
    		if (checkBox!= null)    
    		try
    		{
    			checkBox.Checked = value;    	
    		}
    		catch(Exception ex)
    		{
    			ex.log("in WatiN.Core.CheckBox value");
    		}
    		return checkBox;
    	}
 
    	public static WatiN.Core.CheckBox check(this WatiN.Core.CheckBox checkBox)
    	{    		
    		return checkBox.value(true);
    	}
 
    	public static WatiN.Core.CheckBox uncheck(this WatiN.Core.CheckBox checkBox)
    	{    		
    		return checkBox.value(false);
    	}
 
    	// WatiN TextField Extension methods    	
    	public static TextField field(this WatiN_IE watinIe, string name)
    	{
    		return watinIe.textField(name);
    	}
 
    	public static List<TextField> fields(this WatiN_IE watinIe)
    	{
    		return watinIe.textFields();
    	}
 
    	public static bool hasField(this WatiN_IE watinIe, string name)
    	{
    	   return watinIe.textFieldExists(name);
    	}
 
    	public static TextField textField(this WatiN_IE watinIe, string name)
    	{
    		//watinIe.textFields();   // after some events 
    		foreach(var textField in watinIe.textFields())
    			if (textField.name() == name || textField.title() == name)
    				return textField;
    		"in WatiN_IE could not find TextField with name:{0}".error(name ?? "[null value]");
    		return null;    				
    	}
 
    	public static bool textFieldExists(this WatiN_IE watinIe, string name)
    	{
    		foreach(var textField in watinIe.textFields())
    			if (textField.name() == name || textField.title() == name)
    				return true;
    		return false;
    	}
    	public static List<TextField> textFields(this WatiN_IE watinIe)
    	{
    		return (from textField in watinIe.IE.TextFields
    				select textField).toList();
    	}
 
    	public static string name(this TextField textField)
    	{
    		return (textField != null)
    					? textField.Name
    					: "";
    	}
 
    	public static List<string> names(this List<TextField> textFields)
    	{
    		return (from textField in textFields 
    				select textField.name()).toList();
    	}
 
    	public static string value(this TextField textField)
    	{    		
    		return (textField != null)
    					? textField.Value
    					: "";
    	}
 
    	public static List<string> values(this List<TextField> textFields)
    	{
    		return (from textField in textFields 
    				select textField.value()).toList();
    	}
 
    	public static TextField value(this TextField textField, string value)
    	{
    		if (textField!= null)    		
    			textField.Value = value;    	
    		return textField;
    	}
 
    	// WatiN Forms Extension methods    	
 
    	public static List<WatiN.Core.Form> forms(this WatiN_IE watinIe)
    	{
    		return (from form in watinIe.IE.Forms
    				select form).toList();
    	}
 
 
    	// WatiN Elemetns Extension methods  
 
    	public static List<Element> elements(this WatiN_IE watinIe, string tagName)
    	{
    		return (from element in watinIe.IE.Elements
    				where element.TagName == tagName
    				select element).toList();
    	}
 
    	public static List<Element> elements(this WatiN_IE watinIe)
    	{
    		return (from element in watinIe.IE.Elements
    				select element).toList();
    	}
 
    	public static List<string> tagNames(this List<Element> elements)
    	{    		
    		return (from element in elements
    				select element.TagName).Distinct().toList();
    	}
 
    	public static Dictionary<string, List<Element>> indexedByTagName(this List<Element> elements)
    	{
    		var result = new Dictionary<string,List<Element>>();
    		foreach(var element in elements)
    			result.add(element.TagName, element);
    		return result;
    	}
 
 
    	public static string tagName(this Element element)
    	{
    		return element.TagName;
    	}
 
 
    	// WatiN Divs Extension methods  
 
 
    	public static Div div(this WatiN_IE watinIe, string id)
    	{
    		foreach(var div in watinIe.divs())
    			if (div.Id != null && div.Id == id)
    				return div;
    		return null;
    	}
    	public static List<Div> divs(this WatiN_IE watinIe)
    	{
    		return (from div in watinIe.IE.Divs
    				select div).toList();
    	}
 
    	public static List<string> ids(this List<Div> divs)
    	{
 
    		return (from div in divs 
    				where div.Id != null
    				select div.Id).toList();
    	}
 
    	// WatiN  Element Extension methods    	   	   	
 
    	public static string id(this Element element)
    	{
    		return (element != null)
    					? element.Id
    					: "";
    	}
 
    	public static string text(this Element element)
    	{
    		return (element != null)
    					? element.Text
    					: "";
    	}
 
    	public static string title(this Element element)
    	{
    		return (element != null)
    					? element.Title
    					: "";
    	}
 
    	public static string innerHtml(this Element element)
    	{
    		return (element != null)
    					? element.InnerHtml
    					: "";
    	}
 
    	public static string outerHtml(this Element element)
    	{
    		return (element != null)
    					? element.OuterHtml
    					: "";
    	}
 
    	public static string html(this Element element)
    	{
    		return element.outerHtml();
    	}
 
    	// Captcha Extension methods
 
    	public static string resolveCaptcha(this WatiN_IE watinIe, string captchaImageUrl)
    	{
    		return ascx_CaptchaQuestion.askQuestion(captchaImageUrl);
    	}
 
    	public static string resolveCaptcha(this WatiN_IE watinIe, TextField textField)
    	{
    		return watinIe.resolveCaptcha(textField.value());
    	}
 
    	public static WatiN_IE resolveCaptcha(this WatiN_IE watinIe, string questionField, string answerField)
    	{
    		var questionUrl = watinIe.textField(questionField).value();
    		if (questionUrl.valid())
    		{
    			var captchaAnswer = watinIe.resolveCaptcha(questionUrl);
    			watinIe.textField(answerField).value(captchaAnswer);
    		}
			return watinIe;    		
    	}    	
 
    	public static string askUserQuestion(this WatiN_IE watinIe, string question, string title, string defaultValue)
    	{
    		var assembly =  "Microsoft.VisualBasic".assembly();
			var intercation = assembly.type("Interaction");
 
			var parameters = new object[] {question,title,defaultValue,-1,-1}; 
			return intercation.invokeStatic("InputBox",parameters).str(); 
    	}
 
    	// user interaction 
 
    	public static WatiN_IE askUserToContinue(this WatiN_IE watinIe)
    	{
    		MessageBox.Show("Click OK to Continue the WatiN IE workflow", "O2 Message",MessageBoxButtons.OK, MessageBoxIcon.Question); 
    		return watinIe;
    	}
 
    	public static ICredential askUserForUsernameAndPassword(this WatiN_IE watinIe)
    	{
    		return watinIe.askUserForUsernameAndPassword("");
    	}
    	public static ICredential askUserForUsernameAndPassword(this WatiN_IE watinIe, string loginType)
    	{
	   	var credential = ascx_AskUserForLoginDetails.ask();
	   	if (loginType.valid())
		   	credential.CredentialType = loginType;
	   	return credential;
	    }
    	// TreeView helper 
 
    	public static Panel showElementsInTreeView(this WatiN_IE watinIe)
    	{
    		var hostPanel = O2Gui.open<Panel>("WatiN element details",400,400);
			var controls = hostPanel.add_1x1("Html elements", "Propeties");
			var propertyGrid = controls[1].add_PropertyGrid();
			controls[0].add_TreeView()
					   .add_Nodes(watinIe.elements().indexedByTagName())
					   .sort()
					   .showSelection()
					   .beforeExpand<List<Element>>(
					  		(treeNode, elements) => 
					  					{
					  						try { treeNode.add_Nodes(elements);}
					  						catch(Exception ex) { ex.log("in beforeExpand<List<Element>>");}
					  					})
					   .afterSelect<Element>((element)=> propertyGrid.show(element))
					   .afterSelect<List<Element>>((elements)=> propertyGrid.show(elements[0]));
    		return hostPanel;
    	}
 
    	// Control Extensionmethods
 
    	public static WatiN_IE add_IE(this Control control)
    	{
    		var browser = control.add_Control<System.Windows.Forms.WebBrowser>();
    		return browser.ie();    		
    	}
 
     }
}
