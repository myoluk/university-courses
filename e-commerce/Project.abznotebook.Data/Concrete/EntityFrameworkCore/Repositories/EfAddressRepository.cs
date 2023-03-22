using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.abznotebook.Data.Concrete.EntityFrameworkCore.Contexts;
using Project.abznotebook.Data.Interfaces;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Data.Concrete.EntityFrameworkCore.Repositories
{
    public class EfAddressRepository : EfGenericRepository<Address>, IAddressDal
    {
        private TechnoStoreDbContext _context;

        public EfAddressRepository(TechnoStoreDbContext context)
        {
            _context = context;
        }

        public List<Address> GetAddressesByUserId(int userId)
        {
            return _context.Addresses.Where(I => I.AppUser.Id == userId).ToList();
        }
    }
}
