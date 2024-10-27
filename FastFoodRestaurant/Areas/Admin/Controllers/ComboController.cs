using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FastFoodRestaurant.DTO;
using FastFoodRestaurant.Models;
using FastFoodRestaurant.Repository.IGenericRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FastFoodRestaurant.Controllers
{
    [Area("Admin")]
    public class ComboController : Controller
    {
        private readonly IComboRepository _comboRepo;
        private const int pageSize = 4;
        private readonly Cloudinary _cloudinary;

        public ComboController(IComboRepository comboRepo)
        {
            _comboRepo = comboRepo;
            var account = new Account("dwdhkwu0r", "182449369914218", "d1gJYHWsz6Rz9wFmFD9S6kYtka4");
            _cloudinary = new Cloudinary(account);
        }
        public IActionResult Index(string? keyword, string? name, int pageNumber = 1)
        {
            List<Combo> combosList;

            if (!string.IsNullOrEmpty(keyword) && !string.IsNullOrEmpty(name))
            {
                combosList = _comboRepo.GetAllExpression(c => c.ComboId == keyword && c.Name == name).ToList();
            }
            else if (!string.IsNullOrEmpty(keyword))
            {
                combosList = _comboRepo.GetAllExpression(c => c.ComboId == keyword).ToList();
            }
            else if (!string.IsNullOrEmpty(name))
            {
                combosList = _comboRepo.GetAllExpression(c => c.Name == name).ToList();
            }
            else
            {
                combosList = _comboRepo.GetAll().ToList();
            }

            var pagedComboList = combosList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            ViewData["CurrentPage"] = pageNumber;
            ViewData["TotalPages"] = (int)Math.Ceiling(combosList.Count() / (double)pageSize);

            return View(pagedComboList);
        }
    }
}
