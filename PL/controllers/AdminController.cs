using Microsoft.AspNetCore.Mvc;
using core.entities;
using core.enums;
using core.mapper;
using System.Net;
using bll.interfaces;
using pl.viewModel;
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

        [HttpPost]
        [ServiceFilter(typeof(SuperAdminAuthFilter))]
        [Route("/changeRole")]
        public async Task<IActionResult> ChangeRole([FromForm] AdminChangeRoleViewModel model)
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

        [HttpGet]
        public IActionResult getLogin()
        {
            return View("Login");
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromForm] AdminLoginViewModel model)
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
                return RedirectToAction("Index");
            }
        }


        [ServiceFilter(typeof(AdminAuth))]
        [HttpGet("/")]
        public async Task<IActionResult> Index()
        {
            string user_id = HttpContext.Items["user_id"]?.ToString() ?? "";

            List<UserCredentials>? users = await this.service.GetUsersAsync(user_id);

            return View("Home", users);
        }

        [Route("/error")]
        public IActionResult error(string msg) => View("Error", msg);
    }
}
