// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;

namespace O2.Tool.WebInspectConverter.Converter
{
    public class WebInspectFinding
    {
        public string engineId;
        public string fullUrl;
        public string method;
        public string param;
        public string payload;
        public string sessionId;
        public string signatureId;
        //public FilteredUrl filteredUrl;

        public override string ToString()
        {
            return String.Format("{0} - {1} - {2} - {3}", method, fullUrl, param, engineId);
        }
    }
}
