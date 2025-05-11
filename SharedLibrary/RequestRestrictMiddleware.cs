using Microsoft.AspNetCore.Http;

namespace SharedLibrary
{
    public class RequestRestrictMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            var referrer = context.Request.Headers["Referrer"].ToString();
            if (referrer != "Api-Gateway")
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Forbidden");
            }

            await next(context);
        }
    }
}
