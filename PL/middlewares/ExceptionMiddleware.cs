using System.Net.Mime;
using System.Reflection.Metadata;
using System.Text.Json;
using core.entities;
using dal.exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace pl.middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ICompositeViewEngine _engine;
        private readonly IServiceProvider _provider;

        public ExceptionMiddleware(RequestDelegate next, ICompositeViewEngine engine, IServiceProvider provider)
        {
            this._next = next;
            this._engine = engine;
            this._provider = provider;
        }



        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this._next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }


        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            int statusCode;
            string message;

            if (ex is BaseException _base)
            {
                statusCode = _base.status;
                message = _base.Message;
            }
            else
            {
                statusCode = StatusCodes.Status500InternalServerError;
                message = "Internal Server Error";
            }

            var controller = context.Request.RouteValues["controller"]?.ToString();
            bool isAdmin = controller != null && controller.Contains("Admin");

            if (isAdmin)
            {
                await HandleAdminView(context, statusCode, message);
            }
            else
            {
                await HandleOtherView(context, statusCode, message);
            }
        }

        private async Task HandleOtherView(HttpContext context, int status, string message)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = status;
            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                error = new { status, message }
            }));
        }


        private async Task HandleAdminView(HttpContext context, int statusCode, string message)
        {
            var actionContext = new ActionContext(context, new RouteData(), new ActionDescriptor());
            var viewResult = _engine.FindView(actionContext, "~/Views/Shared/Error.cshtml", false);

            if (!viewResult.Success)
            {
                context.Response.ContentType = MediaTypeNames.Application.Json;
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    error = new { statusCode = 500, message = "Error view not found" }
                }));
                return;
            }

            context.Response.ContentType = MediaTypeNames.Text.Html;
            context.Response.StatusCode = statusCode;

            var viewData = new ViewDataDictionary<dto.ErrDTO>(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = new dto.ErrDTO
                {
                    status = statusCode,
                    message = message
                }
            };

            var tempData = new TempDataDictionary(context, this._provider.GetRequiredService<ITempDataProvider>());
            using var writer = new StringWriter();
            var viewContext = new ViewContext(
                actionContext,
                viewResult.View,
                viewData,
                tempData,
                writer,
                new HtmlHelperOptions()
            );

            await viewResult.View.RenderAsync(viewContext);
            await context.Response.WriteAsync(writer.ToString());
        }
    }
}