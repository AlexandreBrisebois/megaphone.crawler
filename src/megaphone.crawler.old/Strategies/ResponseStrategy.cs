namespace Megaphone.Crawler.Strategies
{
    internal abstract class ResponseStrategy<Model, Result> : Strategy<Model, Result>
    {
        protected readonly IAppConfig configs;

        public ResponseStrategy(IAppConfig serviceContext)
        {
            this.configs = serviceContext;
        }
    }
}