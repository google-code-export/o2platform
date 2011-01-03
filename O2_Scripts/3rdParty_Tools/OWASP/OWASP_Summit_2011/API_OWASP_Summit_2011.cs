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
    public class API_OWASP_Summit_2011
    {    
    	public OwaspWikiAPI wikiApi;
    	
    	public API_OWASP_Summit_2011()
    	{
    		wikiApi = new OwaspWikiAPI(false);  
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
