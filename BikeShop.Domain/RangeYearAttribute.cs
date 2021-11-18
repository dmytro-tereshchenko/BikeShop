using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.Domain
{
    public class RangeYearAttribute : ValidationAttribute
    {
        private int minYear;
        public RangeYearAttribute(int minYear)
        {
            this.minYear = minYear;
            ErrorMessage = $"Year have to be between {minYear} and {DateTime.Now.Year}";
        }
        public override bool IsValid(object value)
        {
            int currentYear;
            if (value != null && int.TryParse(value.ToString(), out currentYear) &&
                currentYear >= minYear && currentYear <= DateTime.Now.Year)
            {
                return true;
            }
            return false;
        }
    }
}
