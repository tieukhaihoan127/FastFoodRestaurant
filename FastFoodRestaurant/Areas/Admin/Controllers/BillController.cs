using FastFoodRestaurant.Data;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodRestaurant.Controllers
{
    [Area("Admin")]
    public class BillController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BillController(ApplicationDbContext db)
        {
            _db = db;   
        }
        public IActionResult Index()
        {
            var billLists = _db.Bills.OrderBy(s => s.BillId).ToList();
            return View(billLists);
        }
    }
}
