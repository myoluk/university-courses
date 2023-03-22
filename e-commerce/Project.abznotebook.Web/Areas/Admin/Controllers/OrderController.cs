using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.abznotebook.Business.Interfaces;
using Project.abznotebook.Entities.Concrete;
using Project.abznotebook.Web.Base.Common.Models;
using Project.abznotebook.Business.Concrete;

namespace Project.abznotebook.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {

        private readonly IOrderDetailService _orderDetailService;
        private readonly IShipperService _shipperService;
        private readonly IAddressService _addressService;
        private readonly IOrderService _orderService;
        private readonly IPaymentService _paymentService;
        private readonly IAppUserService _appUserService;
        private readonly IProductService _productService;
        public OrderController(IOrderDetailService orderDetailService, IShipperService shipperService, IAddressService addressService, IOrderService orderService, IPaymentService paymentService, IAppUserService appUserService, IProductService productService)
        {
            _orderDetailService = orderDetailService;
            _shipperService = shipperService;
            _addressService = addressService;
            _orderService = orderService;
            _paymentService = paymentService;
            _appUserService = appUserService;
            _productService = productService;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult Detail(int orderId)
        {

            List<BillViewModel> bill = new List<BillViewModel>();

            Order order = _orderService.GetOrderWithId(orderId);

            if (order == null)
            {
                return NotFound();
            }

            Address OrderAddress = _addressService.GetAddressesByUserId(order.CustomerId)
                .Single(I => I.Id == order.AddressId);
            Shipper OrderShipper = _shipperService.GetAllShippers().Single(I => I.Id == order.ShipperId);

            OrderDetailViewModel model = new OrderDetailViewModel()
            {
                OrderId = order.Id,
                OrderDate = order.OrderDate,
                ShipDate = order.ShipDate,
                ShipStatus = order.IsShipped ? "Kargoya Verildi" : "Kargoya Verilmedi",
                AllowStatus = order.IsAllowed ? "Onaylandı" : "Onay Bekliyor",
                PaymentMethod = _paymentService.GetPaymentNameWithId(order.PaymentId),
                CustomerFullName = _appUserService.GetOrderOwnerFullNameWithUserId(order.CustomerId),

                AddressId = order.AddressId,
                AddressLine = OrderAddress.AddressLine,
                AddressCity = OrderAddress.City,
                AddressDistrict = OrderAddress.District,
                AddressNeighborhood = OrderAddress.Neighborhood,
                AddressPostalCode = OrderAddress.PostalCode,

                ShipperCompanyName = OrderShipper.CompanyName,
                ShipperPhone = OrderShipper.Phone
            };

            model.OrderDetails = _orderDetailService.GetAllOrderDetails().Where(I => I.OrderId == orderId).ToList();

            foreach (var orderDetail in model.OrderDetails)
            {
                bill.Add(new BillViewModel()
                {
                    UnitPrice = orderDetail.Price,
                    OrderId = orderDetail.OrderId,
                    Quantity = orderDetail.Quantity,
                    Product = _productService.Products.Single(I => I.Id == orderDetail.ProductId),
                    TotalPrice = _orderDetailService.ComputeTotalPriceOfOrder(model.OrderId),
                });
            }

            model.Bill = bill;

            return View(model);


        }

        [HttpPost]
        public IActionResult Detail(int orderId, bool allowStatus)
        {
            Order order = _orderService.GetOrderWithId(orderId);
            order.IsAllowed = allowStatus;
            _orderService.Update(order);

            return RedirectToAction("Detail", new { orderId = orderId });
        }
    }
}
