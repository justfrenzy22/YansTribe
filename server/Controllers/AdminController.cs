using Microsoft.AspNetCore.Mvc;
using server.requests;
using dal.interfaces;
using dal.requests;
using core.entities;
using server.responses;
// using server.services;
using core.enums;
using core.mapper;
using server.mapper;
using System.Net;

namespace server.controllers
{
    [ApiController]
    [Route("admin")]
    public class AdminController : Controller
    {
        private string controller;
        private bool isAdmin;

        private readonly IAdminService admin_service;
        private readonly IUserService user_service;
        private readonly ILogger<AdminController> _logger;
        private readonly services.IAuthService auth_service;

        public AdminController(IAdminService admin_service, IUserService user_service, ILogger<AdminController> logger, services.IAuthService auth_service)
        {
            this.controller = "Admin";
            this.isAdmin = true;
            this.admin_service = admin_service;
            this.user_service = user_service;
            this._logger = logger;
            this.auth_service = auth_service;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {

            try
            {
                string token = HttpContext.Request.Cookies["token"] ?? "";

                if (string.IsNullOrEmpty(token))
                {
                    return View("Login");
                }

                var verify = this.auth_service.VerifyTokenAsync(token, isAdmin: isAdmin);
                if (verify.check)
                {
                    TempData["user_id"] = verify.user_id;
                    return RedirectToAction("Home", "Admin");
                }
                else
                {
                    return View("Login");
                }
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] requests.AdminLoginReq model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resMapper = new AdminLoginResMapper();
            var reqMapper = new AdminLoginReqMapper();
            dal.requests.AdminLoginReq req = reqMapper.MapTo(model);
            var res = await this.admin_service.ValidateLogin(req);

            AdminLoginRes server_res = resMapper.MapTo(res);

            this._logger.LogInformation(res.ToString());

            if (server_res.check)
            {

#pragma warning disable CS8604 // Possible null reference argument.
                string token = this.auth_service.GenerateJwtToken(server_res.user_id.ToString(), isAdmin: isAdmin);
#pragma warning restore CS8604 // Possible null reference argument.

                HttpContext.Response.Cookies.Append("token", token, new CookieOptions
                {
                    Secure = true,
                    Expires = DateTime.Now.AddDays(1)
                });

                return RedirectToAction("Home");
            }

            else
            {
                TempData["err"] = "Invalid credentials. Please try again.";
                return View("Login");
            }
        }

        public async Task<IActionResult> Home()
        {
            try
            {
                if (!TempData.ContainsKey("user_id"))
                {
                    return View("Login");
                }
                int user_id = Convert.ToInt32(TempData["user_id"]);
                List<User>? users = await this.GetUsers(admin_id: user_id);
                return View("Home", users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        private async Task<List<User>?> GetUsers(int admin_id)
        {
            return await this.admin_service.GetUsersAsync(admin_id);
        }

    }
}
