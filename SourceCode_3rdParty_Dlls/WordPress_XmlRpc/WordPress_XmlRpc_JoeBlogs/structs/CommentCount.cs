using System;
using CookComputing.XmlRpc;

namespace JoeBlogs.Structs
{

	/// <summary>
	/// Shows total number of comments, as well as a break down of comments approved, awaiting moderation, marked as spam.
	/// </summary>
	[XmlRpcMissingMapping(MappingAction.Ignore)]
	public struct CommentCount
	{
		public string approved; //i think this maybe an error on WP part - should be int according to their documentation: http://codex.wordpress.org/XML-RPC#wp.getCommentCount
		public int awaiting_moderation;
		public int spam;
		public int total_comments;
	}
}