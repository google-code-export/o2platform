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