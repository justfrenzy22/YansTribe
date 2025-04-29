using bll.interfaces;
using core.entities;
using Microsoft.AspNetCore.Mvc;
using pl.dto;
using pl.middleware;
using server.views;
using Microsoft.Extensions.Logging;

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
        public async Task<IActionResult> CreatePost([FromForm] CreatePostDTO model)
        {
            if (!ModelState.IsValid)
            {
                return view.wrong_credentials();
            }

            if (string.IsNullOrEmpty(model.content))
            {
                return BadRequest(ModelState);
            }

            string? user_id = this.HttpContext.Items["user_id"]?.ToString();

            Post post = new Post(
                user_id: Guid.Parse(user_id ?? ""),
                content: model.content,
                created_at: DateTime.Now
            );

            return this.view.success();

            // if (model.files != null && model.files.Count > 0)
            // {
            //     foreach (var file in model.files)
            //     {
            //         var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            //         if (!Directory.Exists(uploadsFolder))
            //         {
            //             Directory.CreateDirectory(uploadsFolder);
            //         }

            //         var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            //         var filePath = Path.Combine(uploadsFolder, fileName);
            //         await System.IO.File.WriteAllBytesAsync(filePath, file.Content);

            //         post.AddMedia(new PostMedia
            //         (
            //             post_id: Guid.NewGuid(),
            //             media_id: Guid.NewGuid(),
            //             media_type: file.ContentType.StartsWith("image/")
            //                 ? core.enums.MediaType.image
            //                 : core.enums.MediaType.video,
            //             media_src: filePath
            //         ));
            //     }
            // }

            // int? check = await this._postService.CreatePost(post);

            // if (check != null && check > 0)
            // {
            //     return view.created();
            // }
            // else
            // {
            //     return view.not_found();
            // }
        }
    }
}