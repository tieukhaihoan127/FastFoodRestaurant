using FastFoodRestaurant.Models;
using FastFoodRestaurant.Repository.IGenericRepository;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace FastFoodRestaurant.Areas.Client.Controllers
{
    [Area("Client")]
    public class PromotionController : Controller
    {
        private readonly IComboRepository _comboRepo;
        private const int pageSize = 4;
        public PromotionController(IComboRepository comboRepo)
        {
            _comboRepo = comboRepo;
        }
        public IActionResult Index(int pageNumber = 1)
        {
            List<Combo> combosList;
            combosList = _comboRepo.GetAll().ToList();
            var pagedCombosList = combosList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            ViewData["CurrentPage"] = pageNumber;
            ViewData["TotalPages"] = (int)Math.Ceiling(pagedCombosList.Count() / (double)pageSize);
            return View(pagedCombosList);
        }
    }
}
