using FastFoodRestaurant.Data;
using FastFoodRestaurant.DTO;
using FastFoodRestaurant.Models;
using FastFoodRestaurant.Repository.IGenericRepository;
using System.Linq;
using System.Linq.Expressions;

namespace FastFoodRestaurant.Repository
{
    public class SystemUserRepository : GenericRepository<SystemUser>, ISystemUserRepository
    {
        private ApplicationDbContext _db;
        public SystemUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public List<UserIdName> GetAllIds(Expression<Func<SystemUser, UserIdName>> filter)
        {
            IQueryable<SystemUser> query = dbSet;
            return query.Select(filter).ToList();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(SystemUser obj)
        {
            _db.SystemUsers.Update(obj);
        }
    }
}
