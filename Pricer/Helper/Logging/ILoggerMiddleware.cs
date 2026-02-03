/*
A simple interface to invoke a logging middleware.
*/

namespace PricerApi.Log;

public interface ILoggerMiddleware
{
    public Task InvokeAsync(HttpContext context);
}
