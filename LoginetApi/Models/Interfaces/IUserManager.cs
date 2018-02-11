using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoginetApi.Models.DTO;

namespace LoginetApi.Models.Interfaces
{
    public interface IUserManager
    {
        User GetUser(int userId);
        IEnumerable<User> GetUsers();
    }
}
