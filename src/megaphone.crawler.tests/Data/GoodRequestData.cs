using Megaphone.Standard.Messages;
using Xunit;

namespace Megaphone.Crawler.Tests.Data
{
    public class GoodRequestData : TheoryData<CommandMessage, string>
    {
        public GoodRequestData()
        {
            Add(MessageBuilder.NewCommand("crawl-request").WithParameters("uri", "https://devblogs.microsoft.com/dotnet/staying-safe-with-dotnet-containers/").Make(), "page");
            Add(MessageBuilder.NewCommand("crawl-request").WithParameters("uri", "https://devblogs.microsoft.com/dotnet/feed/").Make(), "feed");
        }
    }
}
