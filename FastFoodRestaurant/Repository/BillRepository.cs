using FastFoodRestaurant.Data;
using FastFoodRestaurant.Models;
using FastFoodRestaurant.Repository.IGenericRepository;
using System.Linq.Expressions;

namespace FastFoodRestaurant.Repository
{
    public class BillRepository : GenericRepository<Bill>, IBillRepository
    {
        private ApplicationDbContext _db;
        public BillRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public double calculateSum(Expression<Func<Bill, bool>> filter1, Expression<Func<Bill, double>> filter2)
        {
            var totalSum = _db.Bills.Where(filter1).Sum(filter2);
            return totalSum;
        }

        public int getBillCountPaymentStatus(Expression<Func<Bill, bool>> filter)
        {
            var totalCount = _db.Bills.Count(filter);
            return totalCount;
        }

        public IEnumerable<Bill> GetRecentBill(Expression<Func<Bill, DateTime?>> filter)
        {
            var totalBill = _db.Bills.OrderByDescending(filter).Take(3).ToList();
            return totalBill;
        }

        public int getTotalCount()
        {
            var totalCount = _db.Bills.Count();
            return totalCount;
        }

        public double getTotalPrice(Expression<Func<Bill, double>> filter)
        {
            var totalSum = _db.Bills.Sum(filter);
            return totalSum;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Bill obj)
        {
            _db.Bills.Update(obj);
        }
    }
}
