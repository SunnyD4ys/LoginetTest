using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoginetApi.Models.Common.Interfaces;

namespace LoginetApi.Models.DTO
{
    public class Album : IRepositoryItem<int>
    {
        public int userId { get; set; }
        public string title { get; set; }

        #region IrepositoryMembers
        public int id { get; set; }
        #endregion
    }
}