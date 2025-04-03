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
// using dal.responses;
using dal.interfaces.service;
using server.services;

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
        private readonly IAuthService auth_service;
        private readonly IHashService hash_service;
        private AdminMapper mapper;

        public AdminController(
            IAdminService admin_service,
            IUserService user_service,
            ILogger<AdminController> logger,
            IAuthService auth_service,
            IHashService hash_service,
            AdminMapper mapper
        )
        {
            this.controller = "Admin";
            this.isAdmin = true;
            this.admin_service = admin_service;
            this.user_service = user_service;
            this._logger = logger;
            this.auth_service = auth_service;
            this.hash_service = hash_service;
            this.mapper = mapper;
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



            dal.requests.AdminLoginReq req = this.mapper.mapLoginReq(model);

            var res = await this.admin_service.ValidateLogin(req);

            AdminLoginRes server_res = this.mapper.mapLoginRes(res);

            this._logger.LogInformation(res.ToString());

            if (server_res.check)
            {

                string token = this.auth_service.GenerateJwtToken(server_res.user_id.ToString() ?? "", isAdmin: isAdmin);

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
                var mapped_id = this.mapper.mapGetRoleReq(user_id);

                var res = await this.GetUsers(admin_id: mapped_id);

                AdminGetUsersRes server_res = this.mapper.mapGetUsersRes(res);

                if (server_res.exception != null)
                {
                    return BadRequest(server_res.exception);
                }

                return View("Home", server_res.users);
            }
            catch (Exception e)
            {
                // return BadRequest(e.Message);
                return BadRequest(e.Message);
            }
        }


        private async Task<dal.responses.AdminGetUsersRes> GetUsers(dal.requests.UserGetRoleReq admin_id) =>
            await this.admin_service.GetUsersAsync(admin_id);
    }
}
