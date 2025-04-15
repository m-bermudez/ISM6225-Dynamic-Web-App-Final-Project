using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVC_EF_Start_8.DataAccess;
// https://stackoverflow.com/a/58072137/1385857
using Microsoft.Extensions.Hosting;

namespace MVC_EF_Start_8
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Setup EF connection
            // https://stackoverflow.com/a/43098152/1385857
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration["Data:IEXTradingAzure:ConnectionString"]));

            // added from MVC template
            //services.AddMvc();
            // https://stackoverflow.com/a/58772555/1385857
            services.AddMvc(option => option.EnableEndpointRouting = false);
        }

        // this is the version from the MVC template
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //This ensures that the database and tables are created as per the Models.
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.EnsureCreated();
            }

            // https://stackoverflow.com/a/58072137/1385857
            if (env.IsDevelopment())
            {
                //app.UseBrowserLink();
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