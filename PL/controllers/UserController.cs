using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using bll.views;
using bll.interfaces;
using core.entities;
using dal.dto;
using pl.middleware;
using pl.viewModel;
using bll.dto;
using System.Net.Http.Headers;
using pl.views;

namespace pl.controllers
{

    [ApiController]
    [Route("user")]
    public class UserController : Controller
    {
        private UserView view;
        private FriendView friendView;
        private IUserService service;
        private INotificationsService notificationsService;

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserService service, INotificationsService notificationsService, UserView view, FriendView friendView)
        {
            this._logger = logger;
            this.service = service;
            this.notificationsService = notificationsService;
            this.view = view;
            this.friendView = friendView;
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
                return this.view.bad_credentials();
            }

            BaseUser? user = await this.service.GetUserEssentials(Guid.Parse(user_id));

            if (user == null)
            {
                return this.view.not_found();
            }

            Notifications notifications = await this.notificationsService.GetNotifications(user.user_id);

            user.AddNotifications(notifications);

            return this.view.get_base_user(user);
        }

        [HttpGet("get_user_profile/{username}")]
        [ServiceFilter(typeof(UserAuth))]
        public async Task<IActionResult> GetUserProfile(string username)
        {
            string? user_id = HttpContext.Items["user_id"]?.ToString();

            if (user_id == null)
            {
                return this.view.bad_credentials();
            }

            BaseUser? baseUser = await this.service.GetUserEssentials(Guid.Parse(user_id));

            if (baseUser == null)
            {
                return this.view.not_found();
            }

            ProfileUser? profileUser = await this.service.FetchUserProfile(username, Guid.Parse(user_id));

            if (profileUser == null)
            {
                return this.view.not_found();
            }

            Notifications notifications = await this.notificationsService.GetNotifications(Guid.Parse(user_id));
            baseUser.AddNotifications(notifications);

            bool isOwner = profileUser.user_id == baseUser.user_id;

            if (profileUser.is_private)
            {
                return this.view.get_private_profile_user(new PrivateProfileViewModel
                {
                    user = baseUser,
                    profile = new BaseUser
                (
                        user_id: profileUser.user_id,
                        username: profileUser.username,
                        pfp_src: profileUser.pfp_src,
                        is_private: profileUser.is_private
                    ),
                    posts = new List<Post>()
                });
            }
            else
            {
                return this.view.get_public_profile_user(profileUser, baseUser, notifications);
            }
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.view.bad_credentials();
            }

            string token = await this.service.ValidateUser(model.email, model.password);

            if (token == "")
            {
                return this.view.bad_credentials();
            }

            HttpContext.Response.Headers.Append("auth_token", token);
            return this.view.login_success(token);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.view.bad_credentials();
            }
            FullUser user = new FullUser(
                user_id: Guid.NewGuid(),
                username: model.username,
                email: model.email,
                pfp_src: "",
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
                return this.view.register_success();
            }
            else
            {
                return this.view.not_found();
            }
        }
    }
}