using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Data;
using server.views;
using server.managers;
using core.entities;
using System.Security.Cryptography.X509Certificates;
using server.controllers;
using server.mapper;
using dal.interfaces;
using server.services;

namespace server.controllers
{

    [ApiController]
    [Route("user")]
    public class UserController : Controller
    {
        private bool isAdmin;
        private UserView view;
        // private UserManager userManager;
        private IUserService service;
        private IAuthService auth_service;

        // public override IActionResult Index()
        // {
        //     return View();
        // }

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserService service, IAuthService auth_service, UserView view)
        {
            this._logger = logger;
            this.isAdmin = false;
            this.service = service;
            this.auth_service = auth_service;
            this.view = view;
            // this.userManager = userManager;
        }

        // [HttpPost]
        // [Route("register")]
        // public IActionResult Register()
        // {
        //     try
        //     {
        //         using (var reader = new StreamReader(HttpContext.Request.Body))
        //         {
        //             var postData = reader.ReadToEnd();

        //             var username = postData.Split("&")[0].Split("=")[1];
        //             var email = postData.Split("&")[1].Split("=")[1];
        //             var password = postData.Split("&")[2].Split("=")[1];
        //             var full_name = postData.Split("&")[3].Split("=")[1];
        //             var bio = postData.Split("&")[4].Split("=")[1];
        //             var pfp_src = postData.Split("&")[5].Split("=")[1];
        //             var location = postData.Split("&")[6].Split("=")[1];
        //             var website = postData.Split("&")[7].Split("=")[1];

        //             var user_id = this.userManager.create_user(username, email, password, full_name, bio, pfp_src, location, website);

        //             return view.create_success();
        //         }
        //     }
        //     catch (Exception e)
        //     {
        //         return view.error(e.Message);
        //     }
        // }

        [HttpGet]
        public IActionResult Index()
        {
            return this.view.success();
        }

        [HttpGet]
        [Route("test")]
        public IActionResult test()
        {
            // _logger.LogInformation("Test route hit");
            return this.view.success();
        }

        public IActionResult GetUserById()
        {
            // 
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] server.requests.UserLoginReq model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reqMapper = new UserLoginReqMapper();
            dal.requests.UserLoginReq req = reqMapper.MapTo(model);

            int? user_id = await this.service.ValidateUser(req.email, req.password);

            if (user_id == null)
            {
                return view.not_found();
            }

#pragma warning disable CS8604 // Possible null reference argument.
            string token = this.auth_service.GenerateJwtToken(user_id.ToString(), isAdmin: isAdmin);
#pragma warning restore CS8604 // Possible null reference argument.
            HttpContext.Response.Headers.Append("testaccess", token);
            // HttpContent content = new StringContent(token);
            // HttpContext.Response.Cookies.Append("token", token, new CookieOptions
            // {
            //     Secure = true,
            //     Expires = DateTime.Now.AddDays(1)
            // });
            return view.login_success(token);
        }

        // [HttpGet]
        // [Route("get")]
        // public IActionResult Get_user_by_id()
        // {
        //     try
        //     {
        //         User? check = this.userManager.get_user_by_id(1);

        //         if (check == null)
        //         {
        //             return view.not_found();
        //         }
        //         else
        //         {
        //             // User user = check;
        //             return view.get_user(check);
        //         }

        //     }
        //     catch (Exception e)
        //     {
        //         return view.error(e.Message.ToString());
        //     }
        // }

    }
}