using FastFoodRestaurant.Models;
using FastFoodRestaurant.Repository.IGenericRepository;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodRestaurant.Controllers
{
    [Area("Admin")]
    public class SystemUserController : Controller
    {
        private readonly ISystemUserRepository _systemUserRepo;
        private const int pageSize = 4;

        public SystemUserController(ISystemUserRepository systemUserRepo)
        {
            _systemUserRepo = systemUserRepo;
        }

        public IActionResult Index(string? keyword, string? name, int pageNumber = 1)
        {
            List<SystemUser> usersList;

            if (!string.IsNullOrEmpty(keyword) && !string.IsNullOrEmpty(name))
            {
                usersList = _systemUserRepo.GetAllExpression(u => u.UserId == keyword && u.PhoneNumber == name).ToList();
            }
            else if (!string.IsNullOrEmpty(keyword))
            {
                usersList = _systemUserRepo.GetAllExpression(u => u.UserId == keyword).ToList();
            }
            else if (!string.IsNullOrEmpty(name))
            {
                usersList = _systemUserRepo.GetAllExpression(u => u.PhoneNumber == name).ToList();
            }
            else
            {
                usersList = _systemUserRepo.GetAll().ToList();
            }

            var pagedUsersList = usersList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            ViewData["CurrentPage"] = pageNumber;
            ViewData["TotalPages"] = (int)Math.Ceiling(usersList.Count() / (double)pageSize);

            return View(pagedUsersList);
        }
    }
}
