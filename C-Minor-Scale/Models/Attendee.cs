using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C_Minor_Scale.Models
{
    public class Attendee
    {
        public string Username { get; set; }
        public bool Required { get; set; }
        public string Status { get; set; }
        public int Guests { get; set; }
    }
}