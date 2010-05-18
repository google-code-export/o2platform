using CookComputing.XmlRpc;
using JoeBlogs.Structs;

namespace JoeBlogs.XmlRpcInterfaces
{
	/// <summary>
	/// WordPress XML RPC
	/// http://codex.wordpress.org/XML-RPC_wp
	/// </summary>
	public interface IWordPressXmlRpc : IXmlRpcProxy
	{
		/// <summary>
		/// Gets the user blogs.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <returns></returns>
		[XmlRpcMethod("wp.getUsersBlogs")]
		UserBlog[] GetUserBlogs(string username, string password);

		/// <summary>
		/// Gets the tags.
		/// </summary>
		/// <param name="blogId">The blog id.</param>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <returns></returns>
		[XmlRpcMethod("wp.getTags")]
		TagInfo[] GetTags(string blogId, string username, string password);

		/// <summary>
		/// Gets the comment count.
		/// </summary>
		/// <param name="blogId">The blog id.</param>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <param name="post_id">The post_id.</param>
		/// <returns></returns>
		[XmlRpcMethod("wp.getCommentCount")]
		CommentCount GetCommentCount(string blogId, string username, string password, string post_id);

		[XmlRpcMethod("wp.getPostStatusList")]
		string[] GetPostStatusList(string blogId, string username, string password);

		[XmlRpcMethod("wp.getPageTemplates")]
		PageTemplate[] GetPageTemplates(string blogId, string username, string password);

		[XmlRpcMethod("wp.getOptions")]
		Option[] GetOptions(string blogId, string username, string password, string[] options);

		[XmlRpcMethod("wp.setOptions")]
		Option[] SetOptions(string blogId, string username, string password);

		[XmlRpcMethod("wp.deleteComment")]
		bool DeleteComment(string blogId, string username, string password, string comment_id);

		[XmlRpcMethod("wp.editComment")]
		bool EditComment(string blogId, string username, string password, Comment comment);

		[XmlRpcMethod("wp.newComment")]
		int NewComment(string blogId, string username, string password, string post_id, Comment coment);

		[XmlRpcMethod("wp.getCommentStatusList")]
		string[] GetCommentStatusList(string blogId, string username, string password, string post_id);

		[XmlRpcMethod("wp.getPage")]
		Page GetPage(string blogId, string username, string password, string pageId);

		[XmlRpcMethod("wp.getPages")]
		Page[] GetPages(string blogId, string username, string password);

		[XmlRpcMethod("wp.getPageList")]
		PageMin[] GetPageList(string blogId, string username, string password);

		[XmlRpcMethod("wp.newPage")]
		string NewPage(string blogId, string username, string password, string post_id, Page content);

		[XmlRpcMethod("wp.deletePage")]
		bool DeletePage(string blogId, string username, string password, string post_id, string page_id);

		[XmlRpcMethod("wp.editPage")]
		string EditPage(string blogId, string username, string password, string post_id, Page content);

		[XmlRpcMethod("wp.getAuthors")]
		Author[] GetAuthors(string blogId, string username, string password);

		[XmlRpcMethod("wp.getCategories")]
		Category[] GetCategories(string blogId, string username, string password);

		[XmlRpcMethod("wp.newCategory")]
		string NewCategory(string blogId, string username, string password, Category category);

		[XmlRpcMethod("wp.newCategory")]
		bool DeleteCategory(string blogId, string username, string password, string category_id);

		[XmlRpcMethod("wp.suggestCategories")]
		Category[] SuggestCategories(string blogId, string username, string password, string category, int max_results);

		[XmlRpcMethod("wp.uploadFile")]
		File UploadFile(string blogId, string username, string password, Data data);

		[XmlRpcMethod("wp.getComment")]
		Comment GetComment(string blogId, string username, string password, string commentId);

		[XmlRpcMethod("wp.getComments")]
		Comment[] GetComments(string blogId, string username, string password, CommentFilter filter);
	}
}
