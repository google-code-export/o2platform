using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.CompilerServices;
using System.IO;
using System.Web;
using System.Runtime.Serialization.Json;

using TheWorldsWorst.ApiWrapper.Helpers;
using TheWorldsWorst.ApiWrapper.Model;
using System.IO.Compression;

//O2Dir:C:\Documents and Settings\Administrator\My Documents\Downloads\StackOverflow_API\theworldsworststackoverflowclone\TheWorldsWorstApiWrapper
//O2Ref:System.dll
//O2Ref:System.Core.dll
//O2Ref:System.Data.dll
//O2Ref:System.Data.DataSetExtensions.dll
//O2Ref:System.RunTime.Serialization.dll
//O2Ref:System.ServiceModel.Web.dll
//O2Ref:System.Web.dll
//O2Ref:System.Xml.dll
//O2Ref:System.Xml.Linq.dll

using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;

namespace TheWorldsWorst.ApiWrapper
{	
    public class ApiException : Exception { public ApiException(string msg) : base(msg) { } }
	

    /// <summary>
    /// Exposes StackOverflow API functionality.
    /// </summary>
    public class ApiProxy
    {
    
    	public static bool DownloadUsingGzipEncoding { get; set; }   	//DC
    	public static string DefaultUserAgent { get; set; } 			//DC
        public static readonly string EXPECTED_VERSION = "0.8";

        public static string ErrorCode { get; private set; }

        internal static string m_website = "http://api.stackoverflow.com/" + EXPECTED_VERSION;
        public static string M_Key { get; set; }

        private static WebClient _client = new WebClient();
        public static int? RemainingRequests = null;
        public static int? MaximumRequests = null;

		//DC - added constructor and started changing global fields into properties
		public ApiProxy()
		{
			
			DownloadUsingGzipEncoding = true;			// this is the current default (which doesn't work when using Fiddler)
			DefaultUserAgent = "O2Platform Version of WorldsWorstStackOverflowClone";
			M_Key = "iv1qWOxcwkaKg53RJIXh-A";			
		}
		
		
        private static string ReadResponse(Stream s)
        {        
            using (GZipStream stream = new GZipStream(s, CompressionMode.Decompress))
            {

                var ret = new List<byte>();
                int i;
                while ((i = stream.ReadByte()) != -1) ret.Add((byte)i);

                string retStr = Encoding.UTF8.GetString(ret.ToArray());

                if (!retStr.EndsWith("\n"))
                {
                    ErrorCode = "Unknown";
                    throw new ApiException(ErrorCode);
                }

                if (_client.ResponseHeaders != null)
                {
                    if (_client.ResponseHeaders.AllKeys.Contains("X-RateLimit-Current"))
                        RemainingRequests = Int32.Parse(_client.ResponseHeaders["X-RateLimit-Current"]);
                    if (_client.ResponseHeaders.AllKeys.Contains("X-RateLimit-Max"))
                        MaximumRequests = Int32.Parse(_client.ResponseHeaders["X-RateLimit-Max"]);
                }

                return retStr;
            }
        } 

        /// <summary>
        /// Get a raw string response from an API call on StackOverflow
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static string GetUrl(string path)
        {
            string finalUrl;

            if (path.Contains('?'))
                finalUrl = String.Format("{0}{1}&key={2}", m_website, path, M_Key);
            else
                finalUrl = String.Format("{0}{1}?key={2}", m_website, path, M_Key);

            _client.Headers[HttpRequestHeader.UserAgent] = DefaultUserAgent;
            if (DownloadUsingGzipEncoding)
            {
            	"Downloading using Gzip encoding".error();
            	_client.Headers[HttpRequestHeader.AcceptEncoding] = "gzip";
            }
            _client.Encoding = Encoding.UTF8;

            try
            {                            
				if (DownloadUsingGzipEncoding)
				{
					byte[] resp = _client.DownloadData(finalUrl);
                	return ReadResponse(new MemoryStream(resp));
                }
                return finalUrl.uri().getHtml();						//DC
            }
            catch (WebException ex)
            {
                try
                {                	
                	ex.log("[SOAPI] in ApiProxy.cs GetUrl",true); 	
                    using (var stream = ex.Response.GetResponseStream())
                    {
                        var resp = ReadResponse(stream);
                        var errWrap = ReadObject<Error>(resp);
                        var err = ReadSingleField(errWrap) as Error;
                        ErrorCode = err.Code.ToString();
                    }
                }
                catch (Exception ex2)
                {
                	ex2.log("[SOAPI] in ApiProxy.cs GetUrl",true); 
                    ErrorCode = "Unknown";
                }
            }
            catch (Exception ex3)
            {
            	ex3.log("[SOAPI] in ApiProxy.cs GetUrl",true); 
                ErrorCode = "Unknown";
            }

            throw new ApiException(ErrorCode); 		//DC
            return "";									//DC
        }

