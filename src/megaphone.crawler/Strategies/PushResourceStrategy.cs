using Megaphone.Crawler.Core.Models;
using Megaphone.Crawler.Core.Services;
using Megaphone.Crawler.Representations;
using System.Net;
using System.Threading.Tasks;

namespace Megaphone.Crawler.Strategies
{
    internal class PushResourceStrategy : ResponseStrategy<Resource, SystemTextJsonResult>
    {
        private readonly IRestService service;

        public PushResourceStrategy(IRestService service, IAppConfig configs) : base(configs)
        {
            this.service = service;
        }

        internal override bool CanExecute()
        {
            return base.configs.ResourcePush && !string.IsNullOrEmpty(base.configs.ResourceApiUrl);
        }

        internal override async Task<SystemTextJsonResult> ExecuteAsync(Resource model)
        {
            var response = await service.PostAsync(base.configs.ResourceApiUrl, model);

            if (response != HttpStatusCode.Created)
            {
                return new SystemTextJsonResult(new ErrorPushRepresentation
                {
                    Message = $"failed to push resource to {base.configs.ResourceApiUrl}",
                    StatusCode = (int)response,
                }, statusCode: HttpStatusCode.BadRequest);
            }

            var representation = RepresentationFactory.MakePushedRepresentation(model, configs.ResourceApiUrl);
            return new SystemTextJsonResult(representation,
                                            statusCode: HttpStatusCode.OK); ;
        }
    }
}