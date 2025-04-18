// using System.Runtime.Versioning;
// using bll.dto;
// using bll.interfaces;
// using dal.dto;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Filters;

// namespace pl.middleware
// {
//     public class SuperAdminAuth
//     {
//         // private readonly Req
//         private readonly RequestDelegate next;
//         private readonly IAdminService service;

//         public SuperAdminAuth(RequestDelegate next, IAdminService service)
//         {
//             this.next = next;
//             this.service = service;
//         }

//         public async Task InvokeAsync(HttpContext context)
//         {
//             string? token = context.Request.Cookies["token"];

//             if (string.IsNullOrEmpty(token))
//             {
//                 context.Response.Redirect("/admin/Login");
//                 return;
//             }

//             VerifySuperAdminDTO result = await this.service.AuthSuperAdmin(token);

//             if (!result.check)
//             {
//                 string redirectUrl = $"/admin/Login?error=${result.exception}";
//                 context.Response.Redirect(redirectUrl);
//                 return;
//             }

//             context.Items["check"] = result.check;

//             await next(context);
//         }

//         // public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
//         // {
//         //     string? token = context.HttpContext.Request.Cookies["token"];

//         //     if (string.IsNullOrEmpty(token))
//         //     {
//         //         context.Result = new UnauthorizedResult();
//         //         return;
//         //     }

//         //     VerifySuperAdminDTO res = await this.service.AuthSuperAdmin(token);

//         //     // context.HttpContext.Items["check"] = res.check;
//         //     if (!res.check)
//         //     {
//         //         // context.HttpContext.Items["exception"] = res.exception;
//         //         context.Result = new RedirectToActionResult("Login", "Admin", new { error = res.exception });
//         //         return;
//         //     }

//         //     context.Items["check"]



//         // VerifyTokenRes res = await this.service.AuthAdmin(token);

//         // if (!res.check)
//         // {
//         //     context.Result = new RedirectToActionResult("Login", "Admin", null);
//         //     return;
//         // }

//         // UserDTO? admin = this.

//         // context.HttpContext.Items["user_id"] = res.user_id;
//         // }
//     }
// }