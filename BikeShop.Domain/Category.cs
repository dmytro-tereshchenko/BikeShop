using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BikeShop.Domain
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Length of name have to be between 3 and 30")]
        public string Name { get; set; }
        public virtual List<Product> Products { get; set; } = new List<Product>();
        /*public Category()
        {
            Products = new List<Product>();
        }*/
    }
}
