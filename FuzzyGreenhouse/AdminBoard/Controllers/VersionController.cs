using AdminBoard.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AdminBoard.Controllers
{
    public class VersionController : Controller
    {
        private readonly ILogger<VersionController> _logger;
        private readonly VersionService _versionService;

        public VersionController(VersionService versionService, ILogger<VersionController> logger)
        {
            _versionService = versionService;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetLatestVersion()
        {
            try
            {
                return new JsonResult(_versionService.GetLatest());
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }
    }
}
