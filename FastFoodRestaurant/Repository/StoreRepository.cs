using FastFoodRestaurant.Data;
using FastFoodRestaurant.Models;
using FastFoodRestaurant.Repository.IGenericRepository;


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
