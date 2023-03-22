using System.Collections.Generic;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Business.Interfaces
{
    public interface IAddressService : IGenericService<Address>
    {
        List<Address> GetAddressesByUserId(int userId);
    }
}