using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project.abznotebook.Business.Interfaces;
using Project.abznotebook.Entities.Concrete;
using Project.abznotebook.Web.Models;

namespace Project.abznotebook.Web.Pages
{
    public class CartModel : PageModel
    {
        private IProductService _productService;

        public CartModel(IProductService productService, Cart cartService)
        {
            _productService = productService;
            Cart = cartService;
        }

        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }

        public IActionResult OnPost(int id, string returnUrl, int quantity = 1)
        {
            Product product = _productService.Products.FirstOrDefault(p => p.Id == id);
            Cart.AddItem(product, quantity);
            return RedirectToPage(new { returnUrl = returnUrl });
        }

        public IActionResult OnPostApplyCoupon(string id, string returnUrl)
        {
            return RedirectToPage(new { returnUrl = returnUrl });
        }

        public IActionResult OnPostRemove(int id, string returnUrl)
        {
            Cart.RemoveLine(Cart.Lines.First(cl =>
                cl.Product.Id == id).Product);
            return RedirectToPage(new { returnUrl = returnUrl });
        }

    }
}
