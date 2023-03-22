using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.abznotebook.Business.Interfaces;
using Project.abznotebook.Entities.Concrete;
using Project.abznotebook.Web.Base.Common.Models;
using Project.abznotebook.Web.Areas.Admin.Models;

namespace Project.abznotebook.Web.Areas.Member.Components
{
    public class UserOrdersSummaryViewComponent : ViewComponent
    {

        private readonly IOrderService _orderService;
        private readonly IPaymentService _paymentService;
        private readonly IAppUserService _appUserService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IProductService _productService;

        public UserOrdersSummaryViewComponent(IOrderService orderService, IPaymentService paymentService, IAppUserService appUserService, UserManager<AppUser> userManager, IOrderDetailService orderDetailService, IProductService productService)
        {
            _orderService = orderService;
            _paymentService = paymentService;
            _appUserService = appUserService;
            _userManager = userManager;
            _orderDetailService = orderDetailService;
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);

            var model = GetAllOrderSummaries(appUser).OrderByDescending(I => I.OrderDate).ToList();

            return await Task.FromResult((IViewComponentResult)View("Default", model));
        }

        public List<OrderSummaryViewModel> GetAllOrderSummaries(AppUser appUser)
        {
            List<OrderSummaryViewModel> model = new List<OrderSummaryViewModel>();

            var orders = _orderService.GetAllOrders().Where(I=>I.CustomerId == appUser.Id).ToList();
            
            foreach (var order in orders)
            {
                var orderDetails = _orderDetailService.GetAllOrderDetails().Where(I => I.OrderId == order.Id).ToList();
                List<int> productIds = orderDetails.Select(I => I.ProductId).ToList();
                List<string> productImages = new List<string>();

                foreach (var productId in productIds)
                {
                    productImages.Add(_productService.Products.Single(I => I.Id == productId).Image1);
                }

                string paymentMethod = _paymentService.GetPaymentNameWithId(order.PaymentId);
                string orderOwnerFullName = _appUserService.GetOrderOwnerFullNameWithUserId(order.CustomerId);
                

                model.Add(new OrderSummaryViewModel()
                {
                    OrderId = order.Id,
                    OrderDate = order.OrderDate,
                    CustomerFullName = orderOwnerFullName,
                    ShipStatus = order.IsShipped ? "Kargoya Verildi" : "Kargoya Verilmedi",
                    AllowStatus = order.IsAllowed ? "Onaylandı" : "Onay Bekliyor",
                    ProductCount = _orderDetailService.ComputeTotalProductCount(order.Id),
                    TotalPrice = _orderDetailService.ComputeTotalPriceOfOrder(order.Id),
                    ProductImages = productImages
                });
            }

            return model;
        }

    }
}