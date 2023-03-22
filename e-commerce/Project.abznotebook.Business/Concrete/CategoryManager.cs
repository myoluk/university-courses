using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.abznotebook.Business.Interfaces;
using Project.abznotebook.Data.Interfaces;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public void Save(Category table)
        {
            _categoryDal.Save(table);
        }

        public void Delete(Category table)
        {
            _categoryDal.Delete(table);
        }

        public void Update(Category table)
        {
            _categoryDal.Update(table);
        }

        public Category GetOrderWithId(int id)
        {
            return _categoryDal.GetOrderWithId(id);
        }

        public List<Category> GetAllOrders()
        {
            return _categoryDal.GetAllOrders();
        }

        public IQueryable<Product> GetAllProducts => _categoryDal.GetAllProducts;
        public IQueryable<Category> GetAllCategories => _categoryDal.GetAllCategories;

        public IQueryable<Product> GetProductsOfGivenCategory(int CategoryId)
        {
            return _categoryDal.GetProductsOfGivenCategory(CategoryId);
        }

        public IQueryable<Product> GetProductsOfGivenCategory(string CategoryName)
        {
            return _categoryDal.GetProductsOfGivenCategory(CategoryName);
        }
    }
}
