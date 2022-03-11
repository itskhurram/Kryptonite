using Kryptonite.Application.Interfaces;
using Kryptonite.Application.Services;
using Kryptonite.Domain.Interfaces;
using Kryptonite.Infrastructure.Caching;
using Kryptonite.Persistance.Repositories;

using Microsoft.Extensions.DependencyInjection;

namespace Kryptonite.Infrastructure.IOC {

    public class DependencyContainer {
        public static void RegisterServices(IServiceCollection services) {

            //Application Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITerritoryService, TerritoryService>();

            //Data
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITerritoryRepository, TerritoryRepository>();
            services.AddScoped<IMemoryCacheProvider, MemoryCacheProvider>();

        }
    }
}
