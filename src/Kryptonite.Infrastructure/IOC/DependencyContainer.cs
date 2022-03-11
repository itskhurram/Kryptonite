using Kryptonite.Application.Interfaces;
using Kryptonite.Application.Services;
using Kryptonite.Domain.Interfaces;
using Kryptonite.Persistance.Repositories;

using Microsoft.Extensions.DependencyInjection;

namespace Kryptonite.Infrastructure.IOC {

    public class DependencyContainer {
        public static void RegisterServices(IServiceCollection services) {

            //Application Services
            services.AddTransient<IUserService, UserService>();

            //Data
            services.AddTransient<IUserRepository, UserRepository>();

        }
    }
}
