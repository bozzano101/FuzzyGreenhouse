using AdminBoard.Infrastructure.Services;
using AdminBoard.Models.Identity;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly INotyfService _notificationService;

        public RulesController(ILogger<RulesController> logger, UserManager<User> userManager, SignInManager<User> signInManager, RuleService ruleService, INotyfService notificationService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _ruleService = ruleService;
            _notificationService = notificationService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Index", _ruleService.GetAll());
        }
    }
}
