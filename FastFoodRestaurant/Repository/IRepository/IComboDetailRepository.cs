using FastFoodRestaurant.Models;

namespace FastFoodRestaurant.Repository.IGenericRepository
{
    public interface IComboDetailRepository : IGenericRepository<ComboDetail>
    {
        IEnumerable<ComboDetail> GetIncludeAll();
        void Update(ComboDetail obj);
        void Save();
    }
}
