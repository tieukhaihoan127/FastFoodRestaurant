using FastFoodRestaurant.Data;
using FastFoodRestaurant.Models;
using FastFoodRestaurant.Repository.IGenericRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace FastFoodRestaurant.Repository
{
    public class CartItemRepository : GenericRepository<CartItem>, ICartItemRepository
    {
        private ApplicationDbContext _db;
        public CartItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public IEnumerable<CartItem> GetIncludeCategoryAll()
        {
            return _db.CartItem.Include(m => m.Menu).ToList();
        }

        public IEnumerable<CartItem> GetIncludeCategoryAllExpression(Expression<Func<CartItem, bool>> filter)
        {
            return _db.CartItem.Include(m => m.Menu).Where(filter).ToList();
        }


        public CartItem GetSingleMenuWithCategory(Expression<Func<CartItem, bool>> filter)
        {
            return _db.CartItem.Include(m => m.Menu).Where(filter).FirstOrDefault();
        }


        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Voucher obj)
        {
            _db.Vouchers.Update(obj);
        }
    }
}
