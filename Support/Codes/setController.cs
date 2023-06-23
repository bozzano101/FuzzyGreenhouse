 [Authorize]
    public class SetController : Controller
    {
        private readonly ILogger<SetController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly SetService _setService;
        private readonly SubsystemService _subsystemService;
        private readonly INotyfService _notificationService;

        public SetController(
            ILogger<SetController> logger, 
            UserManager<User> userManager, 
            SignInManager<User> signInManager,
            SubsystemService subsystemService,
            SetService setService, 
            INotyfService notificationService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _setService = setService;
            _subsystemService = subsystemService;
            _notificationService = notificationService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var sets = _setService.GetAll();
            ViewBag.Names = _subsystemService.GetNames();
            return View("Index", sets);
        }

        [HttpGet]
        public IActionResult Create()
        {
            SetViewModel setViewModel = new(_subsystemService.GetAll());
            return View("Create", setViewModel);
        }

        [HttpPost]
        public IActionResult Create(SetViewModel model)
        {
            try
            {
                _setService.Insert(model.ConvertToSet());
                _notificationService.Success("Set inserted successfully.");
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _notificationService.Error("Failed to insert set");
                return StatusCode(500, $"Failed to create set: {e.Message}");
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                _setService.Delete(id);
                return Json(true);
            }
            catch (Exception e)
            {
                _notificationService.Error("Failed to delete set");
                return StatusCode(500, $"Failed to delete set: {e.Message}");
            }
        }
    }