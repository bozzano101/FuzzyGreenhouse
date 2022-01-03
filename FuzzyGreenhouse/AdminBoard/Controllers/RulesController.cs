using AdminBoard.Infrastructure.Services;
using AdminBoard.Models;
using AdminBoard.Models.Identity;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminBoard.Controllers
{
    [Authorize]
    public class RulesController : Controller
    {
        private readonly ILogger<RulesController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RuleService _ruleService;
        private readonly VariableService _variableService;
        private readonly INotyfService _notificationService;

        public RulesController(ILogger<RulesController> logger, UserManager<User> userManager, SignInManager<User> signInManager, RuleService ruleService, VariableService variableService, INotyfService notificationService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _ruleService = ruleService;
            _variableService = variableService;
            _notificationService = notificationService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var rules = new List<RuleViewModel>();
            var sets = _variableService.GetAll();

            foreach (var rule in _ruleService.GetAll())
                rules.Add(new RuleViewModel(rule, sets));

            return View("Index", rules);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                _ruleService.Delete(id);
                return Json(true);
            }
            catch (Exception e)
            {
                _notificationService.Error("Failed to delete rule");
                return StatusCode(500, $"Failed to delete rule: {e.Message}");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                RuleViewModel ruleViewModel = new RuleViewModel();
                var sets = _variableService.GetAll();

                sets.ForEach(set =>
                    set.Values.ToList().ForEach(value =>
                    {
                        string name = set.Name + " - " + value.Name;
                        if (set.Type == Models.FuzzyGreenHouse.SetType.Input)
                        {
                            ruleViewModel.InputList1.Add(new SelectListItem { Text = name, Value = value.ValueID.ToString() });
                            ruleViewModel.InputList2.Add(new SelectListItem { Text = name, Value = value.ValueID.ToString() });
                        }
                        if (set.Type == Models.FuzzyGreenHouse.SetType.Output)
                            ruleViewModel.OutputList.Add(new SelectListItem { Text = name, Value = value.ValueID.ToString() });
                    })
                );

                return View("Create", ruleViewModel);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
