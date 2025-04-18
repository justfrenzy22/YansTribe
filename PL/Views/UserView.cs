using Microsoft.AspNetCore.Mvc;
using core.entities;
using dal.dto;

namespace server.views
{
    public class UserView : ViewComponent
    {
        public ActionResult success()
        {
            var data = new { status = 200, message = "Successful test" };
            return new ObjectResult(data) { StatusCode = 200 };
        }

        public ActionResult register_success()
        {
            var data = new { status = 200, message = "user registered successfully!" };
            return new ObjectResult(data) { StatusCode = 200 };
        }

        public ActionResult login_success(string token)
        {
            var data = new { status = 200, message = "user logged in successfully!", token = token };
            return new ObjectResult(data) { StatusCode = 200 };
        }

        public ActionResult logout_success()
        {
            var data = new { status = 200, message = "user logged out successfully!" };
            return new ObjectResult(data) { StatusCode = 200 };
        }

        public ActionResult get_user(User user)
        {
            var data = new { status = 200, message = "user retrieved successfully!", user };
            return new ObjectResult(data) { StatusCode = 200 };
        }

        public ActionResult get_users(List<User> users)
        {
            var data = new { status = 200, message = "users retrieved successfully!", users };
            return new ObjectResult(data) { StatusCode = 200 };
        }

        public ActionResult not_found()
        {
            var data = new { status = 404, message = "user not found" };
            return new ObjectResult(data) { StatusCode = 404 };
        }

        public ActionResult prob(string msg)
        {
            var data = new { status = 500, message = msg };
            return new ObjectResult(data) { StatusCode = 500 };
        }

        public ActionResult bad_credentials()
        {
            var data = new { status = 401, message = "bad credentials" };
            return new ObjectResult(data) { StatusCode = 401 };
        }

        public ActionResult get_role(core.enums.Role role)
        {
            var data = new { status = 200, message = "role retrieved successfully!", role = role };
            return new ObjectResult(data) { StatusCode = 200 };
        }

        public ActionResult error(string msg)
        {
            var data = new { status = 500, message = msg };
            return new ObjectResult(data) { StatusCode = 500 };
        }
    }
}