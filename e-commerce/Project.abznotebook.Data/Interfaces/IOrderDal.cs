using System;
using System.Collections.Generic;
using System.Text;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Data.Interfaces
{
    public interface IOrderDal : IGenericDal<Order>
    {
        List<Order> GetUnshippedOrders();
        List<Order> GetShippedOrders();
        List<Order> GetAllowedOrders();
    }
}
