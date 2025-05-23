using FastFoodRestaurant.Models;
using System.Linq.Expressions;

namespace FastFoodRestaurant.Repository.IGenericRepository
{
    public interface ICartItemRepository : IGenericRepository<CartItem>
    {
        IEnumerable<CartItem> GetIncludeCategoryAll();
        IEnumerable<CartItem> GetIncludeCategoryAllExpression(Expression<Func<CartItem, bool>> filter);

        CartItem GetSingleMenuWithCategory(Expression<Func<CartItem, bool>> filter);
        void Update(Voucher obj);
        void Save();
    }
}
