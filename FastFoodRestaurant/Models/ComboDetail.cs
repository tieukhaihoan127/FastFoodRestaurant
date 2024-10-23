using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FastFoodRestaurant.Models
{
    public class ComboDetail
    {
        public string ComboId { get; set; }
        [ForeignKey("ComboId")]
        public Combo Combo { get; set; }
        public string MenuId { get; set; }
        [ForeignKey("MenuId")]
        public Menu Menu { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
