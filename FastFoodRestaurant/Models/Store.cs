using System.ComponentModel.DataAnnotations;

namespace FastFoodRestaurant.Models
{
    public class Store
    {
        [Key]
        public string StoreId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string District { get; set; }
        [Required]
        public string Ward { get; set; }
        [Required]
        public string City { get; set; }
        public string? Hotline { get; set; }
        public TimeSpan? OpeningHour { get; set; }
        public TimeSpan? ClosingTime { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        [Required]
        public Boolean Status { get; set; }
    }
}
