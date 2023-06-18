using AdminBoard.Infrastructure.Services;
using AdminBoard.Models;
using AdminBoard.Models.FuzzyGreenHouse;
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

namespace AdminBoard.Controllers
{
    [Authorize]
    public class RulesController : Controller
    {
        private readonly ILogger<RulesController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RuleService _ruleService;
        private readonly SetService _setService;
        private readonly ValuesService _valueService;
        private readonly SubsystemService _subsystemService;
        private readonly INotyfService _notificationService;

        public RulesController(
            ILogger<RulesController> logger, 
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ValuesService valuesService,
            RuleService ruleService,
            SetService setService,
            INotyfService notificationService,
            SubsystemService subsystemService
        )
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _ruleService = ruleService;
            _setService = setService;
            _valueService = valuesService;
            _notificationService = notificationService;
            _subsystemService = subsystemService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var rules = new List<RuleViewModel>();
            var sets = _setService.GetAll();
            var subsystems = _subsystemService.GetAll();

            foreach (var rule in _ruleService.GetAll())
                rules.Add(new RuleViewModel(rule, sets, subsystems));

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

        [HttpPost]
        public IActionResult Create(RuleViewModel model)
        {
            try
            {
                var input1 = _valueService.Get(Convert.ToInt32(model.InputValue1Representation));
                var input2 = _valueService.Get(Convert.ToInt32(model.InputValue2Representation));
                var output = _valueService.Get(Convert.ToInt32(model.OutputValueRepresentation));
                var subsystem = _subsystemService.Get(Convert.ToInt32(model.SubsystemRepresentation));

                Rule rule = new Rule(input1, input2, output, model.Operator, subsystem);

                _ruleService.Insert(rule);

                _notificationService.Success("Rule inserted successfully.");
                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                _notificationService.Error("Failed to insert rule");
                return StatusCode(500, $"Failed to create rule: {e.Message}");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                RuleViewModel ruleViewModel = new RuleViewModel();
                var sets = _setService.GetAll();
                var subsystems = _subsystemService.GetAll();

                sets.ForEach(set =>
                    set.Values.ToList().ForEach(value =>
                    {
                        string name = set.Name + " - " + value.Name;
                        if (set.Type == SetType.Input)
                        {
                            ruleViewModel.InputList1.Add(new SelectListItem { Text = name, Value = value.ValueID.ToString() });
                            ruleViewModel.InputList2.Add(new SelectListItem { Text = name, Value = value.ValueID.ToString() });
                        }
                        if (set.Type == Models.FuzzyGreenHouse.SetType.Output)
                            ruleViewModel.OutputList.Add(new SelectListItem { Text = name, Value = value.ValueID.ToString() });
                    })
                );

                subsystems.ForEach(subsystem =>
                    ruleViewModel.SubsystemList.Add(new SelectListItem { Text = subsystem.Name, Value = subsystem.SubsystemID.ToString() })
                );

                return View("Create", ruleViewModel);
            }
            catch (Exception e)
            {
                _notificationService.Error("Failed to create rule.");
                return StatusCode(500, $"Failed to create rule: {e.Message}");
            }
        }

        [HttpPost]
        public IActionResult Edit(RuleViewModel model)
        {
            try
            {
                _ruleService.Update(model.ConvertToRule());

                _notificationService.Success("Rule successfully edited.");
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _notificationService.Error("Failed to edit diagram");
                return StatusCode(500, $"Failed to edit diagram: {e.Message}");
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var rule = _ruleService.Get(id);
                RuleViewModel model = new RuleViewModel();
                model.RuleID = id;

                var sets = _setService.GetAll();
                var subsystems = _subsystemService.GetAll();

                sets.ForEach(set =>
                    set.Values.ToList().ForEach(value =>
                    {
                        string name = set.Name + " - " + value.Name;
                        if (set.Type == SetType.Input)
                        {
                            model.InputList1.Add(new SelectListItem { Text = name, Value = value.ValueID.ToString() });
                            model.InputList2.Add(new SelectListItem { Text = name, Value = value.ValueID.ToString() });
                        }
                        if (set.Type == Models.FuzzyGreenHouse.SetType.Output)
                            model.OutputList.Add(new SelectListItem { Text = name, Value = value.ValueID.ToString() });
                    })
                );

                subsystems.ForEach(subsystem =>
                    model.SubsystemList.Add(new SelectListItem { Text = subsystem.Name, Value = subsystem.SubsystemID.ToString() })
                );

                ViewBag.Input1 = rule.InputValue1ID;
                ViewBag.Input2 = rule.InputValue2ID;
                ViewBag.Output = rule.OutputValueID;
                ViewBag.Subsystem = rule.SubsystemID;
                ViewBag.Operator = rule.Operator;
                return View("Edit", model);
            }
            catch (Exception)
            {
                _notificationService.Error("Failed to edit rule");
                return StatusCode(500, "Failed to edit rule");
            }
        }

    }
}
