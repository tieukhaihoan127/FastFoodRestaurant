using System.ComponentModel.DataAnnotations;

namespace FastFoodRestaurant.Models
{
    public class Voucher
    {
        [Key]
        public string VoucherId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]  
        public double DiscountPercentage { get; set; }
        [Required]
        public int MaximumUsed { get; set; }
        [Required]
        public DateTime StartedDate { get; set; }
        [Required]
        public DateTime ExpiredDate { get; set; }
        [Required]
        public Boolean Status { get; set; }
        public virtual ICollection<VoucherDetail> VoucherDetails { get; set; }
    }
}
