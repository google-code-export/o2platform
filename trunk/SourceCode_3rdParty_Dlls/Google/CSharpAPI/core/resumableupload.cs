/* Copyright (c) 2006 Google Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/
#region Using directives

#define USE_TRACING

using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Collections.Generic;
using System.Globalization;
using System.ComponentModel;
using System.Threading;
using System.Collections.Specialized;

#endregion


/////////////////////////////////////////////////////////////////////
// <summary>contains support classes to work with 
// the resumable upload protocol. 
//  </summary>
////////////////////////////////////////////////////////////////////
namespace Google.GData.Client.ResumableUpload
{

    internal class AsyncResumableUploadData : AsyncData
    {
        private Authenticator authenticator;
        private string contentType;
        private AbstractEntry entry;
        private string slug;
 

        public AsyncResumableUploadData(AsyncDataHandler handler,
                                        Authenticator authenticator,
                                        Uri uriToUse,
                                        Stream payload,
                                        string contentType,
                                        string slug,
                                        string httpMethod,
                                        SendOrPostCallback callback,
                                        object userData)
            : base(uriToUse, null, userData, callback)
        {
            this.DataHandler = handler;
            this.authenticator = authenticator;
            this.contentType = contentType;
            this.DataStream = payload;
            this.slug = slug;
            this.HttpVerb = httpMethod;
        }

        public AsyncResumableUploadData(AsyncDataHandler handler,
                                                Authenticator authenticator,
                                                AbstractEntry payload,
                                                string httpMethod,
                                                SendOrPostCallback callback,
                                                object userData)
            : base(null, null, userData, callback)
        {
            this.DataHandler = handler;
            this.authenticator = authenticator;
            this.entry = payload;
            this.HttpVerb = httpMethod;
        }


        public string ContentType
        {
            get
            {
                return this.contentType;
            }
        }

        public Authenticator Authentication
        {
            get
            {
                return this.authenticator;
            }
        }

        public AbstractEntry Entry
        {
            get
            {
                return this.entry;
            }
        }

        public string Slug
        {
            get
            {
                return this.slug;
            }
        }
    }

    /// <summary>
    /// this class handles the Resumable Upload protocol
    /// </summary>
    public class ResumableUploader : AsyncDataHandler
    {
        // chunksize in Megabytes
        private int chunkSize;
        private static int MB = 1048576;
        private Authenticator authenticator;

        /// <summary>
        /// The relationship value to be used to find the resumable 
        /// </summary>
        public static string CreateMediaRelation = "http://schemas.google.com/g/2005#resumable-create-media";
        public static string EditMediaRelation = "http://schemas.google.com/g/2005#resumable-edit-media";

        private delegate void WorkerResumableUploadHandler(AsyncResumableUploadData data, AsyncOperation asyncOp,
                            SendOrPostCallback completionMethodDelegate);


        /// <summary>
        /// Default constructor. Uses the default chunksize of 25 megabyte
        /// </summary>
        /// <returns></returns>
        public ResumableUploader() : this(25)
        {
        }

        /// <summary>
        /// ResumableUploader constructor. 
        /// </summary>
        /// <param name="chunkSize">the upload chunksize in Megabytes, needs to be greater 0</param>
        /// <returns></returns>
        public ResumableUploader(int chunkSize)
        {
            if (chunkSize < 1)
                throw new ArgumentException("chunkSize needs to be > 0");
            this.chunkSize = chunkSize;
        }

        /// <summary>
        /// returns the resumable edit media Uri for a given entry
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public static Uri GetResumableEditUri(AtomLinkCollection links)
        {
             // scan the link collection
            AtomLink link = links.FindService(EditMediaRelation, null);
            return link == null ? null : new Uri(link.AbsoluteUri);
        }

        /// <summary>
        /// returns the resumabled create media Uri for a given entry
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public static Uri GetResumableCreateUri(AtomLinkCollection links)
        {
            // scan the link collection
            AtomLink link = links.FindService(CreateMediaRelation, null);
            return link == null ? null : new Uri(link.AbsoluteUri);
        }

        /// <summary>
        /// Uploads an entry, including it's media to the uri given inside the entry. 
        /// </summary>
        /// <param name="authentication">The authentication information to be used</param>
        /// <param name="payload">The entry to be uploaded. This is a complete entry, including the metadata. 
        /// This will create a new entry on the service</param>
        /// <returns></returns>
        public WebResponse Insert(Authenticator authentication, AbstractEntry payload)
        {
            return Insert(authentication, payload, null);
        }

        /// <summary>
        /// Uploads just the media media to the uri given. 
        /// </summary>
        /// <param name="resumableUploadUri"></param>
        /// <param name="authentication">The authentication information to be used</param>
        /// <param name="payload">The media to uploaded.</param>
        /// <param name="contentType">The type of the content, e.g. text/html</param>
        /// <returns></returns>
        public WebResponse Insert(Authenticator authentication, Uri resumableUploadUri, Stream payload, string contentType, string slug)
        {
            return Insert(authentication, resumableUploadUri, payload, contentType, slug, null);
        }


        private WebResponse Insert(Authenticator authentication, AbstractEntry payload, AsyncData data)
        {
            Uri initialUri = ResumableUploader.GetResumableCreateUri(payload.Links);
            if (initialUri == null)
                throw new ArgumentException("payload did not contain a resumabled create media Uri");

            Uri resumeUri = InitiateUpload(initialUri, authentication, payload);
            return UploadStream(HttpMethods.Post, resumeUri,  authentication, 
                                payload.MediaSource.Data, 
                                payload.MediaSource.ContentType, data);
        }

        private WebResponse Insert(Authenticator authentication, Uri resumableUploadUri, 
                                Stream payload, string contentType, string slug, AsyncData data)
        {

            Uri resumeUri = InitiateUpload(resumableUploadUri, authentication, contentType, null, GetStreamLength(payload));
            return UploadStream(HttpMethods.Post, resumeUri, authentication, payload, contentType, data);
        }

        public void InsertAsync(Authenticator authentication, Uri resumableUploadUri, Stream payload, string contentType, string slug, object userData)
        {
            AsyncResumableUploadData data = new AsyncResumableUploadData(this, 
                                                authentication,  
                                                resumableUploadUri, 
                                                payload, 
                                                contentType,
                                                slug,
                                                HttpMethods.Post,
                                                this.ProgressReportDelegate, 
                                                userData);
            WorkerResumableUploadHandler workerDelegate = new WorkerResumableUploadHandler(AsyncInsertWorker);
            this.AsyncStarter(data, workerDelegate, userData);
        }

        public void InsertAsync(Authenticator authentication, AbstractEntry payload, object userData)
        {
            AsyncResumableUploadData data = new AsyncResumableUploadData(this,
                                                authentication,
                                                payload,
                                                HttpMethods.Post,
                                                this.ProgressReportDelegate,
                                                userData);
            WorkerResumableUploadHandler workerDelegate = new WorkerResumableUploadHandler(AsyncInsertWorker);
            this.AsyncStarter(data, workerDelegate, userData);
        }


        /// <summary>
        ///  askes the server about the current status
        /// </summary>
        /// <param name="authentication"></param>
        /// <param name="targetUri"></param>
        /// <returns></returns>
        public static long QueryStatus(Authenticator authentication, Uri targetUri)
        {
            HttpWebRequest request = authentication.CreateHttpWebRequest(HttpMethods.Post, targetUri);
            long result = -1;

            // add a range header
            string contentRange = String.Format("bytes */*");
            request.Headers.Add(HttpRequestHeader.ContentRange, contentRange);
            request.ContentLength = 0;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            // now parse the header
            string range = response.Headers["Range"];
            string []parts = range.Split('-');

            if (parts.Length > 1 && parts[1] != null)
            {
                result = long.Parse(parts[1]);
            }

            return result;
        }


        /// <summary>
        ///  worker method for the the resumable insert
        /// </summary>
        /// <param name="data"></param>
        /// <param name="asyncOp"></param>
        /// <param name="completionMethodDelegate"></param>
        /// <returns></returns>
        private void AsyncInsertWorker(AsyncResumableUploadData data, AsyncOperation asyncOp, SendOrPostCallback completionMethodDelegate)
        {
            try
            {
                if (data.Entry != null)
                {
                   using (var response = Insert(data.Authentication, data.Entry, data))
                    {
                        HandleResponseStream(data, response.GetResponseStream(), -1);
                    }
                }
                else
                {
                    using (var response = Insert(data.Authentication, data.UriToUse, data.DataStream, data.ContentType, data.Slug, data))
                    {
                        HandleResponseStream(data, response.GetResponseStream(), -1);
                    }
                }
            }
            catch (Exception e)
            {
                data.Exception = e;
            }

            this.CompletionMethodDelegate(data);
        }

        
        /// <summary>
        /// Uploads an entry, including it's media to the uri given inside the entry
        /// </summary>
        /// <param name="resumableUploadUri"></param>
        /// <param name="authentication">The authentication information to be used</param>
        /// <param name="payload">The entry to be uploaded. This is a complete entry, including the metadata. 
        /// This will create a new entry on the service</param>
        /// <returns></returns>
        public WebResponse Update(Authenticator authentication, AbstractEntry payload)
        {
            return Update(authentication, payload, null);
        }
        /// <summary>
        /// Uploads just the media media to the uri given.
        /// </summary>
        /// <param name="resumableUploadUri"></param>
        /// <param name="authentication">The authentication information to be used</param>
        /// <param name="payload">The media to uploaded.</param>
        /// <param name="contentType">The type of the content, e.g. text/html</param>
        /// <returns></returns>
        public WebResponse Update(Authenticator authentication, Uri resumableUploadUri, Stream payload, string contentType)
        {
            return Update(authentication, resumableUploadUri, payload, contentType, null);
        }

        private WebResponse Update(Authenticator authentication, AbstractEntry payload, AsyncData data)
        {
            Uri initialUri = ResumableUploader.GetResumableEditUri(payload.Links);
            if (initialUri == null)
                throw new ArgumentException("payload did not contain a resumabled edit media Uri");

            Uri resumeUri = InitiateUpload(initialUri, authentication, payload);
            return UploadStream(HttpMethods.Put, resumeUri, authentication, payload.MediaSource.Data, payload.MediaSource.ContentType, data);
        }

        private WebResponse Update(Authenticator authentication, Uri resumableUploadUri, Stream payload, string contentType, AsyncData data)
        {
            Uri resumeUri = InitiateUpload(resumableUploadUri, authentication, contentType, null, GetStreamLength(payload));
            return UploadStream(HttpMethods.Put, resumeUri, authentication, payload, contentType, data);
        }
        
        public void UpdateAsync(Authenticator authentication, Uri resumableUploadUri, Stream payload, string contentType, object userData)
        {
            AsyncResumableUploadData data = new AsyncResumableUploadData(this,
                                                authentication,
                                                resumableUploadUri,
                                                payload,
                                                contentType,
                                                null,
                                                HttpMethods.Put,
                                                this.ProgressReportDelegate,
                                                userData);
            WorkerResumableUploadHandler workerDelegate = new WorkerResumableUploadHandler(AsyncUpdateWorker);
            this.AsyncStarter(data, workerDelegate, userData);
        }

        public void UpdateAsync(Authenticator authentication, AbstractEntry payload, object userData)
        {
            AsyncResumableUploadData data = new AsyncResumableUploadData(this,
                                                authentication,
                                                payload,
                                                HttpMethods.Put,
                                                this.ProgressReportDelegate,
                                                userData);
            WorkerResumableUploadHandler workerDelegate = new WorkerResumableUploadHandler(AsyncUpdateWorker);
            this.AsyncStarter(data, workerDelegate, userData);
        }

        public WebResponse Resume(Authenticator authentication, Uri resumeUri, String httpMethod, Stream payload, string contentType)
        {
            return Resume(authentication, resumeUri, httpMethod, payload, contentType, null);
        }

        public void ResumeAsync(Authenticator authentication, Uri resumeUri, String httpmethod, Stream payload, string contentType, object userData)
        {
            AsyncResumableUploadData data = new AsyncResumableUploadData(this,
                                                authentication,
                                                resumeUri,
                                                payload,
                                                contentType,
                                                null,
                                                httpmethod,
                                                this.ProgressReportDelegate,
                                                userData);
            WorkerResumableUploadHandler workerDelegate = new WorkerResumableUploadHandler(AsyncResumeWorker);
            this.AsyncStarter(data, workerDelegate, userData);
        }

        private WebResponse Resume(Authenticator authentication, Uri resumeUri, 
                                    String httpmethod, Stream payload, string contentType,
                                    AsyncData data)
        {
            return UploadStream(httpmethod, resumeUri, authentication, payload, contentType, data);
        }

        /// <summary>
        ///  worker method for the the resume
        /// </summary>
        /// <param name="data"></param>
        /// <param name="asyncOp"></param>
        /// <param name="completionMethodDelegate"></param>
        /// <returns></returns>
        private void AsyncResumeWorker(AsyncResumableUploadData data, AsyncOperation asyncOp, SendOrPostCallback completionMethodDelegate)
        {
            try
            {

                using (var response = Resume(data.Authentication, data.UriToUse, data.HttpVerb, data.DataStream, data.ContentType, data))
                {
                    HandleResponseStream(data, response.GetResponseStream(), -1);
                }
            }
            catch (Exception e)
            {
                data.Exception = e;
            }

            this.CompletionMethodDelegate(data);
        }




        /// <summary>
        ///  worker method for the the resumable update
        /// </summary>
        /// <param name="data"></param>
        /// <param name="asyncOp"></param>
        /// <param name="completionMethodDelegate"></param>
        /// <returns></returns>
        private void AsyncUpdateWorker(AsyncResumableUploadData data, AsyncOperation asyncOp, SendOrPostCallback completionMethodDelegate)
        {
            try
            {
                if (data.Entry != null)
                {
                    using (var response = Update(data.Authentication, data.Entry, data))
                    {
                        HandleResponseStream(data, response.GetResponseStream(), -1);
                    }
                }
                else
                {
                    using (var response = Update(data.Authentication, data.UriToUse, data.DataStream, data.ContentType, data))
                    {
                        HandleResponseStream(data, response.GetResponseStream(), -1);
                    }
                }
            }
            catch (Exception e)
            {
                data.Exception = e;
            }

            this.CompletionMethodDelegate(data);
        }


 
        /// <summary>
        /// Note the URI passed in here, is the session URI obtained by InitiateUpload
        /// </summary>
        /// <param name="targetUri"></param>
        /// <param name="authentication"></param>
        /// <param name="payload"></param>
        /// <param name="mediaType"></param>
        public WebResponse UploadStream(string httpMethod, Uri sessionUri, Authenticator authentication, 
                                        Stream payload, string mediaType, AsyncData data)
        {
            HttpWebResponse returnResponse = null;
            // upload one part at a time
            int index = 0;
            bool isDone = false;


            // if the passed in stream is NOT at the beginning, we assume
            // that we are resuming
            try
            {
                // calculate a new index, we will resume in chunk sizes
                if (payload.Position != 0)
                {
                    index = (int)((double)payload.Position / (this.chunkSize * ResumableUploader.MB));
                }
            }
            catch (System.NotSupportedException e)
            {
                index = 0;
            }
  
            do
            {
                HttpWebResponse response;
                try
                {
                    response = UploadStreamPart(index, httpMethod, sessionUri, authentication, payload, mediaType, data);
                    if (data != null && CheckIfOperationIsCancelled(data.UserData) == true)
                    {
                        break;
                    }
              
                    index++;
                    {
                        int status = (int)response.StatusCode;
                        switch (status)
                        {

                            case 308:
                                isDone = false;
                                break;
                            case 200:
                            case 201:
                                isDone = true;
                                returnResponse = response;
                                break;
                            default:
                                throw new ApplicationException("Unexpected return code during resumable upload");

                        }

                    }
                }
                finally 
                {
                    response = null;
                }
            } while (isDone == false);
            return returnResponse;
        }

        private HttpWebResponse UploadStreamPart(int partIndex, string httpMethod, Uri sessionUri, 
                                    Authenticator authentication, Stream payload, string mediaType,
                                    AsyncData data)
        {
            HttpWebRequest request = authentication.CreateHttpWebRequest(httpMethod, sessionUri);
            request.AllowWriteStreamBuffering = false;
            request.Timeout = 600000;

            // write the data
            request.ContentType = mediaType;
            CopyData(payload, request, partIndex, data);
    
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            return response;
        }

        private long GetStreamLength(Stream s)
        {
            long result;
            
            try
            {
                result = s.Length;
            }
            catch (NotSupportedException e)
            {
                result = -1;
            }

            return result;

        }


        //////////////////////////////////////////////////////////////////////
        /// <summary>takes our copy of the stream, and puts it into the request stream
        /// returns FALSE when we are done by reaching the end of the input stream</summary> 
        //////////////////////////////////////////////////////////////////////
        protected bool CopyData(Stream input, HttpWebRequest request, int partIndex, AsyncData data)
        {
           
            long chunkCounter = 0;
            long chunkStart = partIndex * this.chunkSize * ResumableUploader.MB;
            long dataLength;

            dataLength = GetStreamLength(input);

            
            // calculate the range
            // move the source stream to the correct position
            input.Seek(chunkStart, SeekOrigin.Begin);

            const int size = 16384;
            byte[] bytes = new byte[size];
            int numBytes;

            // to reduce memory consumption, we read in 16K chunks
            // first calculate the contentlength. We can not modify
            // headers AFTER we started writing to the stream
            // Note: we want to read in chunksize*MB, but it might be less
            // if we have smaller files or are at the last chunk
            while ((numBytes = input.Read(bytes, 0, size)) > 0)
            {
                chunkCounter += numBytes;

                if (chunkCounter >= (this.chunkSize * ResumableUploader.MB))
                    break;
            }
            request.ContentLength = chunkCounter;
           
            // modify the content-range header
        
            string contentRange = String.Format("bytes {0}-{1}/{2}", chunkStart, chunkStart + chunkCounter - 1, dataLength > 0 ? dataLength.ToString() : "*");
            request.Headers.Add(HttpRequestHeader.ContentRange, contentRange);


            // stream it into the real request stream
            using (Stream req = request.GetRequestStream())
            {
                 // move the source stream to the correct position
                input.Seek(chunkStart, SeekOrigin.Begin);
                chunkCounter = 0;

                // to reduce memory consumption, we read in 16K chunks
            
                while ((numBytes = input.Read(bytes, 0, size)) > 0)
                {
                    req.Write(bytes, 0, numBytes);
                    chunkCounter += numBytes;

            
                    // while we are writing along, send notifications out
                    if (data != null)
                    {
                        if (CheckIfOperationIsCancelled(data.UserData) == true)
                        {
                            break;
                        }
                        else if (data.Delegate != null &&
                            data.DataHandler != null)
                        {
                            AsyncOperationProgressEventArgs args;
                            int current = (int)((double)(chunkStart + chunkCounter - 1) / dataLength * 100);

                            args = new AsyncOperationProgressEventArgs(dataLength, (chunkStart + chunkCounter - 1),
                                (int)current,
                                request.RequestUri,
                                request.Method,
                                data.UserData);
                            data.DataHandler.SendProgressData(data, args);
                        }
                    }

                    if (chunkCounter >= request.ContentLength)
                        break;

                }
            }

            return chunkCounter < (this.chunkSize * ResumableUploader.MB);
    
        }
        /////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// retrieves the resumable URI for the rest of the operation. This will initiate the 
        /// communication with resumable upload server by posting against the starting URI
        /// </summary>
        /// <param name="resumableUploadUri"></param>
        /// <param name="authentication"></param>
        /// <param name="entry"></param>
        /// <returns>The uri to be used for the rest of the operation</returns>
        public Uri InitiateUpload(Uri resumableUploadUri, Authenticator authentication, string contentType, string slug, long contentLength)
        {
            this.authenticator = authentication;

            HttpWebRequest request = PrepareRequest(resumableUploadUri, 
                                                    slug, 
                                                    contentType, 
                                                    contentLength);

            WebResponse response = request.GetResponse();
            return new Uri(response.Headers["Location"]);
        }


        /// <summary>
        /// retrieves the resumable URI for the rest of the operation. This will initiate the 
        /// communication with resumable upload server by posting against the starting URI
        /// </summary>
        /// <param name="resumableUploadUri"></param>
        /// <param name="authentication"></param>
        /// <param name="entry"></param>
        /// <returns>The uri to be used for the rest of the operation</returns>
        public Uri InitiateUpload(Uri resumableUploadUri, Authenticator authentication, AbstractEntry entry)
        {
            this.authenticator = authentication;

            HttpWebRequest request = PrepareRequest(resumableUploadUri,
                                                    entry.MediaSource.Name,
                                                    entry.MediaSource.ContentType,
                                                    entry.MediaSource.ContentLength);

            IVersionAware v = entry as IVersionAware;
            if (v != null)
            {
                // need to add the version header to the request
                request.Headers.Add(GDataGAuthRequestFactory.GDataVersion, v.ProtocolMajor.ToString() + "." + v.ProtocolMinor.ToString());
            }
            
            ISupportsEtag e = entry as ISupportsEtag;
            if (e != null && Utilities.IsWeakETag(e) == false)
            {
                request.Headers.Add(GDataRequestFactory.IfMatch, e.Etag);
             }

            Stream outputStream = request.GetRequestStream();
            entry.SaveToXml(outputStream);
            outputStream.Close();
 
            /// this is the contenttype for the xml post
            request.ContentType = GDataRequestFactory.DefaultContentType;

            WebResponse response = request.GetResponse();
            return new Uri(response.Headers["Location"]);
        }

        private HttpWebRequest PrepareRequest(Uri target, string slug, string contentType, long contentLength)
        {
            HttpWebRequest request = this.authenticator.CreateHttpWebRequest(HttpMethods.Post, target);
            request.Headers.Add(GDataRequestFactory.SlugHeader + ": " + slug);
            request.Headers.Add(GDataRequestFactory.ContentOverrideHeader + ": " + contentType);
            if (contentLength != -1)
            {
                request.Headers.Add(GDataRequestFactory.ContentLengthOverrideHeader + ": " + contentLength);
            }

            return request;
        
        }


        /// <summary>
        /// starts the async job
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userData"></param>
        /// <param name="workerDelegate"></param>
        /// <returns></returns>
        private void AsyncStarter(AsyncResumableUploadData data, WorkerResumableUploadHandler workerDelegate, Object userData)
        {
            AsyncOperation asyncOp = AsyncOperationManager.CreateOperation(userData);

            data.Operation = asyncOp;

            AddUserDataToDictionary(userData, asyncOp);


            // Start the asynchronous operation.
            workerDelegate.BeginInvoke(
                data,
                asyncOp,
                this.CompletionMethodDelegate,
                null,
                null);
        }

    }
}
