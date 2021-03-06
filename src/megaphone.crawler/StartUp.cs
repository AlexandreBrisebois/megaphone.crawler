using Megaphone.Crawler.Core;
using Megaphone.Crawler.Core.Services;
using Megaphone.Crawler.Services;
using Megaphone.Standard.Time;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Net.Http;

namespace megaphone.crawler
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
            services.AddHttpClient();
            services.AddDaprClient();
            
            services.AddSingleton<ICrawlerQueueService, DaprCrawlerQueueService>();
            services.AddSingleton<IResourceService, DaprResourceService>();
            services.AddSingleton<IClock, UtcClock>();

            services.AddScoped<IRestService,RestService>();
            services.AddScoped<IWebResourceCrawler, WebResourceCrawler>();

            services.AddControllers().AddDapr();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "megaphone.crawler", Version = "v1" });
            });

            string key = Environment.GetEnvironmentVariable("INSTRUMENTATION_KEY");
            if (!string.IsNullOrEmpty(key))
                services.AddApplicationInsightsTelemetry(key);
            else
                services.AddApplicationInsightsTelemetry();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "megaphone.crawler v1"));
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
