using System.Collections.Generic;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Data.Interfaces
{
    public interface IShipperDal : IGenericDal<Shipper>
    {
        List<Shipper> GetAllShippers();
    }
}