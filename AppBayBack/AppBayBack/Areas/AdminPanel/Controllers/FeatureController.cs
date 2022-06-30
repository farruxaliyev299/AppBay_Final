using AppBayBack.DAL;
using AppBayBack.Models;
using AppBayBack.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AppBayBack.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class FeatureController : Controller
    {
        private AppDbContext _context { get; }
        private IWebHostEnvironment _env { get; set; }

        public FeatureController(AppDbContext context , IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Feature feature)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!feature.Photo.CheckSize(300))
            {
                ModelState.AddModelError("Photo", "File size must be under 300kb");
                return View();
            }
            if (!feature.Photo.CheckType())
            {
                ModelState.AddModelError("Photo", "File type must be an image");
                return View();
            }
            feature.Url = await feature.Photo.SaveFileAsync(_env.WebRootPath , "assets" , "images");
            await _context.Features.AddAsync(feature);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            Feature featureDb = await _context.Features.FindAsync(id);
            if (featureDb == null)
            {
                return NotFound();
            }
            var path = Path.Combine(Extension.GetPath(_env.WebRootPath, "assets", "images"), featureDb.Url);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.Features.Remove(featureDb);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var featureDb = await _context.Features.FindAsync(id);
            if(featureDb == null)
            {
                return NotFound();
            }
            return View(featureDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id,Feature feature)
        {
            if(id == null)
            {
                return BadRequest();
            }
            var featureDb = await _context.Features.FindAsync(id);
            if( featureDb == null)
            {
                return NotFound();
            }
            var path = Path.Combine(Extension.GetPath(_env.WebRootPath , "assets" , "images") , featureDb.Url);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            featureDb.Url = await feature.Photo.SaveFileAsync(_env.WebRootPath , "assets" , "images");
            featureDb.Title = feature.Title;
            featureDb.Description = feature.Description;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
