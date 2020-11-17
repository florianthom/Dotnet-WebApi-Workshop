using homepageBackend.Data;
using homepageBackend.Domain;
using homepageBackend.Installers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace homepageBackend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the IoC-container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.InstallDb(Configuration);
            
            services.InstallMvc(Configuration);
            
            services.InstallSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "homepageBackend v1"));
            }

            // app.Use(async (context, next) =>
            // {
            //     logger.LogInformation("Middleware (MW) 1: Incoming Request");
            //     await next();
            //     logger.LogInformation("MW2: Outgoing Response");
            // });

            app.UseHttpsRedirection();
            
            app.UseRouting();

            // Setting the User object in HTTP Request Context
            // https://www.codeproject.com/Articles/5160941/ASP-NET-CORE-Token-Authentication-and-Authorizatio
            app.UseAuthentication();

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}