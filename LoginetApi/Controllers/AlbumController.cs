using LoginetApi.Models.DTO;
using LoginetApi.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;


namespace LoginetApi.Controllers
{
    [LogErrorAttribute]
    public class AlbumController : ApiController
    {
        public IEnumerable<Album> Get(int? userId = null)
        {
            IEnumerable<Album> albums = WebApiApplication.Container.DataSource.AlbumManager.GetAlbums();
            if (userId != null)
                albums = albums.Where(x => x.userId == userId);
            return albums;
        }
        public Album Get(int id)
        {
            Album album = WebApiApplication.Container.DataSource.AlbumManager.GetAlbum(id);
            return album;
        }
    }
}
