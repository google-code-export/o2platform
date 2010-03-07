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
using TweetSharp.Fluent;
using TweetSharp.Fluent.Base;
using TweetSharp.Model;

namespace TweetSharp.Service.Base
{
    public abstract class ServiceBase<TFluent, TResult, TNode>
        where TFluent : IFluentBase<TResult>
        where TResult : TweetSharpResult
        where TNode : IFluentNode
    {
        protected abstract TFluent GetQuery();

        protected virtual T Execute<T>(TFluent query, Action<TResult> post) where T : class
        {
            var result = query.Request();

            if (result != null && post != null)
            {
                post.Invoke(result);
            }

            return HandleResponse<T>(query, result);
        }

        protected virtual void Execute(TFluent query, Action<TResult> post)
        {
            var result = query.Request();

            if (result != null && post != null)
            {
                post.Invoke(result);
            }

            HandleResponse(query, result);
        }

        protected virtual void ExecuteAsync(TFluent query, Action<TResult> post)
        {
            query.RequestAsync();
        }

        protected virtual T WithTweetSharp<T>(Func<TFluent, TNode> executor) where T : class
        {
            var query = GetQuery();

            PrepareQuery<T>(query, executor);

            return Execute<T>(query, null);
        }

        protected virtual T WithTweetSharp<T>(Func<TFluent, TFluent> executor) where T : class
        {
            var query = GetQuery();

            executor.Invoke(query);

            return Execute<T>(query, null);
        }

        protected virtual void WithTweetSharp(Func<TFluent, TFluent> executor)
        {
            var query = GetQuery();

            executor.Invoke(query);

            Execute(query, null);
        }

        protected virtual void WithTweetSharpAsync(Func<TFluent, ITwitterLeafNode> executor)
        {
            var query = GetQuery();

            executor.Invoke(query);

            ExecuteAsync(query, null);
        }

        protected virtual void WithTweetSharp(Func<TFluent, ITwitterLeafNode> executor, Action<TResult> post)
        {
            var query = GetQuery();

            executor.Invoke(query);

            Execute(query, post);
        }

        protected virtual T WithTweetSharp<T>(Func<TFluent, TNode> executor, Action<TResult> post) where T : class
        {
            var query = GetQuery();

            PrepareQuery<T>(query, executor);

            return Execute<T>(query, post);
        }

        protected abstract void PrepareQuery<T>(TFluent query, Func<TFluent, TNode> executor);

        protected abstract T HandleResponse<T>(TFluent query, TResult response) where T : class;

        protected abstract void HandleResponse(TFluent query, TResult response);
    }
}