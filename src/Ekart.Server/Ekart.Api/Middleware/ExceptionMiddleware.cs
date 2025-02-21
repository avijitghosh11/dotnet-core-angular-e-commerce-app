using Ekart.Api.Errors;
using System.Net;
using System.Text.Json;

namespace Ekart.Api.Middleware
{
    public class ExceptionMiddleware(IHostEnvironment env,RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				await HandelExceptionAsync(context,ex,env);
			}
        }

        private static Task HandelExceptionAsync(HttpContext context, Exception ex, IHostEnvironment env)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string errorDetails = env.IsDevelopment() ? ex.StackTrace : "Internal server error";
            var response = new ApiErrorResponse(context.Response.StatusCode, ex.Message, errorDetails);

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var json = JsonSerializer.Serialize(response,options);
            return context.Response.WriteAsync(json);
        }
    }
}
