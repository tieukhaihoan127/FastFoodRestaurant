using Microsoft.AspNetCore.Mvc;

namespace FastFoodRestaurant.Controllers
{
    public class StoreController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
