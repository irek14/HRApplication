using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HRApplication.WWW.Controllers
{
    public class ApiController : Controller
    {
        public AzureAdB2COptions AzureAdB2COptions { get; set; }

        public ApiController(IOptions<AzureAdB2COptions> b2cOptions)
        {
            AzureAdB2COptions = b2cOptions.Value;
        }

        public IActionResult LogIn()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("SignIn", "Api");

            if (User.IsInRole("Admin"))
                return RedirectToAction("Index", "AdminPanel");
            else if (User.IsInRole("HR"))
                return RedirectToAction("Index", "HR");
            else if (User.IsInRole("User"))
                return RedirectToAction("Index", "Application");
            else return SignOut();
        }

        [HttpGet("Api/SignIn")]
        public IActionResult SignIn()
        {
            var redirectUrl = Url.Action(nameof(ApiController.LogIn), "Api");
            return Challenge(
                new AuthenticationProperties { RedirectUri = redirectUrl },
                OpenIdConnectDefaults.AuthenticationScheme);
        }


        [HttpGet("Api/SignOut")]
        public IActionResult SignOut()
        {
            var callbackUrl = Url.Action(nameof(ApiController.LogIn), "Api", values: null, protocol: Request.Scheme);
            return SignOut(new AuthenticationProperties { RedirectUri = callbackUrl },
                CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}