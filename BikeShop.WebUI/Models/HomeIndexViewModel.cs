using BikeShop.Domain;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.WebUI.Models
{
    public class HomeIndexViewModel
    {
        //public IPagedList<Product> Products { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public string CurrentCategory { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
