using Microsoft.AspNetCore.Mvc;
using core.entities;
using core.enums;
using core.mapper;
using System.Net;
using bll.interfaces;
using pl.dto;
using bll.dto;
using dal.dto;
using Microsoft.AspNetCore.Authorization;
using pl.middleware;
using Microsoft.AspNetCore.Components.Routing;
using System.Threading.Tasks;
using System.Net.Http.Headers;

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
            this._logger = logger;
            this.service = service;
        }

        // [HttpGet("/")]
        // public IActionResult Index()
        // {
        //     try
        //     {
        //         string token = HttpContext.Request.Cookies["token"] ?? "";
        //         if (string.IsNullOrEmpty(token))
        //         {
        //             return View("Login");
        //         }
        //         VerifyTokenRes res = this.service.AuthAdmin(token);
        //         if (res.check)
        //         {
        //             TempData["user_id"] = res.user_id;
        //             return RedirectToAction("Home");
        //         }
        //         else
        //         {
        //             return View("Login");
        //         }
        //     }
        //     catch (Exception e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }

        [HttpPost]
        [ServiceFilter(typeof(SuperAdminAuthFilter))]
        public async Task<IActionResult> ChangeRole([FromForm] AdminChangeRoleDTO model)
        {
            if (!ModelState.IsValid)
            {
                TempData["message"] = "Invalid credentials. Please try again.";
                return RedirectToAction("Index");
            }

            bool check = HttpContext.Items["check"] is bool value && value;

            if (!check)
            {
                string exceptionStr = HttpContext.Items["exception"] as string ?? "";
                TempData["message"] = exceptionStr;
                return RedirectToAction("Index");
            }

            string resMessage = await this.service.ChangeRole(model.user_id, model.role);

            if (resMessage.Contains("successfully"))
            {
                TempData["message"] = resMessage;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = resMessage;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [Route("admin/Login")]
        public async Task<IActionResult> Login([FromForm] AdminLoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string? token = await this.service.ValidateLogin(model.email, model.password);

            if (token == null)
            {
                TempData["err"] = "Invalid credentials. Please try again.";
                return View("Login");
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


        [ServiceFilter(typeof(AdminAuth))]
        [HttpGet("/")]
        public async Task<IActionResult> Index()
        {
            int user_id = Convert.ToInt32(HttpContext.Items["user_id"]);

            List<UserDTO>? users = await this.service.GetUsersAsync(user_id);

            return View("Home", users);
        }

        // private async Task<dal.responses.AdminGetUsersRes> GetUsers(dal.requests.UserGetRoleReq admin_id) =>
        // await this.admin_service.GetUsersAsync(admin_id);
    }
}
