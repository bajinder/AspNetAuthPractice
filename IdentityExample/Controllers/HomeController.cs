using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityExample.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdentityExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }


        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }


        public IActionResult Login()
        {

            #region OldExample
            //var gramdmaClaims = new List<Claim>()
            //{
            //    new Claim(ClaimTypes.Name,"Bajinder"),
            //    new Claim(ClaimTypes.Email,"bajinder.singh@live.in"),
            //    new Claim("Grandma.Says","Very Nice Boi")
            //};

            //var licenseClaims = new List<Claim>()
            //{
            //    new Claim(ClaimTypes.Name,"Bajinder singh"),
            //    new Claim(ClaimTypes.Email,"bajinder.singh@live.in"),
            //    new Claim("DriversLicense","A+")
            //};

            //var grandmaIdentity = new ClaimsIdentity(gramdmaClaims, "Grandma Identity");
            //var licenseIdentiry = new ClaimsIdentity(licenseClaims, "Government");

            //var userPrinciplal = new ClaimsPrincipal(new[] { grandmaIdentity,licenseIdentiry });

            //HttpContext.SignInAsync(userPrinciplal);
            #endregion
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Login functionality
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user, password,false,false);
                if (signInResult.Succeeded)
                    return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(string username, string password, string email)
        {
            var user = new IdentityUser()
            {
                UserName = username,
                Email = email
            };
            
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                // SignIn User
                var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
                if (signInResult.Succeeded)
                    return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }

    
}
