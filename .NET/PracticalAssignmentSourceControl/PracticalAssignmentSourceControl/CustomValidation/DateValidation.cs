using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PracticalAssignmentSourceControl.CustomValidation
{
    public class DateValidation: ValidationAttribute
    {
        public override bool IsValid(Object value)
        {
            DateTime dateTime = Convert.ToDateTime(value);
            return dateTime < DateTime.Now;
        }
    }
}