
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Screens.data;
using Screens.Models;
using System.Diagnostics;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;


namespace Screens.Controllers
{
    public class ScreenViewController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        public ScreenViewController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index(int imageID,int screenID )
        {

            if (screenID == 2)
            {
                var images = _context.images
                       .Where(x => x.image_status == 1 && x.imageID == imageID && x.imageScreenId == screenID)

                       .ToList();

            }
            else
            { 
                var images = _context.images
                               .Where(x => x.image_status == 1 && x.imageID == imageID && x.imageScreenId == screenID)

                               .ToList();
            }
               

            return View(images);
           
        }
    }
}
