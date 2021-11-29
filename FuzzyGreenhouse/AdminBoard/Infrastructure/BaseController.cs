using Microsoft.AspNetCore.Mvc;

namespace AdminBoard.Infrastructure
{
    [Route("[controller]/[action]", Name = "[controller]_[action]")]
    public abstract class BaseController : Controller
    {
    }
}
