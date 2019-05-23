using Blog.Common.Helpers;
using Blog.Entities.Models.Identity;
using Blog.Entities.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blog.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Route("/login")]
        [AllowAnonymous]
        public ActionResult Login()
        {
            if(_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View();
        }

        [HttpPost]
        [Route("/login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return View(loginViewModel);

            var user = await  _userManager.FindByEmailAsync(loginViewModel.Email);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);

                if (result.Succeeded)
                {

                    return RedirectToAction("Index", "Dashboard");
                }
            }

            ModelState.AddModelError("NotFound", "Username/Password combination not found");

            return View(loginViewModel);
        }

        [Route("/register")]
        [AllowAnonymous]
        public ActionResult Register()
        {
            if(_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/register")]
        public async Task<ActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
                return View(registerViewModel);

            var user = await _userManager.FindByEmailAsync(registerViewModel.Email);
            
            if(user == null)
            {
                var newUser = new ApplicationUser() {
                    FirstName = registerViewModel.FirstName,
                    MiddleName = registerViewModel.Email,
                    LastName = registerViewModel.LastName,
                    Email = registerViewModel.Email,
                    UserName = registerViewModel.Username,
                    EmailConfirmed = true
                };
                
                var result = await _userManager.CreateAsync(newUser, registerViewModel.Password);

                if(result.Succeeded)
                {
                    await _signInManager.SignInAsync(newUser, false);
                    return RedirectToAction("Index", "Blog"); 
                }
            }

            ModelState.AddModelError("Email", "User already exist with that email");
            return View(registerViewModel);
        }

        [Route("/forgetpassword")]
        [AllowAnonymous]
        public ActionResult ForgetPassword() => View();

        [HttpPost]
        [Route("/forgetpassword")]
        [AllowAnonymous]
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

        [AllowAnonymous]
        [Route("/resetpassword")]
        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/resetpassword")]
        public ActionResult ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (!ModelState.IsValid)
                return View(resetPasswordViewModel);


            return View("Login");
        }
        
        [Route("/logout")]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }
    }
}
