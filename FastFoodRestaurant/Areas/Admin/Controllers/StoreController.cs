using FastFoodRestaurant.Models;
using FastFoodRestaurant.Repository.IGenericRepository;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodRestaurant.Controllers
{
    [Area("Admin")]
    public class StoreController : Controller
    {
        private readonly IStoreRepository _storeRepo;
        private const int pageSize = 4;

        public StoreController(IStoreRepository storeRepo)
        {
            _storeRepo = storeRepo;
        }

        public IActionResult Index(string? keyword, int pageNumber = 1)
        {
            List<Store> storesList;

            if (!string.IsNullOrEmpty(keyword))
            {
                storesList = _storeRepo.GetAllExpression(s => s.StoreId == keyword).ToList();
            }
            else
            {
                storesList = _storeRepo.GetAll().ToList();
            }

            var pagedVouchersList = storesList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            ViewData["CurrentPage"] = pageNumber;
            ViewData["TotalPages"] = (int)Math.Ceiling(storesList.Count() / (double)pageSize);

            return View(pagedVouchersList);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var storeItem = _storeRepo.Get(s => s.StoreId == id);
            return View(storeItem);
        }
    }
}
