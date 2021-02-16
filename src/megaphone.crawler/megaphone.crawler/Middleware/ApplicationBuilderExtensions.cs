using Microsoft.Azure.Functions.Worker.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Megaphone.Crawler.Middleware
{
    public static class ApplicationBuilderExtensions
    {
        public static IFunctionsWorkerApplicationBuilder UseSampleMiddleware(this IFunctionsWorkerApplicationBuilder builder)
        {
            return builder;
        }
    }
}