using Microsoft.AspNetCore.Mvc;
// using core.models;
using core.requests;
using dal.interfaces;
using core.entities;
using core.responses;

namespace server.controllers
{
    [ApiController]
    [Route("admin")]
    public class AdminController : Controller
    {

        private readonly IAdminDal service;

        public AdminController(IAdminDal adminDal) => this.service = adminDal;


        [HttpGet]
        public ActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] AdminLoginReq model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AdminLoginRes res = await this.service.ValidateLogin(model);

            if (res.check)
            {
                return View("Home");
            }

            else
            {
                TempData["err"] = "Invalid credentials. Please try again.";
                return RedirectToAction("Index");
            }
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            List<User>? users = await this.service.GetUsers();

            if (users == null)
            {
                return NotFound();
            }
            TempData["users"] = users;
            return View("Users");
        }
    }
}
