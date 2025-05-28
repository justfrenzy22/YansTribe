using bll.interfaces;
using core.entities;
using Microsoft.AspNetCore.Mvc;
using pl.middleware;
using pl.viewModel;
using pl.views;

namespace pl.controllers
{
    [ApiController]
    [Route("comment")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly CommentView _commentView;

        public CommentController(ICommentService commentService, CommentView commentView)
        {
            this._commentService = commentService;
            this._commentView = commentView;
        }

        [HttpPost("add")]
        [ServiceFilter(typeof(UserAuth))]
        public async Task<IActionResult> AddComment(AddCommentViewModel comment)
        {
            if (!ModelState.IsValid)
            {
                return _commentView.bad_request("Invalid comment data");
            }

            string? user_id = this.HttpContext.Items["user_id"]?.ToString();

            if (user_id == null)
            {
                return _commentView.wrong_credentials();
            }

            try
            {
                string result = await _commentService.AddComment(
                    post_id: Guid.Parse(comment.post_id),
                    user_id: Guid.Parse(comment.user_id),
                    content: comment.content,
                    created_at: DateTime.UtcNow
                );

                return _commentView.success(result);
            }
            catch (Exception ex)
            {
                return _commentView.error(ex.Message);
            }
        }

        [HttpGet("get_comments/{post_id}")]
        [ServiceFilter(typeof(UserAuth))]
        public async Task<IActionResult> GetComments(string post_id)
        {
            if (!ModelState.IsValid)
            {
                return this._commentView.bad_request("Invalid post data");
            }

            string? user_id = this.HttpContext.Items["user_id"]?.ToString();

            if (user_id == null)
            {
                return this._commentView.wrong_credentials();
            }

            try
            {
                List<Comment> result = await this._commentService.GetComments(Guid.Parse(post_id), Guid.Parse(user_id));

                return this._commentView.get_comments(result);
            }
            catch (Exception ex)
            {
                return _commentView.error(ex.Message);
            }
        }
    }
}