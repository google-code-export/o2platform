// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Linq;
using System.Reflection;
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
	public static class List_ExtensionMethods
	{
		public static List<string> add_If_Not_There(this List<string> list, string item)
		{
			if (item.notNull())
				if (list.contains(item).isFalse())
					list.add(item);
			return list;
		}
		public static string join(this List<string> list)
		{
			return list.join(",");
		}
		
		public static string join(this List<string> list, string separator)
		{
			if (list.size()==1)
				return list[0];
			if (list.size() > 1)
				return list.Aggregate((a,b)=> "{0} {1} {2}".format(a,separator,b));
			return "";
		}
		
		public static List<T> where<T>(this List<T> list, Func<T,bool> query)
		{
			return list.Where<T>(query).toList();
		}
		
		public static T first<T>(this List<T> list, Func<T,bool> query)
		{
			var results = list.Where<T>(query).toList();
			if (results.size()>0)
				return results.First();
			return default(T);
		}
	}
	
	public static class IEnumerable_ExtensionMethods
	{
		public static bool isIEnumerable(this object list)
		{
			return list.notNull() && list is IEnumerable;
		}
		
		public static int count(this object list)
		{
			if (list.isIEnumerable())
				return (list as IEnumerable).count();
			return -1;
		}
		
		public static int size(this IEnumerable list)
		{
			return list.count();
		}
		public static int count(this IEnumerable list)
		{			
			var count = 0;
			if (list.notNull())
				foreach(var item in list)
					count++;
			return count;
		}
		
		public static object first(this IEnumerable list)
		{
			if(list.notNull())
				foreach(var item in list)
					return item;
			return null;
		}
		
		public static T first<T>(this IEnumerable<T> list)
		{
			try
			{
				if (list.notNull())
					return list.First();
			}
			catch(Exception ex)
			{	
				if (ex.Message != "Sequence contains no elements")
					"[IEnumerable.first] {0}".error(ex.Message);
			}
			return default(T);
		}
		
		public static T last<T>(this IEnumerable<T> list)
		{
			try
			{
				if (list.notNull())
					return list.Last();
			}
			catch(Exception ex)
			{	
				if (ex.Message != "Sequence contains no elements")
					"[IEnumerable.first] {0}".error(ex.Message);
			}
			return default(T);
		}
		
		public static object last(this IEnumerable list)
		{
			object lastItem = null;
			if(list.notNull())
				foreach(var item in list)
					lastItem= item;
			return lastItem;
		}
		
/*		public static List<T> selectMany<T,T1>(this IEnumerable<T> list)
		{
			if (list.notNull()) 
				list.SelectMany<T,T1>((a)=> a);
			return new List<T>();
		}*/
		
		/*public static List<T> toList<T>(this IEnumerable list)
		{
			return list.Cast<T>().toList();
		}*/
	}
	
	public static class Dictionary_ExtensionMethods
	{
		public static Dictionary<string,string> toStringDictionary(this string targetString, string rowSeparator, string keySeparator)
		{
			var stringDictionary = new Dictionary<string,string>();
			try
			{
				foreach(var row in targetString.split(rowSeparator))
				{
					if(row.valid())
					{
						var splittedRow = row.split(keySeparator);
						if (splittedRow.size()!=2)
							"[toStringDictionary] splittedRow was not 2: {0}".error(row);
						else
						{
							if (stringDictionary.hasKey(splittedRow[0]))
								"[toStringDictionary] key already existed in the collection: {0}".error(splittedRow[0]);		
							else
								stringDictionary.Add(splittedRow[0], splittedRow[1]);
						}
					}
				}
			}
			catch(Exception ex)
			{
				"[toStringDictionary] {0}".error(ex.Message);
			}
			return stringDictionary;
		}
		
		public static Dictionary<string,string> remove(this Dictionary<string,string> dictionary, Func<KeyValuePair<string,string>, bool> filter)
		{
			var itemsToRemove = dictionary.Where(filter).toList();
			//var itemsToRemove = CompileEngine.CachedCompiledAssemblies.Where((item)=>item.Value.str().contains("O2_TeamMentor_AspNet"));  
			foreach(var itemToRemove in itemsToRemove)
				dictionary.Remove(itemToRemove.Key);    
			return dictionary;
		}
	}
	
}
    	