using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.abznotebook.Business.Interfaces;

namespace Project.abznotebook.Web.Controllers
{
    public class SearchController : Controller
    {

        private readonly IProductService _productService;

        public SearchController(IProductService productService)
        {
            _productService = productService;
        }


        //split kullan
        public IActionResult Index(string searchString)
        {
            ViewBag.Searched = searchString;

            var products = _productService.Products;
            
            if (!String.IsNullOrEmpty(searchString))
            {
                products = _productService.Products.Where(I =>
                    I.Name.Contains(searchString) || I.Description.Contains(searchString) ||
                    I.SKU.Contains(searchString));
            }

            return View(products);
        }
    }
}
