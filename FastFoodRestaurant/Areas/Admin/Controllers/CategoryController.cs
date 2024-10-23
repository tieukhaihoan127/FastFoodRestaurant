using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FastFoodRestaurant.Data;
using FastFoodRestaurant.Models;
using FastFoodRestaurant.Repository.IGenericRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FastFoodRestaurant.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;
        private const int pageSize = 4;
        private readonly Cloudinary _cloudinary;
        public CategoryController(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
            var account = new Account("dwdhkwu0r", "182449369914218", "d1gJYHWsz6Rz9wFmFD9S6kYtka4");
            _cloudinary = new Cloudinary(account);
        }
        public IActionResult Index(string? keyword,string? name,int pageNumber = 1)
        {
            List<Category> categoriesList;

            if (!string.IsNullOrEmpty(keyword) && !string.IsNullOrEmpty(name))
            {
                categoriesList = _categoryRepo.GetAllExpression(c => c.CategoryId == keyword && c.Name == name).ToList();
            }
            else if (!string.IsNullOrEmpty(keyword))
            {
                categoriesList = _categoryRepo.GetAllExpression(c => c.CategoryId == keyword).ToList();
            }
            else if(!string.IsNullOrEmpty(name))
            {
                categoriesList = _categoryRepo.GetAllExpression(c => c.Name == name).ToList();
            }
            else
            {
                categoriesList = _categoryRepo.GetAll().ToList();
            }

            var pagedCateogryList = categoriesList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            ViewData["CurrentPage"] = pageNumber;
            ViewData["TotalPages"] = (int)Math.Ceiling(categoriesList.Count() / (double)pageSize);

            return View(pagedCateogryList);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var categoryItem = _categoryRepo.Get(c => c.CategoryId == id);
            return View(categoryItem);
        }

        public IActionResult Create()
        {
            var currentId = _categoryRepo.getCurrentId(c => c.CategoryId);
            string numberExtract = currentId.CategoryId.Trim().Substring(2);
            int num = int.Parse(numberExtract);
            num += 1;
            string newId = "";

            if(num < 10)
            {
                newId = "CT000000000" + num; 
            }
            else if(num < 100)
            {
                newId = "CT00000000" + num;
            }
            else if(num < 1000)
            {
                newId = "CT0000000" + num;
            }
            else if (num < 10000)
            {
                newId = "CT000000" + num;
            }
            else if (num < 100000)
            {
                newId = "CT00000" + num;
            }
            else if (num < 1000000)
            {
                newId = "CT0000" + num;
            }
            else if (num < 10000000)
            {
                newId = "CT000" + num;
            }
            else if (num < 100000000)
            {
                newId = "CT00" + num;
            }
            else if (num < 1000000000)
            {
                newId = "CT0" + num;
            }
            else 
            {
                newId = "CT" + num;
            }

            ViewData["CurrentId"] = newId;
            return View();
        }

        [HttpPost] 
        public IActionResult Create(Category obj, IFormFile pictureFile)
        {
            obj.CategoryId = obj.CategoryId?.Trim();
            if (pictureFile != null && pictureFile.Length > 0)
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(pictureFile.FileName, pictureFile.OpenReadStream()),
                    PublicId = obj.CategoryId 
                };

                var uploadResult = _cloudinary.Upload(uploadParams);
                obj.PictureUrl = uploadResult.Url.ToString(); 
            }

            obj.CreatedDate = DateTime.Now;
            obj.UpdatedDate = null;
            obj.DeleteDate = null;

            _categoryRepo.Add(obj);
            _categoryRepo.Save();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(Category obj, IFormFile? pictureFile)
        {
            obj.CategoryId = obj.CategoryId?.Trim();
            var existingCategory = _categoryRepo.Get(c => c.CategoryId == obj.CategoryId);

            if (existingCategory == null)
            {
                return NotFound(); 
            }

            if (pictureFile != null && pictureFile.Length > 0)
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(pictureFile.FileName, pictureFile.OpenReadStream()),
                    PublicId = obj.CategoryId 
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

            if(obj.Name != null)
            {
                existingCategory.Name = obj.Name;
            }

            if (obj.Description != null)
            {
                existingCategory.Description = obj.Description;
            }

            _categoryRepo.Save();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            var categoryItem = _categoryRepo.Get(c => c.CategoryId == id);

            if (categoryItem == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                _categoryRepo.Remove(categoryItem);
                _categoryRepo.Save();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Không thể xóa danh mục này vì nó đang được sử dụng ở nơi khác.");
                return StatusCode(400, new { message = "Không thể xóa danh mục này vì nó đang được sử dụng ở nơi khác." });
            }
        }

        [HttpPost]
        public IActionResult DeleteMulti(string[] idArr )
        {
            if(idArr.Length > 0)
            {
                string[] splitId = idArr[0].Split(',');
                foreach (string id in splitId) 
                {
                    var categoryItem = _categoryRepo.Get(c => c.CategoryId == id);

                    if (categoryItem == null)
                    {
                        return RedirectToAction("Index");
                    }

                    try
                    {
                        _categoryRepo.Remove(categoryItem);
                        _categoryRepo.Save();
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
