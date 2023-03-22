using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.EntityFrameworkCore;
using Project.abznotebook.Data.Concrete.EntityFrameworkCore.Contexts;
using Project.abznotebook.Data.Interfaces;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Data.Concrete.EntityFrameworkCore.Repositories
{
    public class EfProductRepository : EfGenericRepository<Product>, IProductDal
    {
        private TechnoStoreDbContext _context;

        public EfProductRepository(TechnoStoreDbContext context)
        {
            _context = context;
        }

        public IQueryable<Product> Products => _context.Products;

        public List<Product> GetAllProducts()
        {
            return _context.Products.OrderByDescending(I => I.IsAvailable).ToList();
        }

        public Product GetSpesificProduct(int id)
        {
            return _context.Products.Single(I => I.Id == id);
        }

        public List<Product> GetProductsByCategoryId(int Id)
        {
            return _context.Products.Where(I => I.CategoryId == Id).ToList();
        }
    }
}