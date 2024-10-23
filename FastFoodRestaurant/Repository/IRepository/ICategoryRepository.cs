using FastFoodRestaurant.Models;

namespace FastFoodRestaurant.Repository.IGenericRepository
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        void Update(Category obj);
        void Save();
    }
}
