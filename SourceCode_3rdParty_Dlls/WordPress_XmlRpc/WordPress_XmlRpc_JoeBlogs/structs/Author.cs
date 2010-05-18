using System;
using CookComputing.XmlRpc;

namespace JoeBlogs.Structs
{
	/// <summary> 
	/// This struct represents information about a user. 
	/// </summary> 
	[XmlRpcMissingMapping(MappingAction.Ignore)]
	public struct Author
	{
		public string user_id;
		public string user_login;
		public string display_name;
	}
}