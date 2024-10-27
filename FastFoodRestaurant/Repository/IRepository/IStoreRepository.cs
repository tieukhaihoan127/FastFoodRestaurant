using FastFoodRestaurant.Models;
using System.Linq.Expressions;

namespace FastFoodRestaurant.Repository.IGenericRepository
{
    public interface IStoreRepository : IGenericRepository<Store>
    {
        Store getSingleStore(Expression<Func<Store, bool>> filter);
        void Update(Store obj);
        void Save();
    }
}
