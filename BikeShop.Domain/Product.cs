using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeShop.Domain
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Length of name have to be between 3 and 50")]
        public string Model { get; set; }
        [Required]
        [RangeYear(1900)]
        public int Year { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [UIHint("MultilineText")]
        public string Description { get; set; }
        [Required]
        [Range(0, 999999, ErrorMessage = "Price have to be between 0 and 999999")]
        public double Price { get; set; }
        [DisplayName("Category")]
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
