using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using bll.dto;
using bll.interfaces;

namespace pl.middleware
{
    public class SuperAdminAuthFilter : IAsyncActionFilter
    {
        private readonly IAdminService _service;

        public SuperAdminAuthFilter(IAdminService service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var token = context.HttpContext.Request.Cookies["token"];

            if (string.IsNullOrEmpty(token))
            {
                context.Result = new RedirectToActionResult("error", "admin", null);
                return;
            }

            var result = await _service.AuthSuperAdmin(token);

            if (!result.check)
            {
                context.Result = new RedirectToActionResult("error", "admin", new { error = result.exception });
                return;
            }

            context.HttpContext.Items["check"] = result.check;

            await next();
        }
    }

}