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
            string? token = context.HttpContext.Request.Headers["auth_token"];

            if (string.IsNullOrEmpty(token))
            {
                context.Result = this._view.bad_credentials();
                return;
            }

            // Remove "Bearer " prefix if present
            if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = token.Substring("Bearer ".Length).Trim();
            }

            VerifyTokenRes res = await Task.Run(() => this._service.AuthUser(token));

            if (!res.check)
            {
                context.Result = this._view.bad_credentials();
                return;
            }

            context.HttpContext.Items["user_id"] = res.user_id;
        }
    }
}