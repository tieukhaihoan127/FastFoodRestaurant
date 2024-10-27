using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastFoodRestaurant.Models
{
    public class SystemUser
    {
        [Key]
        public string UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
        public string? PictureUrl { get; set; }
        [Required]
        public string StoreId { get; set; }
        [ForeignKey("StoreId")]
        public Store Store { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        [Required]
        public Boolean Status { get; set; }
        [Required]
        public Boolean IsLocked { get; set; }
    }
}
