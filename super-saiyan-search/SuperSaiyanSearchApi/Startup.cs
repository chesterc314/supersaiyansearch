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

        public static string CORS_NAME = "AllowOrigin";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<IWebScrapper, WebScrapper>();
            services.AddSingleton<IHttpClient, HttpClient>();
            services.AddSingleton<IStoreSiteConfiguration, StoreSiteConfiguration>();
            services.AddScoped<IStoreSiteAggregation, StoreSiteAggregation>();
            services.AddScoped<IProductApi, ProductApi>();
            services.AddMemoryCache();
            services.AddCors(c =>
            {
                c.AddPolicy(CORS_NAME, options => options.WithOrigins(Configuration["AllowedOrigin"]));
            });
            services.AddSwaggerGen();
            services.AddControllers().AddNewtonsoftJson(options => options.UseMemberCasing())
            .AddJsonOptions(ops => ops.JsonSerializerOptions.IgnoreNullValues = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "api-docs";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(CORS_NAME);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
