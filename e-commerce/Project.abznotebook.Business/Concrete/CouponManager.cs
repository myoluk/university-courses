using System.Collections.Generic;
using Project.abznotebook.Business.Interfaces;
using Project.abznotebook.Data.Interfaces;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Business.Concrete
{
    public class CouponManager : IGenericService<Coupon>, ICouponService
    {
        private readonly ICouponDal _couponDal;

        public CouponManager(ICouponDal couponDal)
        {
            _couponDal = couponDal;
        }
        public void Save(Coupon table)
        {
            _couponDal.Save(table);
        }

        public void Delete(Coupon table)
        {
            _couponDal.Delete(table);
        }

        public void Update(Coupon table)
        {
            _couponDal.Update(table);
        }

        public Coupon GetOrderWithId(int id)
        {
           return _couponDal.GetOrderWithId(id);
        }

        public List<Coupon> GetAllOrders()
        {
            return _couponDal.GetAllOrders();
        }

        public List<Coupon> GetAllCoupons()
        {
            return _couponDal.GetAllCoupons();
        }
    }
}