using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace C_Minor_Scale.ValidationAttributes
{
    public class ZidListValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            List<long> zids = value as List<long>;

            if (zids == null)
            {
                return false;
            }
            else if (zids.Count == 0)
            {
                return false;
            }

            foreach (long zid in zids)
            {
                if (zid < 1)
                {
                    return false;
                }
            }

            return true;
        }
    }
}