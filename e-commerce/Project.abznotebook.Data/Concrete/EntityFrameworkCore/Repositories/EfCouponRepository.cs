using System.Collections.Generic;
using System.Linq;
using Project.abznotebook.Data.Concrete.EntityFrameworkCore.Contexts;
using Project.abznotebook.Data.Interfaces;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Data.Concrete.EntityFrameworkCore.Repositories
{
    public class EfCouponRepository : EfGenericRepository<Coupon>, ICouponDal
    {
        private readonly TechnoStoreDbContext _dbContext;

        public EfCouponRepository(TechnoStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Coupon> GetAllCoupons()
        {
            return _dbContext.Coupons.ToList();
        }
    }
}