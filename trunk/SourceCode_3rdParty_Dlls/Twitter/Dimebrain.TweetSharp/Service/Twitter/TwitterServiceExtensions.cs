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

using TweetSharp.Extensions;
using TweetSharp.Fluent;
using TweetSharp.Model;

namespace TweetSharp.Service
{
    public static class TwitterServiceExtensions
    {
        // Uses unauthenticated API (IP address)
        public static bool IsFollowing(this TwitterUser user, TwitterUser other)
        {
            var query = FluentTwitter.CreateRequest()
                .Friendships().Show(user.Id, other.Id)
                .AsJson();

            var response = Execute(query);
            var friendship = response.AsFriendship();

            return friendship.Relationship.Source.Following;
        }

        // Uses unauthenticated API (IP address)
        public static bool Follows(this TwitterUser user, TwitterUser other)
        {
            var query = FluentTwitter.CreateRequest()
                .Friendships().Show(user.Id, other.Id)
                .AsJson();

            var response = Execute(query);
            var friendship = response.AsFriendship();

            return friendship.Relationship.Target.FollowedBy;
        }

        // Uses unauthenticated API (IP address)
        private static TwitterResult Execute(ITwitterLeafNode query)
        {
            var response = query.Request();
            if (response.IsTwitterError)
            {
                var error = response.AsError();
                if (error != null)
                {
                    throw new TwitterException(error);
                }
                throw new TweetSharpException("Twitter returned an API error but TweetSharp was unable to parse it.");
            }

            return response;
        }
    }
}