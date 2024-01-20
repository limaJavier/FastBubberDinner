using Microsoft.Extensions.DependencyInjection;
using FastBubberDinner.Application.Services.Authentication;

namespace FastBubberDinner.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        
        return services;
    }
}