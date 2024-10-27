using FastFoodRestaurant.Models;
using System.Linq.Expressions;

namespace FastFoodRestaurant.Repository.IGenericRepository
{
    public interface IMenuRepository : IGenericRepository<Menu>
    {
        IEnumerable<Menu> GetIncludeCategoryAll();
        IEnumerable<Menu> GetIncludeCategoryAllExpression(Expression<Func<Menu, bool>> filter);
        Menu GetSingleMenuWithCategory(Expression<Func<Menu, bool>> filter);
        List<Category> GetUniqueCategory();
        void Update(Menu obj);
        void Save();
    }
}

