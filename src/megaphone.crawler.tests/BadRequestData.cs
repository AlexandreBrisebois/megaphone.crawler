using Megaphone.Standard.Messages;
using Xunit;

namespace megaphone.crawler.tests
{
    public class BadRequestData : TheoryData<CommandMessage>
    {
        public BadRequestData()
        {
            Add(MessageBuilder.NewCommand("crawl").WithParameters("uri", "https://devblogs.microsoft.com/dotnet/staying-safe-with-dotnet-containers/").Make());
        }
    }
}
