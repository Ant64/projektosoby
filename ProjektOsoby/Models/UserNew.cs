using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektOsoby.Models
{
    public class UserNew
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public static object Identity { get; internal set; }
    }
}