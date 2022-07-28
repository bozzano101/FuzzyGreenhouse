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
    public class SubsystemsController : Controller
    {
        private readonly ILogger<SubsystemsController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly INotyfService _notificationService;
        private readonly SubsystemService _subsystemService;

        public SubsystemsController(
            ILogger<SubsystemsController> logger, 
            UserManager<User> userManager, 
            SignInManager<User> signInManager, 
            SubsystemService subsystemService, 
            INotyfService notificationService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _notificationService = notificationService;
            _subsystemService = subsystemService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Index",_subsystemService.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public IActionResult Create(Subsystem model)
        {
            try
            {
                _subsystemService.Insert(model);
                _notificationService.Success("Subsystem inserted successfully.");
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _notificationService.Error("Failed to insert subsystem");
                return StatusCode(500, $"Failed to create subsystem: {e.Message}");
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                _subsystemService.Delete(id);
                return Json(true);
            }
            catch (Exception e)
            {
                _notificationService.Error("Failed to delete subsystem");
                return StatusCode(500, $"Failed to delete subsystem: {e.Message}");
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var model = _subsystemService.Get(id);
                return View("Edit", model);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Failed to find subsystem: {e.Message}");
            }
        }

        [HttpPost]
        public IActionResult Edit(Subsystem model)
        {
            try
            {
                _subsystemService.Update(model);
                _notificationService.Success("Subsystem successfully edited.");
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _notificationService.Error("Failed to edit subsystem");
                return StatusCode(500, $"Failed to update subsystem: {e.Message}");
            }
        }
    }
}
