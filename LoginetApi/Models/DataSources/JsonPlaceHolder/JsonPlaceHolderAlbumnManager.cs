using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using LoginetApi.Models.Interfaces;
using System.IO;
using LoginetApi.Models.Common;
using LoginetApi.Models.DTO;
using System.Configuration;

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
        protected Repository<int, Album> Albums
        {
            get
            {
                if (albums == null)
                    albums = new Repository<int, Album>();
                return albums;
            }
        }
        public int CacheTimeMinutes = int.Parse(ConfigurationManager.AppSettings["cacheMinutes"]);
        private bool dataDownloaded = false;


        public JsonPlaceHolderAlbumManager()
        {
            route = ConfigurationManager.AppSettings["albumRoute"];
            url = ConfigurationManager.AppSettings["JsonPlaceHolderSource"];
            albums = new Repository<int,Album>();
        }

        public Album GetAlbum(int albumId)
        {
            var entry = Albums.GetEntry(albumId);
            if (entry != null && DateTime.Now.Subtract(entry.AdditionDate).TotalMinutes <= CacheTimeMinutes)
                   return entry.Value;  
            Album result =  JsonHelper<Album>.GetJsonResponse(string.Format("{0}/{1}/{2}", Url, Route, albumId));
            if (result != null)
                Albums.Add(result);
            return result;
        }

        public IEnumerable<Album> GetAlbums()
        {
            if (DateTime.Now.Subtract(Albums.LastUpdateDate).TotalMinutes <= CacheTimeMinutes && Albums.Count > 0 && dataDownloaded)
                return Albums.GetValues();

            var result =  JsonHelper<List<Album>>.GetJsonResponse(string.Format("{0}/{1}", Url, Route));
            Albums.Add(result);
            dataDownloaded = true;

            return result;
        }
    }
}