using BikeShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.WebUI.Models
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public string CurrentCategory { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
