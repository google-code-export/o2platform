using System;
using CookComputing.XmlRpc;

namespace JoeBlogs.Structs
{
	/// <summary>
	/// Page.
	/// </summary>
	[XmlRpcMissingMapping(MappingAction.Ignore)]
	public struct Page
	{
		public DateTime dateCreated;
		public string userid;
		public string page_id;
		public string page_status;
		public string description;
		public string title;
		public string link;
		public string permaLink;
		public string[] categories;
        public string[] mt_keywords;
		public string excerpt;
		public string text_more;
		public int mt_allow_comments;
		public int mt_allow_pings;
		public string wp_slug;
		public string wp_password;
		public string wp_author;
		public object wp_page_parent_id;
		public string wp_page_parent_title;
		public string wp_page_order;
		public string wp_author_id;
		public string wp_author_display_name;
		public DateTime date_created_gmt;
		public CustomField[] custom_fields;
		public string wp_page_template;
		public byte[] bits;

		public override string ToString()
		{
			return title;
		}
	}
}
