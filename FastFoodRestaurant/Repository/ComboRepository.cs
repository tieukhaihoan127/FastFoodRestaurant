using FastFoodRestaurant.Data;
using FastFoodRestaurant.Models;
using FastFoodRestaurant.Repository.IGenericRepository;
using Microsoft.EntityFrameworkCore;

namespace FastFoodRestaurant.Repository
{
    public class ComboRepository : GenericRepository<Combo>, IComboRepository
    {
        private ApplicationDbContext _db;
        public ComboRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public List<Combo> GetComboForParty()
        {
            var comboList = _db.Combos.Where(p => p.IsForParty == true).ToList();
            return comboList;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Combo obj)
        {
            _db.Combos.Update(obj);
        }
    }
}
