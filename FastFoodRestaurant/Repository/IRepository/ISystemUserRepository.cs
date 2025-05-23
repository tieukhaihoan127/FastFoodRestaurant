using FastFoodRestaurant.DTO;
using FastFoodRestaurant.Models;
using System.Linq.Expressions;

namespace FastFoodRestaurant.Repository.IGenericRepository
{
    public interface ISystemUserRepository : IGenericRepository<SystemUser>
    {
        public List<UserIdName> GetAllIds(Expression<Func<SystemUser, UserIdName>> filter);
        void Update(SystemUser obj);
        void Save();
    }
}
