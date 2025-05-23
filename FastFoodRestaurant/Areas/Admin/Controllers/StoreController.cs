using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using FastFoodRestaurant.Models;
using FastFoodRestaurant.Repository.IGenericRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.EntityFrameworkCore;

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

        public IActionResult Index(int pageNumber = 1)
        {
            List<Store> storesList;

            storesList = _storeRepo.GetAll().ToList();

            var pagedVouchersList = storesList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            foreach (var obj in pagedVouchersList)
            {
                obj.StoreId = obj.StoreId.Trim();
            }

            ViewData["CurrentPage"] = pageNumber;
            ViewData["TotalPages"] = (int)Math.Ceiling(storesList.Count() / (double)pageSize);

            return View(pagedVouchersList);
        }

        [HttpPost]
        public IActionResult Index(string? object_id,string? city,string? district, string? ward, int pageNumber = 1)
        {
            List<Store> storesList;

            if (!string.IsNullOrEmpty(object_id))
            {
                storesList = _storeRepo.GetAllExpression(s => s.StoreId == object_id).ToList();
            }
            else
            {
                storesList = _storeRepo.GetAll().ToList();
            }

            var pagedCateogryList = storesList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            foreach (var obj in pagedCateogryList)
            {
                obj.StoreId = obj.StoreId.Trim();
            }

            ViewData["CurrentPage"] = pageNumber;
            ViewData["TotalPages"] = (int)Math.Ceiling(storesList.Count() / (double)pageSize);

            return View(pagedCateogryList);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var storeItem = _storeRepo.Get(s => s.StoreId == id);
            return View(storeItem);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var currentId = _storeRepo.getCurrentId(s => s.StoreId);
            string numberExtract = currentId.StoreId.Trim().Substring(2);
            int num = int.Parse(numberExtract);
            num += 1;
            string newId = "";

            if (num < 10)
            {
                newId = "ST000" + num;
            }
            else if (num < 100)
            {
                newId = "ST00" + num;
            }
            else if (num < 1000)
            {
                newId = "ST0" + num;
            }
            else
            {
                newId = "ST" + num;
            }

            ViewData["CurrentId"] = newId;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Store obj)
        {
            List<Store> storesList = _storeRepo.GetAll().ToList();

            obj.CreatedDate = DateTime.Now;
            obj.UpdatedDate = null;
            obj.DeleteDate = null;

            _storeRepo.Add(obj);
            _storeRepo.Save();
            int cnt = storesList.Count() + 1;
            int totalPages = (int)Math.Ceiling(cnt / (double)pageSize);
            return RedirectToAction("Index", new { pageNumber = totalPages });
        }

        [HttpPost]
        public IActionResult Edit(Store obj)
        {
            var existingCategory = _storeRepo.Get(s => s.StoreId == obj.StoreId);

            if (existingCategory == null)
            {
                ModelState.AddModelError("", "Danh mục không tồn tại.");
                return View(obj);
            }

            existingCategory.UpdatedDate = DateTime.Now;

            if (obj.Name != null)
            {
                existingCategory.Name = obj.Name;
            }

            if (obj.Address != null)
            {
                existingCategory.Address = obj.Address;
            }

            if (obj.District != null)
            {
                existingCategory.District = obj.District;
            }

            if (obj.City != null)
            {
                existingCategory.City = obj.City;
            }

            if (obj.Ward != null)
            {
                existingCategory.Ward = obj.Ward;
            }

            if (obj.Hotline != null)
            {
                existingCategory.Hotline = obj.Hotline;
            }

            if (obj.OpeningHour != null)
            {
                existingCategory.OpeningHour = obj.OpeningHour;
            }

            if (obj.ClosingTime != null)
            {
                existingCategory.ClosingTime = obj.ClosingTime;
            }

            _storeRepo.Save();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            var storeItem = _storeRepo.Get(s => s.StoreId == id);

            if (storeItem == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                _storeRepo.Remove(storeItem);
                _storeRepo.Save();
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
                    var storeItem = _storeRepo.Get(s => s.StoreId == id);

                    if (storeItem == null)
                    {
                        return RedirectToAction("Index");
                    }

                    try
                    {
                        _storeRepo.Remove(storeItem);
                        _storeRepo.Save();
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
