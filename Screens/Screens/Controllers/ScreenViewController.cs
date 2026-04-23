
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;

        public ScreenViewController(ILogger<HomeController> logger, AppDbContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;

        }
        [Route("ScreenView/{screenID?}")]
        public IActionResult Index(int screenID )
        {

            var timeout = _configuration.GetValue<int>("CarouselSettings:AutoplayTimeout");

            ViewBag.AutoplayTimeout = timeout;


            var images = _context.images
                               .Where(x => x.image_status == 1  && (x.imageScreenId == screenID || x.imageScreenId == global.allScreens) && (DateTime.UtcNow >=x.imagefromDate) && (DateTime.UtcNow<=x.imagetoDate ))
                              .OrderBy(x => x.imageOrder)
                               .ToList();
                return View(images);

            
               

            //return View(images);
           
        
        }
    }
}
