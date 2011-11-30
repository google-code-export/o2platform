// Tshis file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Net;
using System.Linq;
using System.Drawing;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.Views.ASCX.ExtensionMethods;

namespace O2.XRules.Database.Utils
{	
		
	public static class _Extra_String_ExtensionMethods
	{		 
		public static string ifEmptyUse(this string _string , string textToUse)
		{
			return (_string.empty() )
						?  textToUse
						: _string;
		}
		
		public static string upperCaseFirstLetter(this string _string)
		{
			if (_string.valid())
			{
				return _string[0].str().upper() + _string.subString(1); 
			}
			return _string;
		}								
		
		public static string append(this string _string, string stringToAppend)
		{
			return _string + stringToAppend;
		}
		
		public static string insertAt(this string _string,  int index, string stringToInsert)
		{
			return _string.Insert(index,stringToInsert);
		}
		
		public static string subString(this string _string, int startPosition)
		{
			if (_string.size() < startPosition)
				return "";
			return _string.Substring(startPosition);
		}
		
		public static string subString(this string _string,int startPosition, int size)
		{
			var subString = _string.subString(startPosition);
			if (subString.size() < size)
				return subString;
			return subString.Substring(0,size);
		}
		
		public static string subString_After(this string _string, string _stringToFind)
		{
			var index = _string.IndexOf(_stringToFind);
			if (index >0)
			{
				return _string.subString(index + _stringToFind.size());
			}
			return "";
		}
		
		public static string lastChar(this string _string)
		{
			if (_string.size() > 0)
				return _string[_string.size()-1].str();
			return "";			
		}
		
		public static bool lastChar(this string _string, char lastChar)
		{
			return _string.lastChar(lastChar.str());
		}
		
		public static bool lastChar(this string _string, string lastChar)
		{
			return _string.lastChar() == lastChar;
		}
		
		public static string firstChar(this string _string)
		{
			if (_string.size() > 0)
				return _string[0].str();
			return "";			
		}
		
		public static bool firstChar(this string _string, char lastChar)
		{
			return _string.firstChar(lastChar.str());
		}
		
		public static bool firstChar(this string _string, string lastChar)
		{
			return _string.firstChar() == lastChar;
		}
		
		public static string add_RandomLetters(this string _string)
		{
			return _string.add_RandomLetters(10);
		}
		
		public static string add_RandomLetters(this string _string, int count)
		{
			return "{0}_{1}".format(_string,count.randomLetters());
		}
		
		public static int randomNumber(this int max)
		{
			return max.random();
		}
				
		public static string ascii(this int _int)
		{
			try
			{				
				return ((char)_int).str();					
			}
			catch(Exception ex)
			{
				ex.log();
				return "";
			}
		}
		
		public static Guid next(this Guid guid)
		{
			return guid.next(1);
		}
		
		public static Guid next(this Guid guid, int incrementValue)
		{			
			var guidParts = guid.str().split("-");
			var lastPartNextNumber = Int64.Parse(guidParts[4], System.Globalization.NumberStyles.AllowHexSpecifier);
			lastPartNextNumber+= incrementValue;
			var lastPartAsString = lastPartNextNumber.ToString("X12");
			var newGuidString = "{0}-{1}-{2}-{3}-{4}".format(guidParts[0],guidParts[1],guidParts[2],guidParts[3],lastPartAsString);
			return new Guid(newGuidString); 					
		}
		
		public static Guid emptyGuid(this Guid guid)
		{
			return Guid.Empty;
		}
		
		public static Guid newGuid(this string guidValue)
		{
			return Guid.NewGuid();
		}
		
		public static Guid guid(this string guidValue)
		{
			if (guidValue.inValid())
				return Guid.Empty;			
			return new Guid(guidValue);		
		}
		
		public static bool isGuid(this String guidValue)
		{
			try
			{
				new Guid(guidValue);
				return true;
			}
			catch
			{
				return false;
			}
		}
		
		public static bool toBool(this string _string)
		{
			try
			{
				if (_string.valid())
					return bool.Parse(_string);				
			}
			catch(Exception ex)
			{
				"in toBool, failed to convert provided value ('{0}') into a boolean: {2}".format(_string, ex.Message);				
			}
			return false;
		}
		
		public static IPAddress toIPAddress(this string _string)
		{
			try
			{
				var ipAddress = IPAddress.Loopback;
				IPAddress.TryParse(_string, out ipAddress);
				return ipAddress;
			}
			catch(Exception ex)
			{
				"Error in toIPAddress: {0}".error(ex.Message);
				return null;
			}
		}
										
	}		
}    	