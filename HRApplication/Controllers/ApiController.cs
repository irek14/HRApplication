using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRApplication.WWW.Controllers
{
    public class ApiController : Controller
    {
        public async Task<IActionResult> LogIn()
        {
            bool test1 = User.IsInRole("Admin");

            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("SignIn", "AzureADB2C//Account");

            (User.Identity as ClaimsIdentity).AddClaim(new Claim(ClaimTypes.Role, "Admin"));
            ClaimsPrincipal claims = User as ClaimsPrincipal;

            //await HttpContext.Authentication.SignOutAsync(HttpContext);
            //await HttpContext.Authentication.SignInAsync(DefaultAuthenticationTypes.ApplicationCookie, claims);

            //List<Claim> claims = new List<Claim> { new Claim(ClaimTypes.Role, "Admin") };
            //ClaimsIdentity newIdentity = new ClaimsIdentity(claims);
            //User.AddIdentity(newIdentity);

            //User.Claims.Append(new Claim(ClaimTypes.Role, "Admin"));
            

            bool test2 = User.IsInRole("Admin");
       
            return RedirectToAction("Index", "Application");
        }
    }
}