using System.Collections.Generic;
using System.Linq;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Business.Interfaces
{
    public interface IProductService : IGenericService<Product>
    {
        public IQueryable<Product> Products { get; }
        public Product GetSpesificProduct(int id);
        public List<Product> GetProductsByCategoryId(int Id);

    }
}