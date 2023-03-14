using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Books.MiddleWares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _naxt;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate naxt, ILogger<GlobalExceptionMiddleware> logger)
        {
            this._naxt = naxt;
            this._logger = logger;
        }

        public async Task InvokeAsync(HttpContext context) {

            try
            {
                await _naxt(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                ProblemDetails details = new()
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Title = "Server Error",
                    Type = "Server Error",
                    Detail = ex.Message
                };
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(details));
            }
        }
    }
}
