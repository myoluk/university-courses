using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.abznotebook.Business.Interfaces;
using Project.abznotebook.Entities.Concrete;
using Project.abznotebook.Web.Models;


namespace Project.abznotebook.Web.Controllers
{
    public class OrderController : Controller
    {

        private readonly IAddressService _addressService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IShipperService _shipperService;
        private readonly IPaymentService _paymentService;
        private readonly Cart _cart;
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;

        public OrderController(IAddressService addressService, UserManager<AppUser> userManager, IShipperService shipperService, IPaymentService paymentService, Cart cart, IOrderService orderService, IOrderDetailService orderDetailService)
        {
            _addressService = addressService;
            _userManager = userManager;
            _paymentService = paymentService;
            _shipperService = shipperService;
            _cart = cart;
            _orderService = orderService;
            _orderDetailService = orderDetailService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Checkout()
        {

            OrderViewModel model = new OrderViewModel()
            {
                LinesCount = _cart.Lines.Count
            };

            if (User.Identity.IsAuthenticated)
            {
                var appUser = await _userManager.GetUserAsync(User);
                
                model.Addresses = _addressService.GetAddressesByUserId(appUser.Id);
                model.AddressCollection = new SelectList(model.Addresses, "Id", "Title");
                model.PaymentCollection = new SelectList(_paymentService.GetAllPayments(), "PaymentId", "PaymentType");
                model.ShipperCollection = new SelectList(_shipperService.GetAllShippers(), "Id", "CompanyName");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(OrderViewModel model)
        {
            var appUser = await _userManager.GetUserAsync(User);
            model.LinesCount = _cart.Lines.Count;
            model.Addresses = _addressService?.GetAddressesByUserId(appUser.Id);
            model.AddressCollection = new SelectList(model.Addresses, "Id", "Title");
            model.PaymentCollection = new SelectList(_paymentService.GetAllPayments(), "PaymentId", "PaymentType");
            model.ShipperCollection = new SelectList(_shipperService.GetAllShippers(), "Id", "CompanyName");

            if (model.LinesCount == 0)
            {
                ModelState.AddModelError("", "Sepetiniz boş!");
                return View(model);
            }

            Guid orderNumber = Guid.NewGuid();

            if (ModelState.IsValid)
            {
                _orderService.Save(new Order()
                {
                    PaymentId = model.PaymentId,
                    IsShipped = false,
                    CustomerId = appUser.Id,
                    OrderDate = DateTime.Now,
                    OrderNumber = orderNumber,
                    ShipperId = model.ShipperId,
                    AddressId = model.AddressId
                });

                foreach (var cartLine in _cart.Lines)
                {
                    _orderDetailService.Save(new OrderDetail()
                    {
                        ProductId = cartLine.Product.Id,
                        OrderId = _orderService.GetAllOrders().Single(I=>I.OrderNumber.Equals(orderNumber)).Id,
                        Price = cartLine.Product.UnitPrice,
                        Quantity = cartLine.Quantity,
                    });
                }

                int orderId = _orderService.GetAllOrders().Single(I => I.OrderNumber.Equals(orderNumber)).Id;
                return RedirectToAction("Completed", new{orderId = orderId});
            }

            ModelState.AddModelError("", "Seç");

            return View(model);
        }

        public IActionResult Completed(int orderId)
        {
            return View(orderId);
        }



    }
}
