using System.Collections.Generic;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Data.Interfaces
{
    public interface IPaymentDal : IGenericDal<Payment>
    {
        List<Payment> GetAllPayments();
        string GetPaymentNameWithId(int? paymentId);
    }
}