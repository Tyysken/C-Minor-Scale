using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C_Minor_Scale.Json
{
    public class BookingJson
    {
        public long Bid { get; set; }
        public string Owner { get; set; }
        public long OwnerParent { get; set; }
        public long LastModified { get; set; }
        public long Created { get; set; }
        public long From { get; set; }
        public long Until { get; set; }
        public long Zid { get; set; }
        public string Status { get; set; }
        public long Parent { get; set; }
        public string ZoneType { get; set; }
        public string Subject { get; set; }
        public string Desc { get; set; }
        public string Source { get; set; }
        public bool Private { get; set; }
    }
}