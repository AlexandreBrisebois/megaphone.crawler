using Megaphone.Standard.Messages;
using Xunit;

namespace Megaphone.Crawler.Tests.Data
{
    public class BadRequestData : TheoryData<CommandMessage>
    {
        public BadRequestData()
        {
            Add(MessageBuilder.NewCommand("crawl").WithParameters("uri", "https://devblogs.microsoft.com/dotnet/staying-safe-with-dotnet-containers/").Make());
        }
    }
}
