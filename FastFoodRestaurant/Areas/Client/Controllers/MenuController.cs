using FastFoodRestaurant.Models;
using FastFoodRestaurant.Repository.IGenericRepository;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodRestaurant.Areas.Client.Controllers
{
    [Area("Client")]
    public class MenuController : Controller
    {
        private readonly IComboRepository _comboRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IMenuRepository _menuRepo;

        public MenuController(IComboRepository comboRepo, ICategoryRepository categoryRepo, IMenuRepository menuRepo)
        {
            _comboRepo = comboRepo;
            _categoryRepo = categoryRepo;
            _menuRepo = menuRepo;
        }
        public IActionResult Index()
        {
            List<Category> categoryList;
            List<Combo> comboList;
            List<Menu> menuList;
            List<Category> menuContainCategoryList;

            categoryList = _categoryRepo.GetAll().ToList();
            comboList = _comboRepo.GetAll().ToList();
            menuList = _menuRepo.GetIncludeCategoryAll().ToList();
            menuContainCategoryList = _menuRepo.GetUniqueCategory();


            ViewData["CategoryList"] = categoryList;
            ViewData["ComboList"] = comboList;
            ViewData["MenuList"] = menuList;
            ViewData["UniqueCategory"] = menuContainCategoryList;

            return View();
        }

        public IActionResult DetailCategory(string id)
        {
            List<Category> categoryList;
            List<Category> currentCategory;
            List<Combo> comboList;
            List<Menu> menuList;
            List<Category> menuContainCategoryList;

            currentCategory = _categoryRepo.GetAllExpression(c => c.CategoryId == id).ToList();
            categoryList = _categoryRepo.GetAll().ToList();
            comboList = _comboRepo.GetAll().ToList();
            menuList = _menuRepo.GetIncludeCategoryAll().ToList();
            menuContainCategoryList = _menuRepo.GetUniqueCategory();


            ViewData["CategoryList"] = categoryList;
            ViewData["ComboList"] = comboList;
            ViewData["MenuList"] = menuList;
            ViewData["UniqueCategory"] = menuContainCategoryList;
            ViewData["CurrentCategory"] = currentCategory;

            return View();
        }
    }
}
