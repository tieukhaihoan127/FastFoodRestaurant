using FastFoodRestaurant.Models;
using FastFoodRestaurant.Repository.IGenericRepository;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodRestaurant.Areas.Client.Controllers
{
    [Area("Client")]
    public class PartyController : Controller
    {
        private readonly IComboRepository _comboRepo;
        private readonly IComboDetailRepository _comboDetailRepo;
        public PartyController(IComboRepository comboRepo,IComboDetailRepository comboDetailRepo)
        {
            _comboRepo = comboRepo;
            _comboDetailRepo = comboDetailRepo;
        }
        public IActionResult Index()
        {
            List<Combo> comboList;
            List<ComboDetail> comboDetailList;
            comboList = _comboRepo.GetComboForParty();
            comboDetailList = _comboDetailRepo.GetIncludeAll().ToList();
            ViewData["ComboDetailList"] = comboDetailList;
            return View(comboList);
        }
    }
}
