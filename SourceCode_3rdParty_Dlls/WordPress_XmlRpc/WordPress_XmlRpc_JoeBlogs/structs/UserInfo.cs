using System;
using CookComputing.XmlRpc;

namespace JoeBlogs.Structs
{
	/// <summary> 
	/// This struct represents information about a user. 
	/// </summary> 
	[XmlRpcMissingMapping(MappingAction.Ignore)]
	public struct UserInfo
	{
		public string url;
		public string blogid;
		public string blogName;
		public string firstname;
		public string lastname;
		public string email;
		public string nickname;
	}
}