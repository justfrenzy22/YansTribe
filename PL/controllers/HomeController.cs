using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using core.models;
using server.views;

namespace server.controllers;

[ApiController]
[Route("home/")]
public class HomeController : Controller
{
    private HomeView view;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, HomeView view)
    {
        _logger = logger;
        this.view = view;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return view.success();
    }

    [HttpPost]
    [Route("/register")]
    public IActionResult Register()
    {

        using (var reader = new StreamReader(HttpContext.Request.Body))
        {
            var postData = reader.ReadToEnd();
        }

        return view.success();
        // skibidi
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
