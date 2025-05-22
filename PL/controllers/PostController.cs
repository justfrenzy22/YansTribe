using bll.interfaces;
using core.entities;
using Microsoft.AspNetCore.Mvc;
using pl.viewModel;
using pl.middleware;
using bll.views;
using Microsoft.Extensions.Logging;
using pl.views;

namespace pl.controllers
{
    [ApiController]
    [Route("post")]
    public class PostController : Controller
    {
        private PostView view;
        private IPostService _postService;
        private readonly ILogger<PostController> _logger;

        public PostController(IPostService postService, PostView view, ILogger<PostController> logger)
        {
            this._postService = postService;
            this.view = view;
            this._logger = logger;
        }

        [HttpPost("create_post")]
        [Consumes("multipart/form-data")]
        [ServiceFilter(typeof(UserAuth))]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return view.wrong_credentials();
            }

            if (string.IsNullOrEmpty(model.content))
            {
                return view.wrong_credentials();
            }

            string? user_id = this.HttpContext.Items["user_id"]?.ToString();

            PostCreateEntity post = new PostCreateEntity(
                post_id: Guid.NewGuid(),
                user_id: Guid.Parse(user_id ?? ""),
                content: model.content,
                created_at: DateTime.Now
            );

            int? check = await this._postService.CreatePost(post, files: model.files);

            if (check != null && check > 0)
            {
                return view.created();
            }

            return view.bad_credentials();
        }

        [HttpGet("home")]
        [ServiceFilter(typeof(UserAuth))]
        public async Task<IActionResult> Home()
        {
            string? user_id = this.HttpContext.Items["user_id"]?.ToString();

            if (string.IsNullOrEmpty(user_id))
            {
                return view.wrong_credentials();
            }

            // Guid user = Guid.Parse(user_id);
            List<Post>? posts = await this._postService.GetHomePosts(Guid.Parse(user_id)) ?? new List<Post>();

            return view.get_posts(posts);
        }

        [HttpGet("like/{post_id}")]
        [ServiceFilter(typeof(UserAuth))]
        public async Task<IActionResult> LikePost(string post_id)
        {
            string? user_id = this.HttpContext.Items["user_id"]?.ToString();

            if (string.IsNullOrEmpty(user_id) || string.IsNullOrEmpty(post_id))
            {
                return view.wrong_credentials();
            }
            await this._postService.LikePost(Guid.Parse(post_id), Guid.Parse(user_id));

            return view.liked();
        }

        [HttpGet("dislike/{post_id}")]
        [ServiceFilter(typeof(UserAuth))]
        public async Task<IActionResult> DislikePost(string post_id)
        {
            string? user_id = this.HttpContext.Items["user_id"]?.ToString();

            if (string.IsNullOrEmpty(user_id) || string.IsNullOrEmpty(post_id))
            {
                return view.wrong_credentials();
            }
            await this._postService.DislikePost(Guid.Parse(post_id), Guid.Parse(user_id));

            return view.disliked();
        }
    }
}