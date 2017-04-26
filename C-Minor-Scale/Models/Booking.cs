using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C_Minor_Scale.Models
{
    public class Booking
    {
        public string Owner { get; set; }
        public long LastModified { get; set; }
        public long From { get; set; }
        public long Until { get; set; }
        public long Zid { get; set; }
        public string Subject { get; set; }
        public bool Private { get; set; }
    }
}