using FastFoodRestaurant.Models;

namespace FastFoodRestaurant.Repository.IGenericRepository
{
    public interface IPartyRepository : IGenericRepository<Party>
    {
        void Update(Party obj);
        void Save();
    }
}

