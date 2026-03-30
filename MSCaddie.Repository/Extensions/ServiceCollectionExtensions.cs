using Microsoft.Extensions.DependencyInjection;
using MSCaddie.Repository.Data;
using MSCaddie.Repository.Interfaces;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
    {
        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddScoped<ITourRepository, TourRepository>();
        services.AddScoped<IClubRepository, ClubRepository>();
        services.AddScoped<IMatchRepository, MatchRepository>();
        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<IMatchplayRepository, MatchplayRepository>();
        return services;
    }
}