using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVC_EF_Start_8.Services;
using System;

namespace Group4_Web_App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Register HttpClient for API calls
            builder.Services.AddHttpClient("EIA_API", client =>
            {
                client.BaseAddress = new Uri("https://api.eia.gov/v2/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            // Register NuclearOutageService with proper lifecycle
            // Scoped is typically better for services that hold state per request
            builder.Services.AddScoped<NuclearOutageService>();

            // Add logging
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
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

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Add health check endpoint
            app.MapGet("/health", () => Results.Ok());

            try
            {
                app.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Application failed to start: {ex.Message}");
                throw;
            }
        }
    }
}