using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using homepageBackend.Domain;

namespace homepageBackend
{
    public class Startup
    {

        public IConfiguration Configuration { get; }


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the IoC-container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                // to make entityFramework function with identity
                .AddEntityFrameworkStores<DataContext>();

            services.AddAuthorization(options =>
            {
                // options.AddPolicy("EditRolePolicy",
                //     policy => policy.RequireAssertion(context =>
                //     context.User.IsInRole("Admin")
                //     &&
                //     context.User.HasClaim(claim => claim.Type == "Edit Role" && claim.Value == "true")
                //     ||
                //     context.User.IsInRole("Super Admin")
                //     ));
                // options.AddPolicy("AdminRolePolicy",
                //     policy => policy.RequireRole("Admin"));
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 3;
            });





            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "homepageBackend", Version = "v1" });
            });
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
