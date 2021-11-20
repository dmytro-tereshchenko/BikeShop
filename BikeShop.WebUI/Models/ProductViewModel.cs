using BikeShop.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.WebUI.Models
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        [DisplayName("Images")]
        public IFormFileCollection UploadedFiles { get; set; }
    }
}
