#nullable disable

using Application.Interfaces;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence;

public static class ServicesContainer
{
    public static IServiceCollection InfrastructurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add custom services here
        services.AddDbContext<LibraSysContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            x => x.MigrationsAssembly(typeof(ServicesContainer).Assembly.FullName)),
            ServiceLifetime.Scoped);

        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<ILoanRepository, LoanRepository>();
        services.AddScoped<IMemberRepository, MemberRepository>();
        return services;
    }

}
