using FastFoodRestaurant.Models;

namespace FastFoodRestaurant.Repository.IGenericRepository
{
    public interface ISystemUserRepository : IGenericRepository<SystemUser>
    {
        void Update(SystemUser obj);
        void Save();
    }
}
