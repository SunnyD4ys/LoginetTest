using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using LoginetApi.Models.Interfaces;
using System.IO;
using LoginetApi.Models.Common;
using LoginetApi.Models.DTO;


namespace LoginetApi.Models.DataSources.JsonPlaceHolder
{
    public class JsonPlaceHolderAlbumManager : IAlbumManager
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

        private Repository<int, Album> albums;

        public JsonPlaceHolderAlbumManager()
        {
            route = "albums";
            url = "http://jsonplaceholder.typicode.com";
            albums = new Repository<int,Album>();
        }

        public Album GetAlbum(int albumId)
        {
            return JsonHelper<Album>.GetJsonResponse(string.Format("{0}/{1}/{2}", Url, Route, albumId));
        }

        public IEnumerable<Album> GetAlbums()
        {
            return JsonHelper<List<Album>>.GetJsonResponse(string.Format("{0}/{1}", Url, Route));
        }
    }
}