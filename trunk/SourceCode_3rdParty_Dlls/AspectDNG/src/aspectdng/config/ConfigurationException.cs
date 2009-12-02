/* 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
*/
using System;
using System.Xml;

namespace DotNetGuru.AspectDNG.Config {
    public class ConfigurationException : ApplicationException {
        public ConfigurationException(Exception inner)
            : base("[Configuration] " + inner.Message, inner) {
        }
        public ConfigurationException(string message)
            : base("[Configuration] " + message) {
        }
    }
}