using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.abznotebook.Business.Interfaces;
using Project.abznotebook.Entities.Concrete;
using Project.abznotebook.Web.Base.Common;
using Project.abznotebook.Web.Models;

namespace Project.abznotebook.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IProductService _productService;
        public int pageSize = 6;
        List<FilterViewModel> models = new List<FilterViewModel>();
        public CategoryController(IProductService productService)
        {
            _productService = productService;
        }

        
        [HttpGet]
        public ViewResult Gaming(int productPage = 1, string sortOrder = "", FilterViewModel model=null)
        {
            ProductListViewModel productList = FillProductListViewModelByCategoryId(1, productPage);
            
            if (!string.IsNullOrEmpty(model.SelectedVendor) || !string.IsNullOrEmpty(model.SelectedMemory) || !string.IsNullOrEmpty(model.SelectedProcessor))
            {
                productList.Products = _productService.GetProductsByCategoryId(1);
                productList = FilterLogic.FilterByModel(productList, model);
            }

            productList.PagingInfo.TotalItems = productList.Products.Count();

            ViewBag.OrderStatus = "En İyi Eşleşme";

            switch (sortOrder)
            {
                case "best_match":
                    productList.Products = productList.Products.OrderByDescending(P => P.CreatedDate);
                    ViewBag.OrderStatus = "En İyi Eşleşme";
                    break;

                case "desc_price":
                    productList.Products = productList.Products.OrderByDescending(p => p.UnitPrice);
                    ViewBag.OrderStatus = "Fiyat: Azalan";
                    break;
                case "asc_price":
                    productList.Products = productList.Products.OrderBy(p => p.UnitPrice);
                    ViewBag.OrderStatus = "Fiyat: Artan";
                    break;
            }

            return View(productList);
        }

        [HttpGet]
        public ViewResult HomeOffice(int productPage = 1, string sortOrder = "", FilterViewModel model = null)
        {

            ProductListViewModel productList = FillProductListViewModelByCategoryId(2, productPage);


            if (!string.IsNullOrEmpty(model.SelectedVendor) || !string.IsNullOrEmpty(model.SelectedMemory) || !string.IsNullOrEmpty(model.SelectedProcessor))
            {
                productList.Products = _productService.GetProductsByCategoryId(2);
                productList = FilterLogic.FilterByModel(productList, model);
            }
            productList.PagingInfo.TotalItems = productList.Products.Count();

            ViewBag.OrderStatus = "En İyi Eşleşme";

            switch (sortOrder)
            {
                case "best_match":
                    productList.Products = _productService.GetProductsByCategoryId(2).OrderByDescending(P => P.CreatedDate);
                    ViewBag.OrderStatus = "En İyi Eşleşme";
                    break;

                case "desc_price":
                    productList.Products = _productService.GetProductsByCategoryId(2).OrderByDescending(p => p.UnitPrice);
                    ViewBag.OrderStatus = "Fiyat: Azalan";
                    break;
                case "asc_price":
                    productList.Products = _productService.GetProductsByCategoryId(2).OrderBy(p => p.UnitPrice);
                    ViewBag.OrderStatus = "Fiyat: Artan";
                    break;
            }

            return View(productList);
        }
        
        [HttpGet]
        public ViewResult TwoInOne(int productPage = 1, string sortOrder = "", FilterViewModel model = null)
        {
            ProductListViewModel productList = FillProductListViewModelByCategoryId(3, productPage);


            if (!string.IsNullOrEmpty(model.SelectedVendor) || !string.IsNullOrEmpty(model.SelectedMemory) || !string.IsNullOrEmpty(model.SelectedProcessor))
            {
                productList.Products = _productService.GetProductsByCategoryId(3);
                productList = FilterLogic.FilterByModel(productList, model);
            }
            productList.PagingInfo.TotalItems = productList.Products.Count();

            ViewBag.OrderStatus = "En İyi Eşleşme";

            switch (sortOrder)
            {
                case "best_match":
                    productList.Products = _productService.GetProductsByCategoryId(3).OrderByDescending(P => P.CreatedDate);
                    ViewBag.OrderStatus = "En İyi Eşleşme";
                    break;

                case "desc_price":
                    productList.Products = _productService.GetProductsByCategoryId(3).OrderByDescending(p => p.UnitPrice);
                    ViewBag.OrderStatus = "Fiyat: Azalan";
                    break;
                case "asc_price":
                    productList.Products = _productService.GetProductsByCategoryId(3).OrderBy(p => p.UnitPrice);
                    ViewBag.OrderStatus = "Fiyat: Artan";
                    break;
            }

            return View(productList);
        }

        public ProductListViewModel FillProductListViewModelByCategoryId(int categoryId, int productPage)
        {

            ProductListViewModel productList = new ProductListViewModel()
            {
                Products =
                    _productService.GetProductsByCategoryId(categoryId).OrderBy(p => p.Id).Skip((productPage - 1) * pageSize).Take(pageSize),
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = productPage,
                    ItemsPerPage = pageSize,
                },
                FilterTypes = new FilterViewModel()
                {
                    Vendors = _productService.GetProductsByCategoryId(categoryId).Select(I => I.Vendor).Distinct().OrderBy(I => I).ToList(),
                    Memories = _productService.GetProductsByCategoryId(categoryId).Select(I => I.MemoryCapacity).Distinct().OrderBy(I => I).ToList(),
                    Processors = _productService.GetProductsByCategoryId(categoryId).Select(I => I.ProcessorVendor).Distinct().OrderBy(I => I).ToList()
                }
            };

            return productList;
        }
    }
}
