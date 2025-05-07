using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using bll.views;
using bll.interfaces;
using core.entities;
using dal.dto;
using pl.middleware;
using pl.dto;

namespace pl.controllers
{

    [ApiController]
    [Route("user")]
    public class UserController : Controller
    {
        private UserView view;
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

        [HttpGet("get_user")]
        [ServiceFilter(typeof(UserAuth))]
        public async Task<IActionResult> GetUserById()
        {
            string? user_id = HttpContext.Items["user_id"]?.ToString();

            if (user_id == null)
            {
                return view.bad_credentials();
            }

            User? user = await this.service.GetUserEssentials(Guid.Parse(user_id));

            if (user == null)
            {
                return view.not_found();
            }

            BaseUserDTO userDTO = new BaseUserDTO(
                user.user_id.ToString(),
                user.username,
                user.pfp_src
            );

            return view.get_base_user(userDTO);
        }

        [HttpGet("get_user_profile/{username}")]
        [ServiceFilter(typeof(UserAuth))]
        public async Task<IActionResult> GetUserProfile(string username)
        {
            string? user_id = HttpContext.Items["user_id"]?.ToString();

            if (user_id == null)
            {
                return view.bad_credentials();
            }

            User? getUser = await this.service.FetchUserProfile(username);

            if (getUser == null)
            {
                return view.not_found();
            }

            ProfileUserDTO profileUserDto = new ProfileUserDTO
            {
                user_id = getUser.user_id,
                username = getUser.username,
                email = getUser.email,
                full_name = getUser.full_name,
                bio = getUser.bio,
                pfp_src = getUser.pfp_src,
                location = getUser.location,
                website = getUser.website,
                is_private = getUser.is_private,
                created_at = getUser.created_at,
                role = getUser.role,
                is_own_profile = getUser.user_id.ToString() == user_id
            };

            return view.get_profile_user(profileUserDto);
        }


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

            HttpContext.Response.Headers.Append("auth_token", token);
            return view.login_success(token);
        }

        [HttpPost("register")]
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


            Guid? user_id = await this.service.RegisterUser(user);
            if (user_id != null && user_id != Guid.Empty)
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