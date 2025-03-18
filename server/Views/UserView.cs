using Microsoft.AspNetCore.Mvc;
using server.models;

namespace server.views
{


    public class UserView : ViewComponent
    {
        public ActionResult success()
        {
            var data = new { status = 200, message = "Successful test" };
            return new ObjectResult(data) { StatusCode = 200 };
        }

        public ActionResult nig()
        {
            var data = new { status = 200, message = "nigga" };
            return new ObjectResult(data) { StatusCode = 200 };
        }

        public ActionResult create_success()
        {
            var data = new { status = 200, message = "user registered successfully!" };
            return new ObjectResult(data) { StatusCode = 200 };
        }

        public ActionResult login_success()
        {
            var data = new { status = 200, message = "user logged in successfully!" };
            return new ObjectResult(data) { StatusCode = 200 };
        }

        public ActionResult logout_success()
        {
            var data = new { status = 200, message = "user logged out successfully!" };
            return new ObjectResult(data) { StatusCode = 200 };
        }

        public ActionResult get_user(User user)
        {
            var data = new { status = 200, message = "user retrieved successfully!", user = user };
            return new ObjectResult(data) { StatusCode = 200 };
        }

        public ActionResult not_found()
        {
            var data = new { status = 404, message = "user not found" };
            return new ObjectResult(data) { StatusCode = 404 };
        }

        public ActionResult error(string msg)
        {
            var data = new { status = 500, message = msg };
            return new ObjectResult(data) { StatusCode = 500 };
        }
    }
}