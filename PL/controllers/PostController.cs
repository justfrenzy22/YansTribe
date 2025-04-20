using bll.interfaces;
using core.entities;
using Microsoft.AspNetCore.Mvc;
using pl.dto;
using pl.middleware;
using server.views;

namespace pl.controllers
{
    [ApiController]
    [Route("post")]
    public class PostController : Controller
    {
        private PostView view;

        private IPostService _postService;

        public PostController(IPostService postService, PostView view)
        {
            this._postService = postService;
            this.view = view;
        }

        [HttpPost("/add")]
        [ServiceFilter(typeof(UserAuth))]
        public async Task<IActionResult> Add([FromForm] AddPostDTO model)
        {

            string? user_id = this.HttpContext.Items["user_id"]?.ToString();

            if (user_id == null)
            {
                return view.bad_credentials();
            }

            if (string.IsNullOrEmpty(model.content) || string.IsNullOrEmpty(user_id))
            {
                return view.bad_credentials();
            }

            Post post = new Post(
                user_id: Guid.Parse(user_id),
                content: model.content,
                created_at: DateTime.Now
            );

            if (model.files != null && model.files.Count > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                foreach (var file in model.files)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    post.AddMedia(new PostMedia
                    (
                        post_id: Guid.NewGuid(),
                        media_id: Guid.NewGuid(),
                        media_type: file.ContentType == "image/jpeg" ? core.enums.MediaType.image : core.enums.MediaType.video,
                        media_src: filePath
                    ));
                }
            }

            int? check = await this._postService.AddPost(post);

            if (check != null && check > 0)
            {
                return view.created();
            }
            else
            {
                return view.not_found();
            }
        }
    }
}