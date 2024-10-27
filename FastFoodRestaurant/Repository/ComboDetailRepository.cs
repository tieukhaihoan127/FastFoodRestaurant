using FastFoodRestaurant.Data;
using FastFoodRestaurant.Models;
using FastFoodRestaurant.Repository.IGenericRepository;
using Microsoft.EntityFrameworkCore;

namespace FastFoodRestaurant.Repository
{
    public class ComboDetailRepository : GenericRepository<ComboDetail>, IComboDetailRepository
    {
        private ApplicationDbContext _db;
        public ComboDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<ComboDetail> GetIncludeAll()
        {
            return _db.ComboDetails.Include(c => c.Menu).ToList();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(ComboDetail obj)
        {
            _db.ComboDetails.Update(obj);
        }
    }
}
