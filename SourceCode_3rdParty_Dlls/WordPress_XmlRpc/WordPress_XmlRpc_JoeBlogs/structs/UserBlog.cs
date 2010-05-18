using System;
using CookComputing.XmlRpc;

namespace JoeBlogs.Structs
{
	/// <summary> 
	/// This struct represents information about a user's blog. 
	/// </summary> 
	[XmlRpcMissingMapping(MappingAction.Ignore)]
	public struct UserBlog
	{
		public bool isAdmin;
		public string url;
		public string blogid;
		public string blogName;
		public string xmlrpc;
	}
}