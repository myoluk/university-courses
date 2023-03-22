using System.Collections.Generic;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Business.Interfaces
{
    public interface IOrderDetailService : IGenericService<OrderDetail>
    {
        List<OrderDetail> GetAllOrderDetails();
        dynamic GetGivenUsersDetailedOrder(AppUser user);
        decimal ComputeTotalPriceOfOrder(int orderId);
        int ComputeTotalProductCount(int orderId);


    }
}