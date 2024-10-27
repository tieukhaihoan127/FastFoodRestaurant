using FastFoodRestaurant.Models;
using System.Linq.Expressions;
namespace FastFoodRestaurant.Repository.IGenericRepository
{
    public interface IComboRepository : IGenericRepository<Combo>
    {
        List<Combo> GetComboForParty();
        void Update(Combo obj);
        void Save();
    }
}
