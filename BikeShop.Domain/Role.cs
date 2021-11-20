using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BikeShop.Domain
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name of role is required")]
        public string Name { get; set; }
        public virtual List<User> Users { get; set; }
        public Role()
        {
            Users = new List<User>();
        }
    }
}
