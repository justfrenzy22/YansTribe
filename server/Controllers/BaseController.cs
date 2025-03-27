using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace server.controllers
{
    [ApiController]
    [Authorize]
    public abstract class BaseController : Controller
    {
        [HttpGet]
        protected IActionResult Index() => View();
    }
}