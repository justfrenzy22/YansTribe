using bll.interfaces;
using core.entities;
using Microsoft.AspNetCore.Mvc;
using pl.dto;
using pl.middleware;
using bll.views;
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
                return view.wrong_credentials();
            }

            string? user_id = this.HttpContext.Items["user_id"]?.ToString();

            Post post = new Post(
                post_id: Guid.NewGuid(),
                user_id: Guid.Parse(user_id ?? ""),
                content: model.content,
                created_at: DateTime.Now
            );

            // if (model.files != null && model.files.Count > 0)
            // {
            //     foreach (var file in model.files)
            //     {
            //         var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            //         if (!Directory.Exists(uploadsFolder))
            //         {
            //             Directory.CreateDirectory(uploadsFolder);
            //         }

            //         Guid guid = Guid.NewGuid();

            //         var fileName = guid + Path.GetExtension(file.FileName);
            //         var filePath = Path.Combine(uploadsFolder, fileName);
            //         using (var memoryStream = new MemoryStream())
            //         {
            //             await file.CopyToAsync(memoryStream);
            //             await System.IO.File.WriteAllBytesAsync(filePath, memoryStream.ToArray());
            //         }

            //         post.AddMedia(new PostMedia
            //         (
            //             post_id: post.post_id,
            //             media_id: guid,
            //             media_type: file.ContentType.StartsWith("image/")
            //                 ? core.enums.MediaType.image
            //                 : core.enums.MediaType.video,
            //             media_src: filePath
            //         ));
            //     }
            // }

            int? check = await this._postService.CreatePost(post, files: model.files);

            if (check != null && check > 0)
            {
                return view.created();
            }

            return view.bad_credentials();
        }
    }
}