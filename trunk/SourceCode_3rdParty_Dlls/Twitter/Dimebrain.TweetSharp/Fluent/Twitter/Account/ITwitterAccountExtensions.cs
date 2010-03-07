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

using System;
using TweetSharp.Core.Attributes;
using TweetSharp.Model;

namespace TweetSharp.Fluent
{
    public static class ITwitterAccountExtensions
    {
        [RequiresAuthentication]
        public static ITwitterAccountVerifyCredentials VerifyCredentials(this ITwitterAccount instance)
        {
            instance.Root.Parameters.Action = "verify_credentials";
            return new TwitterAccountVerifyCredentials(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterAccountEndSession EndSession(this ITwitterAccount instance)
        {
            instance.Root.Parameters.Action = "end_session";
            return new TwitterAccountEndSession(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterAccountEndSession UpdateDeliveryDeviceTo(this ITwitterAccount instance,
                                                                       TwitterDeliveryDevice device)
        {
            instance.Root.Parameters.Action = "update_delivery_device";
            instance.Root.Profile.ProfileDeliveryDevice = device;
            return new TwitterAccountEndSession(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterAccountUpdateProfileColors UpdateProfileColors(this ITwitterAccount instance)
        {
            instance.Root.Parameters.Action = "update_profile_colors";
            return new TwitterAccountUpdateProfileColors(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterAccountUpdateProfileImage UpdateProfileImage(this ITwitterAccount instance, string path)
        {
            instance.Root.Parameters.Action = "update_profile_image";
            instance.Root.Profile.ProfileImagePath = path;
            return new TwitterAccountUpdateProfileImage(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterAccountUpdateProfileBackgroundImage UpdateProfileBackgroundImage(
            this ITwitterAccount instance, string path)
        {
            instance.Root.Parameters.Action = "update_profile_background_image";
            instance.Root.Profile.ProfileBackgroundImagePath = path;
            return new TwitterAccountUpdateProfileBackgroundImage(instance.Root);
        }

        [RequiresAuthentication(false, Description = "No authentication retrieves rate limit status by IP address")]
        public static ITwitterAccountRateLimitStatus GetRateLimitStatus(this ITwitterAccount instance)
        {
            instance.Root.Parameters.Action = "rate_limit_status";
            return new TwitterAccountRateLimitStatus(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterAccountUpdateProfile UpdateProfile(this ITwitterAccount instance)
        {
            instance.Root.Parameters.Action = "update_profile";
            return new TwitterAccountUpdateProfile(instance.Root);
        }
    }
}