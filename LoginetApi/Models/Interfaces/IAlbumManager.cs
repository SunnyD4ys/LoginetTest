using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoginetApi.Models.DTO;


namespace LoginetApi.Models.Interfaces
{
    public interface IAlbumManager
    {
        Album GetAlbum(int albumId);
        IEnumerable<Album> GetAlbums();
    }
}