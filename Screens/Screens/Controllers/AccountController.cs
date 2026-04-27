using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Screens.Models;
using Screens.Models.AccountViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace Screens.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser > _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            //ViewBag.ErrorMsg = "Errrrorrr";
            ViewData["ReturnUrl"] = returnUrl;
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);


            //var userTemp = new ApplicationUser
            //{
            //    UserName = "BOARD",
            //    Email = "BOARD@nablus.org",
            //    EmployeeNo = "BOARD",
            //    FullName = "مجلس البلدية",  
            //    RelatedEntityID = 0,
            //};

            //var result = await _userManager.CreateAsync(userTemp, "1234");



            var user = _userManager.Users.FirstOrDefault(u => u.EmployeeNo == model.EmployeeNo);
            if (user == null)
            {
                //ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                ViewBag.ErrorMsg = "يرجى التاكد من اسم المستخدم/كلمة المرور";
                return View(model);
            }

            var check = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: false);
            if (check.Succeeded)
            {
                await _signInManager.SignInAsync(user, model.RememberMe);

                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    return Redirect(model.ReturnUrl);


               TempData["SuccessMsg"] = "تم تسجيل الدخول بنجاح";
               //ViewData["SuccessMsg"] = "تم تسجيل الدخول بنجاح";
               //ViewBag.SuccessMsg = "تم تسجيل الدخول بنجاح";
                return RedirectToAction("Index", "Home");
            }

            if (check.IsLockedOut)
            {
                return View("Lockout");
            }

            //ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            ViewBag.ErrorMsg = "يرجى التاكد من اسم المستخدم/كلمة المرور";
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["SuccessMsg"] = "تم تسجيل الخروج";
            return RedirectToAction("Login", "Account");
        }

        // Optional: allow GET logout if needed
        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            TempData["SuccessMsg"] = "تم تسجيل الخروج";
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            await _userManager.AddToRoleAsync(user, "User");
            return View(model);
        }
    }
}