        /// <summary>
        /// Read the (only) property on the given object, and return its value
        /// </summary>
        /// <param name="toRead"></param>
        /// <returns></returns>
        internal static object ReadSingleField(object toRead)
        {
            var fields = toRead.GetType().GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            if (fields.Length != 1) throw new Exception("Found more than one field on 'toRead'");

            return fields[0].GetValue(toRead);
        }

        internal static object ReadObject<T>(string data) where T : HasWrapperClass, new()
        {
            DataContractJsonSerializer serial = new DataContractJsonSerializer((new T()).WrapperClass);
            object read;
            using (var mem = new MemoryStream(Encoding.UTF8.GetBytes(data)))
            {
                read = serial.ReadObject(mem);
            }

            return read;
        }

        /// <summary>
        /// Query StackOverflow for a JSON object
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        internal static T GetObject<T>(string path) where T : HasWrapperClass, new()
        {
        	var objects = GetObjects<T>(path).toList();
        	return objects.first();
            //return GetObjects<T>(path)[0];
        }

        /// <summary>
        /// Query StackOverflow for a set of JSON objects
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        internal static T[] GetObjects<T>(string path) where T : HasWrapperClass, new()
        {
        	"[API_StackOverflow] Fetching data from Url:{0}".info(path);
            string data = GetUrl(path);
			
			if (data.size() == 0)
			{
				"[API_StackOverflow] in ApiProxy.GetObjects(...) there was no data fetched from Url".error();
				return new List<T>().ToArray();
			}
			"[API_StackOverflow] received data size:{0}".info(data.size());
            var read = ReadObject<T>(data);

            return ReadSingleField(read) as T[];
        }

        /// <summary>
        /// Get a list of "active" questions
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Question> GetActiveQuestions(int pageNum, int pageSize)
        {
            var resp = GetObjects<Question>(
                String.Format(
                    "/questions?page={0}&pagesize={1}&body=true&comments=true&sort=activity", 
                    pageNum, 
                    pageSize
            ));

            return resp;
        }

        /// <summary>
        /// Get a list of "active" questions
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Question> GetActiveQuestions(int pageNum, int pageSize, DateTime min, DateTime max)
        {
            var resp = GetObjects<Question>(
                String.Format(
                    "/questions?page={0}&pagesize={1}&body=true&comments=true&sort=activity&min={2}&max={3}",
                    pageNum,
                    pageSize,
                    min.ToUnixTime(),
                    max.ToUnixTime()
            ));

            return resp;
        }

        /// <summary>
        /// Get a list of "newest" questions
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Question> GetNewestQuestions(int pageNum, int pageSize)
        {
            var resp = GetObjects<Question>(
                String.Format(
                    "/questions?page={0}&pagesize={1}&body=true&comments=true&sort=creation",
                    pageNum,
                    pageSize
            ));


            return resp;
        }

        /// <summary>
        /// Get a list of "newest" questions
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Question> GetNewestQuestions(int pageNum, int pageSize, DateTime min, DateTime max)
        {
            var resp = GetObjects<Question>(
                String.Format(
                    "/questions?page={0}&pagesize={1}&body=true&comments=true&sort=creation&min={2}&max={3}",
                    pageNum,
                    pageSize,
                    min.ToUnixTime(),
                    max.ToUnixTime()
            ));


            return resp;
        }

        /// <summary>
        /// Get a list of "newest" questions
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Question> GetFeaturedQuestions(int pageNum, int pageSize)
        {
            var resp = GetObjects<Question>(
                String.Format(
                    "/questions?page={0}&pagesize={1}&body=true&comments=true&sort=featured",
                    pageNum,
                    pageSize
            ));


            return resp;
        }

        /// <summary>
        /// Get a list of "newest" questions
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Question> GetFeaturedQuestions(int pageNum, int pageSize, DateTime min, DateTime max)
        {
            var resp = GetObjects<Question>(
                String.Format(
                    "/questions?page={0}&pagesize={1}&body=true&comments=true&sort=featured&min={2}&max={3}",
                    pageNum,
                    pageSize,
                    min.ToUnixTime(),
                    max.ToUnixTime()
            ));


            return resp;
        }

        /// <summary>
        /// Get a list of "newest" questions
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Question> GetHotQuestions(int pageNum, int pageSize)
        {
            var resp = GetObjects<Question>(
                String.Format(
                    "/questions?page={0}&pagesize={1}&body=true&comments=true&sort=hot",
                    pageNum,
                    pageSize
            ));


            return resp;
        }

        /// <summary>
        /// Get a list of "newest" questions
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Question> GetHotQuestions(int pageNum, int pageSize, DateTime min, DateTime max)
        {
            var resp = GetObjects<Question>(
                String.Format(
                    "/questions?page={0}&pagesize={1}&body=true&comments=true&sort=hot&min={2}&max={3}",
                    pageNum,
                    pageSize,
                    min.ToUnixTime(),
                    max.ToUnixTime()
            ));


            return resp;
        }

        /// <summary>
        /// Get all the Answers on the given question
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public static IEnumerable<Answer> GetAnswersFor(Question q)
        {
            var resp = GetObject<Question>(String.Format("/questions/{0}?body=true&comments=true", q.QuestionId));

            return resp.Answers;
        }

