using Microsoft.AspNetCore.Mvc;
using Project.abznotebook.Web.Areas.Admin.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.abznotebook.Business.Interfaces;
using Project.abznotebook.Web.Base.Common.Models;

namespace Project.abznotebook.Web.Areas.Admin.Views.Components
{
    public class OrderSummaryViewComponent : ViewComponent
    {

        private readonly IOrderService _orderService;
        private readonly IPaymentService _paymentService;
        private readonly IAppUserService _appUserService;
        public OrderSummaryViewComponent(IOrderService orderService, IPaymentService paymentService, IAppUserService appUserService)
        {
            _orderService = orderService;
            _paymentService = paymentService;
            _appUserService = appUserService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = GetAllOrderSummaries().OrderByDescending(I=>I.OrderDate).ToList();

            return await Task.FromResult((IViewComponentResult)View("Default", model));
        }

        public List<OrderSummaryViewModel> GetAllOrderSummaries()
        {

            List<OrderSummaryViewModel> model = new List<OrderSummaryViewModel>();

            var orders = _orderService.GetAllOrders();
            foreach (var order in orders)
            {
                string paymentMethod = _paymentService.GetPaymentNameWithId(order.PaymentId);
                string orderOwnerFullName = _appUserService.GetOrderOwnerFullNameWithUserId(order.CustomerId);

                model.Add(new OrderSummaryViewModel()
                {
                    OrderId = order.Id,
                    OrderDate = order.OrderDate,
                    PaymentMethod = paymentMethod,
                    CustomerFullName = orderOwnerFullName,
                    ShipStatus = order.IsShipped ? "Kargoya Verildi" : "Kargoya Verilmedi",
                    AllowStatus = order.IsAllowed ? "Onaylandı" : "Onay Bekliyor",
                });
            }

            return model;
        }


    }
}
