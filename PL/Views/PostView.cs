using Microsoft.AspNetCore.Mvc;
using core.entities;
using dal.dto;
using System.Collections.Generic;

namespace pl.views
{
    public class PostView : ViewComponent
    {
        public ActionResult success()
        {
            var data = new { status = 200, message = "Successful test" };
            return new ObjectResult(data) { StatusCode = 200 };
        }

        public ActionResult bad_credentials()
        {
            var data = new { status = 401, message = "bad post credentials" };
            return new ObjectResult(data) { StatusCode = 401 };
        }

        public ActionResult wrong_credentials()
        {
            var data = new { status = 401, message = "wrong credentials" };
            return new ObjectResult(data) { StatusCode = 401 };
        }

        public ActionResult created()
        {
            var data = new { status = 200, message = "Post created successfully!" };
            return new ObjectResult(data) { StatusCode = 201 };
        }

        public ActionResult updated(Post post)
        {
            var data = new { status = 200, message = "Post updated successfully!", post };
            return new ObjectResult(data) { StatusCode = 200 };
        }

        public ActionResult deleted(int postId)
        {
            var data = new { status = 200, message = "Post deleted successfully!", postId };
            return new ObjectResult(data) { StatusCode = 200 };
        }

        public ActionResult get_post(Post post)
        {
            var data = new { status = 200, message = "Post retrieved successfully!", post };
            return new ObjectResult(data) { StatusCode = 200 };
        }

        public ActionResult get_posts(List<Post> posts)
        {
            var data = new { status = 200, message = "Posts retrieved successfully!", posts };
            return new ObjectResult(data) { StatusCode = 200 };
        }

        public ActionResult not_found()
        {
            var data = new { status = 404, message = "Post not found" };
            return new ObjectResult(data) { StatusCode = 404 };
        }

        public ActionResult error(string msg)
        {
            var data = new { status = 500, message = msg };
            return new ObjectResult(data) { StatusCode = 500 };
        }

        public ActionResult bad_request(string msg)
        {
            var data = new { status = 400, message = msg };
            return new ObjectResult(data) { StatusCode = 400 };
        }

        public ActionResult liked()
        {
            var data = new { status = 200, message = "Post liked successfully!" };
            return new ObjectResult(data) { StatusCode = 200 };
        }

        public ActionResult disliked()
        {
            var data = new { status = 200, message = "Post disliked successfully!" };
            return new ObjectResult(data) { StatusCode = 200 };
        }
    }
}
