using System;
using System.Collections.Generic;

namespace O2.Cmd.SpringMvc.Objects
{
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
        public string FileName { get; set; }
        public uint LineNumber { get; set; }        
    }
}
