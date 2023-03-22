using System.Collections.Generic;
using System.Linq;
using Project.abznotebook.Data.Concrete.EntityFrameworkCore.Contexts;
using Project.abznotebook.Data.Interfaces;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Data.Concrete.EntityFrameworkCore.Repositories
{
    public class EfPaymentRepository : EfGenericRepository<Payment>, IPaymentDal
    {
        private readonly TechnoStoreDbContext _dbContext;
        public EfPaymentRepository(TechnoStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Payment> GetAllPayments()
        {
            return _dbContext.Payments.ToList();
        }

        public string GetPaymentNameWithId(int? paymentId)
        {
            return _dbContext.Payments.Single(I => I.PaymentId == paymentId).PaymentType;
        }
    }
}