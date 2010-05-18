using System;
using System.Collections.Generic;
using CookComputing.XmlRpc;

using JoeBlogs.XmlRpcInterfaces;
using JoeBlogs.Structs;

namespace JoeBlogs
{
  /// <summary>
  /// Represents a wrapper for use with Wordpress blogs.
  /// </summary>
	public class WordPressWrapper : MetaWeblogWrapper
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="WordPressWrapper"/> class.
		/// </summary>
		/// <param name="url">The URL.</param>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		public WordPressWrapper(string url, string username, string password)
			: base(url, username, password)
		{
			if (BlogID == null)
				BlogID = GetUserBlogs()[0].blogid;
		}

		private IWordPressXmlRpc wrapper
		{
			get
			{
				IWordPressXmlRpc proxy = (IWordPressXmlRpc)XmlRpcProxyGen.Create(typeof(IWordPressXmlRpc));
				proxy.Url = this.Url;
				return proxy;
			}
		}

		/// <summary>
		/// Retrieve the blogs of the users.
		/// </summary>
		/// <returns></returns>
		public override IList<UserBlog> GetUserBlogs()
		{
			return wrapper.GetUserBlogs(this.Username, this.Password);
		}

		/// <summary>
		/// Get list of all tags. 
		/// </summary>
		/// <returns></returns>
		public TagInfo[] GetTags()
		{
      return wrapper.GetTags(this.BlogID, this.Username, this.Password);
		}

		/// <summary>
		/// Retrieve comment count for a specific post. 
		/// </summary>
		/// <param name="post_id"></param>
		/// <returns></returns>
		public CommentCount GetCommentCount(string post_id)
		{
			return wrapper.GetCommentCount(this.BlogID, this.Username, this.Password, post_id);
		}

		/// <summary>
		/// Retrieve post statuses.
		/// </summary>
		/// <returns></returns>
		public IList<string> GetPostStatusList()
		{
			return wrapper.GetPostStatusList(this.BlogID, this.Username, this.Password);
		}

		/// <summary>
		/// Retrieve page templates.
		/// </summary>
		/// <returns></returns>
		public IList<PageTemplate> GetPageTemplates()
		{
			return wrapper.GetPageTemplates(this.BlogID, this.Username, this.Password);
		}

		/// <summary>
		/// Retrieve blog options. If passing in an array, search for options listed within it. 
		/// </summary>
		/// <param name="blogId"></param>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		public IList<Option> GetOptions(string[] options)
		{
			//todo:fix this
			throw new NotImplementedException();
			//return wrapper.GetOptions(this.BlogID, this.Username, this.Password, options);
		}

		/// <summary>
		/// Update blog options. Returns array of structs showing updated values. 
		/// </summary>
		/// <returns></returns>
		public IList<Option> SetOptions()
		{
			return wrapper.SetOptions(this.BlogID, this.Username, this.Password);
		}

