using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.abznotebook.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Project.abznotebook.Business.Interfaces;
using Project.abznotebook.Entities.Concrete;
using Project.abznotebook.Web.Areas.Admin.Infrastructure;
using Project.abznotebook.Web.Areas.Admin.Models;
using Project.abznotebook.Web.Base.Common.Models;

namespace Project.abznotebook.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class OperationsController : Controller
    {

        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _WebHostEnvironment;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUserService _userService;
        private readonly IOrderService _orderService;
        private readonly IPaymentService _paymentService;
        public OperationsController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment WebHostEnvironment, UserManager<AppUser> userManager, IAppUserService userService, IOrderService orderService, IPaymentService paymentService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _WebHostEnvironment = WebHostEnvironment;
            _userManager = userManager;
            _userService = userService;
            _orderService = orderService;
            _paymentService = paymentService;
        }

        [Route("admin/islemler")]
        public IActionResult Index()
        {
            return View();
        }

        
        //PRODUCT CRUD OPERATIONS
        
        [Route("admin/islemler/urunler")]
        
        public IActionResult Product(string sortOrder, string searchString)
        {
            ViewBag.IdSort = sortOrder == "IdSort" ? "id_desc" : "IdSort";
            ViewBag.NameSort = sortOrder == "NameSort" ? "name_desc" : "NameSort";
            ViewBag.StockSort = sortOrder == "StockSort" ? "stock_desc" : "StockSort";
            ViewBag.VendorSort = sortOrder == "VendorSort" ? "vendor_desc" : "VendorSort";
            ViewBag.StockStatusSort = sortOrder == "StockStatusSort" ? "stockstatus_desc" : "StockStatusSort";
            ViewBag.PriceSort = sortOrder == "PriceSort" ? "price_desc" : "PriceSort";
            ViewBag.FromSearch = searchString;

            return View(this.sortByParameters(sortOrder, searchString));
        }

        [HttpGet]
        [Route("admin/islemler/urunekle")]
        public IActionResult AddProduct()
        {
            ViewBag.Categories = new SelectList(_categoryService.GetAllCategories.ToList(), "Id", "Name");
            ViewBag.Memories = new SelectList(ProductDetailPack.MemoryList, "Seç");
            ViewBag.Vendors = new SelectList(ProductDetailPack.VendorList, "Seç");
            ViewBag.ProcessorVendors = new SelectList(ProductDetailPack.ProcessorVendorList, "Seç");
            ViewBag.DiscCapacities = new SelectList(ProductDetailPack.DiscCapacityList, "Seç");
            return View(new AddProductViewModel());
        }

        [HttpPost]
        [Route("admin/islemler/urunekle")]
        public IActionResult AddProduct(AddProductViewModel model)
        {

            if (ModelState.IsValid)
            {
                string[] uniqueFileNames = UploadedFile(model);

                if (uniqueFileNames.Length < 3)
                {

                }

                _productService.Save(new Product()
                {
                    Name = model.Name,
                    SKU = model.SKU,
                    CategoryId = model.CategoryId,
                    Description = model.Description,
                    DiscCapacity = model.DiscCapacity,
                    IsAvailable = model.IsAvailable,
                    GraphicsCard = model.GraphicsCard,
                    MemoryCapacity = model.MemoryCapacity,
                    ProcessorType = model.ProcessorType,
                    ProcessorVendor = model.ProcessorVendor,
                    UnitInStock = model.UnitInStock,
                    Vendor = model.Vendor,
                    UnitPrice = model.UnitPrice,
                    Image1 = uniqueFileNames[0],
                    Image2 = uniqueFileNames[1],
                    Image3 = uniqueFileNames[2],
                    RealPrice = model.RealPrice
                });
                return RedirectToAction("Product");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            Product product = new Product();

            try
            {
                product = _productService.GetSpesificProduct(id);
            }
            catch (Exception)
            {
                return NotFound();
            }

            EditProductViewModel model = new EditProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Vendor = product.Vendor,
                CategoryId = product.CategoryId,
                ProcessorVendor = product.ProcessorVendor,
                ProcessorType = product.ProcessorType,
                Description = product.Description,
                UnitPrice = product.UnitPrice,
                IsAvailable = product.IsAvailable,
                DiscCapacity = product.DiscCapacity,
                MemoryCapacity = product.MemoryCapacity,
                UnitInStock = product.UnitInStock,
                GraphicsCard = product.GraphicsCard,
                Image1 = product.Image1,
                Image2 = product.Image2,
                Image3 = product.Image3,
                SKU = product.SKU,
                RealPrice = product.RealPrice
            };

            ViewBag.Categories = new SelectList(_categoryService.GetAllCategories.ToList(), "Id", "Name");
            ViewBag.Memories = new SelectList(ProductDetailPack.MemoryList, "Seç");
            ViewBag.Vendors = new SelectList(ProductDetailPack.VendorList, "Seç");
            ViewBag.ProcessorVendors = new SelectList(ProductDetailPack.ProcessorVendorList, "Seç");
            ViewBag.DiscCapacities = new SelectList(ProductDetailPack.DiscCapacityList, "Seç");

            return View(model);
        }

        [HttpPost]
        public IActionResult EditProduct(EditProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                _productService.Update(new Product()
                {
                    Id = model.Id,
                    UnitPrice = model.UnitPrice,
                    CategoryId = model.CategoryId,
                    Name = model.Name,
                    IsAvailable = model.IsAvailable,
                    Description = model.Description,
                    GraphicsCard = model.GraphicsCard,
                    MemoryCapacity = model.MemoryCapacity,
                    DiscCapacity = model.DiscCapacity,
                    UnitInStock = model.UnitInStock,
                    ProcessorVendor = model.ProcessorVendor,
                    Vendor = model.Vendor,
                    ProcessorType = model.ProcessorType,
                    Image1 = model.Image1,
                    Image2 = model.Image2,
                    Image3 = model.Image3,
                    SKU = model.SKU,
                    RealPrice = model.RealPrice
                });
                return RedirectToAction("Product");
            }

            ViewBag.Categories = new SelectList(_categoryService.GetAllCategories.ToList(), "Id", "Name");
            ViewBag.Memories = new SelectList(ProductDetailPack.MemoryList, "Seç");
            ViewBag.Vendors = new SelectList(ProductDetailPack.VendorList, "Seç");
            ViewBag.ProcessorVendors = new SelectList(ProductDetailPack.ProcessorVendorList, "Seç");
            ViewBag.DiscCapacities = new SelectList(ProductDetailPack.DiscCapacityList, "Seç");

            return View(model);
        }

        public IActionResult DeleteProduct(int id)
        {
            _productService.Delete(new Product { Id = id });
            return RedirectToAction("Product");
        }

        
        //CATEGORY CRUD OPERATIONS
        
        public IActionResult Category()
        {
            List<Category> categories = _categoryService.GetAllCategories.ToList();

            List<CategoryViewModel> model = new List<CategoryViewModel>();

            foreach (var category in categories)
            {
                model.Add(new CategoryViewModel()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    CountOfProduct = _categoryService.GetProductsOfGivenCategory(category.Id).Count()
                });
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View(new AddCategoryViewModel());
        }

        [HttpPost]
        public IActionResult AddCategory(AddCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                _categoryService.Save(new Category()
                {
                    Name = model.Name,
                    Description = model.Description
                });
                return RedirectToAction("Category");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult EditCategory(int id)
        {

            try
            {
                var selectedCategory = _categoryService.GetAllCategories.Single(I => I.Id == id);

                EditCategoryViewModel model = new EditCategoryViewModel()
                {
                    CategoryId = selectedCategory.Id,
                    CategoryName = selectedCategory.Name,
                    CategoryDescription = selectedCategory.Description
                };

                return View(model);
            }
            catch (Exception)
            {
                return NotFound();
            }

            
        }

        [HttpPost]
        public IActionResult EditCategory(EditCategoryViewModel model)
        {

            if (ModelState.IsValid)
            {
                _categoryService.Update(new Category()
                {
                    Id = model.CategoryId,
                    Name = model.CategoryName,
                    Description = model.CategoryDescription
                });

                return RedirectToAction("Category");
            }

            return View(model);
        }

        public IActionResult DeleteCategory(int id)
        {
            try
            {
                _categoryService.Delete(new Category { Id = id });
                return RedirectToAction("Category");
            }
            catch (Exception)
            {
                return NotFound();
            }

        }

        
        //USER OPERATIONS
        public IActionResult Customer(string sortOrder, string searchString)
        {

            ViewBag.IdSort = sortOrder == "IdSort" ? "id_desc" : "IdSort";
            ViewBag.NameSort = sortOrder == "NameSort" ? "name_desc" : "NameSort";
            ViewBag.SurnameSort = sortOrder == "SurnameSort" ? "surname_desc" : "SurnameSort";
            ViewBag.UsernameSort = sortOrder == "UsernameSort" ? "username_desc" : "UsernameSort";
            ViewBag.FromSearch = searchString;

            return View(this.sortUserByParameters(sortOrder, searchString));
        }

        public  IActionResult CustomerOrders(int userId)
        {

            try
            {
                AppUser appUser = _userManager.Users.Single(I => I.Id == userId);

                var model = GetAllOrderSummaries(appUser).OrderByDescending(I => I.OrderDate).ToList();
                return View(model);
            }
            catch (Exception)
            {
                return NotFound();
            }

            
        }

        public List<OrderSummaryViewModel> GetAllOrderSummaries(AppUser appUser)
        {

            List<OrderSummaryViewModel> model = new List<OrderSummaryViewModel>();

            var orders = _orderService.GetAllOrders().Where(I=>I.CustomerId == appUser.Id);
            foreach (var order in orders)
            {
                string paymentMethod = _paymentService.GetPaymentNameWithId(order.PaymentId);
                string orderOwnerFullName = _userService.GetOrderOwnerFullNameWithUserId(order.CustomerId);

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
        private string[] UploadedFile(AddProductViewModel model)
        {
            string[] uniqueFileNames = new string[3];

            if (model.Images != null && model.Images.Count > 0)
            {

                int countOfImages = model.Images.Count;

                if (countOfImages > 3)
                {
                    countOfImages = 3;
                }


                for (int i = 0; i < countOfImages; i++)
                {
                    uniqueFileNames[i] = null;
                    string uploadsFolder = Path.Combine(_WebHostEnvironment.WebRootPath, "img");
                    uniqueFileNames[i] = Guid.NewGuid().ToString() + "_" + model.Images[i].FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileNames[i]);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.Images[i].CopyTo(fileStream);
                    }
                }

            }

            return uniqueFileNames;
        }
        private List<UserViewModel> sortUserByParameters(string sortOrder, string searchString)
        {
            var users = _userManager.Users.Where(I => I.UserName != "admin").ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(I =>
                    I.UserName.Contains(searchString) || I.Name.Contains(searchString) ||
                    I.Surname.Contains(searchString)).ToList();
            }

            List<UserViewModel> model = new List<UserViewModel>();

            foreach (var user in users)
            {
                model.Add(new UserViewModel()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Mail = user.Email,
                    Username = user.UserName
                });
            }

            switch (sortOrder)
            {
                case "id_desc":
                    model = model.OrderBy(I => I.Id).ToList();
                    break;

                case "IdSort":
                    model = model.OrderByDescending(I => I.Id).ToList();
                    break;

                case "name_desc":
                    model = model.OrderBy(I => I.Name).ToList();
                    break;
                case "NameSort": 
                    model = model.OrderByDescending(I => I.Name).ToList();
                    break;

                case "surname_desc":
                    model = model.OrderBy(I => I.Surname).ToList();
                    break;

                case "SurnameSort":
                    model = model.OrderByDescending(I => I.Surname).ToList();
                    break;

                case "username_desc":
                    model = model.OrderBy(I => I.Username).ToList();
                    break;
                case "UsernameSort":
                    model = model.OrderByDescending(I => I.Username).ToList();
                    break;
                default:
                    model = model.OrderByDescending(I => I.Id).ToList();
                    break;
            }

            return model;
        }
        private IQueryable<Product> sortByParameters(string sortOrder, string searchString)
        {
            var products = _productService.Products;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(I => I.SKU.Contains(searchString) || I.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderBy(I => I.Name);
                    break;
                case "NameSort":
                    products = products.OrderByDescending(I => I.Name);
                    break;
                case "StockSort":
                    products = products.OrderBy(P => P.UnitInStock);
                    break;
                case "stock_desc":
                    products = products.OrderByDescending(I => I.UnitInStock);
                    break;
                case "vendor_desc":
                    products = products.OrderBy(I => I.Vendor);
                    break;
                case "VendorSort":
                    products = products.OrderByDescending(I => I.Vendor);
                    break;
                case "stockstatus_desc":
                    products = products.OrderBy(I => I.UnitInStock);
                    break;
                case "StockStatusSort":
                    products = products.OrderByDescending(I => I.UnitInStock);
                    break;
                case "id_desc":
                    products = products.OrderBy(I => I.Id);
                    break;
                case "IdSort":
                    products = products.OrderByDescending(I => I.Id);
                    break;
                case "price_desc":
                    products = products.OrderBy(I => I.UnitPrice);
                    break;
                case "PriceSort":
                    products = products.OrderByDescending(I => I.UnitPrice);
                    break;
                default:
                    products = products.OrderByDescending(I => I.Id);
                    break;
            }

            return products;
        }
    }
}
