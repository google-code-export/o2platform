using System;
using CookComputing.XmlRpc;

namespace JoeBlogs.Structs
{
	/// <summary>
	/// Filtering structure for getting comments.
	/// </summary>
	[XmlRpcMissingMapping(MappingAction.Ignore)]
	public struct CommentFilter
	{
		public string status;
		public string post_id;
		public int number;
		public int offset;
	}
}