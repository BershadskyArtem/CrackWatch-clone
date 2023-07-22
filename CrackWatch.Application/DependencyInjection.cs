using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CrackWatch.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssembly(AssemblyMarker.Marker);
        });

        services.AddValidatorsFromAssembly(AssemblyMarker.Marker);

        return services;    
    }
}