using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastFoodRestaurant.Models
{
    public class BillDetail
    {
        public string BillId { get; set; }
        [ForeignKey("BillId")]
        public Bill Bill { get; set; }
        public string MenuId { get; set; }
        [ForeignKey("MenuId")]
        public Menu Menu { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
