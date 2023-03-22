using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.abznotebook.Data.Concrete.EntityFrameworkCore.Contexts;
using Project.abznotebook.Data.Interfaces;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Data.Concrete.EntityFrameworkCore.Repositories
{
    public class EfOrderRepository : EfGenericRepository<Order>, IOrderDal
    {

        private readonly TechnoStoreDbContext _db;

        public EfOrderRepository(TechnoStoreDbContext db)
        {
            _db = db;
        }


        public List<Order> GetUnshippedOrders()
        {

            return _db.Orders.Where(I => I.IsShipped == false).ToList();
        }

        public List<Order> GetShippedOrders()
        {
            return _db.Orders.Where(I => I.IsShipped == true).ToList();
        }

        public List<Order> GetAllowedOrders()
        {
            return _db.Orders.Where(I => I.IsAllowed == true).ToList();
        }
    }
}