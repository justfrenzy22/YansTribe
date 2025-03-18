using System;
using Microsoft.AspNetCore.Mvc;

namespace server.controllers
{
    [ApiController]
    [Route("admin")]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("admin/login")]
        public IActionResult Login(string email, string password)
        {
            // Simple email and password check
            if (email == "admin@admin.com" && password == "admin@admin.com")
            {
                return RedirectToAction("Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid credentials. Please try again.";
                return View();
            }
        }
    }
}
