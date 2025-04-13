using bll.dto;
using bll.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace pl.middleware
{
    public class AdminAuth : Attribute, IAsyncAuthorizationFilter
    {
        private readonly IAdminService _service;

        public AdminAuth (IAdminService service)
        {
            this._service = service;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            string? token = context.HttpContext.Request.Cookies["token"];

            if (string.IsNullOrEmpty(token))
            {
            context.Result = new RedirectToActionResult("getLogin", "admin", null);
            return;
            }

            // Await the asynchronous call to ensure it completes before proceeding
            VerifyTokenRes res = await Task.Run(() => this._service.AuthAdmin(token));

            if (!res.check)
            {
            context.Result = new RedirectToActionResult("getLogin", "admin", null);
            return;
            }

            context.HttpContext.Items["user_id"] = res.user_id;
        }
    }
}