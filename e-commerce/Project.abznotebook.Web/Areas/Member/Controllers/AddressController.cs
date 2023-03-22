using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Project.abznotebook.Business.Interfaces;
using Project.abznotebook.Entities.Concrete;
using Project.abznotebook.Web.Areas.Member.Models;

namespace Project.abznotebook.Web.Areas.Member.Controllers
{
    [Area("Member")]
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUserService _appUserService;
        private readonly SignInManager<AppUser> _signInManager;

        public AddressController(IAddressService addressService, IAppUserService appUserService, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _addressService = addressService;
            _appUserService = appUserService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var appUser = await _userManager.GetUserAsync(User);
            return View(_addressService.GetAddressesByUserId(appUser.Id));
        }

        [HttpGet]
        public IActionResult AddAddress()
        {
            return View(new AddAddressViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAddress(AddAddressViewModel model)
        {

            var appUser = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                _addressService.Save(new Address()
                {
                    Title = model.Title,
                    AddressLine = model.AddressLine,
                    City = model.City,
                    District = model.District,
                    Neighborhood = model.Neighborhood,
                    PostalCode = model.PostalCode,
                    AppUserId = appUser.Id
                });

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditAddress(int addressId)
        {
            var appUser = await _userManager.GetUserAsync(User);

            try
            {
                var selectedAddress = _addressService.GetAddressesByUserId(appUser.Id).Single(I => I.Id == addressId);
                return View(selectedAddress);
            }
            catch (Exception)
            {
                return NotFound();
            }

            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditAddress(Address address)
        {

            if (ModelState.IsValid)
            {
                _addressService.Update(new Address()
                {
                    Title = address.Title,
                    City = address.City,
                    District = address.District,
                    AddressLine = address.AddressLine,
                    PostalCode = address.PostalCode,
                    Neighborhood = address.Neighborhood,
                    Id = address.Id,
                    AppUserId = address.AppUserId
                });

                return RedirectToAction("Index");
            }

            

            return View(address);
        }

    }
}
