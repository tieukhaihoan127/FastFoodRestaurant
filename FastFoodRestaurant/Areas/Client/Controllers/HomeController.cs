using FastFoodRestaurant.Models;
using FastFoodRestaurant.Repository.IGenericRepository;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace FastFoodRestaurant.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
        private readonly IComboRepository _comboRepo;
        public HomeController(IComboRepository comboRepo)
        {
            _comboRepo = comboRepo;
        }
        public IActionResult Index()
        {
            List<Combo> combosList;
            combosList = _comboRepo.GetAll().ToList();

            return View(combosList);
        }
    }
}
