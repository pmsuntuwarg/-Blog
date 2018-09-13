using Blog.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return View(loginViewModel);

            var user = await  _userManager.FindByEmailAsync(loginViewModel.Email);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);

                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Username/Password combination not found");

            return View(loginViewModel);
        }

        public ActionResult Register()
        {
            if(_signInManager.IsSignedIn(HttpContext.User))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
                return View(registerViewModel);

            var user = await _userManager.FindByEmailAsync(registerViewModel.Email);
            
            if(user == null)
            {
                var newUser = new IdentityUser() {
                    Email = registerViewModel.Email,
                    UserName = registerViewModel.Username,
                    EmailConfirmed = true
                };
                
                var result = await _userManager.CreateAsync(newUser, registerViewModel.Password);

                if(result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("Email", "User already exist with that email");
            return View(registerViewModel);
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ForgetPassword(ForgetPasswordViewModel forgetPaswordViewModel)
        {
            if (!ModelState.IsValid)
                return View(forgetPaswordViewModel);

            var user = await _userManager.FindByEmailAsync(forgetPaswordViewModel.Email);
            
            if(user != null)
            {
                var reset = await _userManager.GeneratePasswordResetTokenAsync(user);
            }

            ModelState.AddModelError("Email", "User not found");

            return View(forgetPaswordViewModel);
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (!ModelState.IsValid)
                return View(resetPasswordViewModel);


            return View("Login");
        }

        [HttpPost]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return View("Login", "Account");
        }
    }
}
