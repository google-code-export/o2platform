// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;
using O2.XRules.Database.Utils;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using www.springframework.org.schema.beans;
//O2File:spring-servlet.xsd.cs
//O2Ref:O2_Misc_Microsoft_MPL_Libs.dll

//O2File:_Extra_methods_To_Add_to_Main_CodeBase.cs


namespace O2.XRules.Database.Languages_and_Frameworks.J2EE
{
	[Serializable]
    public class SpringMvcMappings
    {
        public string id { get; set; }
        public string XmlFile { get; set; }
        public List<SpringMvcController> Controllers { get; set; }        
        
        [XmlIgnore]
        public beans springBeans;
        
        public SpringMvcMappings()
        {
        	Controllers = new List<SpringMvcController> ();
        }
        
        public SpringMvcMappings(string xmlFile) : this()
        {
        	XmlFile = xmlFile;
        	springBeans = beans.Load(xmlFile); 
        }
        
    }
    
    [Serializable]
    public class SpringMvcController
    {
        public string JavaClass { get; set; }
        public string JavaFunction { get; set; }
        public string JavaClassAndFunction { get; set; }    
        public string HttpRequestUrl { get; set; }
        public string HttpRequestMethod { get; set; }
        public string HttpMappingParameter { get; set; }
        public List<SpringMvcParameter> AutoWiredJavaObjects { get; set; }
        public string CommandName { get; set; }
        public string FileName { get; set; }
        public uint LineNumber { get; set; }        
        public Items Properties {get;set;}
     
     	public SpringMvcController()
     	{
     		Properties = new Items();
     		AutoWiredJavaObjects = new List<SpringMvcParameter>();
     	}
    }
            
    [Serializable]
    public class SpringMvcParameter
    {
        public string Name { get; set; }
        public string ClassName { get; set; }
        //public bool autoWiredObject { get; set; }
        public string autoWiredMethodUsed { get; set; }
    }
    
    public static class SpringMvcMappings_ExtensioMethods
    {
    	public static List<beans.beanLocalType> urlBeans(this beans _beans)
    	{    		
    		return (from bean in _beans.bean	
			   	    where bean.name.notNull() && bean.name[0]== '/'
			   		select bean).toList();
			return null;
		}
		
		public static Dictionary<string, beans.beanLocalType> beans_byId(this beans _beans)
		{
			var beans_byID = new Dictionary<string, beans.beanLocalType>();
			foreach(var bean in _beans.bean)	
				beans_byID.Add(bean.id ?? bean.name.str(), bean);
			return beans_byID;
		}
	}
}
