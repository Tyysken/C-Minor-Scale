using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C_Minor_Scale.Models
{
    public class User
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public long Parent { get; set; }
    }
}