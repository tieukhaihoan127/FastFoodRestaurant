using FastFoodRestaurant.Models;
using System.Linq.Expressions;

namespace FastFoodRestaurant.Repository.IGenericRepository
{
    public interface IBillRepository : IGenericRepository<Bill>
    {
        void Update(Bill obj);
        IEnumerable<Bill> GetRecentBill(Expression<Func<Bill, DateTime?>> filter);
        double calculateSum(Expression<Func<Bill, bool>> filter1, Expression<Func<Bill, double>> filter2);
        int getBillCountPaymentStatus(Expression<Func<Bill, bool>> filter);
        double getTotalPrice(Expression<Func<Bill, double>> filter);
        int getTotalCount();
        void Save();
    }
}
