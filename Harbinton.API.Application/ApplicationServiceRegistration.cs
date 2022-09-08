using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Harbinton.API.Application.Helper.Interface;
using Harbinton.API.Application.Helper;
using Harbinton.API.Application.Extensions;

namespace Harbinton.API.Application
{
    public static class ApplicationServiceRegistration
    {

        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddTransient<IExtensionCache, ExtensionCache>();
            return services;
        }
    }
}
