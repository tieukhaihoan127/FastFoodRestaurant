using FastFoodRestaurant.Models;

namespace FastFoodRestaurant.Repository.IGenericRepository
{
    public interface IStoreRepository : IGenericRepository<Store>
    {
        void Update(Store obj);
        void Save();
    }
}
