/*
A service to add automapper to an API.
*/

using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace ApiServices.Extension;

public static class ApiAutomapperExtension
{
    public static void AddAutoMapper(this IServiceCollection services, MapperConfiguration config)
    {
        if (config is null)
        {
            throw new ArgumentException("Invalid configuration for automapper.");
        }
        var mapper = config.CreateMapper();
        services.AddSingleton(mapper);
    }
}
