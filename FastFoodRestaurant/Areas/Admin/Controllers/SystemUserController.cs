using Microsoft.AspNetCore.Mvc;

namespace FastFoodRestaurant.Controllers
{
    public class SystemUserController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
