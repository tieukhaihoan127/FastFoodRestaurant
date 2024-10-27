using FastFoodRestaurant.Models;
using FastFoodRestaurant.Repository.IGenericRepository;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodRestaurant.Areas.Client.Controllers
{
    [Area("Client")]
    public class CartController : Controller
    {
        private readonly IComboRepository _comboRepo;
        private readonly IMenuRepository _menuRepo;
        private readonly IStoreRepository _storeRepo;

        public CartController(IComboRepository comboRepo, IMenuRepository menuRepo, IStoreRepository storeRepo)
        {
            _comboRepo = comboRepo;
            _menuRepo = menuRepo;
            _storeRepo = storeRepo;
        }
        public IActionResult Index()
        {
            List<Combo> comboList;
            List<Menu> menuList;
            List<Store> storeRepo;

            comboList = _comboRepo.GetAll().ToList();
            menuList = _menuRepo.GetIncludeCategoryAll().ToList();
            Store selectedStore = _storeRepo.getSingleStore(s => s.StoreId == "ST0002");
            ViewData["ToBuyList"] = comboList;
            ViewData["SuggestList"] = menuList;
            ViewData["SelectedStore"] = selectedStore;
            return View();
        }
    }
}
