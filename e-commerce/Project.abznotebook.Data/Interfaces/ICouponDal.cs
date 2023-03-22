using System.Collections.Generic;
using System.Threading.Channels;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Data.Interfaces
{
    public interface ICouponDal : IGenericDal<Coupon>
    {
        public List<Coupon> GetAllCoupons();
    }
}