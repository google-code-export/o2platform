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

namespace TweetSharp.Fluent
{
    public static partial class IFluentYammerExtensions
    {
        public static IYammerMessages Messages(this IFluentYammer instance)
        {
            instance.Parameters.Activity = "messages";
            return new YammerMessages(instance);
        }

        public static IYammerGroups Groups(this IFluentYammer instance)
        {
            instance.Parameters.Activity = "groups";
            return new YammerGroups(instance);
        }

        public static IYammerTags Tags(this IFluentYammer instance)
        {
            instance.Parameters.Activity = "tags";
            return new YammerTags(instance);
        }

        public static IYammerUsers Users(this IFluentYammer instance)
        {
            instance.Parameters.Activity = "users";
            return new YammerUsers(instance);
        }

        public static IYammerGroupMemberships GroupMemberships(this IFluentYammer instance)
        {
            instance.Parameters.Activity = "group_memberships";
            return new YammerGroupMemberships(instance);
        }

        public static IYammerRelationships Relationships(this IFluentYammer instance)
        {
            instance.Parameters.Activity = "relationships";
            return new YammerRelationships(instance);
        }

        public static IYammerSuggestions Suggestions(this IFluentYammer instance)
        {
            instance.Parameters.Activity = "suggestions";
            return new YammerSuggestions(instance);
        }

        public static IYammerSubscriptions Subscriptions(this IFluentYammer instance)
        {
            instance.Parameters.Activity = "subscriptions";
            return new YammerSubscriptions(instance);
        }

        public static IYammerAutoComplete AutoComplete(this IFluentYammer instance)
        {
            instance.Parameters.Activity = "autocomplete";
            return new YammerAutoComplete(instance);
        }

        public static IYammerInvitations Invitations(this IFluentYammer instance)
        {
            instance.Parameters.Activity = "invitations";
            return new YammerInvitations(instance);
        }

        public static IYammerSearch Search(this IFluentYammer instance)
        {
            instance.Parameters.Activity = "search";
            return new YammerSearch(instance);
        }

        public static IYammerNetworks Networks(this IFluentYammer instance)
        {
            instance.Parameters.Activity = "networks";
            return new YammerNetworks(instance);
        }
    }
}