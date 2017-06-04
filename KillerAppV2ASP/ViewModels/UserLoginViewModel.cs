using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerAppV2ASP.ViewModels
{
    public class UserLoginViewModel
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool Loggedin { get; set; }
        public string LoginMessage { get; set; }
    }
}