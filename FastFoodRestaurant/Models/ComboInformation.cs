using System.ComponentModel.DataAnnotations.Schema;

namespace FastFoodRestaurant.Models
{
    public class ComboInformation
    {
        public string ComboId { get; set; }
        [ForeignKey("ComboId")]
        public Combo Combo { get; set; }
        public string BillId { get; set; }
        [ForeignKey("BillId")]
        public Bill Bill { get; set; }
        public string MenuId { get; set; }
        [ForeignKey("MenuId")]
        public Menu Menu { get; set; }
    }
}
