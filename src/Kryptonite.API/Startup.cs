using Kryptonite.Infrastructure.IOC;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace Kryptonite.API {
    public class Startup {
        private readonly IConfiguration _configuration;
        private readonly SqlDatabase _sqlDatabase;
        public Startup(IConfiguration configuration) {
            _configuration = configuration;
            _sqlDatabase = new SqlDatabase(_configuration["Data:ConnectionString"]);
        }
        public void ConfigureServices(IServiceCollection services) {

            services.AddControllers();
            services.AddSwaggerGen();
            services.AddSingleton(_configuration);
            services.AddSingleton(_sqlDatabase);
            services.AddMemoryCache();
            RegisterServices(services);
        }
        private static void RegisterServices(IServiceCollection services) {
            DependencyContainer.RegisterServices(services);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
