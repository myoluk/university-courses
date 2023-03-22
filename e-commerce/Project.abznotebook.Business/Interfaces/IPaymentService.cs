using System.Collections.Generic;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Business.Interfaces
{
    public interface IPaymentService : IGenericService<Payment>
    {
        List<Payment> GetAllPayments();
        string GetPaymentNameWithId(int? paymentId);
    }
}