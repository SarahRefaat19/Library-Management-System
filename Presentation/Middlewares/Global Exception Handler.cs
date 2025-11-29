using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LibrarySystem.Presentation.Middlewares
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                message = ex.Message,
                errortype = ex.GetType().Name,
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
