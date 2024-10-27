using FastFoodRestaurant.Data;
using FastFoodRestaurant.Models;
using FastFoodRestaurant.Repository.IGenericRepository;

namespace FastFoodRestaurant.Repository
{
    public class PartyRepository : GenericRepository<Party>, IPartyRepository
    {
        private ApplicationDbContext _db;
        public PartyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Party obj)
        {
            _db.Parties.Update(obj);
        }
    }
}
