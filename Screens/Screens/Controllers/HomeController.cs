using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Screens.Models;
using System.Diagnostics;
using Screens.data;
using System.Linq;
using Microsoft.AspNetCore.Antiforgery;

namespace Screens.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var images = _context.images
                .Where(x => x.image_status == 1)
                
                .ToList();

            return View(images);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [RequireAntiforgeryToken]
        public IActionResult Create(Image model)
        {
            ModelState.Remove("imageBath");

            if (model.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "الرجاء اختيار صورة");
            }

            if (!ModelState.IsValid)
            {
                return View(model); 
            }

            string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);

            string filePath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                model.ImageFile.CopyTo(stream);
            }

            model.image_status = 1;
            model.imageBath = "/uploads/" + fileName;

            _context.images.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var image = _context.images.FirstOrDefault(x => x.imageID == id);

            if (image != null)
            {
                _context.images.Update(image);
                image.image_status = 0;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}