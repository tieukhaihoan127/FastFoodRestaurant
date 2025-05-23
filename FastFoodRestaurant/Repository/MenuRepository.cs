﻿using FastFoodRestaurant.Data;
using FastFoodRestaurant.DTO;
using FastFoodRestaurant.Models;
using FastFoodRestaurant.Repository.IGenericRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace FastFoodRestaurant.Repository
{
    public class MenuRepository : GenericRepository<Menu>, IMenuRepository
    {
        private ApplicationDbContext _db;
        public MenuRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<Menu> GetIncludeCategoryAll()
        {
            return _db.Menus.Include(m => m.Category).ToList();
        }

        public List<MenuInfo> GetAll(Expression<Func<Menu, MenuInfo>> filter)
        {
            IQueryable<Menu> query = dbSet;
            return query.Select(filter).ToList();
        }

        public IEnumerable<Menu> GetIncludeCategoryAllExpression(Expression<Func<Menu, bool>> filter)
        {
            return _db.Menus.Include(m => m.Category).Where(filter).ToList();
        }

        public Menu GetSingleMenuWithCategory(Expression<Func<Menu, bool>> filter)
        {
            return _db.Menus.Include(m => m.Category).Where(filter).FirstOrDefault();
        }

        public List<Category> GetUniqueCategory()
        {
            return _db.Menus.Include(m => m.Category) .Select(m => m.Category).Distinct().ToList();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Menu obj)
        {
            _db.Menus.Update(obj);
        }
    }
}
