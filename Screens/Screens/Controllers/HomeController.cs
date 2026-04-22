using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Screens.data;
using Screens.Migrations;
using Screens.Models;
using System.Diagnostics;

using System.Linq;
using static System.Net.Mime.MediaTypeNames;

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

        public IActionResult Index(int? screenID)
        {

            var screens = _context.screens.ToList();
            ViewBag.Screens = screens;
            int selectedScreen = screenID ?? global.allScreens; 
            ViewBag.SelectedScreen = selectedScreen;

            var images = _context.images

                 .Where(x => x.image_status == 1 && (x.imageScreenId == screenID || x.imageScreenId == global.allScreens) && (x.imagetoDate >= DateTime.UtcNow) && (x.imagefromDate <= DateTime.UtcNow))
                       .OrderBy(x => x.imageOrder)
                .ToList();

            return View(images);
        }
  

        public IActionResult Create(int screenId)
        {
            var screen = _context.screens.FirstOrDefault(s => s.screenId == screenId);

            var image = new Screens.Models.Image();
            int count = _context.images
              .Count(x => x.image_status == 1 && (x.imageScreenId == screenId || x.imageScreenId == 2) && (DateTime.UtcNow >= x.imagefromDate) && (DateTime.UtcNow <= x.imagetoDate));

            //int order = _context.images
            //    .Where(x => x.imageScreenId == screenId)
            //    .Select(x => (int?)x.imageOrder)
            //    .Max() ?? 0;




            //if (global.allScreens == screenId)
            //{
            //    image.imageOrder = 1;

            //}
            //else
            //{
            //    image.imageOrder = order + 1;
            //}
            image.imageScreenId = screenId;
            image.screen = screen;
            image.imageCount = count;
            return View(image);
     
        }

        [HttpPost]
        [RequireAntiforgeryToken]
        public IActionResult Create(Screens.Models.Image model)
        {
            ModelState.Remove("imageBath");
            ModelState.Remove("screen");

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

            //if (global.allScreens ==model.imageScreenId   )
            //{
            //    model.imageOrder = 1;

            //}



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

        public IActionResult Edit(int id)
        {
            var image = _context.images.FirstOrDefault(x => x.imageID == id);

            if (image == null)
                return NotFound();

            return View(image);
        }



        [RequireAntiforgeryToken]
        [HttpPost]
        public IActionResult Edit(Screens.Models.Image model) {
            var image = _context.images.FirstOrDefault(x => x.imageID == model.imageID);
            

            if (!ModelState.IsValid)
            {

                image.imageTitle = model.imageTitle;
                image.imagefromDate = model.imagefromDate;
                image.imageOrder = model.imageOrder;

                image.imageDescription = model.imageDescription;
                image.imagetoDate = model.imagetoDate;

                
                _context.images.Update(image);
               
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        return View (model);
        }
    }
}