using System.Net;

namespace Api.Middleware;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch
        {
            context.Response.StatusCode = 500;
        }
    }
}
