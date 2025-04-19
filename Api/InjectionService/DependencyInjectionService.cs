using FluentValidation;
namespace Api.InjectionService;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Muslim.Application.Users;
using Muslim.Assembly.Helper;
using Muslim.Data.EFCore.SqlServer;
using Muslim.GenericRepository.Repositories;
using Muslim.Helpers;
using Muslim.IGenericRepository;
using Muslim.Inject;
using Muslim.Validator;

public static class DependencyInjectionService
{

    public static IServiceCollection AddAutoScoped(this IServiceCollection services)
    {


        // Get all types from the Services class that are interfaces and implement IAutoInjection
        IEnumerable<Type> interfaceTypes = AssemblyHelper.GetTypes()
            .Where(a => a.IsInterface && a.GetInterfaces().Any(i => i == typeof(IAutoInjection)));

        // Get all types from the Services class that are classes and implement IAutoInjection
        List<Type> implementationTypes = AssemblyHelper.GetTypes()
            .Where(a => a.IsClass && a.GetInterfaces().Any(i => i == typeof(IAutoInjection))).ToList();

        // Get all types from the Services class that inherit from the Profile class
        IEnumerable<Type> autoMapperTypes = AssemblyHelper.GetTypes()
            .Where(a => a.BaseType == typeof(Profile));

        // For each interface type that implements IAutoInjection
        foreach (Type interfaceType in interfaceTypes)
        {
            // Get the implementation types that implement the interface
            IEnumerable<Type> types = implementationTypes
                .Where(a => a.GetInterfaces()
                    .Any(
                            i => i.Name == interfaceType.Name
                             && a.Namespace == interfaceType.Namespace
                        )
                );

            // Add the implementation type to the service collection as a scoped service
            foreach (Type type in types)
            {
                services.AddScoped(interfaceType, type);
            }
        }

        // For each type that inherits from the Profile class
        foreach (var type in autoMapperTypes) services.AddAutoMapper(type);

        // Return the service collection
        return services;
    }


    public static IServiceCollection ConfigureValidator(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(AssemblyHelper.GetAssembly("Muslim.Application"));

        // Set the service provider
        ValidateResult.SetServiceProvider(services.BuildServiceProvider());

        return services;
    }


    public static IServiceCollection ConfigureInjectExtensions(this IServiceCollection services)
    {
        InjectExtensions.SetServiceProvider(services.BuildServiceProvider());

        return services;
    }

    public static IServiceCollection InjectionConfig(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddAutoScoped();
        services.AddScoped<ICurrentUser, CurrentUserHelper>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddDbContext<SqlServerDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("SqlServerConnection");
        });


        services.ConfigureValidator();
        services.ConfigureInjectExtensions();

        BaseRepository.SetDbContext<SqlServerDbContext>();


        return services;
    }

}

