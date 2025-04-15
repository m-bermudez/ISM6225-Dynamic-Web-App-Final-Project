using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVC_EF_Start_8.DataAccess;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace MVC_EF_Start_8
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Setup EF connection with the correct configuration key
            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(Configuration["Data:IEXTradingAzure:ConnectionString"]));

            services.AddMvc(option => option.EnableEndpointRouting = false);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Get logger instance early for configuration verification
            var logger = app.ApplicationServices.GetRequiredService<ILogger<Startup>>();
            
            // Verify connection string is loaded correctly
            var connectionString = Configuration["Data:IEXTrading:ConnectionString"];
            logger.LogInformation($"Using Connection String: {connectionString}");

            // Database initialization with improved error handling
            try 
            {
                using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    logger.LogInformation("Attempting to connect to database...");
                    context.Database.EnsureCreated();
                    logger.LogInformation("✅ Database connection successful!");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "❌ Database connection failed!");
                throw; // Fail startup if DB connection fails
            }

            // Configure HTTP request pipeline
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}