using System;
using System.Collections.Generic;
using System.Text;

namespace O2.Kernel.Interfaces.FrameworkSupport.J2EE
{
    public interface IWebXml
    {
        string description { get; set; }
        string displayName { get; set; }
        string listenerClass { get; set; }
        List<IWebXml_Filter> filters { get; set; }
        List<IWebXml_Filter_Mapping> filterMappings { get; set; }
        List<IWebXml_Servlet> servlets { get; set; }
        List<IWebXml_Servlet_Mapping> servletMappings { get; set; }
    }

    public interface IWebXml_Filter
    {
        string filterClass { get; set; }
        string filterName { get; set; }
    }

    public interface IWebXml_Filter_Mapping
    {        
        string filterName { get; set; }
        string urlPattern { get; set; }
        string dispatcher { get; set; }        
    }

    public interface IWebXml_Servlet
    {
        string servletName { get; set; }
        string servletClass { get; set; }
        string loadOnStartUp { get; set; }
        Dictionary<string,string> initParam { get; set; }
    }

    public interface IWebXml_Servlet_Mapping
    {
        string servletName { get; set; }
        string urlPattern { get; set; }        
    }
}