using System;
using System.Collections.Generic;
using System.IO;

using CookComputing.XmlRpc;
using JoeBlogs.XmlRpcInterfaces;
using JoeBlogs.Structs;

namespace JoeBlogs
{
	//<summary> 
	//Implements the MetaWeblog API
	//http://msdn.microsoft.com/en-us/library/bb259697.aspx
	//</summary> 
  public class MetaWeblogWrapper : BaseWrapper
	{
		public MetaWeblogWrapper(string url, string username, string password)
			: base(url, username, password)
		{
			if (BlogID == null)
			{
				try
				{
					BlogID = GetUserBlogs()[0].blogid;
				}
				catch
				{
					BlogID = string.Empty;
				}
			}
		}

		/// <summary> 
		/// Posts a new entry to a blog. 
		/// </summary> 
		/// <param name="content">The content.</param>
		/// <param name="publish">If false, this is a draft post.</param>
		/// <returns>The postid of the newly-created post.</returns>
		public virtual string NewPost(Post content, bool publish)
		{
			return wrapper.NewPost(this.BlogID, this.Username, this.Password, content, publish);
		}

		/// <summary>
		/// Edits an existing entry on a blog.
		/// </summary>
		/// <param name="content">The content.</param>
		/// <param name="publish">If false, this is a draft post.</param>
		/// <returns>Always returns true.</returns>
		public virtual bool EditPost(Post content, bool publish)
		{
			return wrapper.EditPost(this.BlogID, this.Username, this.Password, content, publish);
		}

		public virtual Post GetPost(string post_id)
		{
			return wrapper.GetPost(post_id, this.Username, this.Password);
		}

		/// <summary> 
		/// Returns the list of categories that have been used in the blog. 
		/// </summary> 
		public virtual IList<Category> GetCategories()
		{
			return wrapper.GetCategories(this.BlogID, this.Username, this.Password);
		}

		/// <summary>
		/// Returns the most recent draft and non-draft blog posts sorted in descending order by publish date. 
		/// </summary>
		/// <param name="numberOfPosts">The number of posts to return.</param>
		/// <returns></returns>
		public virtual IList<Post> GetRecentPosts(int numberOfPosts)
		{
			return wrapper.GetRecentPosts(this.BlogID, this.Username, this.Password, numberOfPosts);
		}

		/// <summary>
		/// Deletes a post from the blog.
		/// </summary>
		/// <param name="postid">The ID of the post to delete.</param>
		/// <returns>Always returns true.</returns>
		public virtual bool DeletePost(string postid)
		{
			return wrapper.DeletePost("", postid, this.Username, this.Password, false);
		}

		/// <summary>
		/// Returns basic user info (name, e-mail, userid, and so on).
		/// </summary>
		public virtual UserInfo GetUserInfo()
		{
			return wrapper.GetUserInfo("", this.Username, this.Password);
		}

		/// <summary>
		/// Gets the blogs for the logged in user.
		/// </summary>
		/// <returns></returns>
		public virtual IList<UserBlog> GetUserBlogs()
		{
			return wrapper.GetUserBlogs(this.Username, this.Password);
		}

		/// <summary>
		/// Creates a new media object.
		/// </summary>
		/// <param name="mediaObject">The media object.</param>
		/// <returns></returns>
		public virtual MediaObjectInfo NewMediaObject(MediaObject mediaObject)
		{
			return wrapper.NewMediaObject(this.BlogID, this.Username, this.Password, mediaObject);
		}

		private IMetaWeblogXmlRpc wrapper
		{
			get
			{
				IMetaWeblogXmlRpc proxy = (IMetaWeblogXmlRpc)XmlRpcProxyGen.Create(typeof(IMetaWeblogXmlRpc));
				proxy.Url = this.Url;
				return proxy;
			}
		}

	}
}