        /// <summary>
        /// Get a user given their id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static User GetUserFor(int id)
        {
            var resp = GetObject<User>(String.Format("/users/{0}", id));

            return resp;
        }

        /// <summary>
        /// Get the questions a user has asked, given their id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static IEnumerable<Question> GetUsersActiveQuestions(int id)
        {
            var resp = GetObjects<Question>(String.Format("/users/{0}/questions?sort=activity&order=desc&body=true&comments=true", id));

            return resp;
        }

        /// <summary>
        /// Get the questions a user has asked, given their id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static IEnumerable<Question> GetUsersQuestionsByViews(int id)
        {
            var resp = GetObjects<Question>(String.Format("/users/{0}/questions?sort=views&order=desc&body=true&comments=true", id));

            return resp;
        }

        /// <summary>
        /// Get the questions a user has asked, given their id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static IEnumerable<Question> GetUsersQuestionsByVotes(int id)
        {
            var resp = GetObjects<Question>(String.Format("/users/{0}/questions?sort=votes&order=desc&body=true&comments=true", id));

            return resp;
        }

        /// <summary>
        /// Get the questions a user has asked, given their id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static IEnumerable<Question> GetUsersQuestionsByCreation(int id)
        {
            var resp = GetObjects<Question>(String.Format("/users/{0}/questions?sort=creation&order=desc&body=true&comments=true", id));

            return resp;
        }

        /// <summary>
        /// Get the revisions for a post, given an id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static IEnumerable<Revision> GetRevisionsByPost(int id)
        {
            var resp = GetObjects<Revision>(String.Format("/revisions/{0}", id));

            return resp;
        }


        /// <summary>
        /// Get the revisions for a post, given an id and a revision guid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static IEnumerable<Revision> GetRevisionsByPost(int id, Guid revisionGuid)
        {
            var resp = GetObjects<Revision>(String.Format("/revisions/{0}/{1}", id, revisionGuid));

            return resp;
        }        

        /// <summary>
        /// Get a count of a user's badges
        /// </summary>
        /// <param name="id"></param>
        /// <param name="m_gold"></param>
        /// <param name="m_silver"></param>
        /// <param name="m_bronze"></param>
        /// <returns></returns>
        public static void GetBadgesForUser(long id, out int m_gold, out int m_silver, out int m_bronze)
        {
            var resp = GetObjects<Badge>(String.Format("/users/{0}/badges", id));

            m_gold   = resp.Sum(p => p.Class == Badge.BadgeClass.Gold   ? p.AwardCount : 0);
            m_silver = resp.Sum(p => p.Class == Badge.BadgeClass.Silver ? p.AwardCount : 0);
            m_bronze = resp.Sum(p => p.Class == Badge.BadgeClass.Bronze ? p.AwardCount : 0);
        }

        /// <summary>
        /// Get a list of questions with the given tag, starting from a given page of a given size.
        /// </summary>
        /// <param name="tagged"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IEnumerable<Question> GetTaggedQuestions(string tagged, int pageNumber, int pageSize)
        {
            var resp = GetObjects<Question>(String.Format("/questions/tagged/{0}?page={1}&pagesize={2}&comments=true", HttpUtility.UrlEncode(tagged), pageNumber, pageSize));

            return resp;
        }

        /// <summary>
        /// Get a given page of "unanswered" questions.
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IEnumerable<Question> GetUnansweredQuestions(int pageNum, int pageSize)
        {
            var resp = GetObjects<Question>(
                String.Format(
                    "/unanswered?page={0}&pagesize={1}&comments=true",
                    pageNum,
                    pageSize
            ));


            return resp;
        }

        /// <summary>
        /// Get a question, with a subset of its answers depending on the pageNum &amp; pageSize provided.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static Question GetQuestionFor(int id, int pageNum, int pageSize)
        {
            var obj = GetObject<Question>(String.Format("/questions/{0}?body=true&comments=true&pagesize={1}&page={2}", id, pageSize, pageNum));

            return obj;
        }

        /// <summary>
        /// Get a question given its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Question GetQuestionFor(int id)
        {
            return GetQuestionFor(true, id);
        }
        public static Question GetQuestionFor(bool getComments, int id)
        {
            return GetQuestionsFor(getComments,id).ToArray()[0];
        }

        /// <summary>
        /// Get a Users answers.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static IEnumerable<Answer> GetAnswersFor(User user)
        {
            var objs = GetObjects<Answer>(String.Format("/users/{0}/answers?sort=activity&order=desc&comments=true", user.UserId));

            return objs;
        }

        /// <summary>
        /// Get a Users answers ordered by views
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static IEnumerable<Answer> GetAnswersByViewsFor(User user)
        {
            var objs = GetObjects<Answer>(String.Format("/users/{0}/answers?sort=views&order=desc&comments=true", user.UserId));

            return objs;
        }

