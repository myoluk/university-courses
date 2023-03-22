﻿using System.Linq;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Business.Interfaces
{
    public interface ICategoryService : IGenericService<Category>
    {
        IQueryable<Product> GetAllProducts { get; }
        IQueryable<Category> GetAllCategories { get; }
        IQueryable<Product> GetProductsOfGivenCategory(int CategoryId);
        IQueryable<Product> GetProductsOfGivenCategory(string CategoryName);
    }
}