// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;

namespace O2.XRules.Database.Utils
{	
	
	[Serializable]
	public  class NameValueItems : List<Item> 
	{
		
	}
	
	[Serializable]
	public  class Items : List<Item> 
	{
		public string this[string key] 
		{
			get
			{
				foreach(var item in this)
					if (item.Key == key)
						return item.Value;
				return null;
					//return new Item(value,value);
			}	
		}
	}
	
	[Serializable]
	public  class Item : NameValuePair<string,string>
	{
		public Item()
		{}
		
		public Item(string key, string value) : base(key,value)
		{
			
		}
	}
	
	[Serializable]
	public  class NameValuePair<T,K>
	{
		[XmlAttribute]
		public T Key {get;set;}
		[XmlAttribute]
		public K Value {get;set;}
		
		public NameValuePair()
		{}
		
		public NameValuePair(T key, K value)
		{
			Key = key;
			Value = value;
		}
		
		public override string ToString()
		{
			return Key.str();
		}
	}

	public static class NameValuePair_ExtensionMethods
	{
	
		public static List<Item> add(this List<Item> list, string key, string value)
		{
			list.Add(new Item(key,value));
			return list;
		}
		
		public static List<NameValuePair<T,K>> add<T,K>(this List<NameValuePair<T,K>> list, T key, K value)
		{
			list.Add(new NameValuePair<T,K>(key,value));
			return list;
		}
	}
	
	
		#region tuples

    public class Tuple<T>
    {
        public Tuple(T first)
        {
            First = first;
        }

        public T First { get; set; }
    }

    public class Tuple<T, T2> : Tuple<T>
    {
        public Tuple(T first, T2 second)
            : base(first)
        {
            Second = second;
        }

        public T2 Second { get; set; }
    }

    public class Tuple<T, T2, T3> : Tuple<T, T2>
    {
        public Tuple(T first, T2 second, T3 third)
            : base(first, second)
        {
            Third = third;
        }

        public T3 Third { get; set; }
    }

    public class Tuple<T, T2, T3, T4> : Tuple<T, T2, T3>
    {
        public Tuple(T first, T2 second, T3 third, T4 fourth)
            : base(first, second, third)
        {
            Fourth = fourth;
        }

        public T4 Fourth { get; set; }
    }

    #endregion	        
}
    	