using System;
using System.Collections.Generic;
using System.Text;

namespace PackageTrack.Models
{
    public class UserAdd
    {
        public string UserName { get; set; }
        public string Password { get; set; }


        public static implicit operator UserAdd(User v)
        {
            UserAdd i = new UserAdd();
            i.UserName = v.UserName;
            i.Password = v.Password;

            return i;
        }
    }
}
