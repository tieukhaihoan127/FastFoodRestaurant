using FastFoodRestaurant.Data;
using FastFoodRestaurant.Models;
using FastFoodRestaurant.Repository.IGenericRepository;

namespace FastFoodRestaurant.Repository
{
    public class SystemUserRepository : GenericRepository<SystemUser>, ISystemUserRepository
    {
        private ApplicationDbContext _db;
        public SystemUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
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
