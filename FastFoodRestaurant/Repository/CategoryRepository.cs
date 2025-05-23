using FastFoodRestaurant.Data;
using FastFoodRestaurant.DTO;
using FastFoodRestaurant.Models;
using FastFoodRestaurant.Repository.IGenericRepository;
using System.Linq.Expressions;

namespace FastFoodRestaurant.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db) {
            _db = db;
        }

        public List<CategoryIdName> GetAllIds(Expression<Func<Category, CategoryIdName>> filter)
        {
            IQueryable<Category> query = dbSet;
            return query.Select(filter).ToList();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
