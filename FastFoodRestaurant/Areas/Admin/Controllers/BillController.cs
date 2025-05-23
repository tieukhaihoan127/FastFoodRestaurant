using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using FastFoodRestaurant.Data;
using FastFoodRestaurant.DTO;
using FastFoodRestaurant.Models;
using FastFoodRestaurant.Repository.IGenericRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace FastFoodRestaurant.Controllers
{
    [Area("Admin")]
    public class BillController : Controller
    {
        private readonly IBillRepository _billRepo;
        private readonly ISystemUserRepository _systemUserRepo;
        private const int pageSize = 4;

        public BillController(IBillRepository billRepo, ISystemUserRepository systemUserRepo)
        {
            _billRepo = billRepo;
            _systemUserRepo = systemUserRepo;
        }

        public IActionResult Index(int pageNumber = 1)
        {
            List<Bill> billsList;

            billsList = _billRepo.GetAll().ToList();

            var pagedBillsList = billsList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            foreach (var obj in billsList)
            {
                obj.BillId = obj.BillId.Trim();
            }

            var totalSum = _billRepo.getTotalPrice(b => b.TotalPrice);
            var totalCount = _billRepo.getTotalCount();

            var totalUnpaidSum = _billRepo.calculateSum(b => b.PaymentStatus == true, b => b.TotalPrice);
            var totalUnpaidCount = _billRepo.getBillCountPaymentStatus(b => b.PaymentStatus == true);

            var totalPaidSum = _billRepo.calculateSum(b => b.PaymentStatus == false, b => b.TotalPrice);
            var totalPaidCount = _billRepo.getBillCountPaymentStatus(b => b.PaymentStatus == false);

            ViewData["CurrentPage"] = pageNumber;
            ViewData["TotalPages"] = (int)Math.Ceiling(billsList.Count() / (double)pageSize);
            ViewData["TotalSum"] = totalSum;
            ViewData["TotalCount"] = totalCount;
            ViewData["UnpaidSum"] = totalUnpaidSum;
            ViewData["UnpaidCount"] = totalUnpaidCount;
            ViewData["PaidSum"] = totalPaidSum;
            ViewData["PaidCount"] = totalPaidCount;

            return View(pagedBillsList);
        }

        [HttpPost]
        public IActionResult Index(string? object_id, string? object_name, Boolean? isPaid, string? Status, int pageNumber = 1)
        {
            List<Bill> billsList;

            if (!string.IsNullOrEmpty(object_id) && !string.IsNullOrEmpty(object_name))
            {
                billsList = _billRepo.GetAllExpression(b => b.BillId == object_id && b.ClientPhoneNumber == object_name).ToList();
            }
            else if (!string.IsNullOrEmpty(object_id))
            {
                billsList = _billRepo.GetAllExpression(b => b.BillId == object_id).ToList();
            }
            else if(isPaid.HasValue)
            {
                if (isPaid == false)
                {
                    billsList = _billRepo.GetAllExpression(v => v.PaymentStatus == false).ToList();
                }
                else
                {
                    billsList = _billRepo.GetAllExpression(v => v.PaymentStatus == true).ToList();
                }
            }
            else if(!string.IsNullOrEmpty(Status))
            {
                if (Status == "new")
                {
                    billsList = _billRepo.GetAllExpression(v => v.Status == 1).ToList();
                }
                else if(Status == "prepare")
                {
                    billsList = _billRepo.GetAllExpression(v => v.Status == 2).ToList();
                }
                else
                {
                    billsList = _billRepo.GetAllExpression(v => v.Status == 3).ToList();
                }
            }
            else if (!string.IsNullOrEmpty(object_name))
            {
                billsList = _billRepo.GetAllExpression(b => b.ClientPhoneNumber == object_name).ToList();
            }
            else
            {
                billsList = _billRepo.GetAll().ToList();
            }

            var pagedBillsList = billsList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var totalSum = _billRepo.getTotalPrice(b => b.TotalPrice);
            var totalCount = _billRepo.getTotalCount();

            var totalUnpaidSum = _billRepo.calculateSum(b => b.PaymentStatus == true, b => b.TotalPrice);
            var totalUnpaidCount = _billRepo.getBillCountPaymentStatus(b => b.PaymentStatus == true);

            var totalPaidSum = _billRepo.calculateSum(b => b.PaymentStatus == false, b => b.TotalPrice);
            var totalPaidCount = _billRepo.getBillCountPaymentStatus(b => b.PaymentStatus == false);

            ViewData["CurrentPage"] = pageNumber;
            ViewData["TotalPages"] = (int)Math.Ceiling(billsList.Count() / (double)pageSize);
            ViewData["TotalSum"] = totalSum;
            ViewData["TotalCount"] = totalCount;
            ViewData["UnpaidSum"] = totalUnpaidSum;
            ViewData["UnpaidCount"] = totalUnpaidCount;
            ViewData["PaidSum"] = totalPaidSum;
            ViewData["PaidCount"] = totalPaidCount;

            return View(pagedBillsList);
        }

        public IActionResult Edit(string id)
        {
            var billItem = _billRepo.Get(b => b.BillId == id);

            var userList = _systemUserRepo.GetAllIds(s => new UserIdName
            {
                UserId = s.UserId,
                Name = s.Name
            });

            ViewData["UserList"] = userList;
            return View(billItem);
        }

        [HttpPost]
        public IActionResult Edit(Bill obj)
        {
            var existingCategory = _billRepo.Get(b => b.BillId == obj.BillId);

            if (existingCategory == null)
            {
                ModelState.AddModelError("", "Danh mục không tồn tại.");
                return View(obj);
            }

            existingCategory.UpdatedDate = DateTime.Now;

            if (obj.ClientName != null)
            {
                existingCategory.ClientName = obj.ClientName;
            }

            if (obj.ClientPhoneNumber != null)
            {
                existingCategory.ClientPhoneNumber = obj.ClientPhoneNumber;
            }

            if (obj.ClientEmail != null)
            {
                existingCategory.ClientEmail = obj.ClientEmail;
            }

            if (obj.TotalPrice != null)
            {
                existingCategory.TotalPrice = obj.TotalPrice;
            }

            if (obj.Note != null)
            {
                existingCategory.Note = obj.Note;
            }

            if (obj.Status != null)
            {
                existingCategory.Status = obj.Status;
            }

            if (obj.ShippingDate != null)
            {
                existingCategory.ShippingDate = obj.ShippingDate;
            }

            if (obj.PaymentMethod != null)
            {
                existingCategory.PaymentMethod = obj.PaymentMethod;
            }


            _billRepo.Save();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            var billItem = _billRepo.Get(b => b.BillId == id);

            if (billItem == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                _billRepo.Remove(billItem);
                _billRepo.Save();
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
                    var billItem = _billRepo.Get(b => b.BillId == id);

                    if (billItem == null)
                    {
                        return RedirectToAction("Index");
                    }

                    try
                    {
                        _billRepo.Remove(billItem);
                        _billRepo.Save();
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
