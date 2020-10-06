using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Basics.Controllers
{
    public class HomeController : Controller
    {
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


        public IActionResult Authenticate()
        {
            var gramdmaClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"Bajinder"),
                //new Claim(ClaimTypes.Email,"bajinder.singh@live.in"),
                new Claim("Grandma.Says","Very Nice Boi")
            };

            var licenseClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"Bajinder singh"),
                //new Claim(ClaimTypes.Email,"bajinder.singh@live.in"),
                new Claim("DriversLicense","A+")
            };

            var grandmaIdentity = new ClaimsIdentity(gramdmaClaims, "Grandma Identity");
            var licenseIdentiry = new ClaimsIdentity(licenseClaims, "Government");
            
            var userPrinciplal = new ClaimsPrincipal(new[] { grandmaIdentity,licenseIdentiry });

            HttpContext.SignInAsync(userPrinciplal);

            return RedirectToAction("Index");
        }
    }

    
}
