using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace OdeToFood
{
    using Microsoft.AspNetCore.Hosting.Internal;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.Extensions.Configuration;

    using OdeToFood.Entities;
    using OdeToFood.Services;

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            this.Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddEntityFramework()
                .AddEntityFrameworkSqlServer()
                .AddDbContext<OdeToFoodDbContext>(options => options.UseSqlServer(this.Configuration["database:connection"]));

            services.AddDbContext<OdeToFoodDbContext>(options =>
                options.UseSqlServer(Configuration["database:connection"]));


            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<OdeToFoodDbContext>();

            services.AddSingleton(provider => this.Configuration);
            services.AddSingleton<IGreeter, Greeter>();
            services.AddScoped<IRestaurantData, SqlRestaurantData>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IHostingEnvironment hostingEnvironment,  IGreeter greeter)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRuntimeInfoPage("/info");

            //app.UseDefaultFiles();
            //app.UseStaticFiles();
            app.UseFileServer();

            app.UseNodeModules((HostingEnvironment)hostingEnvironment); // an extension method that serves files from node_modules

            app.UseIdentity();

            //app.UseMvcWithDefaultRoute();
            app.UseMvc(ConfigureRoute);

            app.Run(async (context) =>
            {
                //throw new Exception("error");
                //var greeting = this.Configuration["greeting"];
                var greeting = greeter.GetGreeting();
                await context.Response.WriteAsync(greeting);
            });
        }

        private void ConfigureRoute(IRouteBuilder routeBuilder)
        {
            // /Home/Index
            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
