using AppBayBack.Models;
using AppBayBack.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AppBayBack.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class AdminAccountController : Controller
    {
        private UserManager<AppUser> _userManager { get; }
        private SignInManager<AppUser> _signInManager { get; }

        public AdminAccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
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
            if (userDb == null)
            {
                ModelState.AddModelError("", "Email or Password is incorrect");
                return View(user);
            }
            var signInResult = await _signInManager.PasswordSignInAsync(userDb, user.Password, user.IsPersistent, lockoutOnFailure: true);
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Email or Password is incorrect");
                return View(user);
            }
            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError("", "Too many false attempts , plase try again in a few minutes");
                return View(user);
            }
            if (!userDb.IsAdmin)
            {
                ModelState.AddModelError("", "Access denied , Wait for permisson by Admin");
                return View(user);
            }
            return RedirectToAction("Index","Feature");
        }
    }
}
