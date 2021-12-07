using AdminBoard.Infrastructure.Services;
using AdminBoard.Models.FuzzyGreenHouse;
using AdminBoard.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace AdminBoard.Controllers
{
    [Authorize]
    public class VariableController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly VariableService _variableService;

        public VariableController(ILogger<HomeController> logger, UserManager<User> userManager, SignInManager<User> signInManager, VariableService variableService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _variableService = variableService;
        }

        [HttpGet]
        [Route("/Variables")]
        public IActionResult Index()
        {
            return View("Index",_variableService.GetAll());
        }

        [HttpGet]
        [Route("/Variables/Create")]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [Route("/Variables/Create")]
        public IActionResult Create(Set model)
        {
            try
            {
                _variableService.Insert(model);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Failed to create set: {e.Message}");
            }
        }

        [HttpPost]
        [Route("/Variables/Delete/")]
        public IActionResult Delete([FromBody] string obj)
        {
            return Ok(obj);
        }

        [HttpGet]
        [Route("/Variables/Edit/{id:int}")]
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
        [Route("/Variables/Edit/{id:int}")]
        public IActionResult Edit(Set model)
        {
            try
            {
                _variableService.Update(model);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Failed to update set: {e.Message}");
            }
        }
    }
}
