using bll.interfaces;
using core.entities;
using Microsoft.AspNetCore.Mvc;
using pl.middleware;
using pl.views;

namespace pl.controllers
{

    [ApiController]
    [Route("friend")]
    public class FriendController : Controller
    {
        private readonly UserView userView;
        private readonly FriendView friendView;
        private readonly INotificationsService notificationsService;
        private readonly IUserService userService;
        private IFriendService service;

        public FriendController(UserView userView, FriendView friendView, IFriendService service, INotificationsService notificationsService, IUserService userService)
        {
            this.notificationsService = notificationsService;
            this.userService = userService;
            this.service = service;
            this.userView = userView;
            this.friendView = friendView;
            this.service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("add/{user2_id}")]
        [ServiceFilter(typeof(UserAuth))]
        public async Task<IActionResult> AddFriend(string user2_id)
        {
            string? user_id = HttpContext.Items["user_id"]?.ToString();

            if (user_id == null)
            {
                return this.userView.bad_credentials();
            }

            if (user2_id == null)
            {
                return this.userView.bad_credentials();
            }

            await this.service.AddFriend(Guid.Parse(user_id), Guid.Parse(user2_id));

            return this.friendView.requested();
        }

        [HttpGet("remove/{user2_id}")]
        [ServiceFilter(typeof(UserAuth))]
        public async Task<IActionResult> RemoveFriend(string user2_id)
        {
            string? user_id = HttpContext.Items["user_id"]?.ToString();

            if (user_id == null)
            {
                return this.userView.bad_credentials();
            }

            if (user2_id == null)
            {
                return this.userView.bad_credentials();
            }

            string? result = await this.service.RemoveFriend(Guid.Parse(user_id), Guid.Parse(user2_id));

            if (result == null)
            {
                return this.userView.not_found();
            }

            return this.friendView.removed(result);

        }

        [HttpGet("cancel/{user2_id}")]
        [ServiceFilter(typeof(UserAuth))]
        public async Task<IActionResult> CancelFriend(string user2_id)
        {
            string? user_id = HttpContext.Items["user_id"]?.ToString();

            if (user_id == null)
            {
                return this.userView.bad_credentials();
            }

            if (user2_id == null)
            {
                return this.userView.bad_credentials();
            }

            string? result = await this.service.CancelFriend(Guid.Parse(user_id), Guid.Parse(user2_id));

            if (result == null)
            {
                return this.userView.not_found();
            }

            return this.friendView.removed(result);

        }

        [HttpGet("accept/{user2_id}")]
        [ServiceFilter(typeof(UserAuth))]
        public async Task<IActionResult> AcceptFriend(string user2_id)
        {
            string? user_id = HttpContext.Items["user_id"]?.ToString();

            if (user_id == null)
            {
                return this.userView.bad_credentials();
            }

            if (user2_id == null)
            {
                return this.userView.bad_credentials();
            }

            string? result = await this.service.AcceptFriend(Guid.Parse(user_id), Guid.Parse(user2_id));

            if (result == null)
            {
                return this.userView.not_found();
            }

            return this.friendView.accepted(result);
        }

        [HttpGet("decline/{user2_id}")]
        [ServiceFilter(typeof(UserAuth))]
        public async Task<IActionResult> DeclineFriend(string user2_id)
        {
            string? user_id = HttpContext.Items["user_id"]?.ToString();

            if (user_id == null)
            {
                return this.userView.bad_credentials();
            }

            if (user2_id == null)
            {
                return this.userView.bad_credentials();
            }

            string? result = await this.service.DeclineFriend(Guid.Parse(user_id), Guid.Parse(user2_id));

            if (result == null)
            {
                return this.userView.not_found();
            }

            return this.friendView.declined(result);
        }
    }
}