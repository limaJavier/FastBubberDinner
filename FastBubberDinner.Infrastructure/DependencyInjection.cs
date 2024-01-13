using FastBubberDinner.Application.Common.Interfaces.Authentication;
using FastBubberDinner.Application.Common.Interfaces.Persistence;
using FastBubberDinner.Application.Common.Interfaces.Services;
using FastBubberDinner.Infrastructure.Authentication;
using FastBubberDinner.Infrastructure.Persistence;
using FastBubberDinner.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FastBubberDinner.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IUserRepository, InMemoryUserRepository>();
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SECTION_NAME));
        return services;
    }
}