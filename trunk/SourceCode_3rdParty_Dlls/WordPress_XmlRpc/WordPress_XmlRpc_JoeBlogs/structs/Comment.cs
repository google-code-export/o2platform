using System;
using CookComputing.XmlRpc;

namespace JoeBlogs.Structs
{
	/// <summary>
	/// A comment on a blog item
	/// </summary>
	[XmlRpcMissingMapping(MappingAction.Ignore)]
	public struct Comment
	{
		public DateTime date_created_gmt;
		public string user_id;
		public string comment_id;
		public string comment_parent;
		public string status;
		public string content;
		public string link;
		public string post_id;
		public string post_title;
		public string author;
		public string author_url;
		public string author_email;
		public string author_ip;
		public string type;
	}
}