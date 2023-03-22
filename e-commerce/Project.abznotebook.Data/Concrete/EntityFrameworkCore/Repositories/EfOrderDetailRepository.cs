using System.Collections.Generic;
using System.Linq;
using Project.abznotebook.Data.Concrete.EntityFrameworkCore.Contexts;
using Project.abznotebook.Data.Interfaces;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Data.Concrete.EntityFrameworkCore.Repositories
{
    public class EfOrderDetailRepository : EfGenericRepository<OrderDetail>, IOrderDetailDal
    {
        private readonly TechnoStoreDbContext _dbContext;

        public EfOrderDetailRepository(TechnoStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<OrderDetail> GetAllOrderDetails()
        {
            return _dbContext.OrderDetails.ToList();
        }

        public dynamic GetGivenUsersDetailedOrder(AppUser user)
        {
            List<Order> userOrders = _dbContext.Orders.Where(I => I.CustomerId == user.Id).ToList();
            List<OrderDetail> allOrderDetails = _dbContext.OrderDetails.ToList();

            var orderDetailJoined = userOrders.Join(allOrderDetails, order => order.Id, detail => detail.OrderId,
                (order, detail) => new
                {
                    orderId = detail.OrderId,
                    orderDate = order.OrderDate,
                    shipDate = order.ShipDate,
                    isShipped = order.IsShipped,
                    customerId = order.CustomerId,
                    isAllowed = order.IsAllowed,
                    shipperId = order.ShipperId,
                    paymentId = order.PaymentId,
                    addressId = order.AddressId,
                    detailId = detail.Id,
                    productPrice = detail.Price,
                    productQuantity = detail.Quantity,
                    productId = detail.ProductId
                }).ToList();

            return orderDetailJoined;
        }

        public decimal ComputeTotalPriceOfOrder(int orderId)
        {
            decimal totalValue = 0;
            var orderDetails = _dbContext.OrderDetails.Where(I => I.OrderId == orderId);

            foreach (var orderDetail in orderDetails)
            {
                totalValue += orderDetail.Price;
            }

            return totalValue;
        }

        public int ComputeTotalProductCount(int orderId)=> _dbContext.OrderDetails.Count(I => I.OrderId == orderId);
        
    }
}