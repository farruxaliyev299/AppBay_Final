using AppBayBack.DAL;
using AppBayBack.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AppBayBack.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context { get; }

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM home = new HomeVM
            {
                Features = _context.Features.ToList()
            };
            return View(home);
        }
    }
}
