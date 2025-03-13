using Microsoft.AspNetCore.Mvc;

public class HomeView() : ViewComponent
{
    public ActionResult success()
    {
        var data = new { status = 200, message = "success" };
        return new ObjectResult(data) { StatusCode = 200 };
        // return Content(System.Text.Json.JsonSerializer.Serialize(data), "application/json");
    }

    public ActionResult error()
    {
        var data = new { status = 500, message = "error" };
        return new ObjectResult(data) { StatusCode = 500 };
    }

    public ActionResult not_found()
    {
        var data = new { status = 404, message = "not found" };
        return new ObjectResult(data) { StatusCode = 404 };
    }

    public ActionResult unauthorized()
    {
        var data = new { status = 401, message = "unauthorized" };
        return new ObjectResult(data) { StatusCode = 401 };
    }

    public ActionResult forbidden()
    {
        var data = new { status = 401, message = "forbidden" };
        return new ObjectResult(data) { StatusCode = 401 };
    }
}