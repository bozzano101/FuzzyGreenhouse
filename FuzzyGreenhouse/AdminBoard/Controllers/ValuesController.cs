using AdminBoard.Infrastructure.Services;
using AdminBoard.Models.FuzzyGreenHouse;
using AdminBoard.Models.Identity;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace AdminBoard.Controllers
{
    public class ValuesController : Controller
    {
        private readonly ILogger<ValuesController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ValuesService _valuesService;
        private readonly INotyfService _notificationService;

        [HttpGet]
        public IActionResult Index()
        {
            return View("Index", _valuesService.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public IActionResult Create(Value model)
        {
            try
            {
                _valuesService.Insert(model);
                _notificationService.Success("Value inserted successfully.");
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _notificationService.Error("Failed to insert value");
                return StatusCode(500, $"Failed to create value: {e.Message}");
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                _valuesService.Delete(id);
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
                var model = _valuesService.Get(id);
                return View("Edit", model);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Failed to find value: {e.Message}");
            }
        }

        [HttpPost]
        public IActionResult Edit(Value model)
        {
            try
            {
                _valuesService.Update(model);
                _notificationService.Success("Variable successfully edited.");
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _notificationService.Error("Failed to edit value");
                return StatusCode(500, $"Failed to update set: {e.Message}");
            }
        }
    }
}
