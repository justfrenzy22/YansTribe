using Microsoft.AspNetCore.Mvc;
using core.entities;
// using server.services;
using core.enums;
using core.mapper;
using System.Net;
using bll.interfaces;
using pl.dto;
using bll.dto;
// using dal.responses;

namespace pl.controllers
{
    [ApiController]
    [Route("admin")]
    public class AdminController : Controller
    {
        // private string controller;
        private readonly ILogger<AdminController> _logger;
        private readonly IAdminService service;

        public AdminController(
            ILogger<AdminController> logger,
            IAdminService service
        )
        {
            // this.controller = "Admin";
            this._logger = logger;
            this.service = service;
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
                VerifyTokenRes res = this.service.AuthAdmin(token);
                if (res.check)
                {
                    TempData["user_id"] = res.user_id;
                    return RedirectToAction("Home");
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
        public async Task<IActionResult> Login([FromForm] AdminLoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string token = await this.service.ValidateLogin(model.email, model.password);
            if (token == "")
            {
                return BadRequest("Invalid credentials. Please try again.");
            }
            else
            {
                HttpContext.Response.Cookies.Append("token", token, new CookieOptions
                {
                    Secure = true,
                    Expires = DateTime.Now.AddDays(1)
                });
                return RedirectToAction("Home");
            }
        }

        public async Task<IActionResult> Home()
        {
            if (!TempData.ContainsKey("user_id"))
            {
                return View("Login");
            }

            int user_id = Convert.ToInt32(TempData["user_id"]);

            List<User>? users = await this.service.GetUsersAsync(user_id);

            return View("Home", users);
        }

        // private async Task<dal.responses.AdminGetUsersRes> GetUsers(dal.requests.UserGetRoleReq admin_id) =>
        // await this.admin_service.GetUsersAsync(admin_id);
    }
}
