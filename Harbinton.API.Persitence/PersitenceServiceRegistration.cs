using Harbinton.API.Application.Contracts.Persitence;
using Harbinton.API.Persitence.Contract.Implementation;
using Harbinton.API.Persitence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Harbinton.API.Persitence
{
    public static class PersitenceServiceRegistration
    {

        public static IServiceCollection ConfigurePersitenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration["ConnectionStrings:HarbintonDatabase"]);
            });

            services.AddScoped<IAccountCreationRepository, AccountCreationRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            return services;
        }
    }
}