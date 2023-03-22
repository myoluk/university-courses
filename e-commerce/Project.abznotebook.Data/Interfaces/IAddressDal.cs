using System.Collections.Generic;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Data.Interfaces
{
    public interface IAddressDal : IGenericDal<Address>
    {
        List<Address> GetAddressesByUserId(int userId);
    }
}