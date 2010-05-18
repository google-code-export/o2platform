using System;
using CookComputing.XmlRpc;

namespace JoeBlogs.Structs
{
	/// <summary>
	/// A comment on a blog item
	/// </summary>
	[XmlRpcMissingMapping(MappingAction.Ignore)]
	public struct PostStatusList
	{
		public string Status;
	}
}