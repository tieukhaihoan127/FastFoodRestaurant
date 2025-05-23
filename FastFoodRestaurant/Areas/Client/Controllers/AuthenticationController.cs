using Microsoft.AspNetCore.Mvc;

namespace FastFoodRestaurant.Areas.Client.Controllers
{
    [Area("Client")]
    public class AuthenticationController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
