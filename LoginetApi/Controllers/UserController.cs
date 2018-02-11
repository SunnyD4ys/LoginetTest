using LoginetApi.Models.DTO;
using LoginetApi.Models.Filters;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace LoginetApi.Controllers
{
    [LogErrorAttribute]
    public class UserController : ApiController
    {
        public IEnumerable<User> Get()
        {
            IEnumerable<User> users = WebApiApplication.Container.DataSource.UserManager.GetUsers();
            return users;
        }

        public User Get(int id)
        {
            User user = WebApiApplication.Container.DataSource.UserManager.GetUser(id);
            return user;
        }
    }
}
