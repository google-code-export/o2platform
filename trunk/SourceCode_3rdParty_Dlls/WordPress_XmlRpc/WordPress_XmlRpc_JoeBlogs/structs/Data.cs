using System;
using CookComputing.XmlRpc;

namespace JoeBlogs.Structs
{
	/// <summary> 
	/// This struct represents information about a user. 
	/// </summary> 
	[XmlRpcMissingMapping(MappingAction.Ignore)]
	public struct Data
	{
		public string name;
		public string type;
		public string base64;
		public bool overwrite;
	}
}