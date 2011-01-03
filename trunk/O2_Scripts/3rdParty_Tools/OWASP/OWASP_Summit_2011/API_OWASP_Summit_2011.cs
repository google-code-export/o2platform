// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Views.ASCX.classes.MainGUI;
using O2.Views.ASCX.ExtensionMethods;
//O2File:OwaspWikiAPI.cs
//O2File:FileCache.cs 
//O2Ref:O2_Misc_Microsoft_MPL_Libs.dll
//O2File:WikiText.cs

namespace O2.XRules.Database.APIs
{
	public class SummitAttendee
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Wiki_UserName { get; set; }
		public string OWASP_Sponsor { get; set; }
		public string TimePaidBy { get; set; }
		public string ExpensesPaidBy { get; set; }
		public string Status { get; set; }
		
		public Dictionary<string,string> variables;// { get; set; }
		
		public SummitAttendee(WikiText_Template templateData)
		{
			variables = templateData.Variables;	
			setValue("Name", "summit_attendee_name1");
			setValue("Email", "summit_attendee_email1");
			setValue("Wiki_UserName", "summit_attendee_wiki_username1");
			setValue("OWASP_Sponsor", "summit_attendee_owasp_sponsor");
			setValue("TimePaidBy", "summit_attendee_summit_time_paid_by_name1");
			setValue("ExpensesPaidBy", "summit_attendee_summit_expenses_paid_by_name1");
			setValue("Status", "status");
			//Name = Variables.get("summit_attendee_name1");
			//Email = Variables.get("summit_attendee_email1");
		}
		
		void setValue(string localVariableName, string variableName)
		{
			this.prop(localVariableName, this.variables.get(variableName));
		}
	}
	
    public class API_OWASP_Summit_2011
    {    
    	public OwaspWikiAPI wikiApi;
    	
    	public API_OWASP_Summit_2011()
    	{
    		wikiApi = new OwaspWikiAPI(false);  
    	}
    	
    	//Attendee details
    	
    	
    	public List<SummitAttendee> getAttendees(List<string> pages)
    	{
    		var attendees = new List<SummitAttendee>();
    		foreach(var page in pages)
    		{
    			var attendee = getAttendee(page);
    			if (attendee.Name.valid())
    				attendees.add(attendee);
    		}
    		return attendees;
    	}
    	public SummitAttendee getAttendee(string page)
    	{
    		var wikiApi = new OwaspWikiAPI(false);   
					
			var templateData = new WikiText_Template();  
					  
			templateData.parse(wikiApi,page); 
			//return templateData.ParseTree.Root.ChildNodes;    
			return new SummitAttendee(templateData);//.Variables; 
    	}
    	
    	//All Attendess funds
    	public List<string> attendees()
    	{
    		return attendees(false);
    	}
    	
		public List<string> attendees(bool useCache)
		{
			var attendees = new List<String>();
			attendees.AddRange(attendees_SeekingFunds(useCache));
			attendees.AddRange(attendees_Confirmed(useCache));
			attendees.AddRange(attendees_UnConfirmed(useCache));
			return attendees;
		}
		
    	//Seeking funds
    	public List<string> attendees_SeekingFunds()
    	{
    		return attendees_SeekingFunds(false);
    	}
    	
		public List<string> attendees_SeekingFunds(bool useCache)
		{
			return getAttendeesMappings(useCache)["Confirmed Summit Attendees: Seeking Funds/Sponsorship"];
		}
		
		//Confirmed
		public List<string> attendees_Confirmed()
    	{
    		return attendees_Confirmed(false);
    	}
    	
		public List<string> attendees_Confirmed(bool useCache)
		{
			return getAttendeesMappings(useCache)["Confirmed Summit Attendees: with Funding"];
		}
		
		//Unconfirmed
		public List<string> attendees_UnConfirmed()
    	{
    		return attendees_UnConfirmed(false);
    	}
    	
		public List<string> attendees_UnConfirmed(bool useCache)
		{
			return getAttendeesMappings(useCache)["Unconfirmed Summit Attendees"];
		}
		
		//get attendees mappings
		public Dictionary<string,List<string>> getAttendeesMappings()	
		{
			return getAttendeesMappings(false);
		}
		
		public Dictionary<string,List<string>> getAttendeesMappings(bool useCache)
		{
			var pageName1 = "Template:Summit_2011_Attendee";      
			var pageName2 = "Summit_2011_Confirmed_Attendees";  
			var mappings = new WikiText_HeadersAndTemplates("Summit_2011_Attendee")
								   .useCache(useCache)
								   .parse(wikiApi,pageName1)
								   .parse(wikiApi,pageName2)
								   .Templates;
			return mappings;								   
		}
		
    }
}
