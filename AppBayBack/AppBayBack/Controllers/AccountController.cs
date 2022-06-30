using AppBayBack.Models;
using AppBayBack.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AppBayBack.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager { get; }
        private SignInManager<AppUser> _signInManager { get; }

        public AccountController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            AppUser newUser = new AppUser
            {
                UserName = user.Username,
                Email = user.Email,
            };
            var identityResult = await _userManager.CreateAsync(newUser , user.Password);
            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    return View(user);
                }
            }
            await _signInManager.SignInAsync(newUser, true);
            return RedirectToAction("Index" , "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM user)
        {
            AppUser userDb = await _userManager.FindByEmailAsync(user.Email);
            if(userDb == null)
            {
                ModelState.AddModelError("", "Email or Password is incorrect");
                return View();
            }
            var signInResult = await _signInManager.PasswordSignInAsync(userDb, user.Password, user.IsPersistent, lockoutOnFailure:true);
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Email or password is incorrect");
                return View(user);
            }
            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError("", "Too many false attempts , plase try again in a few minutes");
                return View(user);
            }
            return RedirectToAction("Index", "Home");
        }


    }
}