		/// <summary>
		/// Remove comment.
		/// </summary>
		/// <param name="comment_id"></param>
		/// <returns></returns>
		public bool DeleteComment(string comment_id)
		{
			//todo: make validation better - add clearer error handling
			try
			{
				var comment = GetComment(comment_id); ;
				return wrapper.DeleteComment(this.BlogID, this.Username, this.Password, comment_id);
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// Edit comment.
		/// </summary>
		/// <param name="comment"></param>
		/// <returns></returns>
		public bool EditComment(Comment comment)
		{
			return wrapper.EditComment(this.BlogID, this.Username, this.Password, comment);
		}

		/// <summary>
		/// Create new comment.
		/// </summary>
		/// <param name="postid"></param>
		/// <param name="comment"></param>
		/// <returns></returns>
		public string NewComment(string postid, Comment comment)
		{
			return wrapper.NewComment(this.BlogID, this.Username, this.Password, postid, comment)
				.ToString();
		}

		/// <summary>
		/// Retrieve all of the comment status.
		/// </summary>
		/// <param name="post_id"></param>
		/// <returns></returns>
		public IList<string> GetCommentStatusList(string post_id)
		{
			return wrapper.GetCommentStatusList(this.BlogID, this.Username, this.Password, post_id);
		}

		/// <summary>
		/// Get the page identified by the page id. 
		/// </summary>
		/// <param name="pageid"></param>
		/// <returns></returns>
		public Page GetPage(string pageid)
		{
			return wrapper.GetPage(this.BlogID, pageid, this.Username, this.Password);
		}

		/// <summary>
		/// Get an array of all the pages on a blog. 
		/// </summary>
		/// <returns></returns>
		public IList<Page> GetPages()
		{
			return wrapper.GetPages(this.BlogID, this.Username, this.Password);
		}

		/// <summary>
		/// Get an array of all the pages on a blog. Just the minimum details, lighter than GetPages. 
		/// </summary>
		/// <returns></returns>
		public IList<PageMin> GetPageList()
		{
			return wrapper.GetPageList(this.BlogID, this.Username, this.Password);
		}

		/// <summary>
		/// Create a new page. Similar to metaWeblog.newPost.
		/// </summary>
		/// <param name="page"></param>
		/// <returns></returns>
		public int NewPage(Page page)
		{
			//todo: newPage
			throw new NotImplementedException();
		}

		/// <summary>
		/// Removes a page from the blog. 
		/// </summary>
		/// <param name="page_id"></param>
		/// <returns></returns>
		public bool DeletePage(string page_id)
		{
			//todo: DeletePage
			throw new NotImplementedException();
		}

		/// <summary>
		/// Make changes to a blog page.
		/// </summary>
		/// <param name="page"></param>
		/// <returns></returns>
		public bool EditPage(Page page)
		{
			//todo: EditPage
			throw new NotImplementedException();
		}

		/// <summary>
		/// Get an array of users for the blog.
		/// </summary>
		/// <returns></returns>
		public IList<Author> GetAuthors()
		{
			return wrapper.GetAuthors(this.BlogID, this.Username, this.Password);
		}

		/// <summary>
		/// Get an array of available categories on a blog.
		/// </summary>
		/// <returns></returns>
		public override IList<Category> GetCategories()
		{
			return wrapper.GetCategories(this.BlogID, this.Username, this.Password);
		}

		/// <summary>
		/// Create a new category.
		/// </summary>
		/// <param name="title">The title of the category.</param>
		/// <param name="description">Description of the category.</param>
		/// <param name="parentId">The ID of the categories parent. If null, will default to 0 (root).</param>
		/// <returns></returns>
		public string NewCategory(string title, string description, string parentId)
		{
			if (parentId == null || parentId == string.Empty)
				parentId = "0";

			var category = new Category
			{
				categoryName = title,
				title = title,
				description = description,
				parentId = parentId
			};

			return wrapper.NewCategory(this.BlogID, this.Username, this.Password, category);
		}

		/// <summary>
		/// Delete a category.
		/// </summary>
		/// <param name="category_id"></param>
		/// <returns></returns>
		public bool DeleteCategory(string category_id)
		{
			return wrapper.DeleteCategory(this.BlogID, this.Username, this.Password, category_id);
		}

		/// <summary>
		/// Get an array of categories that start with a given string.
		/// </summary>
		/// <param name="category"></param>
		/// <param name="max_results"></param>
		/// <returns></returns>
		public Category[] SuggestCategories(string category, int max_results)
		{
			return wrapper.SuggestCategories(this.BlogID, this.Username, this.Password, category, max_results);
		}

		/// <summary>
		/// Upload a file.
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public File UploadFile(Data data)
		{
			return wrapper.UploadFile(this.BlogID, this.Username, this.Password, data);
		}

		/// <summary>
		/// Gets a comment, given it's comment ID. Note that this only works for WordPress version 2.6.1 or higher.
		/// </summary>
		/// <param name="comment_id"></param>
		/// <returns></returns>
		public Comment GetComment(string comment_id)
		{
			return wrapper.GetComment(this.BlogID, this.Username, this.Password, comment_id);
		}

		/// <summary>
		/// Gets a set of comments for a given post. Note that this only works for WordPress version 2.6.1 or higher.
		/// </summary>
		/// <param name="post_id"></param>
		/// <param name="status"></param>
		/// <param name="number"></param>
		/// <param name="offset"></param>
		/// <returns></returns>
		public IList<Comment> GetComments(string post_id, string status, int number, int offset)
		{
			//var statusList = GetCommentStatusList(post_id);
			//todo: add some validation to make sure supplied status is available.

			var filter = new CommentFilter
			{
				post_id = post_id,
				number = number,
				offset = offset,
				status = status
			};

			return wrapper.GetComments(this.BlogID, this.Username, this.Password, filter);
		}
	}
}