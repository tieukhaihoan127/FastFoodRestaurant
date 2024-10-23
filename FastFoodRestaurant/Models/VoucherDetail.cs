using System.ComponentModel.DataAnnotations.Schema;

namespace FastFoodRestaurant.Models
{
    public class VoucherDetail
    {
        public string ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }
        public string VoucherId { get; set; }
        [ForeignKey("VoucherId")]
        public Voucher Voucher { get; set; }
    }
}
