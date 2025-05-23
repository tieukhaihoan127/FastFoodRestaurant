using System.ComponentModel.DataAnnotations;

namespace FastFoodRestaurant.Models
{
    public class OrderViewModel
    {
        public string ClientName { get; set; }
        public string ClientPhoneNumber { get; set; }
        public string ClientEmail { get; set; }

        public string Address { get; set; }

        public List<CartItem> CartItems { get; set; } = new List<CartItem>();

        public double TotalPrice { get; set; }
    }
}
