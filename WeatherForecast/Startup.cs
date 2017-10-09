using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherForecastData;
using Microsoft.EntityFrameworkCore;
using WeatherForecastService;
using Microsoft.AspNetCore.Http;

namespace WeatherForecast
{
    public class Startup
    {
        private string _contentRootPath;
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _contentRootPath = env.ContentRootPath;

            var builder = new ConfigurationBuilder()
                  .SetBasePath(_contentRootPath)
                  .AddJsonFile("appsettings.json")
                  .AddJsonFile("apisettings.json");
            Configuration = builder.Build();

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<IWeatherService, WeatherService>(provider =>
                        new WeatherService(provider.GetService<WeatherForecastContext>(),
                        Configuration["Appid"],
                        Configuration.GetValue<int>("UpdateInterval")));

            //connect to remote server
            var conn = Configuration.GetConnectionString("ConnectionStringServer");

            //connect to LocalDB
            //var conn = Configuration.GetConnectionString("ConnectionStringLocalDB");

            //connect to LocalDB file (App_Data\Cities.mdf)
            //var conn = Configuration.GetConnectionString("ConnectionStringLocalFileDB");

            if (conn.Contains("%CONTENTROOTPATH%"))
            {
                conn = conn.Replace("%CONTENTROOTPATH%", _contentRootPath);
            }

            services.AddDbContext<WeatherForecastContext>(options
                => options.UseSqlServer(conn));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
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

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
