using Application.Interfaces.GenericRepository;
using Application.Interfaces.Services;
using Infrastructure.Implementations.Repositories;
using Infrastructure.Implementations.Services;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Dependency
{
    public static class InfrastructureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString,
                    b => b.MigrationsAssembly("Infrastructure")));

            services.AddTransient<IGenericRepository, GenericRepository>();
            services.AddScoped<ICandidateService,CandidateService >();

            return services;
        }
    }
}

