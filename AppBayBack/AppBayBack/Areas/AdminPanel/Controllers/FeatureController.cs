using AppBayBack.DAL;
using AppBayBack.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AppBayBack.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class FeatureController : Controller
    {
        private AppDbContext _context { get; }

        public FeatureController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Feature> features = _context.Features.ToList();
            return View(features);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
