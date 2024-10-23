using Microsoft.AspNetCore.Mvc;

namespace FastFoodRestaurant.Areas.Client.Controllers
{
    [Area("Client")]
    public class StoreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
