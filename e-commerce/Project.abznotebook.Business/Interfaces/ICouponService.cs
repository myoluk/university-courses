using System.Collections.Generic;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Business.Interfaces
{
    public interface ICouponService : IGenericService<Coupon>
    { 
        public List<Coupon> GetAllCoupons();
    }
}