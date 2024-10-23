using Microsoft.AspNetCore.Mvc;

namespace FastFoodRestaurant.Controllers
{
    [Area("Admin")]
    public class PartyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
