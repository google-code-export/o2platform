using CookComputing.XmlRpc;
using JoeBlogs.Structs;

namespace JoeBlogs.XmlRpcInterfaces
{
	public interface IMetaWeblogXmlRpc : IXmlRpcProxy
	{
		/// <summary>
		/// Deletes the post.
		/// </summary>
		/// <param name="appKey">The app key.</param>
		/// <param name="postid">The postid.</param>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <param name="publish">if set to <c>true</c> [publish].</param>
		/// <returns></returns>
		[XmlRpcMethod("metaWeblog.deletePost")]
		bool DeletePost(string appKey, string postid, string username, string password, bool publish);

		/// <summary>
		/// Edits the post.
		/// </summary>
		/// <param name="postid">The postid.</param>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <param name="content">The content.</param>
		/// <param name="publish">if set to <c>true</c> [publish].</param>
		/// <returns></returns>
		[XmlRpcMethod("metaWeblog.editPost")]
		bool EditPost(string postid, string username, string password, Post content, bool publish);

		/// <summary>
		/// Gets the categories.
		/// </summary>
		/// <param name="blogid">The blogid.</param>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <returns></returns>
		[XmlRpcMethod("metaWeblog.getCategories")]
		Category[] GetCategories(string blogid, string username, string password);

		/// <summary>
		/// Gets the recent posts.
		/// </summary>
		/// <param name="blogid">The blogid.</param>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <param name="numberOfPosts">The number of posts.</param>
		/// <returns></returns>
		[XmlRpcMethod("metaWeblog.getRecentPosts")]
		Post[] GetRecentPosts(string blogid, string username, string password, int numberOfPosts);

		/// <summary>
		/// Gets the user info.
		/// </summary>
		/// <param name="appKey">The app key.</param>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <returns></returns>
		[XmlRpcMethod("blogger.getUserInfo")]
		UserInfo GetUserInfo(string appKey, string username, string password);

		/// <summary>
		/// News the media object.
		/// </summary>
		/// <param name="blogid">The blogid.</param>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <param name="mediaObject">The media object.</param>
		/// <returns></returns>
		[XmlRpcMethod("metaWeblog.newMediaObject")]
		MediaObjectInfo NewMediaObject(string blogid, string username, string password, MediaObject mediaObject);

		/// <summary>
		/// News the post.
		/// </summary>
		/// <param name="blogid">The blogid.</param>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <param name="content">The content.</param>
		/// <param name="publish">if set to <c>true</c> [publish].</param>
		/// <returns></returns>
		[XmlRpcMethod("metaWeblog.newPost")]
		string NewPost(string blogid, string username, string password, Post content, bool publish);

		/// <summary>
		/// Gets the post.
		/// </summary>
		/// <param name="postid">The postid.</param>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <returns></returns>
		[XmlRpcMethod("metaWeblog.getPost")]
		Post GetPost(string postid, string username, string password);

		/// <summary>
		/// Gets the user blogs.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <returns></returns>
		[XmlRpcMethod("metaWeblog.getUserBlogs")]
		UserBlog[] GetUserBlogs(string username, string password);
	}
}