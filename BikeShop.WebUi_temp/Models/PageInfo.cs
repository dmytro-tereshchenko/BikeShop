using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.WebUI.Models
{
    public class PageInfo
    {
        public int PageNumber { get; set; } // number of current page
        public int PageSize { get; set; } // amount of elements on page
        public int TotalItems { get; set; } // all elements
        public int TotalPages // all pages
        {
            get => (int)Math.Ceiling((decimal)TotalItems / PageSize);
        }
    }
}
