using Kryptonite.Infrastructure.IOC;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
