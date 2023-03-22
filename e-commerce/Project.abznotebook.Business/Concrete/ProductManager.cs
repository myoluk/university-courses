using System.Collections.Generic;
using System.Linq;
using Project.abznotebook.Business.Interfaces;
using Project.abznotebook.Data.Interfaces;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public void Save(Product table)
        {
            _productDal.Save(table);
        }

        public void Delete(Product table)
        {
            _productDal.Delete(table);
        }

        public void Update(Product table)
        {
            _productDal.Update(table);
        }

        public Product GetOrderWithId(int id)
        {
            return _productDal.GetOrderWithId(id);
        }

        public List<Product> GetAllOrders()
        {
            return _productDal.GetAllOrders();
        }

        public IQueryable<Product> Products => _productDal.Products;



        public Product GetSpesificProduct(int id)
        {
            return _productDal.GetSpesificProduct(id);
        }

        public List<Product> GetProductsByCategoryId(int Id)
        {
            return _productDal.GetProductsByCategoryId(Id);
        }
    }
}