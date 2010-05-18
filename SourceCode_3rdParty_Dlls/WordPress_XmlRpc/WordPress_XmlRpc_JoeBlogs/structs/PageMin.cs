using System;
using CookComputing.XmlRpc;

namespace JoeBlogs.Structs
{
	/// <summary>
	/// Page.
	/// </summary>
	[XmlRpcMissingMapping(MappingAction.Ignore)]
	public struct PageMin
	{
		public DateTime dateCreated;
		public string page_id;
		public string page_title;
		public object page_parent_id;
	}
}
