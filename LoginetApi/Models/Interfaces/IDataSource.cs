using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginetApi.Models.Interfaces
{
    public interface IDataSource
    {
        IUserManager UserManager
        {
            get;
            set;
        }
        IAlbumManager AlbumManager
        {
            get;
            set;
        }
        
        
    }
}
