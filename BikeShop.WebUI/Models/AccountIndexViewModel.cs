using BikeShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.WebUI.Models
{
    public class AccountIndexViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public string CurrentRole { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
