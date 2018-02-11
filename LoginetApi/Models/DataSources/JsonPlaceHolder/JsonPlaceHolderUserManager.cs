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

        public int CacheTimeMinutes = 2;
        private bool dataDownloaded = false;


        private Repository<int, User> users;
        protected Repository<int, User> Users
        {
            get
            {
                if (users == null)
                    users = new Repository<int, User>();
                return users;
            }
        }

        public JsonPlaceHolderUserManager()
        {
            route = "users";
            url = "http://jsonplaceholder.typicode.com"; // add to config
            users = new Repository<int, User>();
        }

        public User GetUser(int userId)
        {
            var entry = Users.GetEntry(userId);
            if (entry != null && DateTime.Now.Subtract(entry.AdditionDate).TotalMinutes <= CacheTimeMinutes)
                return entry.Value;
            User result = JsonHelper<User>.GetJsonResponse(string.Format("{0}/{1}/{2}", Url, Route, userId));
            if (result != null)
                Users.Add(result);
            return result;
        }

        public IEnumerable<User> GetUsers()
        {
            if (DateTime.Now.Subtract(Users.LastUpdateDate).TotalMinutes <= CacheTimeMinutes && Users.Count > 0 && dataDownloaded)
                return Users.GetValues();

            var result = JsonHelper<List<User>>.GetJsonResponse(string.Format("{0}/{1}", Url, Route));
            dataDownloaded = true;
            Users.Add(result);

            return result;
        }
    }
}