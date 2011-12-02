using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;

//O2Ref:System.Web.Extensions.dll
//O2Ref:System.Data.dll

namespace O2.XRules.Database.APIs
{	
	public class TM_GUI_Objects
	{		
		public List<string> GuidanceItemsMappings 	{ get; set;}
		public List<string> UniqueStrings			{ get; set;}
		
		public TM_GUI_Objects()
		{
			GuidanceItemsMappings = new List<string>();						
			UniqueStrings = new List<string>();
		}								
		
		public int add_UniqueString(string value)
		{				
			if (UniqueStrings.Contains(value).isFalse())
				UniqueStrings.Add(value);				
			return get_UniqueString(value);						
		}
		
		public int get_UniqueString(string value)
		{
			return UniqueStrings.IndexOf(value);
		}		
	}
}
