using AppBayBack.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppBayBack.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class UserController : Controller
    {
        public UserManager<AppUser> _userManager { get; set; }

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            List<AppUser> users = _userManager.Users.ToList();
            return View(users);
        }

        public async Task<IActionResult> Grant(string id)
        {
            AppUser userDb = await _userManager.FindByNameAsync(id);
            if (userDb == null)
            {
                return NotFound();
            }
            userDb.IsGranted = true;
            await _userManager.UpdateAsync(userDb);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Revoke(string id)
        {
            AppUser userDb = await _userManager.FindByNameAsync(id);
            if (userDb == null)
            {
                return NotFound();
            }
            userDb.IsGranted = false;
            await _userManager.UpdateAsync(userDb);
            return RedirectToAction("Index");
        }
    }
}
