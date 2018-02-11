using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoginetApi.Models.Interfaces;
using LoginetApi.Models.Common;
using LoginetApi.Models.DTO;


namespace LoginetApi.Models.DataSources.JsonPlaceHolder
{
    public class JsonPlaceHolderUserManager : IUserManager
    {
        private string url;
        public string Url
        {
            get { return url; }
        }
        private string route;
        public string Route
        {
            get { return route; }
        }

        public int UpdateCacheIntervalMinutes { get; set; }

        private Repository<int, User> users;

        public JsonPlaceHolderUserManager()
        {
            route = "users";
            url = "http://jsonplaceholder.typicode.com"; // add to config
            users = new Repository<int, User>();
            UpdateCacheIntervalMinutes = 10;
        }

        public User GetUser(int userId)
        {
            User result = null;
            if (users.LastUpdate.Subtract(DateTime.Now).Minutes >= UpdateCacheIntervalMinutes || users[userId] == default(User))
            {
                result = JsonHelper<User>.GetJsonResponse(string.Format("{0}/{1}/{2}", Url, Route, userId));
                users.Add(result);
                return result;
            }
            else
                return users[userId];
        }

        public IEnumerable<User> GetUsers()
        {
            return JsonHelper<List<User>>.GetJsonResponse(string.Format("{0}/{1}", Url, Route));
        }
    }
}