using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MaxWell.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaxWell.Server.Controllers
{
    public class HomeController : Controller
    {

        [AllowAnonymous]
        public async Task<IActionResult> SignIn()
        {

            //Create a plain C# Class
            //In real-life, get this from a database after verifying the username and password
            Person user = new Person()
            {
                Id = 1,
                Name = "Fred Fish"
            };

            //Add the ID of the user and the name to a collection of Claims
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
            };

            //Create the principal user from the claims
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            AuthenticationProperties authenticationProperties = new AuthenticationProperties() { IsPersistent = false };

            //Ask MVC to create the auth cookie and store it
            await this.HttpContext
                .SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    principal, authenticationProperties);


            //DONE!
            return this.RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