        /// <summary>
        /// Get a Users answers.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static IEnumerable<Answer> GetAnswersByCreationFor(User user)
        {
            var objs = GetObjects<Answer>(String.Format("/users/{0}/answers?sort=creation&order=desc&comments=true", user.UserId));

            return objs;
        }

        /// <summary>
        /// Get a Users answers.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static IEnumerable<Answer> GetAnswersByVotesFor(User user)
        {
            var objs = GetObjects<Answer>(String.Format("/users/{0}/answers?sort=votes&order=desc&comments=true", user.UserId));

            return objs;
        }

        /// <summary>
        /// Get an answer given its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Answer GetAnswerFor(int id)
        {
            return GetAnswersFor(id).ToArray()[0];
        }

        /// <summary>
        /// Get the badges a user has.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static IEnumerable<Badge> GetBadgeListFor(User user)
        {
            return GetBadgeListFor(user.UserId);
        }

        /// <summary>
        /// Get the badges a user has, given the user id.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static IEnumerable<Badge> GetBadgeListFor(int uid)
        {
            var objs = GetObjects<Badge>(String.Format("/users/{0}/badges", uid));

            return objs;
        }

        /// <summary>
        /// Get the tags a user is active in.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static IEnumerable<Tag> GetTagListFor(User user)
        {
            var objs = GetObjects<Tag>(String.Format("/users/{0}/tags", user.UserId));

            return objs;
        }

        /// <summary>
        /// Get the newest questions (accounting for a page & pageSize offset) with the given tag.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IEnumerable<Question> GetNewestQuestionsWithTag(string name, int page, int pageSize)
        {
            var objs = GetObjects<Question>(String.Format("/questions/tagged/{0}?page={1}&pagesize={2}&comments=true", HttpUtility.UrlEncode(name), page, pageSize));

            return objs;
        }

        /// <summary>
        /// Get all "normal" badges.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Badge> GetNonTagBadges()
        {
            var objs = GetObjects<Badge>("/badges");

            return objs;
        }

        /// <summary>
        /// Get all "tag" badges.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Badge> GetTagBadges()
        {
            var objs = GetObjects<Badge>("/badges/tags");

            return objs;
        }

        /// <summary>
        /// Get a badge given its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Badge GetBadgeFor(int id)
        {
            var ret = GetNonTagBadges().SingleOrDefault(p => p.BadgeId == id);

            if (ret == null)
                return GetTagBadges().SingleOrDefault(p => p.BadgeId == id);

            return ret;
        }

        /// <summary>
        /// Get a list of unanswered questions (starting at pageNum for pageSize pages) ordered by votes
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IEnumerable<Question> GetUnansweredQuestionsByVotes(int pageNum, int pageSize)
        {
            var objs = GetObjects<Question>(String.Format("/questions/unanswered?page={0}&pagesize={1}&sort=votes&comments=true", pageNum, pageSize));

            return objs;
        }

        /// <summary>
        /// Get a list of unanswered questions (starting at pageNum for pageSize pages) ordered by age
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IEnumerable<Question> GetUnansweredQuestionsByAge(int pageNum, int pageSize)
        {
            var objs = GetObjects<Question>(String.Format("/questions/unanswered?page={0}&pagesize={1}&sort=newest&comments=true", pageNum, pageSize));

            return objs;
        }

        /// <summary>
        /// Gets the given page of tags, ordered by name, with the given filter
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static IEnumerable<Tag> GetTagsByName(int pageNum, int pageSize, string filter)
        {
            var objs = GetObjects<Tag>(
                String.Format("/tags?page={0}&pageSize={1}&filter={2}&sort=name&order=asc",
                pageNum,
                pageSize,
                filter ?? string.Empty));

            return objs;
        }

        /// <summary>
        /// Gets the given page of tags, ordered by recent activity, with the given filter
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static IEnumerable<Tag> GetTagsByActivity(int pageNum, int pageSize, string filter)
        {
            var objs = GetObjects<Tag>(
                String.Format("/tags?page={0}&pageSize={1}&filter={2}&sort=activity&order=desc",
                pageNum,
                pageSize,
                filter ?? string.Empty));

            return objs;
        }

        /// <summary>
        /// Gets the given page of tags, ordered by name, with the given filter
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static IEnumerable<Tag> GetTagsByPopularity(int pageNum, int pageSize, string filter)
        {
            var objs = GetObjects<Tag>(
                String.Format("/tags?page={0}&pageSize={1}&filter={2}&sort=popular&order=desc",
                pageNum,
                pageSize,
                filter ?? string.Empty));

            return objs;
        }

        /// <summary>
        /// Get a timeline for the given question (by id).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static IEnumerable<PostEvent> GetQuestionTimeline(int id)
        {
            var objs = GetObjects<PostEvent>(String.Format("/questions/{0}/timeline", id));

            return objs;
        }

