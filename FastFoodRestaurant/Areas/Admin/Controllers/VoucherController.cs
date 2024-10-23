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
    public class VoucherController : Controller
    {
        private readonly IVoucherRepository _voucherRepo;
        private const int pageSize = 4;

        public VoucherController(IVoucherRepository voucherRepo)
        {
            _voucherRepo = voucherRepo;
        }

        public IActionResult Index(string? keyword, int pageNumber = 1)
        {
            List<Voucher> vouchersList;

            if (!string.IsNullOrEmpty(keyword))
            {
                vouchersList = _voucherRepo.GetAllExpression(v => v.VoucherId == keyword).ToList();
            }
            else
            {
                vouchersList = _voucherRepo.GetAll().ToList();
            }

            var pagedVouchersList = vouchersList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            ViewData["CurrentPage"] = pageNumber;
            ViewData["TotalPages"] = (int)Math.Ceiling(vouchersList.Count() / (double)pageSize);

            return View(pagedVouchersList);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var voucherItem = _voucherRepo.Get(v => v.VoucherId == id);
            return View(voucherItem);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var currentId = _voucherRepo.getCurrentId(v => v.VoucherId);
            string numberExtract = currentId.VoucherId.Trim().Substring(2);
            int num = int.Parse(numberExtract);
            num += 1;
            string newId = "";

            if (num < 10)
            {
                newId = "VC000000000" + num;
            }
            else if (num < 100)
            {
                newId = "VC00000000" + num;
            }
            else if (num < 1000)
            {
                newId = "VC0000000" + num;
            }
            else if (num < 10000)
            {
                newId = "VC000000" + num;
            }
            else if (num < 100000)
            {
                newId = "VC00000" + num;
            }
            else if (num < 1000000)
            {
                newId = "VC0000" + num;
            }
            else if (num < 10000000)
            {
                newId = "VC000" + num;
            }
            else if (num < 100000000)
            {
                newId = "VC00" + num;
            }
            else if (num < 1000000000)
            {
                newId = "VC0" + num;
            }
            else
            {
                newId = "VC" + num;
            }

            ViewData["CurrentId"] = newId;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Voucher obj)
        {
            _voucherRepo.Add(obj);
            _voucherRepo.Save();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(Voucher obj)
        {
            obj.VoucherId = obj.VoucherId?.Trim();
            var existingVoucher = _voucherRepo.Get(v => v.VoucherId == obj.VoucherId);

            if (existingVoucher == null)
            {
                return NotFound();
            }

            if (obj.MaximumUsed != null)
            {
                existingVoucher.MaximumUsed = obj.MaximumUsed;
            }

            if (obj.Description != null)
            {
                existingVoucher.Description = obj.Description;
            }

            if (obj.DiscountPercentage != null)
            {
                existingVoucher.DiscountPercentage = obj.DiscountPercentage;
            }

            if (obj.Status != null)
            {
                existingVoucher.Status = obj.Status;
            }

            if (obj.StartedDate != null)
            {
                existingVoucher.StartedDate = obj.StartedDate;
            }

            if (obj.ExpiredDate != null)
            {
                existingVoucher.ExpiredDate = obj.ExpiredDate;
            }

            _voucherRepo.Save();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            var voucherItem = _voucherRepo.Get(v => v.VoucherId == id);

            if (voucherItem == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                _voucherRepo.Remove(voucherItem);
                _voucherRepo.Save();
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
                    var voucherItem = _voucherRepo.Get(v => v.VoucherId == id);

                    if (voucherItem == null)
                    {
                        return RedirectToAction("Index");
                    }

                    try
                    {
                        _voucherRepo.Remove(voucherItem);
                        _voucherRepo.Save();
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
