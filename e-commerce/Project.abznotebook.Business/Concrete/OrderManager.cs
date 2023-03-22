using System.Collections.Generic;
using Project.abznotebook.Business.Interfaces;
using Project.abznotebook.Data.Interfaces;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }
        public List<Order> GetUnshippedOrders()
        {
            return _orderDal.GetUnshippedOrders();
        }

        public List<Order> GetShippedOrders()
        {
            return _orderDal.GetShippedOrders();
        }

        public List<Order> GetAllowedOrders()
        {
            return _orderDal.GetAllowedOrders();
        }

        public void Save(Order table)
        {
            _orderDal.Save(table);
        }

        public void Delete(Order table)
        {
            _orderDal.Delete(table);
        }

        public void Update(Order table)
        {
            _orderDal.Update(table);
        }

        public Order GetOrderWithId(int id)
        {
            return _orderDal.GetOrderWithId(id);
        }

        public List<Order> GetAllOrders()
        {
            return _orderDal.GetAllOrders();
        }
    }
}