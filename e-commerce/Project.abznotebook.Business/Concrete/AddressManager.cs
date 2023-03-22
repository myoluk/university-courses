using System.Collections.Generic;
using Project.abznotebook.Business.Interfaces;
using Project.abznotebook.Data.Interfaces;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Business.Concrete
{
    public class AddressManager : IAddressService
    {
        private readonly IAddressDal _addressDal;

        public AddressManager(IAddressDal addressDal)
        {
            _addressDal = addressDal;
        }

        public void Save(Address table)
        {
            _addressDal.Save(table);
        }

        public void Delete(Address table)
        {
            _addressDal.Delete(table);
        }

        public void Update(Address table)
        {
            _addressDal.Update(table);
        }

        public Address GetOrderWithId(int id)
        {
            return _addressDal.GetOrderWithId(id);
        }

        public List<Address> GetAllOrders()
        {
            return _addressDal.GetAllOrders();
        }

        public List<Address> GetAddressesByUserId(int userId)
        {
            return _addressDal.GetAddressesByUserId(userId);
        }
    }
}