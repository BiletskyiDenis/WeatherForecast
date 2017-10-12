using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherForecastData;
using Microsoft.EntityFrameworkCore;
using WeatherForecastService;
using System;
using WeatherForecastService.Models;

namespace WeatherForecast
{
    public class Startup
    {
        private string _contentRootPath;
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            _contentRootPath = env.ContentRootPath;

            var builder = new ConfigurationBuilder()
                  .SetBasePath(_contentRootPath)
                  .AddJsonFile("appsettings.json");
            if (env.IsDevelopment())
            {
                builder.AddJsonFile("appsettings.Development.json");
            }
            Configuration = builder.Build();

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<WeatherServiceSettings>(Configuration.GetSection("WeatherServiceSettings"));

            var conn = Configuration.GetConnectionString("WeatherForecastDb");

            if (conn.Contains("%CONTENTROOTPATH%"))
            {
                conn = conn.Replace("%CONTENTROOTPATH%", _contentRootPath);
            }

            services.AddDbContext<WeatherForecastContext>(options => options.UseSqlServer(conn));

            services.AddScoped<IWeatherService, WeatherService>();

            var config = new AutoMapper.MapperConfiguration(c =>
              {
                  c.AddProfile(new ApplicationProfile());
              });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc();

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
