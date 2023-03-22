using System.Collections.Generic;
using System.Linq;
using Project.abznotebook.Data.Concrete.EntityFrameworkCore.Contexts;
using Project.abznotebook.Data.Interfaces;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Data.Concrete.EntityFrameworkCore.Repositories
{
    public class EfShipperRepository:EfGenericRepository<Shipper>, IShipperDal
    {
        private readonly TechnoStoreDbContext _dbContext;

        public EfShipperRepository(TechnoStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Shipper> GetAllShippers()
        {
            return _dbContext.Shippers.ToList();
        }
    }
}