using Demo.DAL.Models;
using Demo.Presentation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class AccountController(UserManager<ApplicationUser> userManager
        , SignInManager<ApplicationUser> signInManager) 
        : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Login(LoginViewModel model)
        {
           if (!ModelState.IsValid) return View(model);

           var user = _userManager.FindByEmailAsync(model.Email).Result;
            if (user is not null)
            {
                if(_userManager.CheckPasswordAsync(user, model.Password).Result)
                {
                   var result = _signInManager.PasswordSignInAsync
                        (user, model.Password, model.RememberMe,false).Result;

                    if (result.Succeeded)
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    
                }
               
            }

            ModelState.AddModelError(string.Empty, "Invalid Email or Password");
            return View(model);


        }

        #endregion

        #region SignOut
        public IActionResult SignOut()
        {
            _signInManager.SignOutAsync().GetAwaiter().GetResult();

            return RedirectToAction(nameof(Login));
        }


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