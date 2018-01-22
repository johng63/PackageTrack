using System;
using System.Collections.Generic;
using System.Text;

namespace PackageTrack.Models
{
    public class User
    {
        public string _id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime AddedDate { get; set; }
        public string ___v { get; set; }
    }
}