        /// <summary>
        /// Get a user's activities (by id) between the two dates.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static IEnumerable<UserEvent> GetUserEvents(int id, DateTime from, DateTime to)
        {
            var user = GetUserFor(id);

            var objs = GetObjects<UserEvent>(String.Format("/users/{0}/timeline?fromdate={1}&todate={2}", id, from.ToUnixTime(), to.ToUnixTime()));

            return objs.Select(
                delegate(UserEvent p)
                {
                    p.UserName = user.Name;
                    p.UserId = (int)user.UserId;
                    return p;
                });
        }

        /// <summary>
        /// Get the comments a user has made, ordered by creation date.
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IEnumerable<Comment> GetUserCommentsByCreation(int uid, int pageNum, int pageSize)
        {
            var objs = GetObjects<Comment>(String.Format("/users/{0}/comments?sort=creation&order=desc&page={1}&pagesize={2}", uid, pageNum, pageSize));

            return objs;
        }

        /// <summary>
        /// Get the comments a user has made, ordered by votes.
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IEnumerable<Comment> GetUserCommentsByVotes(int uid, int pageNum, int pageSize)
        {
            var objs = GetObjects<Comment>(String.Format("/users/{0}/comments?sort=votes&order=desc&page={1}&pagesize={2}", uid, pageNum, pageSize));

            return objs;
        }

        /// <summary>
        /// Get the comments one user has directed to the other.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IEnumerable<Comment> GetCommentsBetweenUsers(int from, int to, int pageNum, int pageSize)
        {
            var objs = GetObjects<Comment>(String.Format("/users/{0}/comments/{1}?sort=votes&order=desc&page={2}&pagesize={3}", from, to, pageNum, pageSize));

            return objs;
        }

        /// <summary>
        /// Get the questions a user favorited between the given dates.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static IEnumerable<Question> GetUserFavorites(int id, DateTime from, DateTime to)
        {
            var objs = GetObjects<Question>(String.Format("/users/{0}/favorites?fromdate={1}&todate={2}&comments=true&sort=added", id, from.ToUnixTime(), to.ToUnixTime()));

            return objs;
        }

        /// <summary>
        /// Get the questions in a user's favorites, ordered by recent activity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IEnumerable<Question> GetUserFavoritesByActivity(int id, int pageNum, int pageSize)
        {
            var objs = GetObjects<Question>(String.Format("/users/{0}/favorites?sort=activity&order=desc&page={1}&pagesize={2}", id, pageNum, pageSize));

            return objs;
        }

        /// <summary>
        /// Get the questions in a user's favorites, ordered by view count
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IEnumerable<Question> GetUserFavoritesByViews(int id, int pageNum, int pageSize)
        {
            var objs = GetObjects<Question>(String.Format("/users/{0}/favorites?sort=views&order=desc&page={1}&pagesize={2}", id, pageNum, pageSize));

            return objs;
        }

        /// <summary>
        /// Get the questions in a user's favorites, ordered by creation date
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IEnumerable<Question> GetUserFavoritesByCreation(int id, int pageNum, int pageSize)
        {
            var objs = GetObjects<Question>(String.Format("/users/{0}/favorites?sort=creation&order=desc&page={1}&pagesize={2}", id, pageNum, pageSize));

            return objs;
        }

        /// <summary>
        /// Get the questions in a user's favorites, ordered by date favorited
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IEnumerable<Question> GetUserFavoritesByAdded(int id, int pageNum, int pageSize)
        {
            var objs = GetObjects<Question>(String.Format("/users/{0}/favorites?sort=added&order=desc&page={1}&pagesize={2}", id, pageNum, pageSize));

            return objs;
        }

        /// <summary>
        /// Get the questions in a user's favorites, ordered by votes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IEnumerable<Question> GetUserFavoritesByVotes(int id, int pageNum, int pageSize)
        {
            var objs = GetObjects<Question>(String.Format("/users/{0}/favorites?sort=votes&order=desc&page={1}&pagesize={2}", id, pageNum, pageSize));

            return objs;
        }

        /// <summary>
        /// Get a questions events between the two dates.
        /// </summary>
        /// <param name="q"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static IEnumerable<PostEvent> GetQuestionEvents(Question q, DateTime from, DateTime to)
        {
            var objs = GetObjects<PostEvent>(String.Format("/questions/{0}/timeline?fromdate={1}&todate={2}", q.QuestionId, from.ToUnixTime(), to.ToUnixTime()));

            return objs;
        }

        /// <summary>
        /// Get comments which were directed at the given user, between the two dates.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static IEnumerable<Comment> GetUserMentions(int id, DateTime from, DateTime to)
        {
            var objs = GetObjects<Comment>(String.Format("/users/{0}/mentioned?fromdate={1}&todate={2}", id, from.ToUnixTime(), to.ToUnixTime()));

            return objs;
        }

        /// <summary>
        /// Returns true if the API is the expected version.
        /// </summary>
        /// <returns></returns>
        public static bool CheckVersion()
        {
            var obj = GetObjects<Stats>("/stats");

            return obj[0].ApiVersion.Version.ToString() == EXPECTED_VERSION;
        }

