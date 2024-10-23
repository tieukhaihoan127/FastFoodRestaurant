using FastFoodRestaurant.DTO;
using System.Linq.Expressions;

namespace FastFoodRestaurant.Repository.IGenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllExpression(Expression<Func<T, bool>> filter);
        List<CategoryIdName> GetAllIds(Expression<Func<T, CategoryIdName>> filter);
        T Get(Expression<Func<T,bool>> filter);
        T getCurrentId(Expression<Func<T, string>> filter);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
