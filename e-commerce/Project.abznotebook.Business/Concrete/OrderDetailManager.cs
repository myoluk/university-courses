using System.Collections.Generic;
using Project.abznotebook.Business.Interfaces;
using Project.abznotebook.Data.Interfaces;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Business.Concrete
{
    public class OrderDetailManager : IOrderDetailService
    {
        private readonly IOrderDetailDal _orderDetailDal;

        public OrderDetailManager(IOrderDetailDal orderDetailDal)
        {
            _orderDetailDal = orderDetailDal;
        }

        public void Save(OrderDetail table)
        {
            _orderDetailDal.Save(table);
        }

        public void Delete(OrderDetail table)
        {
            _orderDetailDal.Delete(table);
        }

        public void Update(OrderDetail table)
        {
            _orderDetailDal.Update(table);
        }

        public OrderDetail GetOrderWithId(int id)
        {
            return _orderDetailDal.GetOrderWithId(id);
        }

        public List<OrderDetail> GetAllOrders()
        {
            return _orderDetailDal.GetAllOrders();
        }

        public List<OrderDetail> GetAllOrderDetails()
        {
            return _orderDetailDal.GetAllOrderDetails();
        }

        public dynamic GetGivenUsersDetailedOrder(AppUser user)
        {
            return _orderDetailDal.GetGivenUsersDetailedOrder(user);
        }

        public decimal ComputeTotalPriceOfOrder(int orderId)
        {
            return _orderDetailDal.ComputeTotalPriceOfOrder(orderId);
        }

        public int ComputeTotalProductCount(int orderId) => _orderDetailDal.ComputeTotalProductCount(orderId);
    }
}