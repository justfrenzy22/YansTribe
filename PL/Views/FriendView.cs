using Microsoft.AspNetCore.Mvc;

namespace pl.views
{
    public class FriendView : ViewComponent
    {
        public ActionResult success()
        {
            var data = new { status = 200, message = "Successful test" };
            return new ObjectResult(data) { StatusCode = 200 };
        }

        public ActionResult removed(string result)
        {
            var data = new { status = 200, message = result };
            return new ObjectResult(data) { StatusCode = 200 };
        }

        public ActionResult accepted(string result)
        {
            var data = new { status = 200, message = result };
            return new ObjectResult(data) { StatusCode = 200 };
        }

        public ActionResult error()
        {
            var data = new { status = 500, message = "There was an error processing your request" };
            return new ObjectResult(data) { StatusCode = 500 };
        }

        public ActionResult declined(string result)
        {
            var data = new { status = 200, message = result };
            return new ObjectResult(data) { StatusCode = 200 };
        }

        public ActionResult requested()
        {
            var data = new { status = 200, message = "friend request sent successfully!" };
            return new ObjectResult(data) { StatusCode = 200 };
        }

    }
}