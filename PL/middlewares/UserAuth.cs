using bll.dto;
using bll.interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using server.views;

namespace pl.middleware
{
    public class UserAuth : Attribute, IAsyncAuthorizationFilter
    {
        private readonly UserView _view;
        private readonly IUserService _service;

        public UserAuth(UserView view, IUserService service)
        {
            this._view = view;
            this._service = service;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            string? token = context.HttpContext.Request.Cookies["token"];

            if (string.IsNullOrEmpty(token))
            {
                context.Result = this._view.bad_credentials();
                return;
            }

            VerifyTokenRes res = await Task.Run(() => this._service.AuthUser(token ?? ""));

            if (!res.check)
            {
                context.Result = this._view.bad_credentials();
                return;
            }

            context.HttpContext.Items["user_id"] = res.user_id;
        }
    }
}