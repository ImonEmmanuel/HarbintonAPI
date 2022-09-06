using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace Harbinton.API.Application
{
    public static class ApplicationServiceRegistration
    {

        public static IServiceCollection AddConfig(this IServiceCollection services)
        {
            return services;
        }
    }
}
