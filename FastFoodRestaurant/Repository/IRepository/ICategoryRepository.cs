using FastFoodRestaurant.DTO;
using FastFoodRestaurant.Models;
using System.Linq.Expressions;

namespace FastFoodRestaurant.Repository.IGenericRepository
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        List<CategoryIdName> GetAllIds(Expression<Func<Category, CategoryIdName>> filter);
        void Update(Category obj);
        void Save();
    }
}
