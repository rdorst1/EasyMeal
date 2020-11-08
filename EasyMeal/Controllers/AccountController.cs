using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyMeal.Models;
using EasyMealOrder.Models;
using EasyMealOrder.Models.Repositories;
using EasyMealOrder.Models.ViewModels.UserViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyMealOrder.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        private IUserRepository UserRepository;

        public AccountController(UserManager<AppUser> userMgr, SignInManager<AppUser> signInMgr, IUserRepository repo)
        {
            userManager = userMgr;
            signInManager = signInMgr;
            UserRepository = repo;
        }

        public User GetCurrentUserByEmail(string email)
        {
            return UserRepository.GetUserByEmail(email);
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ViewResult Register() => View();

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(CreateModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.TelephoneNumber,
                    Email = model.Email,
                    UserName = model.Email
                };

                User user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    TelephoneNumber = model.TelephoneNumber,
                    Address = model.Address,
                    Birthday = model.Birthday,
                    Saltless = model.Saltless,
                    Diabetes = model.Diabetes,
                    GlutenAllergy = model.Diabetes
                };

                IdentityResult result = await userManager.CreateAsync(appUser, model.Password);

                if (result.Succeeded)
                {
                    UserRepository.InsertUser(user);
                    return RedirectToAction("Login");
                } else
                {
                    foreach(IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel details, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByEmailAsync(details.Email);
                if(user != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, details.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                }
                ModelState.AddModelError(nameof(LoginModel.Email), "Invalid user or password");
            }
            return View(details);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return View("Login");
        }

        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Update(UpdateModel model)
        {
            if (ModelState.IsValid)
            {
                User user = GetCurrentUserByEmail(model.Email);

                user.Address = model.Address;
                user.Birthday = model.Birthday;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Diabetes = model.Diabetes;
                user.GlutenAllergy = model.GlutenAllergy;
                user.Saltless = model.Saltless;
                user.TelephoneNumber = model.TelephoneNumber;

                UserRepository.UpdateUser(user);
            }

            return View();
        }
    }
}