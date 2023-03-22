using System.Collections.Generic;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Data.Interfaces
{
    public interface IOrderDetailDal : IGenericDal<OrderDetail>
    {
        List<OrderDetail> GetAllOrderDetails();
        dynamic GetGivenUsersDetailedOrder(AppUser user);
        decimal ComputeTotalPriceOfOrder(int orderId);
        int ComputeTotalProductCount(int orderId);
    }
}