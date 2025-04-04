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
using server.responses;
using core.enums;
using Microsoft.AspNetCore.Authorization;
using dal.interfaces.service;

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

        private IHashService hash_service;
        private UserMapper mapper;

        // public override IActionResult Index()
        // {
        //     return View();
        // }

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserService service, IAuthService auth_service, IHashService hash_service, UserView view, UserMapper mapper)
        {
            this._logger = logger;
            this.isAdmin = false;
            this.service = service;
            this.auth_service = auth_service;
            this.hash_service = hash_service;
            this.view = view;
            this.mapper = mapper;
        }
        
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

        [HttpGet("get_user/{user_id}")]
        public async Task<IActionResult> GetUserById([FromRoute] requests.UserGetUserReq model)
        {
            if (!ModelState.IsValid)
            {
                return view.bad_credentials();
            }

            dal.requests.UserGetUserReq req = this.mapper.mapGetUserReq(model);
            var res = await this.service.GetUserById(req);
            UserGetUserRes server_res = this.mapper.mapGetUserRes(res);

            if (server_res.exception != null)
            {
                return view.error(server_res.exception);
            }

            if (server_res.user == null)
            {
                return view.not_found();
            }
            else
            {
                return view.get_user(server_res.user);
            }
        }


        [HttpGet("role/{user_id}")]
        // [Authorize] ask my teacher
        public async Task<IActionResult> GetRoleById([FromRoute] requests.UserGetRoleReq model)
        {
            if (!ModelState.IsValid)
            {
                return view.bad_credentials();
            }

            dal.requests.UserGetRoleReq req = this.mapper.mapGetRoleReq(model);
            var res = await this.service.GetRoleById(req);
            UserGetRoleRes server_res = this.mapper.mapGetRoleRes(res);

            if (server_res.exception != null)
            {
                return view.error(server_res.exception);
            }

            if (Enum.IsDefined(typeof(Role), server_res.role) == false)
            {
                return view.not_found();
            }
            else
            {
                return view.get_role(server_res.role);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] requests.UserLoginReq model)
        {
            if (!ModelState.IsValid)
            {
                return view.bad_credentials();
            }

            model.password = this.hash_service.hash(model.password);

            dal.requests.UserLoginReq req = this.mapper.mapLoginReq(model);
            var res = await this.service.ValidateUser(req);
            UserLoginRes server_res = this.mapper.mapLoginRes(res);

            if (server_res.exception != null)
            {
                return view.error(server_res.exception);
            }

            if (!server_res.check)
            {
                return view.not_found();
            }

            string token = this.auth_service.GenerateJwtToken(server_res.user_id.ToString() ?? "", isAdmin: isAdmin);
            HttpContext.Response.Headers.Append("testaccess", token);
            return view.login_success(token);
        }

        public async Task<IActionResult> Register([FromBody] requests.UserRegisterReq model)
        {
            if (!ModelState.IsValid)
            {
                return view.bad_credentials();
            }

            // Hash Password
            model.password = this.hash_service.hash(model.password);

            dal.requests.UserRegisterReq req = this.mapper.mapRegisterReq(model);
            var res = await this.service.AddUser(req);
            UserRegisterRes server_res = this.mapper.mapRegisterRes(res);

            if (server_res.exception != null)
            {
                return view.error(server_res.exception);
            }

            if (server_res.user_id != 0)
            {
                return view.register_success();
            }
            else
            {
                return view.not_found();
            }
        }

        // public 
    }
}