        /// <summary>
        /// Get a page of users of the given size, ordered by reputation.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IEnumerable<User> GetUsersByReputation(string filter, int pageNum, int pageSize)
        {
            var objs = GetObjects<User>(String.Format("/users?filter={0}&page={1}&pagesize={2}&sort=reputation", filter, pageNum, pageSize));

            return objs;
        }

        /// <summary>
        /// Get a page of users of the given size, ordered by name.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IEnumerable<User> GetUsersByName(string filter, int pageNum, int pageSize)
        {
            var objs = GetObjects<User>(String.Format("/users?filter={0}&page={1}&pagesize={2}&sort=name", filter, pageNum, pageSize));

            return objs;
        }

        /// <summary>
        /// Get a page of users of the given size, ordered by ascending age.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IEnumerable<User> GetNewestUsers(string filter, int pageNum, int pageSize)
        {
            var objs = GetObjects<User>(String.Format("/users?filter={0}&page={1}&pagesize={2}&sort=creation&order=desc", filter, pageNum, pageSize));

            return objs;
        }

        /// <summary>
        /// Get a page of users of the given size, ordered by descending age.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IEnumerable<User> GetOldestUsers(string filter, int pageNum, int pageSize)
        {
            var objs = GetObjects<User>(String.Format("/users?filter={0}&page={1}&pagesize={2}&sort=creation&order=asc", filter, pageNum, pageSize));

            return objs;
        }

        /// <summary>
        /// Get a comment given its id.
        /// </summary>
        /// <param name="id"></param>
        public static Comment GetCommentFor(int id)
        {
            return GetCommentsFor(id).ToArray()[0];
        }

        /// <summary>
        /// Get a user's reputation change over the given timeframe.
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public static IEnumerable<Reputation> GetReputationGraphFor(int uid, DateTime fromDate, DateTime toDate, int pageNum, int pageSize)
        {
            var objs = GetObjects<Reputation>(String.Format("/users/{0}/reputation?fromdate={1}&todate={2}&page={3}&pagesize={4}", uid, fromDate.ToUnixTime(), toDate.ToUnixTime(), pageNum, pageSize));

            return objs;
        }

        public static IEnumerable<Question> GetQuestionsFor(params int[] ids)
        {
            return GetQuestionsFor(true, ids);
        }
        public static IEnumerable<Question> GetQuestionsFor(bool getComments, params int[] ids)
        {
            string q = string.Join(";", ids.Select(p => p.ToString()).ToArray());

            var objs = GetObjects<Question>(String.Format("/questions/{0}?body=true&comments={1}", q,getComments));

            return objs;
        }

        public static IEnumerable<Answer> GetAnswersFor(params int[] ids)
        {
            string q = string.Join(";", ids.Select(p => p.ToString()).ToArray());

            //var objs = GetObjects<Answer>(String.Format("/answers/{0}?comments=true", q));
            var objs = GetObjects<Answer>(String.Format("/answers/{0}?body=true&comments=true", q));  //DC

            return objs;
        }

        public static IEnumerable<Comment> GetCommentsFor(params int[] ids)
        {
            string q = string.Join(";", ids.Select(p => p.ToString()).ToArray());

            var objs = GetObjects<Comment>(String.Format("/comments/{0}", q));

            return objs;
        }

        public static IEnumerable<PostEvent> GetPostTimelines(params int[] ids)
        {
            string q = string.Join(";", ids.Select(p => p.ToString()).ToArray());

            var objs = GetObjects<PostEvent>(String.Format("/questions/{0}/timeline", q));

            return objs;
        }

        public static IEnumerable<Revision> GetRevisions(params int[] ids)
        {
            string q = string.Join(";", ids.Select(p => p.ToString()).ToArray());

            var objs = GetObjects<Revision>(String.Format("/revisions/{0}", q));

            return objs;
        }

        public static IEnumerable<User> GetUsersFor(params int[] ids)
        {
            string q = string.Join(";", ids.Select(p => p.ToString()).ToArray());

            var objs = GetObjects<User>(String.Format("/users/{0}", q));

            return objs;
        }

        public static IEnumerable<Question> GetQuestionsForUsers(params int[] ids)
        {
            string q = string.Join(";", ids.Select(p => p.ToString()).ToArray());

            var objs = GetObjects<Question>(String.Format("/users/{0}/questions", q));

            return objs;
        }

        public static IEnumerable<Answer> GetAnswersForUsers(params int[] ids)
        {
            string q = string.Join(";", ids.Select(p => p.ToString()).ToArray());

            var objs = GetObjects<Answer>(String.Format("/users/{0}/answers", q));

            return objs;
        }

        public static IEnumerable<Question> GetFavoritesForUsers(params int[] ids)
        {
            string q = string.Join(";", ids.Select(p => p.ToString()).ToArray());

            var objs = GetObjects<Question>(String.Format("/users/{0}/favorites", q));

            return objs;
        }

