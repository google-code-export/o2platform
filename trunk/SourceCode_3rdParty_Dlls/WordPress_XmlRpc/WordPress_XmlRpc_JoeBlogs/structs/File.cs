using System;
using CookComputing.XmlRpc;

namespace JoeBlogs.Structs
{
	/// <summary> 
	/// This struct represents information about a user. 
	/// </summary> 
	[XmlRpcMissingMapping(MappingAction.Ignore)]
	public struct File
	{
		public string file;
		public string url;
		public string type;
	}
}