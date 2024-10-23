using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastFoodRestaurant.Models
{
    public class Bill
    {
        [Key]
        public string BillId { get; set; }
        [Required]
        public string ClientName { get; set; }
        [Required]
        public string ClientPhoneNumber { get; set; }
        public string? ClientEmail { get; set; }
        [Required]
        public double TotalPrice { get; set; }
        public string? Note { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public Boolean PaymentStatus { get; set; }
        [Required]  
        public DateTime ShippingDate { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public SystemUser SystemUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public virtual ICollection<BillDetail> BillDetails { get; set; }
        public virtual ICollection<ComboInformation> ComboInformations { get; set; }
    }
}
