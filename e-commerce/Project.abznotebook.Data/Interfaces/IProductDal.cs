using System.Collections.Generic;
using System.Linq;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Data.Interfaces
{
    public interface IProductDal : IGenericDal<Product>
    {
        public IQueryable<Product> Products { get; }

        Product GetSpesificProduct(int id);
        List<Product> GetProductsByCategoryId(int Id);
    }
}