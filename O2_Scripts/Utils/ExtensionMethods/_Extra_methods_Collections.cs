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
		public static List<string> removeEmpty(this List<string> list)
		{
			return (from item in list
					where item.valid()
					select item).toList();
		}
				
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
		
		public static T value<T>(this List<T> list, int index)
		{
			if (list.size() > index)
				return list[index];
			return default(T);
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
		
		public static List<T> removeRange<T>(this List<T> list, int start, int end)
		{
			list.RemoveRange(start,end);
			return list;
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
		
		
		public static List<T> insert<T>(this List<T> list, T value)
		{
			return list.insert(0, value);
		}
		
		public static List<T> insert<T>(this List<T> list, int position, T value)
		{
			list.Insert(position, value);
			return list;
		}
		
		
		//these helps with COM Objects received from IE	
		public static List<string> extractList_String(this object _object)
		{
			return _object.extractList<string>();
		}
		
		public static List<T> extractList<T>(this object _object)
		{
			var results = new List<T>();
			if (_object is IEnumerable)
			{
				foreach(var item in (IEnumerable)_object)
					if (item is T)
						results.Add((T)item);
					else
						"[extractList] inside the IEnumerable, this item was not of type '{0}: {1}".error(item.type(), item);
			}
			else
				"[extractList] the provided object was not IEnumerable: {0}".error(_object);
			return results;
		}
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
	
	public static class Loop_ExtensionMethods
	{
		public static Action loop(this int count , Action action)
		{
			return count.loop(0,action);
		}
		
		public static Action loop(this int count , int delay,  Action action)
		{
			"Executing provided action for {0} times with a delay of {1} milliseconds".info(count, delay);
			for(var i=0 ; i < count ; i ++)
			{
				action();
				if (delay > 0)
					count.sleep(delay);
			}
			return action;
		}
		
		public static Action<int> loop(this int count , Action<int> action)
		{
			return count.loop(0, action);
		}
		
		public static Action<int> loop(this int count , int start, Action<int> action)
		{
			return count.loop(start,1, action);
		}
		
		public static Action<int> loop(this int count, int start , int step, Action<int> action)
		{
			for(var i=start ; i < count ; i+=step)			
				action(i);							
			return action;
		}
		
		public static List<T> loopIntoList<T>(this int count , Func<int,T> action)
		{
			return count.loopIntoList(0, action);
		}
		
		public static List<T> loopIntoList<T>(this int count , int start, Func<int,T> action)
		{
			return count.loopIntoList(start,1, action);
		}
		
		public static List<T> loopIntoList<T>(this int count, int start , int step, Func<int,T> action)
		{
			var results = new List<T>();
			for(var i=start ; i < count ; i+=step)			
				results.Add(action(i));
			return results;
		}		
	}
		
}