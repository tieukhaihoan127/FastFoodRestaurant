using Microsoft.AspNetCore.Mvc;

namespace FastFoodRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
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
