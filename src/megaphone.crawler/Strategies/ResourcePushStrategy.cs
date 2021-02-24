using Megaphone.Crawler.Core.Models;
using Megaphone.Crawler.Representations;
using Megaphone.Crawler.Services;
using System.Net;
using System.Threading.Tasks;

namespace Megaphone.Crawler.Strategies
{
    internal class ResourcePushStrategy : ResponseStrategy<SystemTextJsonResult>
    {
        private readonly PushService service;

        public ResourcePushStrategy(PushService service, AppConfig configs) : base(configs)
        {
            this.service = service;
        }

        internal override bool CanExecute()
        {
            return base.serviceContext.ResourcePush;
        }

        internal override async Task<SystemTextJsonResult> ExecuteAsync(Resource resource)
        {
            var response = await service.PushAsync($"{base.serviceContext.ResourceApiUrl}/api/resources", resource);

            if (response != HttpStatusCode.OK)
            {
                return new SystemTextJsonResult(new ErrorPushRepresentation
                {
                    Message = $"failed to push resource to {base.serviceContext.ResourceApiUrl}/api/resources",
                    StatusCode = (int)response,
                }, statusCode: HttpStatusCode.BadRequest);
            }

            var representation = RepresentationFactory.MakePushedRepresentation(resource, base.serviceContext.ResourceApiUrl);
            return new SystemTextJsonResult(representation,
                                            statusCode: HttpStatusCode.OK); ;
        }
    }
}