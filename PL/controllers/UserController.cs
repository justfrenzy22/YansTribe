using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using server.views;
using bll.interfaces;
using core.entities;
using dal.dto;

namespace pl.controllers
{

    [ApiController]
    [Route("user")]
    public class UserController : Controller
    {
        // private bool isAdmin;
        private UserView view;
        // private UserManager userManager;
        private IUserService service;

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserService service, UserView view)
        {
            this._logger = logger;
            // this.isAdmin = false;
            this.service = service;
            this.view = view;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return this.view.success();
        }

        [HttpGet("get_user/{user_id}")]
        public async Task<IActionResult> GetUserById([FromRoute] dto.GetUserDTO model)
        {
            if (!ModelState.IsValid)
            {
                return view.bad_credentials();
            }

            User? user = await this.service.GetUserById(Convert.ToInt32(model.user_id));

            if (user == null)
            {
                return view.not_found();
            }

            return view.get_user(user);
        }
        // [HttpGet("role/{user_id}")]
        // [Authorize] ask my teacher
        // public async Task<IActionResult> GetRoleById([FromRoute] requests.UserGetRoleReq model)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return view.bad_credentials();
        //     }

        //     dal.requests.UserGetRoleReq req = this.mapper.mapGetRoleReq(model);
        //     var res = await this.service.GetRoleById(req);
        //     UserGetRoleRes server_res = this.mapper.mapGetRoleRes(res);

        //     if (server_res.exception != null)
        //     {
        //         return view.error(server_res.exception);
        //     }

        //     if (Enum.IsDefined(typeof(Role), server_res.role) == false)
        //     {
        //         return view.not_found();
        //     }
        //     else
        //     {
        //         return view.get_role(server_res.role);
        //     }
        // }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] dto.UserLoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                return view.bad_credentials();
            }

            string token = await this.service.ValidateUser(model.email, model.password);

            if (token == "")
            {
                return view.bad_credentials();
            }

            HttpContext.Response.Headers.Append("testaccess", token);
            return view.login_success(token);
        }

        public async Task<IActionResult> Register([FromBody] dto.UserRegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return view.bad_credentials();
            }
            User user = new User(
                username: model.username,
                email: model.email,
                password: model.password,
                full_name: model.full_name,
                bio: model.bio,
                location: model.location,
                website: model.website,
                role: core.enums.Role.User,
                is_private: false,
                created_at: DateTime.Now
            );


            int? user_id = await this.service.RegisterUser(user);
            if (user_id != null && user_id != -1)
            {
                return view.register_success();
            }
            else
            {
                return view.not_found();
            }
        }
    }
}