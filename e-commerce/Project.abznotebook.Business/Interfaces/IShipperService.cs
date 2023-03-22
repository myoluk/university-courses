using System.Collections.Generic;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Business.Interfaces
{
    public interface IShipperService : IGenericService<Shipper>
    {
        List<Shipper> GetAllShippers();
    }
}