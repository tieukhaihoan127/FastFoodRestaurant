using Microsoft.AspNetCore.Mvc;

namespace FastFoodRestaurant.Areas.Client.Controllers
{
    [Area("Client")]
    public class PartyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
