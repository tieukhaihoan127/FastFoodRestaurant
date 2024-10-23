using Microsoft.AspNetCore.Mvc;

namespace FastFoodRestaurant.Areas.Client.Controllers
{
    [Area("Client")]
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
