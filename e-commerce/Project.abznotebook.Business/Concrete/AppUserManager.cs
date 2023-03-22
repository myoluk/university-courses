using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Project.abznotebook.Business.Interfaces;
using Project.abznotebook.Data.Interfaces;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Business.Concrete
{
    public class AppUserManager : IAppUserService
    {
        private IAppUserDal _appUserDal;

        public AppUserManager(IAppUserDal appUserDal)
        {
            _appUserDal = appUserDal;
        }

        public List<Order> GetGivenCustomersOrders(AppUser user)
        {
            return _appUserDal.GetGivenCustomersOrders(user);
        }

        public string GetOrderOwnerFullNameWithUserId(int userId)
        {
            return _appUserDal.GetOrderOwnerFullNameWithUserId(userId);
        }

        public async Task<List<AppUser>> GetUsersAsync()
        {
            return await _appUserDal.GetUsersAsync();
        }
    }
}
