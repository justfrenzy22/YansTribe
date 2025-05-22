using Microsoft.AspNetCore.Mvc;
using core.entities;
using dal.dto;
using pl.viewModel;
using bll.dto;

namespace pl.views
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

        public ActionResult get_user(object userDto)
        {

            var data = new
            {
                status = 200,
                message = "user retrieved successfully!",
                user = userDto
            };
            return new ObjectResult(data) { StatusCode = 200 };
        }
        
        public IActionResult get_private_profile_user(PrivateProfileViewModel userDTO)
        {
            var data = new
            {
                status = 200,
                message = "Private Profile user retrieved successfully!",
                user = userDTO.user,
                posts = userDTO.posts
            };

            return new ObjectResult(data) { StatusCode = 200 };
        }

        public IActionResult get_public_profile_user(ProfileUser profileUser, BaseUser user, Notifications notifications)
        {
            var data = new
            {
                status = 200,
                message = "Public Profile user retrieved successfully!",
                user = user,
                profile = profileUser,
            };

            return new ObjectResult(data) { StatusCode = 200 };
        }

        public ActionResult get_base_user(BaseUser? userDTO)
        {
            var data = new
            {
                status = 200,
                message = "Base user retrieved successfully!",
                user = userDTO
            };
            return new ObjectResult(data) { StatusCode = 200 };
        }

        public ActionResult get_users(List<FullUser> users)
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
            var data = new { status = 400, message = "bad credentials" };
            return new ObjectResult(data) { StatusCode = 400 };
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

        internal IActionResult get_profile_user(object profileViewModel)
        {
            throw new NotImplementedException();
        }
    }
}