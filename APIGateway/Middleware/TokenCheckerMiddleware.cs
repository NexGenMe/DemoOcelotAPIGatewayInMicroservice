namespace APIGateway.Middleware
{
    public class TokenCheckerMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            string requestPath = context.Request.Path.Value!;
            if (requestPath.Contains("auth/login", StringComparison.InvariantCultureIgnoreCase) 
                || requestPath.Contains("auth/register", StringComparison.InvariantCultureIgnoreCase)
                || requestPath.Equals("/"))
            {
                // Skip token validation for login and register endpoints
                await next(context);
            }
            else
            {
                var authHeader = context.Request.Headers.Authorization;
                if (authHeader.FirstOrDefault() == null)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Sorry, Access Denied");
                }
                else
                {
                    await next(context);
                }
            }
        }
    }
}
