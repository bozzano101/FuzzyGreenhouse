﻿using AdminBoard.Infrastructure;
using AdminBoard.Models;
using AdminBoard.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AdminBoard.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        [TempData]
        public string StatusMessage { get; set; }

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/icons")]
        public IActionResult Icons()
        {
            return View();
        }

        [HttpGet("/maps")]
        public IActionResult Maps()
        {
            return View();
        }

        [HttpGet("/Profile")]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return View(new UserViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Phone = user.Phone,
                PostalCode = user.PostalCode,
                Country = user.Country,
                City = user.City
            });
        }

        [HttpPost("/Profile")]
        public async Task<IActionResult> UpdateProfile([FromForm] UserViewModel input)
        {
            if (!ModelState.IsValid)
            { 
                return RedirectToAction(nameof(Profile));
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            
            var email = await _userManager.GetEmailAsync(user);
            if (input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, input.Email);
                if (!setEmailResult.Succeeded)
                {
                    foreach (var error in setEmailResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            // Model state might not be valid anymore if we weren't able to change the e-mail address
            // so we need to check for that before proceeding
            if (ModelState.IsValid)
            {
                if (input.Email != user.Email)
                {
                    // If we receive an empty string, set a null full name instead
                    user.Email = string.IsNullOrWhiteSpace(input.Email) ? null : input.Email;
                }

                await _userManager.UpdateAsync(user);

                await _signInManager.RefreshSignInAsync(user);

                StatusMessage = "Your profile has been updated";
            }

            return RedirectToAction(nameof(Profile));
        }

        [HttpGet("/tables")]
        public IActionResult Tables()
        {
            return View();
        }
        
        [HttpGet("/upgrade")]
        public IActionResult Upgrade()
        {
            return View();
        }

        [HttpGet("/privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost("/logout")]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet("/error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("/status-code")]
        public IActionResult StatusCodeHandler(int code)
        {
            ViewBag.StatusCode = code;
            ViewBag.StatusCodeDescription = ReasonPhrases.GetReasonPhrase(code);
            ViewBag.OriginalUrl = null;


            var statusCodeReExecuteFeature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            if (statusCodeReExecuteFeature != null)
            {
                ViewBag.OriginalUrl =
                    statusCodeReExecuteFeature.OriginalPathBase
                    + statusCodeReExecuteFeature.OriginalPath
                    + statusCodeReExecuteFeature.OriginalQueryString;
            }

            if (code == 404)
            {
                return View("Status404");
            }

            return View("Status4xx");
        }
    }
}
