using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastFoodRestaurant.Models
{
    public class CartItem
    {
        [Key]
        public string CartId { get; set; }
        public string MenuId { get; set; }
        [ForeignKey("MenuId")]
        public Menu Menu { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