        public static IEnumerable<Tag> GetTagListForUsers(params int[] ids)
        {
            string q = string.Join(";", ids.Select(p => p.ToString()).ToArray());

            int page = 1;

            Tag[] objs;

            var ret = new List<Tag>();

            do
            {
                objs = GetObjects<Tag>(String.Format("/users/{0}/tags?page={1}&pagesize=100", q, page));
                page++;

                ret.AddRange(objs);
            } while (objs.Length == 100);

            return ret;
        }

        public static IEnumerable<Badge> GetBadgeListForUsers(params int[] ids)
        {
            string q = string.Join(";", ids.Select(p => p.ToString()).ToArray());

            var objs = GetObjects<Badge>(String.Format("/users/{0}/badges", q));

            return objs;
        }

        public static IEnumerable<Comment> GetCommentsMentioning(int pageNum, int pageSize, params int[] ids)
        {
            string q = string.Join(";", ids.Select(p => p.ToString()).ToArray());

            var objs = GetObjects<Comment>(String.Format("/users/{0}/mentioned?page={1}&pagesize={2}", q, pageNum, pageSize));

            return objs;
        }

        public static IEnumerable<Reputation> GetReputationGraphForUsers(params int[] ids)
        {
            string q = string.Join(";", ids.Select(p => p.ToString()).ToArray());

            var objs = GetObjects<Reputation>(String.Format("/users/{0}/reputation", q));

            return objs;
        }

        public static void CauseError(int error)
        {
            GetObject<Error>(String.Format("/errors/{0}", error));
        }

        public static IEnumerable<Comment> GetUserCommentsFor(int pageNum, int pageSize, params int[] ids)
        {
            string q = string.Join(";", ids.Select(p => p.ToString()).ToArray());

            var objs = GetObjects<Comment>(String.Format("/users/{0}/comments?page={1}&pagesize={2}", q, pageNum, pageSize));

            return objs;
        }

        public static IEnumerable<User> GetBadgeRecipientsFor(params int[] ids)
        {
            string q = string.Join(";", ids.Select(p => p.ToString()).ToArray());

            var objs = GetObjects<User>(String.Format("/badges/{0}", q));

            return objs;
        }

        public static IEnumerable<Question> SearchQuestions(string title, IEnumerable<string> tagged, IEnumerable<string> notTagged, string sort, string order)
        {
            string taggedStr = string.Empty;
            string notTaggedStr = string.Empty;

            if (tagged != null)
                taggedStr = string.Join(";", tagged.ToArray());

            if(notTagged != null)
                notTaggedStr = string.Join(";", notTagged.ToArray());

            var objs = GetObjects<Question>(String.Format("/search?intitle={0}&tagged={1}&nottagged={2}&sort={3}&order={4}", title, taggedStr, notTaggedStr, sort, order));

            return objs;
        }

        public static IEnumerable<Question> SearchQuestionsWithRange(string title, IEnumerable<string> tagged, IEnumerable<string> notTagged, string sort, string order, long min, long max)
        {
            string taggedStr = string.Empty;
            string notTaggedStr = string.Empty;

            if (tagged != null)
                taggedStr = string.Join(" ", tagged.ToArray());

            if (notTagged != null)
                notTaggedStr = string.Join(" ", notTagged.ToArray());

            var objs = GetObjects<Question>(String.Format("/search?intitle={0}&tagged={1}&nottagged={2}&sort={3}&order={4}&min={5}&max={6}", title, taggedStr, notTaggedStr, sort, order, min, max));

            return objs;
        }

        public static IEnumerable<Comment> GetUserCommentsByVotesWithRange(int uid, int pageNum, int pageSize, int min, int max)
        {
            var objs = GetObjects<Comment>(String.Format("/users/{0}/comments?sort=votes&order=desc&page={1}&pagesize={2}&min={3}&max={4}", uid, pageNum, pageSize, min, max));

            return objs;
        }

        public static IEnumerable<Comment> GetUserCommentsByCreationWithRange(int uid, int pageNum, int pageSize, DateTime start, DateTime stop)
        {
            var objs = GetObjects<Comment>(String.Format("/users/{0}/comments?sort=creation&order=desc&page={1}&pagesize={2}&min={3}&max={4}", uid, pageNum, pageSize, start.ToUnixTime(), stop.ToUnixTime()));

            return objs;
        }

        public static IEnumerable<Tag> GetTagsByPopularityWithRange(int min, int max, int pageSize, int pageNum, string filter)
        {
            var objs = GetObjects<Tag>(
                String.Format("/tags?page={0}&pageSize={1}&filter={2}&sort=popular&order=desc&min={3}&max={4}",
                pageNum,
                pageSize,
                filter ?? string.Empty,
                min,
                max));

            return objs;
        }

        public static IEnumerable<Answer> GetRecentAnswersWithRange(DateTime min, DateTime max, User user)
        {
            var objs = GetObjects<Answer>(String.Format("/users/{0}/answers?sort=activity&order=desc&comments=true&min={1}&max={2}", user.UserId, min.ToUnixTime(), max.ToUnixTime()));

            return objs;
        }

