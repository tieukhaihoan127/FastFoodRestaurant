using Microsoft.AspNetCore.Mvc;

namespace FastFoodRestaurant.Controllers
{
    [Area("Admin")]
    public class ClientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
