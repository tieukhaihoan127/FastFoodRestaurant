using FastFoodRestaurant.Models;
using FastFoodRestaurant.Repository.IGenericRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FastFoodRestaurant.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IBillRepository _billRepo;
        private const int pageSize = 4;

        public HomeController(IBillRepository billRepo)
        {
            _billRepo = billRepo;
        }

        public IActionResult Index()
        {
            List<Bill> billList;

            DateTime today = DateTime.Now;
            string dayOfWeek = today.DayOfWeek.ToString();
            string dow = "";
            switch(dayOfWeek)
            {
                case "Monday":
                    dow = "Thứ hai";
                    break;
                case "Tuesday":
                    dow = "Thứ ba";
                    break;
                case "Wednesday":
                    dow = "Thứ tư";
                    break;
                case "Thursday":
                    dow = "Thứ năm";
                    break;
                case "Friday":
                    dow = "Thứ sáu";
                    break;
                case "Saturday":
                    dow = "Thứ bảy";
                    break;
                case "Sunday":
                    dow = "Chủ nhật";
                    break;
                default:
                    break;
            }
            int day = today.Day;
            int month = today.Month;
            int year = today.Year;
            string todayContent = $"{dow}, ngày {day} tháng {month}, {year}";
            var startOfWeek = DateTime.Now;
            var endOfWeek = DateTime.Now;

            if ((int)DateTime.Today.DayOfWeek == 0)
            {
                endOfWeek = DateTime.Today;
                startOfWeek = startOfWeek.AddDays(-7);
            }
            else if ((int)DateTime.Today.DayOfWeek == 1)
            {
                startOfWeek = DateTime.Today;
                endOfWeek = startOfWeek.AddDays(7);
            }
            else if ((int)DateTime.Today.DayOfWeek == 2)
            {
                startOfWeek = startOfWeek.AddDays(-1);
                endOfWeek = startOfWeek.AddDays(7);
            }
            else if ((int)DateTime.Today.DayOfWeek == 3)
            {
                startOfWeek = startOfWeek.AddDays(-2);
                endOfWeek = startOfWeek.AddDays(7);
            }
            else if ((int)DateTime.Today.DayOfWeek == 4)
            {
                startOfWeek = startOfWeek.AddDays(-3);
                endOfWeek = startOfWeek.AddDays(7);
            }
            else if ((int)DateTime.Today.DayOfWeek == 5)
            {
                startOfWeek = startOfWeek.AddDays(-4);
                endOfWeek = startOfWeek.AddDays(7);
            }
            else if ((int)DateTime.Today.DayOfWeek == 6)
            {
                startOfWeek = startOfWeek.AddDays(-5);
                endOfWeek = startOfWeek.AddDays(7);
            }
            double thisWeekSum = _billRepo.calculateSum(b => b.CreatedDate >= startOfWeek && b.CreatedDate < endOfWeek, b => b.TotalPrice);
            int thisWeekCount = _billRepo.getBillCountPaymentStatus(b => b.CreatedDate >= startOfWeek && b.CreatedDate < endOfWeek);

            var startOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1);
            double thisMonthSum = _billRepo.calculateSum(b => b.CreatedDate >= startOfMonth && b.CreatedDate < endOfMonth, b => b.TotalPrice);
            int thisMonthCount = _billRepo.getBillCountPaymentStatus(b => b.CreatedDate >= startOfMonth && b.CreatedDate < endOfMonth);

            billList = _billRepo.GetRecentBill(b => b.CreatedDate).ToList();

            ViewData["Today"] = todayContent;
            ViewData["ThisWeekSum"] = thisWeekSum;
            ViewData["ThisWeekCount"] = thisWeekCount;
            ViewData["ThisMonthSum"] = thisMonthSum;
            ViewData["ThisMonthCount"] = thisMonthCount;

            return View(billList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
