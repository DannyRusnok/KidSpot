using KidSpot.Application.Common;
using KidSpot.Application.Places.Commands;
using KidSpot.Application.Reviews.Commands;
using KidSpot.Application.Reviews.Queries;
using KidSpot.Application.Users.Commands;
using KidSpot.Domain.Repositories;
using KidSpot.Infrastructure.Auth;
using KidSpot.Infrastructure.Persistence;
using KidSpot.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KidSpot.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Persistence
        services.AddDbContext<KidSpotDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(KidSpotDbContext).Assembly.FullName)));

        services.AddScoped<IPlaceRepository, PlaceRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();

        // Auth
        services.AddSingleton<IGoogleAuthService, GoogleAuthService>();
        services.AddSingleton<IJwtTokenService, JwtTokenService>();

        // Application handlers
        services.AddScoped<AuthenticateWithGoogleHandler>();
        services.AddScoped<CreatePlaceHandler>();
        services.AddScoped<UpdatePlaceHandler>();
        services.AddScoped<AddReviewHandler>();
        services.AddScoped<GetReviewsByPlaceHandler>();

        return services;
    }
}
