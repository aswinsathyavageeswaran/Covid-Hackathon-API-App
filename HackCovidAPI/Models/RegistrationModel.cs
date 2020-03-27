using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackCovidAPI.Models
{
    public class RegistrationModel
    {
        public UserModel UserData { get; set; }
        public ShopModel ShopData { get; set; }

    }
}