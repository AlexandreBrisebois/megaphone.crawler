namespace Megaphone.Crawler
{
    public interface IAppConfig
    {
        string? CrawlMessageApiUrl { get; }
        string? ResourceApiUrl { get; }
        bool ResourcePush { get; }
    }
}