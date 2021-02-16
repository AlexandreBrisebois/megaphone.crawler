using Microsoft.Azure.Functions.Worker.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Megaphone.Crawler.Middleware
{
    public static class ApplicationBuilderExtensions
    {
        public static IFunctionsWorkerApplicationBuilder UseSampleMiddleware(this IFunctionsWorkerApplicationBuilder builder)
        {
            builder.Services.AddSingleton<SampleMiddleware>();

            builder.Use(next =>
            {
                return context =>
                {
                    var middleware = context.InstanceServices.GetRequiredService<SampleMiddleware>();

                    return middleware.Invoke(context, next);
                };
            });

            return builder;
        }
    }
}