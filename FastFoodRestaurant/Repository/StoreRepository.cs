using FastFoodRestaurant.Data;
using FastFoodRestaurant.DTO;
using FastFoodRestaurant.Models;
using FastFoodRestaurant.Repository.IGenericRepository;
using System.Linq.Expressions;


namespace FastFoodRestaurant.Repository
{
    public class StoreRepository : GenericRepository<Store>, IStoreRepository
    {
        private ApplicationDbContext _db;
        public StoreRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public Store getSingleStore(System.Linq.Expressions.Expression<Func<Store, bool>> filter)
        {
            var singleItem = _db.Stores.Where(filter).FirstOrDefault();
            return singleItem;
        }

        public List<StoreIdName> GetAllIds(Expression<Func<Store, StoreIdName>> filter)
        {
            IQueryable<Store> query = dbSet;
            return query.Select(filter).ToList();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Store obj)
        {
            _db.Stores.Update(obj);
        }
    }
}
