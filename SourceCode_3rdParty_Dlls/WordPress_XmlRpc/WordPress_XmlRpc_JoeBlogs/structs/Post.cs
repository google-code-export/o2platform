using System;
using CookComputing.XmlRpc;

namespace JoeBlogs.Structs
{
	/// <summary> 
	/// This struct represents the information about a post that could be returned by the 
	/// EditPost(), GetRecentPosts() and GetPost() methods. 
	/// </summary> 
	[XmlRpcMissingMapping(MappingAction.Ignore)]
	public struct Post
	{
		public DateTime dateCreated;
		public string description;
		public string title;
		public string postid;

		public string[] categories;

		public string[] mt_keywords;

		public override string ToString()
		{
			return this.description;
		}
	}
}