        public static IEnumerable<Answer> GetAnswersByViewsForWithRange(int min, int max, User user)
        {
            var objs = GetObjects<Answer>(String.Format("/users/{0}/answers?sort=views&order=desc&comments=true&min={1}&max={2}", user.UserId, min, max));

            return objs;
        }

        public static IEnumerable<Answer> GetAnswersByCreationForWithRange(DateTime min, DateTime max, User user)
        {
            var objs = GetObjects<Answer>(String.Format("/users/{0}/answers?sort=creation&order=desc&comments=true&min={1}&max={2}", user.UserId, min.ToUnixTime(), max.ToUnixTime()));

            return objs;
        }

        public static IEnumerable<Answer> GetAnswersByVotesForWithRange(int min, int max, User user)
        {
            var objs = GetObjects<Answer>(String.Format("/users/{0}/answers?sort=votes&order=desc&comments=true&min={1}&max={2}", user.UserId, min, max));

            return objs;
        }

        public static IEnumerable<Question> GetUserFavoritesByActivityWithRange(DateTime min, DateTime max, int id, int pageNum, int pageSize)
        {
            var objs = GetObjects<Question>(String.Format("/users/{0}/favorites?sort=activity&order=desc&page={1}&pagesize={2}&min={3}&max={4}", id, pageNum, pageSize, min.ToUnixTime(), max.ToUnixTime()));

            return objs;
        }

        public static IEnumerable<Question> GetUserFavoritesByViewsWithRange(int min, int max, int id, int pageNum, int pageSize)
        {
            var objs = GetObjects<Question>(String.Format("/users/{0}/favorites?sort=views&order=desc&page={1}&pagesize={2}&min={3}&max={4}", id, pageNum, pageSize, min, max));

            return objs;
        }

        public static IEnumerable<Question> GetUserFavoritesByCreationWithRange(DateTime min, DateTime max, int id, int pageNum, int pageSize)
        {
            var objs = GetObjects<Question>(String.Format("/users/{0}/favorites?sort=creation&order=desc&page={1}&pagesize={2}&min={3}&max={4}", id, pageNum, pageSize, min.ToUnixTime(), max.ToUnixTime()));

            return objs;
        }

        public static IEnumerable<Question> GetUserFavoritesByVotesWithRange(int min, int max, int id, int pageNum, int pageSize)
        {
            var objs = GetObjects<Question>(String.Format("/users/{0}/favorites?sort=votes&order=desc&page={1}&pagesize={2}&min={3}&max={4}", id, pageNum, pageSize, min, max));

            return objs;
        }

        public static IEnumerable<Question> GetUsersActiveQuestionsWithRange(DateTime min, DateTime max, int id)
        {
            var resp = GetObjects<Question>(String.Format("/users/{0}/questions?sort=activity&order=desc&body=true&comments=true&min={1}&max={2}", id, min.ToUnixTime(), max.ToUnixTime()));

            return resp;
        }

        public static IEnumerable<Question> GetUsersQuestionsByViewsWithRange(int min, int max, int id)
        {
            var resp = GetObjects<Question>(String.Format("/users/{0}/questions?sort=views&order=desc&body=true&comments=true&min={1}&max={2}", id, min, max));

            return resp;
        }

        public static IEnumerable<Question> GetUsersQuestionsByCreationWithRange(DateTime min, DateTime max, int id)
        {
            var resp = GetObjects<Question>(String.Format("/users/{0}/questions?sort=creation&order=desc&body=true&comments=true&min={1}&max={2}", id, min.ToUnixTime(), max.ToUnixTime()));

            return resp;
        }

        public static IEnumerable<Question> GetUsersQuestionsByVotesWithRange(int min, int max, int id)
        {
            var resp = GetObjects<Question>(String.Format("/users/{0}/questions?sort=votes&order=desc&body=true&comments=true&min={1}&max={2}", id, min, max));

            return resp;
        }

        public static IEnumerable<User> GetUsersByReputationWithRange(int min, int max, string filter, int pageNum, int pageSize)
        {
            var objs = GetObjects<User>(String.Format("/users?filter={0}&page={1}&pagesize={2}&sort=reputation&min={3}&max={4}", filter, pageNum, pageSize, min, max));

            return objs;
        }

        public static IEnumerable<User> GetNewestUsersWithRange(DateTime min, DateTime max, string filter, int pageNum, int pageSize)
        {
            var objs = GetObjects<User>(String.Format("/users?filter={0}&page={1}&pagesize={2}&sort=creation&order=desc&min={3}&max={4}", filter, pageNum, pageSize, min.ToUnixTime(), max.ToUnixTime()));

            return objs;
        }

        public static IEnumerable<User> GetOldestUsersWithRange(DateTime min, DateTime max, string filter, int pageNum, int pageSize)
        {
            var objs = GetObjects<User>(String.Format("/users?filter={0}&page={1}&pagesize={2}&sort=creation&order=asc&min={3}&max={4}", filter, pageNum, pageSize, min.ToUnixTime(), max.ToUnixTime()));

            return objs;
        }
    }
}
