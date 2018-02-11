using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoginetApi.Models.Common.Interfaces;
using System.Xml.Serialization;

namespace LoginetApi.Models.DTO
{
    public class User : IRepositoryItem<int>
    {
        #region IrepositoryMembers
        public int id { get; set; }
        #endregion

        public string Name { get; set; }
        public string UserName { get; set; }
        private string email;
        public string Email
        {
            //тк мы используем класс, как Data Transfer Object, без всякой бизнес-логики, сразу шифруем нужное нам поле тут:
            get
            {
                string key = Common.Enctyptor.key;
                string vector = Common.Enctyptor.vector;
                var result = Models.Common.Enctyptor.Encrypt(email,key,vector);
                //var result2 = Models.Common.Enctyptor.Decrypt(result, key,vector);

                return result;
            }
            set { email = value; }
        }
        public string Phone { get; set; }
        public string Website { get; set; }
        public Address Address { get; set; }
        public Company Company { get; set; }


       
    }
}