using FastFoodRestaurant.Models;
using FastFoodRestaurant.Repository.IGenericRepository;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodRestaurant.Areas.Client.Controllers
{
    [Area("Client")]
    public class StoreController : Controller
    {
        private readonly IStoreRepository _storeRepo;

        public StoreController(IStoreRepository storeRepo)
        {
            _storeRepo = storeRepo;
        }
        public IActionResult Index()
        {
            List<Store> storesList;
            storesList = _storeRepo.GetAll().ToList();
            return View(storesList);
        }
    }
}
