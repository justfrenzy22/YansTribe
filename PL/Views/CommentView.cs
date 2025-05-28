using core.entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace pl.views
{
    public class CommentView : ViewComponent
    {
        public ActionResult success(string result)
        {
            var data = new { status = 200, message = result };
            return new ObjectResult(data) { StatusCode = 200 };
        }

        public ActionResult bad_request(string message)
        {
            var data = new { status = 400, message = message };
            return new ObjectResult(data) { StatusCode = 400 };
        }

        public ActionResult error(string message)
        {
            var data = new { status = 500, message = message };
            return new ObjectResult(data) { StatusCode = 500 };
        }

        public ActionResult get_comments(List<Comment> comments)
        {
            var data = new { status = 200, message = "Comments retrieved successfully!", comments };
            return new ObjectResult(data) { StatusCode = 200 };
        }

        public ActionResult wrong_credentials()
        {
            var data = new { status = 401, message = "wrong credentials" };
            return new ObjectResult(data) { StatusCode = 401 };
        }
    }
}