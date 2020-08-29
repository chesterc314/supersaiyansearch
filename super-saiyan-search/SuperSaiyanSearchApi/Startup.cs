using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using SuperSaiyanSearch.Domain.Interfaces;
using SuperSaiyanSearch.Integration.Interfaces;
using SuperSaiyanSearch.Integration;
using SuperSaiyanSearch.Api;
using SuperSaiyanSearch.Api.Interfaces;

namespace SuperSaiyanSearchApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<IWebScrapper, WebScrapper>();
            services.AddScoped<IHttpClient, HttpClient>();
            services.AddScoped<IStoreSiteConfiguration, StoreSiteConfiguration>();
            services.AddScoped<IStoreSiteAggregation, StoreSiteAggregation>();
            services.AddScoped<IProductApi, ProductApi>();
            services.AddControllers().AddNewtonsoftJson(options => options.UseMemberCasing());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
