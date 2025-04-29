using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MVC_EF_Start_8.Services; // Make sure this namespace is correct
using MVC_EF_Start_8.DataAccess;  // Add this line for ApplicationDbContext

namespace Group4_Web_App
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
            // Add services to the container.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration["Data:IEXTradingAzure:ConnectionString"]));

            services.AddControllersWithViews();

            // Register NuclearOutageService as a singleton
            services.AddSingleton<NuclearOutageService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory) //Added LoggerFactory
        {
            // Get logger instance
            var logger = loggerFactory.CreateLogger<Startup>();

            // Verify connection string
            var connectionString = Configuration["Data:IEXTradingAzure:ConnectionString"];
            logger.LogInformation($"Connection String: {connectionString}");


            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting(); // Add this

            app.UseAuthorization(); // Add this

            app.UseEndpoints(endpoints =>  // Use Endpoints.
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
