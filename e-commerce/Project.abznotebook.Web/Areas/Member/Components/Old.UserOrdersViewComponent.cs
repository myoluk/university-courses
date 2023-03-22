//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Project.TechnoStore.Business.Interfaces;
//using Project.TechnoStore.Data.Concrete.EntityFrameworkCore.Contexts;
//using Project.TechnoStore.Data.Interfaces;
//using Project.TechnoStore.Entities.Concrete;
//using Project.TechnoStore.Web.Areas.Member.Models;

//namespace Project.TechnoStore.Web.Areas.Member.Components
//{
//    public class UserOrdersViewComponent : ViewComponent
//    {

//        private readonly IOrderService _orderService;
//        private readonly UserManager<AppUser> _userManager;
//        private readonly IAppUserService _appUserService;
//        private readonly IOrderDetailService _orderDetailService;
//        private readonly IAddressService _addressService;
//        private readonly IShipperService _shipperService;
//        private readonly IProductService _productService;



//        public UserOrdersViewComponent(IOrderService orderService, UserManager<AppUser> userManager, IAppUserService appUserService, IOrderDetailService orderDetailService, IAddressService addressService, IShipperService shipperService, IProductService productService)
//        {
//            _orderService = orderService;
//            _userManager = userManager;
//            _appUserService = appUserService;
//            _orderDetailService = orderDetailService;
//            _addressService = addressService;
//            _shipperService = shipperService;
//            _productService = productService;
//        }

//        public async Task<IViewComponentResult> InvokeAsync()
//        {

//            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);

//            var model = this.JoinTwoTables(appUser);


//            return await Task.FromResult((IViewComponentResult)View("Default", model));
//        }

//        public List<UserOrderDetailViewModel> JoinTwoTables(AppUser appUser)
//        {

//            List<Order> userOrders = _orderService.GetAllOrders().Where(I => I.CustomerId == appUser.Id).ToList();
//            List<OrderDetail> allOrderDetails = _orderDetailService.GetAllOrderDetails();

//            List<Address> currentUserAddresses = _addressService.GetAddressesByUserId(appUser.Id);
//            string customerFullName = _appUserService.GetOrderOwnerFullNameWithUserId(appUser.Id);

//            var orderDetailJoned = userOrders.Join(allOrderDetails, order => order.Id, detail => detail.OrderId,
//                (order, detail) => new UserOrderDetailViewModel()
//                {
//                    OrderId = detail.OrderId,
//                    OrderDate = order.OrderDate,
//                    UnitPrice = detail.Price,
//                    IsShipped = order.IsShipped,
//                    CustomerFullName = customerFullName,
//                    ProductId = order.CustomerId,
//                    ShipperId = order.ShipperId,
//                    AddressId = order.AddressId,
//                    ProductQuantity = detail.Quantity,
//                    ShipDate = order.ShipDate,
//                    ShipTrackNumber = "000000"
//                }).ToList();


//            foreach (var item in orderDetailJoned)
//            {
//                Product product = _productService.GetSpesificProduct(item.ProductId);
//                string shipperName = _shipperService.GetAllShippers().Single(I => I.Id == item.ShipperId).CompanyName;
//                decimal totalPrice = _orderDetailService.ComputeTotalPriceOfOrder(item.OrderId);
//                Address orderAddress = currentUserAddresses.Single(I => I.Id == item.AddressId);

//                item.ProductName = product.Name;
//                item.TotalPrice = totalPrice;
//                item.Address = orderAddress;
//                item.ShipperName = shipperName;
//            }


//            return orderDetailJoned;
//        }


//    }
//}
