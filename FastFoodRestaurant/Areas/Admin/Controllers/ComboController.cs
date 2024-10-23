using Microsoft.AspNetCore.Mvc;

namespace FastFoodRestaurant.Controllers
{
    [Area("Admin")]
    public class ComboController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
