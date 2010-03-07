#region License

// TweetSharp
// Copyright (c) 2010 Daniel Crenna and Jason Diller
// 
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using TweetSharp.Model;

namespace TweetSharp.Fluent
{
    /// <summary>
    /// Interface representing a user profile
    /// </summary>
    public interface IFluentTwitterProfile
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        string ProfileName { get; set; }

        /// <summary>
        /// Gets or sets the profile URL.
        /// </summary>
        /// <value>The profile URL.</value>
        string ProfileUrl { get; set; }

        /// <summary>
        /// Gets or sets the user location.
        /// </summary>
        /// <value>The user location.</value>
        string ProfileLocation { get; set; }

        /// <summary>
        /// Gets or sets the user description.
        /// </summary>
        /// <value>The user description.</value>
        string ProfileDescription { get; set; }

        /// <summary>
        /// Gets or sets the profile delivery device.
        /// </summary>
        /// <value>The profile delivery device.</value>
        TwitterDeliveryDevice? ProfileDeliveryDevice { get; set; }

        /// <summary>
        /// Gets or sets the color of the profile background.
        /// </summary>
        /// <value>The color of the profile background.</value>
        string ProfileBackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the profile text.
        /// </summary>
        /// <value>The color of the profile text.</value>
        string ProfileTextColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the profile link.
        /// </summary>
        /// <value>The color of the profile link.</value>
        string ProfileLinkColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the profile sidebar fill.
        /// </summary>
        /// <value>The color of the profile sidebar fill.</value>
        string ProfileSidebarFillColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the profile sidebar border.
        /// </summary>
        /// <value>The color of the profile sidebar border.</value>
        string ProfileSidebarBorderColor { get; set; }

        /// <summary>
        /// Gets or sets the profile image path.
        /// </summary>
        /// <value>The profile image path.</value>
        string ProfileImagePath { get; set; }

        /// <summary>
        /// Gets or sets the profile background image path.
        /// </summary>
        /// <value>The profile background image path.</value>
        string ProfileBackgroundImagePath { get; set; }
    }
}