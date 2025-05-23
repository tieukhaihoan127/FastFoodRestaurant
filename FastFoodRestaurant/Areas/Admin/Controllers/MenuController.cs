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
    public class MenuController : Controller
    {
        private readonly IMenuRepository _menuRepo;
        private readonly ICategoryRepository _categoryRepo;
        private const int pageSize = 4;
        private readonly Cloudinary _cloudinary;

        public MenuController(IMenuRepository menuRepo, ICategoryRepository categoryRepo)
        {
            _menuRepo = menuRepo;
            _categoryRepo = categoryRepo;
            var account = new Account("dwdhkwu0r", "182449369914218", "d1gJYHWsz6Rz9wFmFD9S6kYtka4");
            _cloudinary = new Cloudinary(account);
        }
        public IActionResult Index(int pageNumber = 1)
        {
            List<Menu> menusList;
            string[] categoryIdList = new string[pageSize];
            var categoriesList = _categoryRepo.GetAllIds(c => new CategoryIdName
            {
                CategoryId = c.CategoryId.Trim(),
                Name = c.Name
            });

            menusList = _menuRepo.GetIncludeCategoryAll().ToList();

            var pagedMenuList = menusList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            foreach (var obj in menusList)
            {
                obj.MenuId = obj.MenuId.Trim();
            }

            ViewData["CurrentPage"] = pageNumber;
            ViewData["CategoryList"] = categoriesList;
            ViewData["TotalPages"] = (int)Math.Ceiling(menusList.Count() / (double)pageSize);

            return View(pagedMenuList);
        }

        [HttpPost]
        public IActionResult Index(string? object_id, string? object_name, string? CategoryId, int pageNumber = 1)
        {
            List<Menu> menusList;
            string[] categoryIdList = new string[pageSize];
            var categoriesList = _categoryRepo.GetAllIds(c => new CategoryIdName
            {
                CategoryId = c.CategoryId.Trim(),
                Name = c.Name
            });

            if (!string.IsNullOrEmpty(object_id) && !string.IsNullOrEmpty(object_name))
            {
                menusList = _menuRepo.GetIncludeCategoryAllExpression(m => m.MenuId == object_id && m.Name == object_name).ToList();
            }
            else if (!string.IsNullOrEmpty(object_id))
            {
                menusList = _menuRepo.GetIncludeCategoryAllExpression(c => c.MenuId == object_id).ToList();
            }
            else if (!string.IsNullOrEmpty(object_name))
            {
                menusList = _menuRepo.GetIncludeCategoryAllExpression(c => c.Name == object_name).ToList();
            }
            else if (!string.IsNullOrEmpty(CategoryId))
            {
                menusList = _menuRepo.GetIncludeCategoryAllExpression(c => c.CategoryId == CategoryId).ToList();
            }
            else
            {
                menusList = _menuRepo.GetIncludeCategoryAll().ToList();
            }

            var pagedMenuList = menusList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            foreach (var obj in menusList)
            {
                obj.MenuId = obj.MenuId.Trim();
            }

            ViewData["CurrentPage"] = pageNumber;
            ViewData["CategoryList"] = categoriesList;
            ViewData["TotalPages"] = (int)Math.Ceiling(menusList.Count() / (double)pageSize);

            return View(pagedMenuList);
        }

        public IActionResult Create() 
        {
            var categoriesList = _categoryRepo.GetAllIds(c => new CategoryIdName
            {
                CategoryId = c.CategoryId,
                Name = c.Name
            });
            var currentId = _menuRepo.getCurrentId(m => m.MenuId);
            string numberExtract = currentId.MenuId.Trim().Substring(2);
            int num = int.Parse(numberExtract);
            num += 1;
            string newId = "";

            if (num < 10)
            {
                newId = "MN000000000" + num;
            }
            else if (num < 100)
            {
                newId = "MN00000000" + num;
            }
            else if (num < 1000)
            {
                newId = "MN0000000" + num;
            }
            else if (num < 10000)
            {
                newId = "MN000000" + num;
            }
            else if (num < 100000)
            {
                newId = "MN00000" + num;
            }
            else if (num < 1000000)
            {
                newId = "MN0000" + num;
            }
            else if (num < 10000000)
            {
                newId = "MN000" + num;
            }
            else if (num < 100000000)
            {
                newId = "MN00" + num;
            }
            else if (num < 1000000000)
            {
                newId = "MN0" + num;
            }
            else
            {
                newId = "MN" + num;
            }

            ViewData["CurrentId"] = newId;
            ViewData["CategoryList"] = categoriesList;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Menu obj, IFormFile pictureFile)
        {
            List<Menu> menusList = _menuRepo.GetIncludeCategoryAll().ToList();

            if (pictureFile != null && pictureFile.Length > 0)
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(pictureFile.FileName, pictureFile.OpenReadStream()),
                    PublicId = obj.MenuId.Trim()
                };

                var uploadResult = _cloudinary.Upload(uploadParams);
                obj.PictureUrl = uploadResult.Url.ToString();
            }

            obj.CreatedDate = DateTime.Now;
            obj.UpdatedDate = null;
            obj.DeleteDate = null;

            _menuRepo.Add(obj);
            _menuRepo.Save();
            int cnt = menusList.Count() + 1;
            int totalPages = (int)Math.Ceiling(cnt / (double)pageSize);
            return RedirectToAction("Index", new { pageNumber = totalPages });
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var menuItem = _menuRepo.Get(m => m.MenuId == id);
            var categoriesList = _categoryRepo.GetAllIds(c => new CategoryIdName
            {
                CategoryId = c.CategoryId,
                Name = c.Name
            });
            ViewData["CategoryList"] = categoriesList;
            return View(menuItem);
        }

        [HttpPost]
        public IActionResult Edit(Menu obj, IFormFile? pictureFile) 
        {
            var existingCategory = _menuRepo.Get(m => m.MenuId == obj.MenuId);

            if (existingCategory == null)
            {
                return NotFound();
            }

            if (pictureFile != null && pictureFile.Length > 0)
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(pictureFile.FileName, pictureFile.OpenReadStream()),
                    PublicId = obj.MenuId.Trim()
                };

                var uploadResult = _cloudinary.Upload(uploadParams);

                if (uploadResult.StatusCode == HttpStatusCode.OK)
                {
                    existingCategory.PictureUrl = uploadResult.Url.ToString();
                }
                else
                {
                    ModelState.AddModelError("", "Upload hình ảnh không thành công.");
                    return RedirectToAction("Index");
                }
            }

            existingCategory.UpdatedDate = DateTime.Now;

            if (obj.Name != null)
            {
                existingCategory.Name = obj.Name;
            }

            if (obj.Description != null)
            {
                existingCategory.Description = obj.Description;
            }

            if (obj.Price != null)
            {
                existingCategory.Price = obj.Price;
            }

            if (obj.CategoryId != null)
            {
                existingCategory.CategoryId = obj.CategoryId;
            }

            _menuRepo.Save();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            var menuItem = _menuRepo.Get(m => m.MenuId == id);

            if (menuItem == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                _menuRepo.Remove(menuItem);
                _menuRepo.Save();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Không thể xóa danh mục này vì nó đang được sử dụng ở nơi khác.");
                return StatusCode(400, new { message = "Không thể xóa danh mục này vì nó đang được sử dụng ở nơi khác." });
            }
        }

        [HttpPost]
        public IActionResult DeleteMulti(string[] idArr)
        {
            if (idArr.Length > 0)
            {
                string[] splitId = idArr[0].Split(',');
                foreach (string id in splitId)
                {
                    var menuItem = _menuRepo.Get(m => m.MenuId == id);

                    if (menuItem == null)
                    {
                        return RedirectToAction("Index");
                    }

                    try
                    {
                        _menuRepo.Remove(menuItem);
                        _menuRepo.Save();
                    }
                    catch (DbUpdateException ex)
                    {
                        ModelState.AddModelError("", "Không thể xóa danh mục này vì nó đang được sử dụng ở nơi khác.");
                        return StatusCode(400, new { message = "Không thể xóa danh mục này vì nó đang được sử dụng ở nơi khác." });
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Vui lòng chọn danh mục để thao tác");
                return StatusCode(400, new { message = "Vui lòng chọn danh mục để thao tác." });
            }

            return RedirectToAction("Index");
        }
    }
}

