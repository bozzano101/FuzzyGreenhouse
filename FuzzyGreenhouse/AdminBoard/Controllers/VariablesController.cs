using AdminBoard.Infrastructure.Services;
using AdminBoard.Models.FuzzyGreenHouse;
using AdminBoard.Models.Identity;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace AdminBoard.Controllers
{
    [Authorize]
    public class VariablesController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly VariableService _variableService;
        private readonly INotyfService _notificationService;

        public VariablesController(ILogger<HomeController> logger, UserManager<User> userManager, SignInManager<User> signInManager, VariableService variableService, INotyfService notificationService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _variableService = variableService;
            _notificationService = notificationService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Index",_variableService.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public IActionResult Create(Set model)
        {
            try
            {
                _variableService.Insert(model);
                _notificationService.Success("Variable inserted successfully.");
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _notificationService.Error("Failed to insert variable");
                return StatusCode(500, $"Failed to create set: {e.Message}");
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                _variableService.Delete(id);
                return Json(true);
            }
            catch (Exception e)
            {
                _notificationService.Error("Failed to delete variable");
                return StatusCode(500, $"Failed to delete set: {e.Message}");
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var model = _variableService.Get(id);
                return View("Edit", model);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Failed to find set: {e.Message}");
            }
        }

        [HttpPost]
        public IActionResult Edit(Set model)
        {
            try
            {
                _variableService.Update(model);
                _notificationService.Success("Variable successfully edited.");
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _notificationService.Error("Failed to edit variable");
                return StatusCode(500, $"Failed to update set: {e.Message}");
            }
        }
    }
}
