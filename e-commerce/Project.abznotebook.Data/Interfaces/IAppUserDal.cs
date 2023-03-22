using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Data.Interfaces
{
    public interface IAppUserDal
    {
        Task<List<AppUser>> GetUsersAsync();
        List<Order> GetGivenCustomersOrders(AppUser user);
        string GetOrderOwnerFullNameWithUserId(int userId);
    }
}
