using System.ComponentModel.DataAnnotations;

namespace FastFoodRestaurant.Models
{
    public class Category
    {
        [Key]
        public string CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
