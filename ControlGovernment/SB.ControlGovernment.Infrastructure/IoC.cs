using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SB.ControlGovernment.Application.Interfaces;
using SB.ControlGovernment.Application.Services;
using SB.ControlGovernment.Infrastructure.Repositories;

namespace SB.ControlGovernment.Infrastructure
{
    public static class IoC
    {
        public static void AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var governmentEntityFilePath = Path.Combine(AppContext.BaseDirectory, configuration["FileSettings:GovernmentEntityFilePath"] ?? throw new InvalidOperationException());
            var userFilePath = Path.Combine(AppContext.BaseDirectory, configuration["FileSettings:UserFilePath"] ?? throw new InvalidOperationException());

            if (string.IsNullOrEmpty(governmentEntityFilePath) || string.IsNullOrEmpty(userFilePath))
            {
                throw new ArgumentException("La ruta de los archivos no está configurada correctamente en appsettings.json.");
            }

            serviceCollection.AddTransient<IGovernmentEntityRepository>(provider =>
                new GovernmentEntityRepository(governmentEntityFilePath));
            serviceCollection.AddTransient<IUserRepository>(provider =>
                new UserRepository(userFilePath));

            serviceCollection.AddSingleton<IJwtTokenService>(provider =>
            {
                var key = configuration["Jwt:Key"];
                var issuer = configuration["Jwt:Issuer"];
                var audience = configuration["Jwt:Audience"];
                return new JwtTokenService(key, issuer, audience);
            });
            serviceCollection.AddTransient<IGovernmentEntityService, GovernmentEntityService>();
            serviceCollection.AddTransient<IUserService, UserService>();
        }
    }
}
