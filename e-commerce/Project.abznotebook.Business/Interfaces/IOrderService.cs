using System;
using System.Collections.Generic;
using System.Text;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Business.Interfaces
{
    public interface IOrderService : IGenericService<Order>
    {
        List<Order> GetUnshippedOrders();
        List<Order> GetShippedOrders();
        List<Order> GetAllowedOrders();
    }
}
