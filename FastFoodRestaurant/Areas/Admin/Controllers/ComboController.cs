using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FastFoodRestaurant.DTO;
using FastFoodRestaurant.Models;
using FastFoodRestaurant.Repository.IGenericRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using System.Net;

namespace FastFoodRestaurant.Controllers
{
    [Area("Admin")]
    public class ComboController : Controller
    {
        private readonly IComboRepository _comboRepo;
        private readonly IMenuRepository _menuRepo;
        private const int pageSize = 4;
        private readonly Cloudinary _cloudinary;

        public ComboController(IComboRepository comboRepo, IMenuRepository menuRepo)
        {
            _comboRepo = comboRepo;
            _menuRepo = menuRepo;
            var account = new Account("dwdhkwu0r", "182449369914218", "d1gJYHWsz6Rz9wFmFD9S6kYtka4");
            _cloudinary = new Cloudinary(account);
        }
        public IActionResult Index(string? keyword, string? name, int pageNumber = 1)
        {
            List<Combo> combosList;

            combosList = _comboRepo.GetAll().ToList();

            var pagedComboList = combosList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            foreach (var obj in pagedComboList)
            {
                obj.ComboId = obj.ComboId.Trim();
            }

            ViewData["CurrentPage"] = pageNumber;
            ViewData["TotalPages"] = (int)Math.Ceiling(combosList.Count() / (double)pageSize);

            return View(pagedComboList);
        }

        public IActionResult Create()
        {
            var menusList = _menuRepo.GetAll(m => new MenuInfo
            {
                MenuId = m.MenuId,
                Name = m.Name,
                Price = m.Price,
            });

            var currentId = _comboRepo.getCurrentId(c => c.ComboId);
            string numberExtract = currentId.ComboId.Trim().Substring(2);
            int num = int.Parse(numberExtract);
            num += 1;
            string newId = "";

            if (num < 10)
            {
                newId = "CB000000000" + num;
            }
            else if (num < 100)
            {
                newId = "CB00000000" + num;
            }
            else if (num < 1000)
            {
                newId = "CB0000000" + num;
            }
            else if (num < 10000)
            {
                newId = "CB000000" + num;
            }
            else if (num < 100000)
            {
                newId = "CB00000" + num;
            }
            else if (num < 1000000)
            {
                newId = "CB0000" + num;
            }
            else if (num < 10000000)
            {
                newId = "CB000" + num;
            }
            else if (num < 100000000)
            {
                newId = "CB00" + num;
            }
            else if (num < 1000000000)
            {
                newId = "CB0" + num;
            }
            else
            {
                newId = "CB" + num;
            }

            ViewData["CurrentId"] = newId;
            ViewData["MenusList"] = menusList;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Combo obj, List<Object> idArr,IFormFile pictureFile)
        {
            List<Combo> combosList = _comboRepo.GetAll().ToList();

            if (pictureFile != null && pictureFile.Length > 0)
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(pictureFile.FileName, pictureFile.OpenReadStream()),
                    PublicId = obj.ComboId.Trim()
                };

                var uploadResult = _cloudinary.Upload(uploadParams);
                obj.PictureUrl = uploadResult.Url.ToString();
            }

            obj.CreatedDate = DateTime.Now;
            obj.UpdatedDate = null;
            obj.DeleteDate = null;
            obj.Description = obj.Name;
            obj.IsActive = true;
            obj.IsForParty = false;

            _comboRepo.Add(obj);
            _comboRepo.Save();
            int cnt = combosList.Count() + 1;
            int totalPages = (int)Math.Ceiling(cnt / (double)pageSize);
            return RedirectToAction("Index", new { pageNumber = totalPages });
        }
    }

    
}
