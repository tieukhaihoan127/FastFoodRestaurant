using System.ComponentModel.DataAnnotations;

namespace FastFoodRestaurant.Models
{
    public class Party
    {
        [Key]
        public string PartyId { get; set; }
        [Required]
        public string ClientName { get; set; }
        [Required]
        public string BookingName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public Boolean Type { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        [Required]
        public Boolean Status { get; set; }

    }
}
