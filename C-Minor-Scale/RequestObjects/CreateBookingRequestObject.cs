using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace C_Minor_Scale.RequestObjects
{
    public class CreateBookingRequestObject
    {
        [Required]
        public string Owner { get; set; }

        [Range(minimum: 1, maximum: int.MaxValue)]
        public long LastModified { get; set; }

        [Range(minimum: 1, maximum: int.MaxValue)]
        public long From { get; set; }

        [Range(minimum: 1, maximum: int.MaxValue)]
        public long Until { get; set; }

        [Range(minimum: 1, maximum: int.MaxValue)]
        public long Zid { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public bool Private { get; set; }
    }
}