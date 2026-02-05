/*
A simple interface to invoke a logging middleware.
*/

using Microsoft.AspNetCore.Http;

namespace ApiServices.Logging;

public interface ILoggerMiddleware
{
    public Task InvokeAsync(HttpContext context);
}
