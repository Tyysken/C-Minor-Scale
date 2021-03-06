﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using C_Minor_Scale.ValidationAttributes;

namespace C_Minor_Scale.RequestObjects
{
    public class CreateBookingMultiRequestObject
    {
        [Required]
        public string Owner { get; set; }

        [Range(minimum: 1, maximum: long.MaxValue)]
        public long LastModified { get; set; }

        [Range(minimum: 1, maximum: long.MaxValue)]
        public long From { get; set; }

        [Range(minimum: 1, maximum: long.MaxValue)]
        public long Until { get; set; }

        [ZidListValidation(ErrorMessage = "One or more zids is out of range.")]
        public List<long> Zids { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public bool Private { get; set; }
    }
}