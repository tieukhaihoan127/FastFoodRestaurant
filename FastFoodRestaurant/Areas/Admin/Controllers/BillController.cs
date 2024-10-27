using FastFoodRestaurant.Data;
using FastFoodRestaurant.Models;
using FastFoodRestaurant.Repository.IGenericRepository;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodRestaurant.Controllers
{
    [Area("Admin")]
    public class BillController : Controller
    {
        private readonly IBillRepository _billRepo;
        private const int pageSize = 4;

        public BillController(IBillRepository billRepo)
        {
            _billRepo = billRepo;
        }

        public IActionResult Index(string? keyword, string? name, int pageNumber = 1)
        {
            List<Bill> billsList;

            if (!string.IsNullOrEmpty(keyword) && !string.IsNullOrEmpty(name))
            {
                billsList = _billRepo.GetAllExpression(b => b.BillId == keyword && b.ClientPhoneNumber == name).ToList();
            }
            else if (!string.IsNullOrEmpty(keyword))
            {
                billsList = _billRepo.GetAllExpression(b => b.BillId == keyword).ToList();
            }
            else if (!string.IsNullOrEmpty(name))
            {
                billsList = _billRepo.GetAllExpression(b => b.ClientPhoneNumber == name).ToList();
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
    }
}
