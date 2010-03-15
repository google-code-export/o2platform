using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TweetSharp.Fluent;
using TweetSharp.Model;

namespace O2.API.Visualization.Twitter
{
    public class O2TwitterAPI
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public ITwitterStatuses Statuses { get; set; }
        //public TwitterResult LastResult { get; set;} 

        public O2TwitterAPI(string username, string password)
        {
            Username = username;
            Password = password;
            login();
        }

        public bool login()
        {
            var twitter = FluentTwitter.CreateRequest().AuthenticateAs(Username, Password); //.Statuses.OnUserTimeline().AsJson();
            Statuses = twitter.Statuses();
            //var jason =  statuses.OnUserTimeline().AsJson();
            var result = Statuses.OnUserTimeline().AsJson().Request();
            //show.info(LastResult);
            return checkResponse(result);
        }

        public bool update(string updateString)
        {
            var result = Statuses.Update(updateString).AsJson().Request();
            return checkResponse(result);
        }

        public bool checkResponse(TwitterResult result)
        {
            return result.ResponseHttpStatusDescription == "OK";
        }
    }   
}
