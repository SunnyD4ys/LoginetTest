using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoginetApi.Models.Interfaces;

namespace LoginetApi.Models.DataSources.JsonPlaceHolder
{
    public class JsonPlaceHolderSource : IDataSource
    {
        private IUserManager userManager = new JsonPlaceHolderUserManager();
        public IUserManager UserManager
        {
            get
            {
                return userManager;
            }
            set
            {
                userManager = value;
            }
        }
        private IAlbumManager albumManager = new JsonPlaceHolderAlbumManager();
        public IAlbumManager AlbumManager
        {
            get
            {
                return albumManager;
            }
            set
            {
                albumManager = value;
            }
        }
    }
}