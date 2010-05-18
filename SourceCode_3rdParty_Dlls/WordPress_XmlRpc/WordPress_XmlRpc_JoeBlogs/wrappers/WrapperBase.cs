using System;
using System.Collections.Generic;
using System.Text;

using CookComputing.XmlRpc;

namespace JoeBlogs
{
	/// <summary>
	/// Represents a wrapper.
	/// </summary>
	public class BaseWrapper
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BaseWrapper"/> class.
		/// </summary>
		/// <param name="url">The URL.</param>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		public BaseWrapper(string url, string username, string password)
		{
			this.Url = url;
			this.Username = username;
			this.Password = password;
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="WrapperBase"/> class.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		public BaseWrapper(string username, string password)
		{
			this.Username = username;
			this.Password = password;
		}

		/// <summary>
		/// Gets or sets the blog ID.
		/// </summary>
		/// <value>The blog ID.</value>
		protected string BlogID { get; set; }
		
		/// <summary>
		/// Gets or sets the username.
		/// </summary>
		/// <value>The username.</value>
		protected string Username { get; set; }
		
		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		/// <value>The password.</value>
		protected string Password { get; set; }
		
		/// <summary>
		/// Gets or sets the URL.
		/// </summary>
		/// <value>The URL.</value>
		protected string Url { get; set; }
	}
}