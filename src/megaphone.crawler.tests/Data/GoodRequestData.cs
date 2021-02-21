using Megaphone.Standard.Messages;
using Xunit;

namespace Megaphone.Crawler.Tests.Data
{
    public class GoodRequestData : TheoryData<CommandMessage, string>
    {
        public GoodRequestData()
        {
            Add(MessageBuilder.NewCommand("crawl-request").WithParameters("uri", "https://devblogs.microsoft.com/dotnet/staying-safe-with-dotnet-containers/").Make(), "page");
            Add(MessageBuilder.NewCommand("crawl-request")
                              .WithParameters("uri", "https://devblogs.microsoft.com/dotnet/staying-safe-with-dotnet-containers/")
                              .WithParameters("id", "d64bc5a5-f2d2-572c-bfbf-b99e5340c0d9")
                              .WithParameters("display", "Staying safe with .NET containers")
                              .WithParameters("description", "<p>Container-based application deployment and execution has become very common. Nearly all cloud and server app developers we talk to use containers in some way. We mostly hear about public cloud use, but also IoT and have even heard of .NET containers pulled and used over satellite links on cruise ships.</p>\n<p>The post <a rel=\"nofollow\" href=\"https://devblogs.microsoft.com/dotnet/staying-safe-with-dotnet-containers/\">Staying safe with .NET containers</a> appeared first on <a rel=\"nofollow\" href=\"https://devblogs.microsoft.com/dotnet\">.NET Blog</a>.</p>\n")
                              .WithParameters("published", "2021-02-11T19:01:56+00:00")
                              .Make(), "page"); 
            Add(MessageBuilder.NewCommand("crawl-request").WithParameters("uri", "https://devblogs.microsoft.com/dotnet/feed/").Make(), "feed");
        }
    }
}