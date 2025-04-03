using Demo.DAL.Models;
using Demo.Presentation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class AccountController(UserManager<ApplicationUser> userManager) : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {

            //1.Server Side Validation
            if (!ModelState.IsValid) return View(model);

            //Manual Mapping

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            var result = _userManager.CreateAsync(user, model.Password).Result;
            if (result.Succeeded)
                return RedirectToAction("Login");

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(model);
        }
        #endregion

        #region Login



        #endregion
    }
    /* Security
     * 
     * 1. Authentication
     * Who are you?
     * 
     * 2. Authorization
     * What are you allowed to do?
     * Role Based
     * Claims
     * Policy
     * 
     * 
     * User => [UserName , Email ,....] xxxx
     * use Microsoft.AspNetCore.Identity.EntityFrameworkCore
    */
}