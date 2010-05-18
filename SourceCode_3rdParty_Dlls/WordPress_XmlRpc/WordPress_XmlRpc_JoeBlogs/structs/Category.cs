using System;
using CookComputing.XmlRpc;

namespace JoeBlogs.Structs
{
	/// <summary> 
	/// This struct represents the information about a category that could be returned by the 
	/// getCategories() method. 
	/// </summary> 
	[XmlRpcMissingMapping(MappingAction.Ignore)]
	public struct Category
	{
		public string categoryId;
		public string parentId;
		public string description;
		public string categoryName;
		public string title;
		public string htmlUrl;
		public string rssUrl;
	}
}
